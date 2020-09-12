using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicMapper;
using EpicOrbit.Emulator.Game;
using EpicOrbit.Emulator.Game.Controllers;
using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Server.Data.Implementations;
using EpicOrbit.Server.Data.Models;
using EpicOrbit.Server.Data.Models.Enumerables;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Server.Data.Models.ViewModels.Account;
using EpicOrbit.Server.Data.Models.ViewModels.Cooldown;
using EpicOrbit.Server.Data.ViewModels.Hangar;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels.Account;
using EpicOrbit.Shared.ViewModels.Clan;
using EpicOrbit.Shared.ViewModels.Vault;
using FightZone.Emulator.Extensions;
using MongoDB.Driver;

namespace EpicOrbit.Emulator.Services {
    public static class AccountService {

        public static async Task<ValidatedView> Register(AccountRegisterView accountRegisterView) {
            if (!accountRegisterView.IsValid(out string message)) {
                return ValidatedView.Invalid(message);
            }

            try {

                if (await Model<AccountModel>.AsQueryable()
                    .Any(x => x.Username == accountRegisterView.Username)) {
                    return ValidatedView.Invalid(ErrorCode.USERNAME_ALREADY_IN_USE);
                }

                AccountModel accountModel = new AccountModel(accountRegisterView.Username,
                    accountRegisterView.Password, accountRegisterView.Email, accountRegisterView.Faction);

                int tries = 5;
                while (await Model<AccountModel>.AsQueryable().Any(x => x.ID == accountModel.ID)) {
                    accountModel.ID = RandomGenerator.UniqueIdentifier();

                    if (tries-- == 0) {
                        return ValidatedView.Invalid(ErrorCode.PROBLEM_WHILE_CREATING_ACCOUNT);
                    }
                }

                AccountVaultModel accountVaultModel = new AccountVaultModel(accountModel.ID);
                List<HangarModel> hangarModels = accountVaultModel.Ships
                    .Select(ship => new HangarModel(accountModel.ID, ship, accountModel.FactionID))
                    .ToList();

                await Model<AccountModel>.AsCollection().InsertOneAsync(accountModel);
                await Model<AccountChatModel>.AsCollection().InsertOneAsync(new AccountChatModel(accountModel.ID));
                await Model<AccountCooldownModel>.AsCollection().InsertOneAsync(new AccountCooldownModel(accountModel.ID));
                await Model<AccountVaultModel>.AsCollection().InsertOneAsync(accountVaultModel);
                await Model<HangarModel>.AsCollection().InsertManyAsync(hangarModels);

                return ValidatedView.Valid();

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<AccountSessionView>> Login(AccountLoginView accountLoginView) {
            if (!accountLoginView.IsValid(out string message)) {
                return ValidatedView<AccountSessionView>.Invalid(message);
            }

            try {

                var accountInfo = await Model<AccountModel>.AsQueryable()
                    .Where(x => x.Username == accountLoginView.Username)
                    .Select(x => new { x.ID, x.Password, x.Salt, x.EmailVerificationDate, x.BanHistory })
                    .FirstOrDefault();

                if (accountInfo == null || !accountInfo.Password.Is(accountLoginView.Password.ComputeHash(accountInfo.Salt))) {
                    return ValidatedView<AccountSessionView>.Invalid(ErrorCode.PASSWORD_USERNAME_NOT_FOUND);
                }

                /* skip for now
                if (accountInfo.EmailVerificationDate == DateTime.MinValue) { // email not verified
                    return ValidatedView<AccountSessionView>.Invalid(ErrorCode.EMAIL_NOT_VERIFIED);
                }
                */

                AccountSessionModel sessionModel = new AccountSessionModel { AccountID = accountInfo.ID };
                await Model<AccountSessionModel>.AsCollection().InsertOneAsync(sessionModel);

                return ValidatedView<AccountSessionView>
                    .Valid(Mapper<AccountSessionModel>.Map<AccountSessionView>(sessionModel));
            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<AccountSessionView>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<GlobalRole>> Authenticate(AccountSessionView accountSessionView) {
            if (!accountSessionView.IsValid(out string message)) {
                return ValidatedView<GlobalRole>.Invalid(message);
            }

            try {

                if (!await Model<AccountSessionModel>.AsQueryable()
                    .Any(x => x.AccountID == accountSessionView.AccountID && x.Token == accountSessionView.Token)) {
                    return ValidatedView<GlobalRole>.Invalid(ErrorCode.INVALID_SESSION);
                }

                GlobalRole? role = await Model<AccountModel>.AsQueryable()
                    .Where(x => x.ID == accountSessionView.AccountID)
                    .Select(x => (GlobalRole?)x.Role).FirstOrDefault();

                if (role.HasValue) {
                    return ValidatedView<GlobalRole>.Valid(role.Value);
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<GlobalRole>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<AccountView>> RetrieveAccount(int accountId) {
            try {

                AccountModel accountModel = await Model<AccountModel>.AsQueryable()
                    .FirstOrDefault(x => x.ID == accountId);
                if (accountModel == null) {
                    return ValidatedView<AccountView>.Invalid(ErrorCode.ACCOUNT_NOT_FOUND);
                }

                AccountView accountView = Mapper<AccountModel>.Map<AccountView>(accountModel);
                accountView.GGRings = 64;

                ValidatedView<HangarView> validatedHangarView = await HangarService.RetrieveHangar(accountModel.ID, accountModel.ActiveShipID);
                if (!validatedHangarView.IsValid) {
                    return ValidatedView<AccountView>.Invalid(validatedHangarView.Message);
                }
                accountView.CurrentHangar = validatedHangarView.Object;

                ValidatedView<VaultView> validatedVaultView = await RetrieveVault(accountId);
                if (!validatedVaultView.IsValid) {
                    return ValidatedView<AccountView>.Invalid(validatedVaultView.Message);
                }
                accountView.Vault = validatedVaultView.Object;

                ValidatedView<CooldownView> validatedCooldownView = await RetrieveCooldown(accountId);
                if (!validatedCooldownView.IsValid) {
                    return ValidatedView<AccountView>.Invalid(validatedCooldownView.Message);
                }
                accountView.Cooldown = validatedCooldownView.Object;

                accountView.Clan = new ClanOverview();
                ValidatedView<ClanOverview> validatedClanOverview = await ClanService.RetrieveClanOverviewFromMember(accountId);
                if (validatedClanOverview.IsValid && validatedClanOverview.Object != null) {
                    accountView.Clan = validatedClanOverview.Object;
                }

                return ValidatedView<AccountView>.Valid(accountView);

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<AccountView>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<VaultView>> RetrieveVault(int accountId) {
            try {

                // check if player is ingame
                if (GameManager.Players.TryGetValue(accountId, out PlayerController controller)) {
                    return ValidatedView<VaultView>.Valid(controller.Account.Vault);
                }

                AccountVaultModel accountVaultModel = await Model<AccountVaultModel>.AsQueryable()
                    .FirstOrDefault(x => x.AccountID == accountId);
                if (accountVaultModel == null) {
                    return ValidatedView<VaultView>.Invalid(ErrorCode.ACCOUNT_VAULT_NOT_FOUND);
                }

                return ValidatedView<VaultView>.Valid(
                    Mapper<AccountVaultModel>.Map<VaultView>(accountVaultModel));

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<VaultView>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<CooldownView>> RetrieveCooldown(int accountId) {
            try {

                // check if player is ingame
                if (GameManager.Players.TryGetValue(accountId, out PlayerController controller)) {
                    return ValidatedView<CooldownView>.Valid(controller.Account.Cooldown);
                }

                AccountCooldownModel accountCooldownModel = await Model<AccountCooldownModel>.AsQueryable()
                    .FirstOrDefault(x => x.AccountID == accountId);
                if (accountCooldownModel == null) {
                    return ValidatedView<CooldownView>.Invalid(ErrorCode.ACCOUNT_VAULT_NOT_FOUND);
                }

                return ValidatedView<CooldownView>.Valid(
                    Mapper<AccountCooldownModel>.Map<CooldownView>(accountCooldownModel));

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<CooldownView>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<string>> RetrieveUsername(int accountId) {
            try {

                // check if player is ingame
                if (GameManager.Players.TryGetValue(accountId, out PlayerController controller)) {
                    return ValidatedView<string>.Valid(controller.Username);
                }

                string username = await Model<AccountModel>.AsQueryable()
                    .Where(x => x.ID == accountId)
                    .Select(x => x.Username)
                    .FirstOrDefault();

                if (username == null) {
                    return ValidatedView<string>.Invalid(ErrorCode.ACCOUNT_NOT_FOUND);
                }

                return ValidatedView<string>.Valid(username);

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<string>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> DeclareBan(AccountDeclareBanView accountDeclareBanView) {
            if (!accountDeclareBanView.IsValid(out string message)) {
                return ValidatedView.Invalid(message);
            }

            try {

                AccountModel accountModel = await Model<AccountModel>.AsQueryable()
                    .Where(x => x.ID == accountDeclareBanView.ID)
                    .FirstOrDefault();

                if (accountModel == null) {
                    return ValidatedView.Invalid(ErrorCode.ACCOUNT_NOT_FOUND);
                }

                accountModel.BanHistory.Add(new BanHistory {
                    Reason = accountDeclareBanView.Reason,
                    Until = DateTime.Now.Add(accountDeclareBanView.Duration)
                });

                UpdateResult result = await Model<AccountModel>.AsCollection()
                    .UpdateOneAsync(x => x.ID == accountDeclareBanView.ID,
                        new UpdateDefinitionBuilder<AccountModel>().Set(x => x.BanHistory, accountModel.BanHistory)
                    );

                if (result.IsModifiedCountAvailable && result.ModifiedCount == 1) {
                    return ValidatedView.Valid();
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> UpdateVault(int accountId, VaultView vault) {
            try {

                ReplaceOneResult result = await Model<AccountVaultModel>.AsCollection()
                    .ReplaceOneAsync(x => x.AccountID == accountId, Mapper<VaultView>
                    .Map(vault, new AccountVaultModel { AccountID = accountId }));

                if (result.IsModifiedCountAvailable && result.ModifiedCount == 1) {
                    return ValidatedView.Valid();
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> UpdateCooldown(int accountId, CooldownView cooldown) {
            try {

                ReplaceOneResult result = await Model<AccountCooldownModel>.AsCollection()
                    .ReplaceOneAsync(x => x.AccountID == accountId, Mapper<CooldownView>
                    .Map(cooldown, new AccountCooldownModel { AccountID = accountId }));

                if (result.IsModifiedCountAvailable && result.ModifiedCount == 1) {
                    return ValidatedView.Valid();
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> UpdateAccount(AccountView accountView) {
            try {

                AccountModel accountModel = await Model<AccountModel>.AsQueryable()
                    .Where(x => x.ID == accountView.ID).FirstOrDefault();
                if (accountModel == null) {
                    return ValidatedView.Invalid(ErrorCode.ACCOUNT_NOT_FOUND);
                }

                bool success = true;

                ReplaceOneResult result = await Model<AccountModel>.AsCollection()
                    .ReplaceOneAsync(x => x.ID == accountView.ID,
                    Mapper<AccountView>.Map(accountView, accountModel));
                success &= result.IsModifiedCountAvailable && result.ModifiedCount == 1;

                ValidatedView validatedHangarResultView = await HangarService
                    .UpdateHangar(accountView.ID, accountView.CurrentHangar);
                if (!validatedHangarResultView.IsValid) {
                    GameContext.Logger.LogInformation($"Player: '{accountView.ID}': {validatedHangarResultView.Message}");
                }
                success &= validatedHangarResultView.IsValid;

                ValidatedView validatedVaultResultView = await UpdateVault(accountView.ID, accountView.Vault);
                if (!validatedVaultResultView.IsValid) {
                    GameContext.Logger.LogInformation($"Player: '{accountView.ID}': {validatedVaultResultView.Message}");
                }
                success &= validatedVaultResultView.IsValid;

                ValidatedView validatedCooldownResultView = await UpdateCooldown(accountView.ID, accountView.Cooldown);
                if (!validatedCooldownResultView.IsValid) {
                    GameContext.Logger.LogInformation($"Player: '{accountView.ID}': {validatedCooldownResultView.Message}");
                }
                success &= validatedCooldownResultView.IsValid;

                if (success) {
                    return ValidatedView.Valid();
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<AccountOverview>> RetrieveAccountOverview(int accountId) {
            try {

                // check if player is ingame
                if (GameManager.Players.TryGetValue(accountId, out PlayerController controller)) {
                    AccountOverview accountOverview = Mapper<AccountView>.Map<AccountOverview>(controller.Account);
                    accountOverview.ActiveShipID = controller.Account.CurrentHangar.ShipID;
                    return ValidatedView<AccountOverview>.Valid(accountOverview);
                }

                AccountModel accountModel = await Model<AccountModel>.AsQueryable()
                    .FirstOrDefault(x => x.ID == accountId);
                if (accountModel == null) {
                    return ValidatedView<AccountOverview>.Invalid(ErrorCode.ACCOUNT_NOT_FOUND);
                }

                return ValidatedView<AccountOverview>.Valid(Mapper<AccountModel>.Map<AccountOverview>(accountModel));

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<AccountOverview>.Invalid(ErrorCode.OPERATION_FAILED);
        }

    }
}
