using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicMapper;
using EpicOrbit.Server.Data.Models;
using EpicOrbit.Server.Data.Models.Modules;
using MongoDB.Driver;
using EpicOrbit.Server.Data.ViewModels.Hangar;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels.Configuration;
using FightZone.Emulator.Extensions;
using EpicOrbit.Shared.ViewModels.Hangar;
using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Game;
using EpicOrbit.Emulator.Game.Controllers;
using EpicOrbit.Shared.ViewModels.Vault;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Server.Data.Models.ViewModels.Account;

namespace EpicOrbit.Emulator.Services {
    public static class HangarService {

        private static void UpdateHangarIngame(int accountId, HangarView hangar) {
            if (GameManager.Get(accountId, out PlayerController controller)) {

                controller.BoosterAssembly.Divide(BoosterType.SHIELD_REGNERATION,
                    Math.Max(0.0000001, controller.Account.CurrentHangar.CurrentConfiguration.Regeneration));

                controller.BoosterAssembly.Divide(BoosterType.SHIELD_ABSORBATION,
                    Math.Max(0.0000001, controller.Account.CurrentHangar.CurrentConfiguration.Absorption));

                if (hangar.ShipID == controller.Account.CurrentHangar.ShipID) {
                    controller.Account.CurrentHangar.Configuration_1 = hangar.Configuration_1;
                    controller.Account.CurrentHangar.Configuration_2 = hangar.Configuration_2;
                } else {

                    controller.UpdateState();

                    controller.PlayerAbilityAssembly.CheckOrStopAfterburner(true);
                    controller.PlayerAbilityAssembly.CheckOrStopFortress(true);
                    controller.PlayerAbilityAssembly.CheckOrStopPrismaticShield(true);
                    controller.PlayerAbilityAssembly.CheckOrStopSingularity(true);
                    controller.PlayerAbilityAssembly.CheckOrStopWeakenShields(true);

                    controller.Account.CurrentHangar = hangar;
                }

                controller.Account.CurrentHangar.Check(GameContext.Logger, controller.Account.ID, controller.Account.Vault);
                controller.Account.CurrentHangar.Calculate();

                // Wtf denkt mein kopf
                ((Action)(async () => await UpdateHangar(accountId, controller.Account.CurrentHangar)))();

                controller.HangarAssembly.BroadcastHealth();
                controller.HangarAssembly.BroadcastShield();

                controller.BoosterAssembly.Multiply(BoosterType.SHIELD_REGNERATION,
                    Math.Max(0.0000001, controller.Account.CurrentHangar.CurrentConfiguration.Regeneration));

                controller.BoosterAssembly.Multiply(BoosterType.SHIELD_ABSORBATION,
                    Math.Max(0.0000001, controller.Account.CurrentHangar.CurrentConfiguration.Absorption));

                controller.AttackTraceAssembly.Trace.Clear();
                controller.Refresh();

                // refresh all entities
                controller.EntitesInRange(x => {
                    bool wasLocked = false;
                    if (x.Locked != null && x.Locked.ID == accountId) {
                        wasLocked = true;
                        x.Lock(null);
                    }

                    x.Send(PacketBuilder.ShipRemoveCommand(accountId));
                    x.EntityAddedToMap(controller);

                    if (wasLocked) {
                        x.Lock(controller);
                    }
                });

                if (controller.Locked != null) {
                    controller.Lock(controller.Locked);
                }
            }
        }


        public static async Task<ValidatedView<HangarDetailView>> RetrieveHangarDetailView(int accountId, int shipId) {
            try {

                ValidatedView<HangarView> validatedHangarView = await RetrieveHangar(accountId, shipId);
                if (!validatedHangarView.IsValid) {
                    return ValidatedView<HangarDetailView>.Invalid(validatedHangarView.Message);
                }

                HangarDetailView hangarDetailView = Mapper<HangarView>.Map<HangarDetailView>(validatedHangarView.Object);
                hangarDetailView.IsActive = await Model<AccountModel>.AsQueryable()
                    .Any(x => x.ID == accountId && x.ActiveShipID == shipId);

                return ValidatedView<HangarDetailView>.Valid(hangarDetailView);

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<HangarDetailView>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<List<HangarOverview>>> RetrieveHangarOverviews(int accountId) {
            try {

                int? activeShipId = await Model<AccountModel>.AsQueryable()
                    .Where(x => x.ID == accountId)
                    .Select(x => (int?)x.ActiveShipID)
                    .FirstOrDefault();

                if (!activeShipId.HasValue) {
                    return ValidatedView<List<HangarOverview>>.Invalid(ErrorCode.NO_HANGAR_FOUND);
                }

                return ValidatedView<List<HangarOverview>>.Valid(await Model<HangarModel>.AsQueryable()
                    .Where(x => x.AccountID == accountId)
                    .Select(x => new HangarOverview {
                        ShipID = x.ShipID,
                        IsActive = x.ShipID == activeShipId.Value
                    }).ToList()
                );

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<List<HangarOverview>>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView<HangarView>> RetrieveHangar(int accountId, int shipId) {
            try {

                HangarModel hangarModel = await Model<HangarModel>.AsQueryable()
                    .FirstOrDefault(x => x.AccountID == accountId && x.ShipID == shipId);
                if (hangarModel == null) {
                    return ValidatedView<HangarView>.Invalid(ErrorCode.NO_HANGAR_FOUND);
                }

                HangarView hangarView = Mapper<HangarModel>.Map<HangarView>(hangarModel);
                hangarView.Configuration_1 = Mapper<Configuration>.Map<ConfigurationView>(hangarModel.Configuration_1);
                hangarView.Configuration_2 = Mapper<Configuration>.Map<ConfigurationView>(hangarModel.Configuration_2);

                return ValidatedView<HangarView>.Valid(hangarView);

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView<HangarView>.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> UpdateHangar(int accountId, HangarView hangarView) {
            if (!hangarView.IsValid(out string message)) {
                return ValidatedView.Invalid(message);
            }

            try {

                HangarModel hangarModel = Mapper<HangarView>.Map(hangarView, new HangarModel { AccountID = accountId });
                hangarModel.Configuration_1 = Mapper<ConfigurationView>.Map<Configuration>(hangarView.Configuration_1);
                hangarModel.Configuration_2 = Mapper<ConfigurationView>.Map<Configuration>(hangarView.Configuration_2);

                ReplaceOneResult result = await Model<HangarModel>.AsCollection()
                    .ReplaceOneAsync(x => x.AccountID == accountId && x.ShipID == hangarView.ShipID, hangarModel);

                if (result.IsModifiedCountAvailable && result.ModifiedCount == 1) {
                    return ValidatedView.Valid();
                }

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> UpdateHangar(int accountId, HangarDetailView hangarDetailView) {
            if (!hangarDetailView.IsValid(out string message)) {
                return ValidatedView.Invalid(message);
            }

            try {

                if (GameManager.Get(accountId, out PlayerController controller) && !controller.ZoneAssembly.CanEquip) {
                    return ValidatedView.Invalid(ErrorCode.EQUIPMENT_NOT_POSSIBLE);
                }

                HangarModel hangarModel = await Model<HangarModel>.AsQueryable()
                    .FirstOrDefault(x => x.AccountID == accountId && x.ShipID == hangarDetailView.ShipID);
                if (hangarModel == null) {
                    return ValidatedView<HangarView>.Invalid(ErrorCode.NO_HANGAR_FOUND);
                }

                if (controller != null && controller.HangarAssembly.Ship.ID == hangarDetailView.ShipID) {
                    HangarView hangarView = Mapper<HangarModel>.Map<HangarView>(hangarModel);
                    hangarView.Configuration_1 = hangarDetailView.Configuration_1;
                    hangarView.Configuration_2 = hangarDetailView.Configuration_2;

                    UpdateHangarIngame(accountId, hangarView);
                } else {
                    hangarModel.Configuration_1 = Mapper<ConfigurationView>.Map<Configuration>(hangarDetailView.Configuration_1);
                    hangarModel.Configuration_2 = Mapper<ConfigurationView>.Map<Configuration>(hangarDetailView.Configuration_2);

                    ReplaceOneResult result = await Model<HangarModel>.AsCollection()
                        .ReplaceOneAsync(x => x.AccountID == accountId && x.ShipID == hangarDetailView.ShipID, hangarModel);
                }

                return ValidatedView.Valid();

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

        public static async Task<ValidatedView> ActivateHangar(int accountId, int shipId) {
            try {

                if (GameManager.Get(accountId, out PlayerController controller) && !controller.ZoneAssembly.CanEquip) {
                    return ValidatedView.Invalid(ErrorCode.EQUIPMENT_NOT_POSSIBLE);
                }

                HangarModel hangarModel = await Model<HangarModel>.AsQueryable()
                    .FirstOrDefault(x => x.AccountID == accountId && x.ShipID == shipId);
                if (hangarModel == null) {
                    return ValidatedView<HangarView>.Invalid(ErrorCode.NO_HANGAR_FOUND);
                }

                if (await Model<AccountModel>.AsQueryable()
                    .Any(x => x.ID == accountId && x.ActiveShipID == shipId)) {
                    return ValidatedView.Invalid(ErrorCode.HANGAR_ALREADY_ACTIVE);
                }

                if (controller != null) {
                    HangarView hangarView = Mapper<HangarModel>.Map<HangarView>(hangarModel);
                    hangarView.Configuration_1 = Mapper<Configuration>.Map<ConfigurationView>(hangarModel.Configuration_1);
                    hangarView.Configuration_2 = Mapper<Configuration>.Map<ConfigurationView>(hangarModel.Configuration_2);
                    UpdateHangarIngame(accountId, hangarView);
                }

                await Model<AccountModel>.AsCollection()
                    .UpdateOneAsync(x => x.ID == accountId,
                        new UpdateDefinitionBuilder<AccountModel>()
                        .Set(x => x.ActiveShipID, shipId));

                return ValidatedView.Valid();

            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
            return ValidatedView.Invalid(ErrorCode.OPERATION_FAILED);
        }

    }
}
