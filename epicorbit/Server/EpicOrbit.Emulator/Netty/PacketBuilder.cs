using EpicOrbit.Emulator.Game.Controllers;
using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Enumerables;
using EpicOrbit.Emulator.Game.Objects;
using EpicOrbit.Emulator.Netty.Builders;
using EpicOrbit.Emulator.Netty.Commands;
using System.Collections.Generic;
using System.Text;
using static EpicOrbit.Emulator.Game.Controllers.GroupController;
using static EpicOrbit.Emulator.Netty.Builders.ClientUIMenuPacketBuilder;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Shared.Implementations;
using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty {
    public static class PacketBuilder {

        public static class Rewards {

            public static LegacyModule ChangeInt(string item, int change, long total, int meta) {
                return Legacy($"0|LM|ST|{item}|{change}|{total}|{meta}");
            }

            public static LegacyModule ChangeInt(string item, int change, long total) {
                return Legacy($"0|LM|ST|{item}|{change}|{total}");
            }

            public static LegacyModule Change(string item, string change, long total) {
                return Legacy($"0|LM|ST|{item}|{change}|{total}");
            }

            public static LegacyModule Uridium(int change, long total) {
                return ChangeInt("URI", change, total);
            }

            public static LegacyModule Credits(int change, long total) {
                return ChangeInt("CRE", change, total);
            }

            public static LegacyModule Honor(int change, long total) {
                return ChangeInt("HON", change, total);
            }

            public static IEnumerable<ICommand> Experience(int change, long total, int oldLevel) {
                int level = total.Level();
                yield return ChangeInt("EP", change, total, level);
                if (level > oldLevel) {
                    yield return Legacy($"0|A|LUP|{level}|{level.Experience() - total}");
                }
            }

        }

        public static class Group {

            public static GroupInvitationCommand InvitiationCommand(Invitation invitation) {
                return new GroupInvitationCommand(invitation.Initiator.ID, invitation.Initiator.Username, new class_504(10),
                    invitation.Target.ID, invitation.Target.Username, new class_504(10));
            }

            public static GroupInvitationStateCommand InvitiationState(PlayerController initiator) {
                return new GroupInvitationStateCommand(!initiator.PlayerGroupAssembly.AcceptInvitations);
            }

            public static class_501 InviationBlocked() {
                return new class_501(0, 0, 8);
            }

            public static class_501 PlayerDoesNotExist() {
                return new class_501(0, 0, 2);
            }

            public static class_501 PlayerAlreadyInGroup() {
                return new class_501(0, 0, 1);
            }

            public static class_1087 InvitationTimeout(Invitation invitation) {
                return new class_1087(invitation.Initiator.ID, invitation.Target.ID, 1);
            }

            public static class_1087 InvitationRejected(Invitation invitation) {
                return new class_1087(invitation.Initiator.ID, invitation.Target.ID, 3);
            }

            public static class_1087 InvitationRevoked(Invitation invitation) {
                return new class_1087(invitation.Initiator.ID, invitation.Target.ID, 2);
            }

            public static class_1087 InvitationRemoveNoReason(Invitation invitation) {
                return new class_1087(invitation.Initiator.ID, invitation.Target.ID, 0);
            }

        }

        public static class SpecialItems {

            public static LegacyModule Smartbomb(EntityControllerBase initiator) {
                return Legacy($"0|n|SMB|{initiator.ID}");
            }

            public static LegacyModule EMP(EntityControllerBase initiator) {
                return Legacy($"0|n|EMP|{initiator.ID}");
            }

            public static LegacyModule Instashield(EntityControllerBase initiator) {
                return Legacy($"0|n|ISH|{initiator.ID}");
            }

        }

        public static class Messages {

            public static LegacyModule BigMessage(string message) {
                //return Legacy($"0|n|KSMSG|{message}");
                return Legacy($"0|n|MSG|4|1|{message}");
            }

            public static LegacyModule FromIdentifier(string identifier) {
                return Legacy($"0|A|STM|{identifier}");
            }

            public static LegacyModule OutOfRange() {
                return FromIdentifier("outofrange");
            }

            public static LegacyModule NoLasersOnBoard() {
                return FromIdentifier("no_lasers_on_board");
            }


            public static LegacyModule AttackEscaped() {
                return FromIdentifier("attescape");
            }

            public static LegacyModule ConfigurationChangeFailed() {
                return FromIdentifier("config_change_failed_time");
            }

            public static LegacyModule SpeedhackWarning() {
                return FromIdentifier("speedhack_warning");
            }

            public static LegacyModule SpeedhackBan() {
                return FromIdentifier("speedhack_ban");
            }

            public static LegacyModule TargetingHarmed() {
                return FromIdentifier("msg_own_targeting_harmed");
            }

            public static LegacyModule NoLaserAmmo() {
                return FromIdentifier("chgbatmanual");
            }

            public static LegacyModule NoRocketAmmo() {
                return FromIdentifier("chgrokmanual");
            }

            public static LegacyModule PeaceArea() {
                return FromIdentifier("peacearea");
            }

        }

        public static class KillScreen {

            public static KillScreenPostCommand KillScreenCommand(short cause, PlayerController initiator = null) {
                if (initiator != null) {
                    return new KillScreenPostCommand(initiator.Username, "", initiator.HangarAssembly.Ship.Name,
                        new DestructionTypeModule(cause), new List<KillScreenOptionModule> {
                            KillScreenOptionCommand()
                        }
                    );
                }
                return new KillScreenPostCommand("Unknown", "", "", new DestructionTypeModule(cause), new List<KillScreenOptionModule> {
                    KillScreenOptionCommand()
                });
            }

            public static KillScreenOptionModule KillScreenOptionCommand() {
                return new KillScreenOptionModule(
                    new KillScreenOptionTypeModule(KillScreenOptionTypeModule.BASIC_REPAIR),
                    new PriceModule(PriceModule.CREDITS, 0),
                    true, 0,
                    new MessageLocalizedWildcardCommand(),
                    new MessageLocalizedWildcardCommand(),
                    new MessageLocalizedWildcardCommand(),
                    new MessageLocalizedWildcardCommand()
                );
            }


        }

        public static class Slotbar {

            public static ClientUISlotBarCategoryItemTimerModule ItemCooldownCommand(string lootId, double cooldown, bool activateable = true) {
                return new ClientUISlotBarCategoryItemTimerModule(lootId,
                    new ClientUISlotBarCategoryItemTimerStateModule(ClientUISlotBarCategoryItemTimerStateModule.ACTIVE),
                    cooldown, 90000000, activateable);
            }

            public static ClientUISlotBarCategoryItemStatusModule DroneFormationItemStatus(string lootId, bool selected) {
                return ClientUISlotBarPacketBuilder.CreateItemStatus(lootId, lootId, false, 0, 0, true, selected, 3);
            }

            public static ClientUISlotBarCategoryItemStatusModule ExtraItemStatus(string lootId, string ttipID, bool selected) {
                return ClientUISlotBarPacketBuilder.CreateItemStatus(lootId, ttipID, false, 0, 0, true, selected, 3);
            }

            public static ClientUISlotBarCategoryItemStatusModule ExplosiveItemStatus(string lootId, long count, bool selected) {
                return CountableItemStatus(lootId, "ttip_explosive", count, 50, selected);
            }

            public static ClientUISlotBarCategoryItemStatusModule TechItemStatus(string lootId, long count, bool selected) {
                return CountableItemStatus(lootId, lootId, count, 0, selected, 3);
            }

            public static ClientUISlotBarCategoryItemStatusModule LaserItemStatus(string lootId, long count, bool selected) {
                return CountableItemStatus(lootId, "ttip_laser", count, 1000, selected);
            }

            public static ClientUISlotBarCategoryItemStatusModule RocketItemStatus(string lootId, long count, bool selected) {
                return CountableItemStatus(lootId, "ttip_rocket", count, 200, selected);
            }

            public static ClientUISlotBarCategoryItemStatusModule CountableItemStatus(string lootId, string tooltipId, long count, long maxCount, bool selected, short localisation = 5) {
                return ClientUISlotBarPacketBuilder.CreateItemStatus(lootId, tooltipId, true, count, maxCount, true, selected, localisation);
            }

            /*  public static ClientUISlotBarCategoryItemStatusModule SelectItemCommand(string lootId, bool active) {
                  return new ClientUISlotBarCategoryItemStatusModule(true, true, lootId,
                      null, null, false, 0, 0, ClientUISlotBarCategoryItemStatusModule.BLUE,
                      lootId, true, active, false);
              }


              public static ClientUISlotBarCategoryItemStatusModule SelectCountableItemCommand(string lootId, int count, int maxCount, bool active) {
                  short color = ClientUISlotBarCategoryItemStatusModule.BLUE;
                  if (count * 5 <= maxCount) {
                      color = ClientUISlotBarCategoryItemStatusModule.RED;
                  }

                  return new ClientUISlotBarCategoryItemStatusModule(count > 0, true, lootId,
                      null, null, false, maxCount, count, color,
                      lootId, count > 0, active, false);
              }


              public static ClientUISlotBarCategoryItemStatusModule SelectLaserItemCommand(string lootId, int count, bool active) {
                  return SelectCountableItemCommand(lootId, count, 1000, active);
              }

              public static ClientUISlotBarCategoryItemStatusModule SelectSpecialItemCommand(string lootId, int count, bool active) {
                  return SelectCountableItemCommand(lootId, count, 100, active);
              } */

            public static ClientUISlotBarsCommand SlotBarsCommand(PlayerController initiator, bool full = true) {
                return ClientUISlotBarPacketBuilder.Generate(initiator.ClientConfiguration, initiator, full);
            }

            public static ClientUISlotBarCategoryItemModule RocketLauncherCommand(PlayerController initiator) {
                return ClientUISlotBarPacketBuilder.CreateRocketLauncher(initiator);
            }

            public static ClientUISlotBarCategoryItemStatusModule RocketLauncherStateCommand(PlayerController initiator) {
                return ClientUISlotBarPacketBuilder.RocketLauncherState(initiator, out _);
            }

        }

        public static ClanChangedCommand ClanChangedCommand(PlayerController initiator) {
            return new ClanChangedCommand(initiator.Account.Clan.Tag, initiator.Account.Clan.ID, initiator.ID);
        }

        public static LegacyModule MapNoiseCommand() {
            return Legacy("0|UI|MM|NOISE|1|1");
        }

        public static LegacyModule MainAttackerChangedCommand(EntityControllerBase target, int initiator) {
            return Legacy($"0|n|LSH|{target.ID}|{initiator}"); // dieser Command ist nicht ganz richtig, man müsste eine verneinung dazu finden
        }

        public static LegacyModule MainAttackerUnsetCommand(EntityControllerBase target) {
            return Legacy($"0|n|USH|{target.ID}");
        }

        public static UserKeyBindingsUpdate UserKeyBindinsCommand(PlayerController initiator) {
            return new UserKeyBindingsUpdate(initiator.ClientConfiguration.KeyBindings, false);
        }

        public static ClientUIMenuBarsCommand UIMenuBarsCommand(PlayerController initiator) {
            return ClientUIMenuPacketBuilder.Generate(initiator.ClientConfiguration, new Dictionary<string, Window>() {
                //  { "traininggrounds", "title_traininggrounds" },
                //  { "booster", "title_booster" }
            });
        }

        public static LegacyModule MapWarnCountCommand(int count) {
            return Legacy($"0|n|w|{count}");
        }

        public static JumpgateCreateCommand CreatePortalCommand(PortalObject portal) {
            return new JumpgateCreateCommand(1, 0, 1, portal.Position.X, portal.Position.Y, true, true, null);
        }


        public static LoginErrorCommand DoubleLoggedIn(PlayerController initiator) {
            return new LoginErrorCommand(LoginErrorCommand.ALREADY_LOGGED_IN, initiator.Account.CurrentHangar.MapID);
        }

        public static LoginErrorCommand InvalidSession() {
            return new LoginErrorCommand(LoginErrorCommand.INVALID_SESSION_ID, 0);
        }

        public static ShipInitializationCommand InitializeShipCommand(PlayerController initiator) {
            return new ShipInitializationCommand(initiator.ID, initiator.Account.Username, initiator.HangarAssembly.Ship.Name,
                initiator.HangarAssembly.Speed, initiator.HangarAssembly.Shield, initiator.HangarAssembly.MaxShield,
                initiator.HangarAssembly.Hitpoints, initiator.HangarAssembly.MaxHitpoints, 0, initiator.HangarAssembly.Ship.Cargo,
                0, 0, initiator.HangarAssembly.Position.X, initiator.HangarAssembly.Position.Y,
                initiator.HangarAssembly.Map.ID, initiator.Account.FactionID, initiator.Account.Clan.ID, initiator.HangarAssembly.Ship.Expansionstage,
                initiator.Account.IsPremium, initiator.Account.Experience, initiator.Account.Honor, initiator.Account.Level,
                initiator.Account.Credits, initiator.Account.Uridium, 10000, initiator.Account.RankID,
                initiator.Account.Clan.Tag, initiator.Account.GGRings, true, initiator.EffectsAssembly.Cloaked, true);
        }

        public static LegacyModule DroneCommand(PlayerController initiator) {
            StringBuilder sb = new StringBuilder($"0|n|d|{initiator.ID}|{initiator.DroneFormationAssembly.DroneFormation.ID}"); // implement drone formations
            foreach (var drone in initiator.Account.CurrentHangar.CurrentConfiguration.Drones) {
                sb.Append($"|{drone.DroneID / 10}|{drone.DroneID - (drone.DroneID / 10) * 10}|{drone.StatsDesignID}|{drone.VisualDesignID}");
            }
            return new LegacyModule(sb.ToString());
        }

        public static LegacyModule ConfigurationCommand(PlayerController initiator) {
            return new LegacyModule($"0|A|CC|{(initiator.Account.CurrentHangar.Configuration ? 2 : 1)}");
        }

        public static AttributeSpeedUpdateCommand SpeedChangeCommand(EntityControllerBase initiator) {
            return new AttributeSpeedUpdateCommand(initiator.HangarAssembly.Speed,
                initiator.HangarAssembly.Speed);
        }

        public static LegacyModule Legacy(string message) {
            return new LegacyModule(message);
        }

        public static ShipCreateCommand CreateShipCommand(PlayerController initiator, EntityControllerBase target) {
            switch (target) { // fehlt noch switch für protegiten und clanbasen
                case PlayerController targetPlayerController:

                    Map map = targetPlayerController.HangarAssembly.Map;
                    bool isOnDifferentStarter = targetPlayerController.Faction.ID != map.OwnerFaction.ID && map.IsStarter;

                    ClanRelationModule clanRelation = new ClanRelationModule(ClanRelationModule.NONE);
                    if (targetPlayerController.Account.Clan.ID != 0) {
                        if (targetPlayerController.Account.Clan.ID == initiator.Account.Clan.ID) {
                            clanRelation.type = ClanRelationModule.ALLIED;
                        }

                        // do other checking
                    }

                    return new ShipCreateCommand(targetPlayerController.ID, targetPlayerController.HangarAssembly.Ship.Name,
                        targetPlayerController.HangarAssembly.Ship.Expansionstage, targetPlayerController.Account.Clan.Tag,
                        targetPlayerController.Account.Username, targetPlayerController.HangarAssembly.Position.X,
                        targetPlayerController.HangarAssembly.Position.Y, targetPlayerController.Faction.ID, targetPlayerController.Account.Clan.ID,
                        targetPlayerController.Account.RankID, isOnDifferentStarter, clanRelation, targetPlayerController.Account.GGRings, true, false,
                        targetPlayerController.EffectsAssembly.Cloaked, 0, 0, "");

                default:
                    return new ShipCreateCommand(target.ID, target.HangarAssembly.Ship.Name, target.HangarAssembly.Ship.Expansionstage,
                        "", target.Username, target.HangarAssembly.Position.X, target.HangarAssembly.Position.Y, target.Faction.ID,
                        0, 0, false, null, 0, true, true, target.EffectsAssembly.Cloaked, 0, 0, "");
            }

        }

        /// <summary>
        /// Sende ich dem Spieler der einen anderen gelockt hat. Dieser Command kann auch "reparieren"
        /// </summary>
        /// <param name="initiator"></param>
        /// <returns></returns>
        public static AttackHitCommand TargetHealthChangeCommand(EntityControllerBase initiator, EntityControllerBase target) {
            return AttackCommand(initiator, target, AttackTypeModule.REPAIR, 0);
        }

        public static AttackHitCommand AttackCommand(EntityControllerBase initiator, EntityControllerBase target, short attackType, int damage) {
            if (target == null) {
                return null;
            }

            return new AttackHitCommand(new AttackTypeModule(attackType), initiator.ID, target.ID,
                target.HangarAssembly.Hitpoints, target.HangarAssembly.Shield, 0, damage, false); // letzter parameter muss noch adaptiert werden (pilot sheet)
        }

        public static HellstormAttackCommand AttackHellstormCommand(EntityControllerBase initiator, EntityControllerBase target, int amount, short rocketType) {
            return new HellstormAttackCommand(initiator.ID, target.ID, true, amount, new AmmunitionTypeModule(rocketType));
        }

        public static LegacyModule AttackRocketCommand(EntityControllerBase initiator, EntityControllerBase target, int rocketType) {
            int effectID = 0;
            if (initiator is PlayerController playerController && playerController.PlayerTechAssembly.PrecisionTargeterActive) {
                effectID = 2;
            }

            return Legacy($"0|v|{initiator.ID}|{target.ID}|H|{rocketType}|{effectID}|1");
        }

        public static LegacyModule SendEffect(EntityControllerBase initiator, string effect) {
            return Legacy($"0|n|fx|start|{effect}|{initiator.ID}");
        }

        public static LegacyModule SendEffect2(EntityControllerBase initiator, string effect) {
            return Legacy($"0|n|{effect}|SET|{initiator.ID}");
        }

        public static LegacyModule RemoveEffect2(EntityControllerBase initiator, string effect) {
            return Legacy($"0|n|{effect}|REM|{initiator.ID}");
        }

        public static LegacyModule RemoveEffect(EntityControllerBase initiator, string effect) {
            return Legacy($"0|n|fx|end|{effect}|{initiator.ID}");
        }

        public static LegacyModule SendTechEffect(EntityControllerBase initiator, string effect) {
            return Legacy($"0|TX|A|S|{effect}|{initiator.ID}");
        }

        public static VisualModifierCommand VisualModifier(EntityControllerBase initiator, short modifier, bool activated, string attribute = "") {
            return new VisualModifierCommand(initiator.ID, modifier, 0, attribute, 1, activated);
        }

        public static LegacyModule RemoveTechEffect(EntityControllerBase initiator, string effect) {
            return Legacy($"0|TX|D|S|{effect}|{initiator.ID}");
        }

        public static LegacyModule CloakCommand(EntityControllerBase initiator) {
            return Legacy($"0|n|INV|{initiator.ID}|1");
        }

        public static LegacyModule UnCloakCommand(EntityControllerBase initiator) {
            return Legacy($"0|n|INV|{initiator.ID}|0");
        }

        public static AttackLaserRunCommand AttackLaserCommand(EntityControllerBase initiator, EntityControllerBase target, int laserType) {
            return new AttackLaserRunCommand(initiator.ID, target.ID, laserType, false, true);
        }

        public static DroneFormationChangeCommand DroneFormationChangeCommand(PlayerController initiator) {
            return new DroneFormationChangeCommand(initiator.ID, initiator.DroneFormationAssembly.DroneFormation.ID);
        }

        public static AttributeShieldUpdateCommand ShieldChangeCommand(EntityControllerBase initiator) {
            return new AttributeShieldUpdateCommand(initiator.HangarAssembly.Shield, initiator.HangarAssembly.MaxShield);
        }

        public static LegacyModule ShieldChangeDisplayedCommand(EntityControllerBase initiator, int change) {
            return Legacy("0|A|HL|" + initiator.ID + "|" + initiator.ID + "|SHD|" + initiator.HangarAssembly.Shield + "|" + change);
        }

        public static LegacyModule HitpointsChangeDisplayedCommand(EntityControllerBase initiator, int change) {
            return Legacy("0|A|HL|" + initiator.ID + "|" + initiator.ID + "|HPT|" + initiator.HangarAssembly.Hitpoints + "|" + change);
        }

        public static AttributeHitpointUpdateCommand HitpointsChangeCommand(EntityControllerBase initiator) {
            return new AttributeHitpointUpdateCommand(initiator.HangarAssembly.Hitpoints, initiator.HangarAssembly.MaxHitpoints, 0, 0);
        }

        public static ShipDeselectionCommand DeselectionCommand() {
            return new ShipDeselectionCommand();
        }

        public static ShipSelectionCommand SelectCommand(EntityControllerBase target) {
            // wtf is shipType? maybe the group icon???
            return new ShipSelectionCommand(target.ID, 1, target.HangarAssembly.Shield, target.HangarAssembly.MaxShield,
                target.HangarAssembly.Hitpoints, target.HangarAssembly.MaxHitpoints, 0, 0, true); // letzter param, pilot sheet
        }

        public static ShipSelectionCommand TargetHealthBaseChangedCommand(EntityControllerBase target) {
            return SelectCommand(target);
        }

        public static MoveCommand MoveCommand(EntityControllerBase initiator, Position destination, int time) {
            return new MoveCommand(initiator.ID, destination.X, destination.Y, time);
        }

        public static KillCommand KillCommand(EntityControllerBase initiator) {
            return new KillCommand(initiator.ID);
        }

        public static ShipRemoveCommand ShipRemoveCommand(int id) {
            return new ShipRemoveCommand(id);
        }

    }
}
