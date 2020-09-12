using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicMapper;
using EpicOrbit.Emulator.Game;
using EpicOrbit.Emulator.Game.Controllers;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Server.Data.Models;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Server.Data.Models.ViewModels.Account;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels;
using EpicOrbit.Shared.ViewModels.Account;
using EpicOrbit.Shared.ViewModels.Clan;
using FightZone.Emulator.Extensions;
using MongoDB.Driver;

namespace EpicOrbit.Emulator.Services {
    public static class ClanService {

        public static async Task<ValidatedView<EnumerableResultView<ClanView>>> RetrieveClans(int accountId, string query, int offset, int count) {
            try {

                int totalCount = await Model<ClanModel>.AsQueryable().Count();
                List<ClanView> clanViews = new List<ClanView>();

                List<int> pending = await Model<ClanMemberPendingModel>.AsQueryable()
                    .Where(x => x.AccountID == accountId).Select(x => x.ClanID).ToList();

                List<int> clans = await Model<ClanModel>.AsQueryable().Where(x => pending.Contains(x.ID) && x.Name.ToLower().Contains(query) && x.Tag.ToLower().Contains(query)).OrderBy(x => x.Points)
                    .Union(Model<ClanModel>.AsQueryable().Where(x => !pending.Contains(x.ID) && x.Name.ToLower().Contains(query) && x.Tag.ToLower().Contains(query)).OrderBy(x => x.Points))
                    .Skip(Math.Max(0, offset))
                    .Take(Math.Max(0, Math.Min(count, 50)))
                    .Select(x => x.ID)
                    .ToList();

                foreach (int id in clans) {
                    ValidatedView<ClanView> validatedView = await RetrieveClan(accountId, id);
                    if (validatedView.IsValid) {
                        clanViews.Add(validatedView.Object);
                    } else {
                        GameContext.Logger.LogWarning($"Clan retrieval failed: {validatedView.Message}");
                    }
                }

                return ValidatedView<EnumerableResultView<ClanView>>
                    .Valid(new EnumerableResultView<ClanView>(Math.Max(0, offset), clanViews.Count,
                                                                totalCount, clanViews));

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<EnumerableResultView<ClanView>>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<List<AccountClanView>>> RetrievePendingFromMember(int accountId) {
            try {

                List<AccountClanView> accountClanViews = new List<AccountClanView>();

                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>
                   .AsQueryable().FirstOrDefault(x => x.AccountID == accountId);
                if (clanMemberModel != null) {
                    ClanModel clanModel = await Model<ClanModel>.AsQueryable()
                        .FirstOrDefault(x => x.ID == clanMemberModel.ClanID);
                    if (clanModel == null) {
                        GameContext.Logger.LogWarning($"Player [id: '{accountId}'] is member of a clan [id: '{clanMemberModel.ClanID}'] which does not even exist");
                    } else {
                        foreach (ClanMemberPendingModel pendingMember in await Model<ClanMemberPendingModel>
                            .AsQueryable()
                            .Where(x => x.ClanID == clanModel.ID).ToList()) {

                            AccountClanView accountClanView = new AccountClanView {
                                JoinDate = pendingMember.CreationDate,
                                Message = pendingMember.Message
                            };

                            if (GameManager.Players.TryGetValue(pendingMember.AccountID, out PlayerController controller)) {
                                accountClanView = Mapper<AccountView>.Map(controller.Account, accountClanView);
                                accountClanView.ActiveShipID = controller.Account.CurrentHangar.ShipID;
                                accountClanView.Online = true;
                                accountClanView.Map = controller.HangarAssembly.Map.Name;
                            } else {
                                AccountModel accountModel = await Model<AccountModel>.AsQueryable()
                                    .FirstOrDefault(x => x.ID == pendingMember.AccountID);
                                if (accountModel == null) {
                                    GameContext.Logger.LogCritical($"Player [id: '{pendingMember.AccountID}'] not found!");
                                    continue;
                                }

                                accountClanView = Mapper<AccountModel>.Map(accountModel, accountClanView);
                            }

                            accountClanViews.Add(accountClanView);
                        }
                    }
                } else {
                    return ValidatedView<List<AccountClanView>>.Invalid(ErrorCode.CLAN_NOT_MEMBER);
                }

                return ValidatedView<List<AccountClanView>>.Valid(accountClanViews);

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<List<AccountClanView>>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<AccountClanView>> RetrieveSelf(int accountId) {
            try {

                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>
                  .AsQueryable().FirstOrDefault(x => x.AccountID == accountId);
                if (clanMemberModel != null) {
                    ClanModel clanModel = await Model<ClanModel>.AsQueryable()
                        .FirstOrDefault(x => x.ID == clanMemberModel.ClanID);
                    if (clanModel == null) {
                        GameContext.Logger.LogWarning($"Player [id: '{accountId}'] is member of a clan [id: '{clanMemberModel.ClanID}'] which does not even exist");
                    } else {

                        AccountClanView accountClanView = new AccountClanView {
                            Role = clanMemberModel.Role,
                            JoinDate = clanMemberModel.CreationDate
                        };

                        if (GameManager.Players.TryGetValue(clanMemberModel.AccountID, out PlayerController controller)) {
                            accountClanView = Mapper<AccountView>.Map(controller.Account, accountClanView);

                            accountClanView.ActiveShipID = controller.Account.CurrentHangar.ShipID;
                            accountClanView.Online = true;
                            accountClanView.Map = controller.HangarAssembly.Map.Name;

                            Position actualPosition = controller.MovementAssembly.ActualPosition();
                            accountClanView.Position = (actualPosition.X, actualPosition.Y);
                        } else {
                            AccountModel accountModel = await Model<AccountModel>.AsQueryable()
                                .FirstOrDefault(x => x.ID == clanMemberModel.AccountID);
                            if (accountModel == null) {
                                GameContext.Logger.LogCritical($"Player [id: '{clanMemberModel.AccountID}'] not found!");
                                return ValidatedView<AccountClanView>.Invalid(ErrorCode.ACCOUNT_NOT_FOUND);
                            }

                            accountClanView = Mapper<AccountModel>.Map(accountModel, accountClanView);
                        }

                        return ValidatedView<AccountClanView>.Valid(accountClanView);
                    }
                } else {
                    return ValidatedView<AccountClanView>.Invalid(ErrorCode.CLAN_NOT_MEMBER);
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<AccountClanView>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<List<AccountClanView>>> RetrieveClanMembersFromMember(int accountId) {
            try {

                List<AccountClanView> accountClanViews = new List<AccountClanView>();

                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>
                   .AsQueryable().FirstOrDefault(x => x.AccountID == accountId);
                if (clanMemberModel != null) {
                    ClanModel clanModel = await Model<ClanModel>.AsQueryable()
                        .FirstOrDefault(x => x.ID == clanMemberModel.ClanID);
                    if (clanModel == null) {
                        GameContext.Logger.LogWarning($"Player [id: '{accountId}'] is member of a clan [id: '{clanMemberModel.ClanID}'] which does not even exist");
                    } else {

                        foreach (ClanMemberModel clanMember in await Model<ClanMemberModel>
                            .AsQueryable()
                            .Where(x => x.ClanID == clanModel.ID).ToList()) {

                            AccountClanView accountClanView = new AccountClanView {
                                Role = clanMember.Role,
                                JoinDate = clanMember.CreationDate
                            };

                            if (GameManager.Players.TryGetValue(clanMember.AccountID, out PlayerController controller)) {
                                accountClanView = Mapper<AccountView>.Map(controller.Account, accountClanView);

                                accountClanView.ActiveShipID = controller.Account.CurrentHangar.ShipID;
                                accountClanView.Online = true;
                                accountClanView.Map = controller.HangarAssembly.Map.Name;

                                Position actualPosition = controller.MovementAssembly.ActualPosition();
                                accountClanView.Position = (actualPosition.X, actualPosition.Y);
                            } else {
                                AccountModel accountModel = await Model<AccountModel>.AsQueryable()
                                    .FirstOrDefault(x => x.ID == clanMember.AccountID);
                                if (accountModel == null) {
                                    GameContext.Logger.LogCritical($"Player [id: '{clanMember.AccountID}'] not found!");
                                    continue;
                                }

                                accountClanView = Mapper<AccountModel>.Map(accountModel, accountClanView);
                            }

                            accountClanViews.Add(accountClanView);

                        }

                    }
                } else {
                    return ValidatedView<List<AccountClanView>>.Invalid(ErrorCode.CLAN_NOT_MEMBER);
                }

                return ValidatedView<List<AccountClanView>>.Valid(accountClanViews);

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<List<AccountClanView>>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<ClanView>> RetrieveClanViewFromMember(int accountId) {
            try {

                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>
                    .AsQueryable().FirstOrDefault(x => x.AccountID == accountId);
                if (clanMemberModel != null) {
                    return await RetrieveClan(accountId, clanMemberModel.ClanID);
                }

                return ValidatedView<ClanView>.Valid(null);

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<ClanView>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<ClanOverview>> RetrieveClanOverviewFromMember(int accountId) {
            try {

                ClanOverview clanOverview = null;
                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>
                    .AsQueryable().FirstOrDefault(x => x.AccountID == accountId);
                if (clanMemberModel != null) {
                    ClanModel clanModel = await Model<ClanModel>.AsQueryable()
                        .FirstOrDefault(x => x.ID == clanMemberModel.ClanID);
                    if (clanModel == null) {
                        GameContext.Logger.LogWarning($"Player [id: '{accountId}'] is member of a clan [id: '{clanMemberModel.ClanID}'] which does not even exist");
                    } else {
                        clanOverview = Mapper<ClanModel>.Map<ClanOverview>(clanModel);
                    }
                }

                return ValidatedView<ClanOverview>.Valid(clanOverview);
            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<ClanOverview>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<ClanView>> RetrieveClan(int accountId, int clanId) {
            try {

                ClanModel clanModel = await Model<ClanModel>.AsQueryable().FirstOrDefault(x => x.ID == clanId);
                if (clanModel == null) { // wtf kann nd sein
                    return ValidatedView<ClanView>.Invalid(ErrorCode.CLAN_NOT_FOUND);
                }

                ClanView clanOverview = Mapper<ClanModel>.Map<ClanView>(clanModel);

                clanOverview.LeaderUsername = "";
                ValidatedView<int> validatedLeaderIDView = await RetrieveLeaderID(clanModel.ID);
                if (validatedLeaderIDView.IsValid) {
                    ValidatedView<string> validedLeaderUsernameView = await AccountService.RetrieveUsername(validatedLeaderIDView.Object);
                    if (validedLeaderUsernameView.IsValid) {
                        clanOverview.LeaderUsername = validedLeaderUsernameView.Object;
                    }
                }

                clanOverview.MembersCount = await Model<ClanMemberModel>.AsQueryable().Count(x => x.ClanID == clanModel.ID);

                if (await Model<ClanMemberPendingModel>.AsQueryable()
                    .Any(x => x.AccountID == accountId && x.ClanID == clanId)) {
                    clanOverview.Pending = true;
                }

                return ValidatedView<ClanView>.Valid(clanOverview);

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<ClanView>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<int>> RetrieveLeaderID(int clanId) {
            try {

                int? leaderId = await Model<ClanMemberModel>.AsQueryable()
                     .Where(x => x.ClanID == clanId && x.Role == ClanRole.LEADER)
                     .Select(x => (int?)x.AccountID)
                     .FirstOrDefault();

                if (!leaderId.HasValue) {
                    return ValidatedView<int>.Invalid(ErrorCode.CLAN_NOT_FOUND);
                }

                return ValidatedView<int>.Valid(leaderId.Value);

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<int>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> CreateClan(int accountId, ClanCreateView clanCreateView) {
            if (!clanCreateView.IsValid(out string message)) {
                return ValidatedView.Invalid(message);
            }

            try {

                if (await Model<ClanMemberModel>.AsQueryable().Any(x => x.AccountID == accountId)) {
                    GameContext.Logger.LogCritical($"Player [id: '{accountId}'] tries creating a clan while being member of another!");
                    return ValidatedView.Invalid(ErrorCode.CLAN_ALREADY_MEMBER);
                }

                if (await Model<ClanModel>.AsQueryable().Any(x => x.Name == clanCreateView.Name)) {
                    return ValidatedView.Invalid(ErrorCode.CLAN_NAME_ALREADY_IN_USE);
                }

                if (await Model<ClanModel>.AsQueryable().Any(x => x.Tag == clanCreateView.Tag)) {
                    return ValidatedView.Invalid(ErrorCode.CLAN_TAG_ALREADY_IN_USE);
                }

                ClanModel clanModel = Mapper<ClanCreateView>.Map<ClanModel>(clanCreateView);
                ClanMemberModel clanMemberModel = new ClanMemberModel { ClanID = clanModel.ID, AccountID = accountId, Role = ClanRole.LEADER };

                await Model<ClanMemberPendingModel>.AsCollection().DeleteManyAsync(x => x.AccountID == accountId);
                await Model<ClanModel>.AsCollection().InsertOneAsync(clanModel);
                await Model<ClanMemberModel>.AsCollection().InsertOneAsync(clanMemberModel);

                if (GameManager.Get(accountId, out PlayerController controller)) {
                    controller.Account.Clan = Mapper<ClanModel>.Map<ClanOverview>(clanModel);

                    ClanChangedCommand clanChangedCommand = PacketBuilder.ClanChangedCommand(controller);
                    controller.Send(clanChangedCommand);
                    controller.EntitesInRange(x => x.Send(clanChangedCommand));
                }

                return ValidatedView.Valid();

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> RevokeJoinRequest(int accountId, int clanId) {
            try {

                var result = await Model<ClanMemberPendingModel>
                    .AsCollection()
                    .DeleteOneAsync(x => x.AccountID == accountId && x.ClanID == clanId);

                if (result.DeletedCount == 0) {
                    return ValidatedView.Invalid(ErrorCode.CLAN_FAILED_TO_REVOKE_REQUEST);
                }

                return ValidatedView.Valid();

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> CreateJoinRequest(int accountId, ClanJoinView clanJoinView) {
            if (!clanJoinView.IsValid(out string message)) {
                return ValidatedView.Invalid(message);
            }

            try {

                if (await Model<ClanMemberPendingModel>.AsQueryable()
                    .Any(x => x.AccountID == accountId && x.ClanID == clanJoinView.ClanID)) {
                    return ValidatedView.Invalid(ErrorCode.CLAN_REQUEST_ALREADY_EXISTS);
                }

                if (!await Model<ClanModel>.AsQueryable()
                    .Any(x => x.ID == clanJoinView.ClanID)) {
                    return ValidatedView.Invalid(ErrorCode.CLAN_NOT_FOUND);
                }

                await Model<ClanMemberPendingModel>.AsCollection().InsertOneAsync(new ClanMemberPendingModel {
                    AccountID = accountId,
                    ClanID = clanJoinView.ClanID,
                    Message = clanJoinView.Description
                });

                return ValidatedView.Valid();

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> AcceptJoinRequest(int accountId, int targetId) {
            try {

                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>.AsQueryable()
                   .FirstOrDefault(x => x.AccountID == accountId);

                if (clanMemberModel == null || clanMemberModel.Role < ClanRole.VICE_LEADER) {
                    return ValidatedView.Invalid(ErrorCode.CLAN_ACCEPT_MEMBER_INSUFFICIENT_RIGHTS);
                }

                if (await Model<ClanMemberModel>.AsQueryable().Count(x => x.ClanID == clanMemberModel.ClanID) >= 50) {
                    return ValidatedView.Invalid(ErrorCode.CLAN_FULL);
                }

                ClanMemberPendingModel clanMemberPendingModel = await Model<ClanMemberPendingModel>.AsQueryable()
                    .FirstOrDefault(x => x.AccountID == targetId && x.ClanID == clanMemberModel.ClanID);

                if (clanMemberPendingModel == null) {
                    return ValidatedView.Invalid(ErrorCode.CLAN_PENDING_DID_NOT_REQUEST);
                }

                ClanMemberModel newClanMemberModel = new ClanMemberModel { ClanID = clanMemberModel.ClanID, AccountID = targetId };
                await Model<ClanMemberPendingModel>.AsCollection().DeleteManyAsync(x => x.AccountID == targetId);
                await Model<ClanMemberModel>.AsCollection().InsertOneAsync(newClanMemberModel);

                if (GameManager.Get(accountId, out PlayerController controller)) {
                    ValidatedView<ClanOverview> validatedView = await RetrieveClanOverviewFromMember(accountId);
                    if (validatedView.IsValid) {
                        controller.Account.Clan = validatedView.Object ?? new ClanOverview();
                    }

                    ClanChangedCommand clanChangedCommand = PacketBuilder.ClanChangedCommand(controller);
                    controller.Send(clanChangedCommand);
                    controller.EntitesInRange(x => x.Send(clanChangedCommand));
                }

                return ValidatedView.Valid();

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> RejectJoinRequest(int accountId, int targetId) {
            try {

                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>.AsQueryable()
                   .FirstOrDefault(x => x.AccountID == accountId);

                if (clanMemberModel == null || clanMemberModel.Role < ClanRole.VICE_LEADER) {
                    return ValidatedView.Invalid(ErrorCode.CLAN_ACCEPT_MEMBER_INSUFFICIENT_RIGHTS);
                }

                ClanMemberPendingModel clanMemberPendingModel = await Model<ClanMemberPendingModel>.AsQueryable()
                    .FirstOrDefault(x => x.AccountID == targetId && x.ClanID == clanMemberModel.ClanID);

                if (clanMemberPendingModel == null) {
                    return ValidatedView.Invalid(ErrorCode.CLAN_PENDING_DID_NOT_REQUEST);
                }

                await Model<ClanMemberPendingModel>.AsCollection()
                    .DeleteOneAsync(x => x.AccountID == targetId && x.ClanID == clanMemberModel.ClanID);

                return ValidatedView.Valid();

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> Delete(int clanId) {
            try {

                await Model<ClanMemberModel>.AsCollection()
                            .DeleteManyAsync(x => x.ClanID == clanId);

                await Model<ClanMemberPendingModel>.AsCollection()
                    .DeleteManyAsync(x => x.ClanID == clanId);

                await Model<ClanRelationModel>.AsCollection()
                    .DeleteManyAsync(x => x.InitiatorID == clanId || x.TargetID == clanId);

                await Model<ClanRelationPendingModel>.AsCollection()
                   .DeleteManyAsync(x => x.InitiatorID == clanId || x.TargetID == clanId);

                await Model<ClanLogModel>.AsCollection()
                    .DeleteManyAsync(x => x.ClanID == clanId);

                await Model<ClanModel>.AsCollection()
                    .DeleteOneAsync(x => x.ID == clanId);

                // Update emulator to remove from cache

                return ValidatedView.Valid();

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> Leave(int accountId) {
            try {

                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>
                   .AsQueryable().FirstOrDefault(x => x.AccountID == accountId);
                if (clanMemberModel != null) {
                    ClanModel clanModel = await Model<ClanModel>.AsQueryable()
                        .FirstOrDefault(x => x.ID == clanMemberModel.ClanID);
                    if (clanModel == null) {
                        return ValidatedView.Invalid(ErrorCode.CLAN_NOT_FOUND);
                    } else {

                        if (await Model<ClanMemberModel>.AsQueryable()
                            .Where(x => x.ClanID == clanModel.ID).Count() > 1
                            && clanMemberModel.Role == ClanRole.LEADER) {
                            return ValidatedView.Invalid(ErrorCode.CLAN_CANNOT_LEAVE_WHILE_LEADER);
                        }

                        if (GameManager.Get(accountId, out PlayerController controller)) {
                            if (!controller.ZoneAssembly.CanEquip) {
                                return ValidatedView.Invalid(ErrorCode.CLAN_CANNOT_LEAVE_WHILE_NOT_AT_BASE);
                            }
                        }

                        await Model<ClanMemberModel>.AsCollection()
                            .DeleteOneAsync(x => x.AccountID == accountId);

                        if (clanMemberModel.Role == ClanRole.LEADER) {
                            await Delete(clanModel.ID);
                        }

                        if (GameManager.Get(accountId, out controller)) {
                            controller.Account.Clan = new ClanOverview();

                            ClanChangedCommand clanChangedCommand = PacketBuilder.ClanChangedCommand(controller);
                            controller.Send(clanChangedCommand);
                            controller.EntitesInRange(x => x.Send(clanChangedCommand));
                        }

                        return ValidatedView.Valid();

                    }
                } else {
                    return ValidatedView.Invalid(ErrorCode.CLAN_NOT_MEMBER);
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> AssignRole(int accountId, int targetId, ClanRole role) {
            try {

                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>
                   .AsQueryable().FirstOrDefault(x => x.AccountID == accountId);
                if (clanMemberModel != null) {
                    ClanModel clanModel = await Model<ClanModel>.AsQueryable()
                        .FirstOrDefault(x => x.ID == clanMemberModel.ClanID);
                    if (clanModel == null) {
                        return ValidatedView.Invalid(ErrorCode.CLAN_NOT_FOUND);
                    } else {

                        ClanMemberModel targetMemberModel = await Model<ClanMemberModel>
                            .AsQueryable()
                            .FirstOrDefault(x => x.AccountID == targetId && x.ClanID == clanModel.ID);

                        if (targetMemberModel == null) {
                            return ValidatedView.Invalid(ErrorCode.CLAN_TARGET_NOT_MEMBER);
                        }

                        if (targetMemberModel.Role == ClanRole.LEADER
                            || clanMemberModel.Role < ClanRole.VICE_LEADER
                            || clanMemberModel.Role < role) {
                            return ValidatedView.Invalid(ErrorCode.CLAN_MANAGE_MEMBER_INSUFFICIENT_RIGHTS);
                        }

                        if (targetMemberModel.Role == role) {
                            return ValidatedView.Valid();
                        }

                        targetMemberModel.Role = role;
                        await Model<ClanMemberModel>.AsCollection().ReplaceOneAsync(x =>
                                x.AccountID == targetId && x.ClanID == clanModel.ID, targetMemberModel);

                        if (role == ClanRole.LEADER) {
                            clanMemberModel.Role = ClanRole.VICE_LEADER;
                            await Model<ClanMemberModel>.AsCollection().ReplaceOneAsync(x =>
                               x.AccountID == accountId && x.ClanID == clanModel.ID, clanMemberModel);
                        }

                        return ValidatedView.Valid();

                    }
                } else {
                    return ValidatedView.Invalid(ErrorCode.CLAN_NOT_MEMBER);
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> Edit(int accountId, ClanCreateView clanUpdateView) {
            if (!clanUpdateView.IsValid(out string message)) {
                return ValidatedView.Invalid(message);
            }

            try {

                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>
                    .AsQueryable().FirstOrDefault(x => x.AccountID == accountId);
                if (clanMemberModel != null) {
                    ClanModel clanModel = await Model<ClanModel>.AsQueryable()
                        .FirstOrDefault(x => x.ID == clanMemberModel.ClanID);
                    if (clanModel == null) {
                        return ValidatedView.Invalid(ErrorCode.CLAN_NOT_FOUND);
                    } else {

                        if (clanMemberModel.Role < ClanRole.LEADER) {
                            return ValidatedView.Invalid(ErrorCode.CLAN_MANAGE_INSUFFICIENT_RIGHTS);
                        }

                        if (clanModel.Name == clanUpdateView.Name
                            && clanModel.Tag == clanUpdateView.Tag
                            && clanModel.Description == clanUpdateView.Description) {
                            return ValidatedView.Valid();
                        }

                        if (clanModel.Name != clanUpdateView.Name
                            && await Model<ClanModel>.AsQueryable().Any(x => x.Name == clanUpdateView.Name)) {
                            return ValidatedView.Invalid(ErrorCode.CLAN_NAME_ALREADY_IN_USE);
                        }

                        bool clanTagChanged = clanModel.Tag != clanUpdateView.Tag;
                        if (clanTagChanged
                            && await Model<ClanModel>.AsQueryable().Any(x => x.Tag == clanUpdateView.Tag)) {
                            return ValidatedView.Invalid(ErrorCode.CLAN_TAG_ALREADY_IN_USE);
                        }

                        clanModel = Mapper<ClanCreateView>.Map(clanUpdateView, clanModel);
                        await Model<ClanModel>.AsCollection()
                            .ReplaceOneAsync(x => x.ID == clanModel.ID, clanModel);

                        foreach (ClanMemberModel member in await Model<ClanMemberModel>.AsQueryable()
                            .Where(x => x.ClanID == clanModel.ID).ToList()) {
                            if (GameManager.Get(member.AccountID, out PlayerController controller)) {
                                controller.Account.Clan = Mapper<ClanModel>.Map<ClanOverview>(clanModel);

                                ClanChangedCommand clanChangedCommand = PacketBuilder.ClanChangedCommand(controller);
                                controller.Send(clanChangedCommand);
                                controller.EntitesInRange(x => x.Send(clanChangedCommand));
                            }
                        }

                        return ValidatedView.Valid();

                    }
                } else {
                    return ValidatedView.Invalid(ErrorCode.CLAN_NOT_MEMBER);
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }


        // Clan diplomacy

        public static async Task<ValidatedView<List<ClanDiplomacyView>>> RetrieveDiplomacies(int accountId) {
            try {

                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>
                   .AsQueryable().FirstOrDefault(x => x.AccountID == accountId);
                if (clanMemberModel != null) {
                    ClanModel clanModel = await Model<ClanModel>.AsQueryable()
                        .FirstOrDefault(x => x.ID == clanMemberModel.ClanID);
                    if (clanModel == null) {
                        return ValidatedView<List<ClanDiplomacyView>>.Invalid(ErrorCode.CLAN_NOT_FOUND);
                    } else {

                        List<ClanDiplomacyView> diplomacies = new List<ClanDiplomacyView>();
                        foreach (ClanRelationModel clanRelationModel in await Model<ClanRelationModel>
                            .AsQueryable().Where(x => x.InitiatorID == clanModel.ID || x.TargetID == clanModel.ID)
                            .ToList()) {
                            ClanDiplomacyView clanDiplomacyView = Mapper<ClanRelationModel>.Map<ClanDiplomacyView>(clanRelationModel);
                            ValidatedView<ClanView> validatedView = await RetrieveClan(accountId,
                                (clanRelationModel.InitiatorID == clanModel.ID ? clanRelationModel.TargetID
                                : clanRelationModel.InitiatorID));

                            if (!validatedView.IsValid) {
                                GameContext.Logger.LogWarning($"Failed to load diplomacy for {clanModel.ID}!");
                                continue;
                            }

                            clanDiplomacyView.Clan = validatedView.Object;
                            diplomacies.Add(clanDiplomacyView);
                        }

                        return ValidatedView<List<ClanDiplomacyView>>.Valid(diplomacies);

                    }
                } else {
                    return ValidatedView<List<ClanDiplomacyView>>.Invalid(ErrorCode.CLAN_NOT_MEMBER);
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<List<ClanDiplomacyView>>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<List<ClanDiplomacyView>>> RetrievePendingDiplomacies(int accountId) {
            try {

                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>
                  .AsQueryable().FirstOrDefault(x => x.AccountID == accountId);
                if (clanMemberModel != null) {
                    ClanModel clanModel = await Model<ClanModel>.AsQueryable()
                        .FirstOrDefault(x => x.ID == clanMemberModel.ClanID);
                    if (clanModel == null) {
                        return ValidatedView<List<ClanDiplomacyView>>.Invalid(ErrorCode.CLAN_NOT_FOUND);
                    } else {

                        List<ClanDiplomacyView> diplomacies = new List<ClanDiplomacyView>();
                        foreach (ClanRelationPendingModel clanRelationModel in await Model<ClanRelationPendingModel>
                            .AsQueryable().Where(x => x.InitiatorID == clanModel.ID || x.TargetID == clanModel.ID)
                            .ToList()) {
                            ClanDiplomacyView clanDiplomacyView = Mapper<ClanRelationPendingModel>.Map<ClanDiplomacyView>(clanRelationModel);
                            ValidatedView<ClanView> validatedView = await RetrieveClan(accountId,
                                (clanRelationModel.InitiatorID == clanModel.ID ? clanRelationModel.TargetID
                                : clanRelationModel.InitiatorID));

                            if (!validatedView.IsValid) {
                                GameContext.Logger.LogWarning($"Failed to load pending diplomacy for {clanModel.ID}!");
                                continue;
                            }

                            clanDiplomacyView.Clan = validatedView.Object;
                            diplomacies.Add(clanDiplomacyView);
                        }

                        return ValidatedView<List<ClanDiplomacyView>>.Valid(diplomacies);

                    }
                } else {
                    return ValidatedView<List<ClanDiplomacyView>>.Invalid(ErrorCode.CLAN_NOT_MEMBER);
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<List<ClanDiplomacyView>>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> CreateDiplomacy(int accountId, ClanDiplomacyCreateView clanDiplomacyCreateView) {
            if (!clanDiplomacyCreateView.IsValid(out string message)) {
                return ValidatedView.Invalid(message);
            }

            try {

                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>
                    .AsQueryable().FirstOrDefault(x => x.AccountID == accountId);
                if (clanMemberModel != null) {
                    ClanModel clanModel = await Model<ClanModel>.AsQueryable()
                        .FirstOrDefault(x => x.ID == clanMemberModel.ClanID);
                    if (clanModel == null) {
                        return ValidatedView.Invalid(ErrorCode.CLAN_NOT_FOUND);
                    } else {

                        if (clanMemberModel.Role < ClanRole.DIPLOMAT) {
                            return ValidatedView.Invalid(ErrorCode.CLAN_MANAGE_DIPLOMACIES_INSUFFICIENT_RIGHTS);
                        }

                        if (await Model<ClanRelationModel>.AsQueryable()
                            .Any(x => (x.InitiatorID == clanModel.ID || x.InitiatorID == clanDiplomacyCreateView.TargetID)
                                && (x.TargetID == clanModel.ID || x.TargetID == clanDiplomacyCreateView.TargetID))) {
                            return ValidatedView.Invalid(ErrorCode.CLAN_RELATION_ALREADY_EXISTS);
                        }

                        await Model<ClanRelationPendingModel>.AsCollection()
                            .DeleteManyAsync(x => (x.InitiatorID == clanModel.ID || x.InitiatorID == clanDiplomacyCreateView.TargetID)
                                && (x.TargetID == clanModel.ID || x.TargetID == clanDiplomacyCreateView.TargetID));

                        if (clanDiplomacyCreateView.Type == ClanRelationType.WAR) { // sofortige Wirkung

                            ClanRelationModel clanRelationModel = Mapper<ClanDiplomacyCreateView>.Map<ClanRelationModel>(clanDiplomacyCreateView);
                            clanRelationModel.InitiatorID = clanModel.ID;

                            await Model<ClanRelationModel>.AsCollection()
                                .InsertOneAsync(clanRelationModel);

                        } else {

                            ClanRelationPendingModel clanRelationPendingModel = Mapper<ClanDiplomacyCreateView>.Map<ClanRelationPendingModel>(clanDiplomacyCreateView);
                            clanRelationPendingModel.InitiatorID = clanModel.ID;

                            await Model<ClanRelationPendingModel>.AsCollection()
                               .InsertOneAsync(clanRelationPendingModel);

                        }

                        return ValidatedView.Valid();

                    }
                } else {
                    return ValidatedView.Invalid(ErrorCode.CLAN_NOT_MEMBER);
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> AcceptDiplomacy(int accountId, int targetId) {
            try {

                ClanMemberModel clanMemberModel = await Model<ClanMemberModel>
                    .AsQueryable().FirstOrDefault(x => x.AccountID == accountId);
                if (clanMemberModel != null) {
                    ClanModel clanModel = await Model<ClanModel>.AsQueryable()
                        .FirstOrDefault(x => x.ID == clanMemberModel.ClanID);
                    if (clanModel == null) {
                        return ValidatedView.Invalid(ErrorCode.CLAN_NOT_FOUND);
                    } else {

                        if (clanMemberModel.Role < ClanRole.DIPLOMAT) {
                            return ValidatedView.Invalid(ErrorCode.CLAN_MANAGE_DIPLOMACIES_INSUFFICIENT_RIGHTS);
                        }

                        if (await Model<ClanRelationModel>.AsQueryable()
                           .Any(x => (x.InitiatorID == clanModel.ID || x.InitiatorID == targetId)
                               && (x.TargetID == clanModel.ID || x.TargetID == targetId))) {
                            return ValidatedView.Invalid(ErrorCode.CLAN_RELATION_ALREADY_EXISTS);
                        }

                        ClanRelationPendingModel clanRelationPendingModel = await Model<ClanRelationPendingModel>.AsQueryable()
                            .FirstOrDefault(x => (x.InitiatorID == clanModel.ID || x.InitiatorID == targetId)
                               && (x.TargetID == clanModel.ID || x.TargetID == targetId));

                        if (clanRelationPendingModel == null) {
                            return ValidatedView.Invalid(ErrorCode.CLAN_PENDING_RELATION_NOT_FOUND);
                        }

                        await Model<ClanRelationPendingModel>.AsCollection()
                          .DeleteManyAsync(x => (x.InitiatorID == clanModel.ID || x.InitiatorID == targetId)
                              && (x.TargetID == clanModel.ID || x.TargetID == targetId));

                        if (clanRelationPendingModel.Type == ClanRelationType.WAR) {



                        } else {

                        }

                        return ValidatedView.Valid();

                    }
                } else {
                    return ValidatedView.Invalid(ErrorCode.CLAN_NOT_MEMBER);
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

    }
}
