using EpicOrbit.Server.Data.Models.Enumerables;
using EpicOrbit.Server.Data.Models.Items;
using EpicOrbit.Server.Data.Models.Items.Extensions;
using EpicOrbit.Emulator.Game.Controllers;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection;
using EpicOrbit.Emulator.Game.Objects;
using EpicOrbit.Emulator.Netty.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.Items.Extensions;

namespace EpicOrbit.Emulator.Netty.Builders {
    public static class ClientUISlotBarPacketBuilder {

        public static ClientUISlotBarsCommand Generate(UserClientConfiguration configuration, PlayerController controller, bool full = true) {
            List<ClientUISlotBarModule> slotBars = new List<ClientUISlotBarModule>() {
                    new ClientUISlotBarModule("standardSlotBar", configuration.StandardSlotBar, configuration.SlotbarPositions.standardSlotBarPosition, configuration.SlotbarPositions.standardSlotBarLayout, true),
                    new ClientUISlotBarModule("proActionBar", configuration.ProActionBar, configuration.SlotbarPositions.proActionBarPosition, configuration.SlotbarPositions.proActionBarLayout, false),
                    new ClientUISlotBarModule("premiumSlotBar", configuration.PremiumSlotBar, configuration.SlotbarPositions.premiumSlotBarPosition, configuration.SlotbarPositions.premiumSlotBarLayout, true)
            };

            if (full) {
                List<ClientUISlotBarCategoryModule> categories = new List<ClientUISlotBarCategoryModule>() {
                    LaserCategory(controller),
                    RocketCategory(controller),
                    RocketLauncherCategory(controller),
                    SpecialItemCategory(controller),
                    MineCategory(controller),
                    CpuCategory(controller),
                    new ClientUISlotBarCategoryModule("buy_now"),
                    TechItemCategory(controller),
                    AbilityCategory(controller),
                    DroneFormationCategory(controller)
            };

                return new ClientUISlotBarsCommand(categories, configuration.SlotbarPositions.categoryBarPosition, slotBars);
            }

            return new ClientUISlotBarsCommand(null, configuration.SlotbarPositions.categoryBarPosition, slotBars);
        }

        private static ClientUISlotBarCategoryModule LaserCategory(PlayerController controller) {

            List<ClientUISlotBarCategoryItemModule> items = new List<ClientUISlotBarCategoryItemModule>();
            foreach (var ammunition in AmunitionSelectionHandler.Instance.Items.OrderBy(x => x.ID)) {
                controller.Account.Vault.Ammunitions.TryGetValue(ammunition.ID, out int count);

                items.Add(CreateCategoryItem(ammunition.Name, "ttip_laser", true, count, 1000, 0, true,
                    controller.Account.CurrentHangar.Selection.Laser == ammunition.ID));
            }

            return new ClientUISlotBarCategoryModule("lasers", items);

        }

        private static ClientUISlotBarCategoryModule RocketCategory(PlayerController controller) {

            List<ClientUISlotBarCategoryItemModule> items = new List<ClientUISlotBarCategoryItemModule>();
            foreach (var rocket in RocketAmunitionSelectionHandler.Instance.Items.OrderBy(x => x.ID)) {
                controller.Account.Vault.RocketAmmunitions.TryGetValue(rocket.ID, out int count);

                items.Add(CreateCategoryItem(rocket.Name, "ttip_rocket", true, count, 200, 0, true,
                    controller.Account.CurrentHangar.Selection.Rocket == rocket.ID));
            }

            return new ClientUISlotBarCategoryModule("rockets", items);

        }

        private static ClientUISlotBarCategoryModule RocketLauncherCategory(PlayerController controller) {

            List<ClientUISlotBarCategoryItemModule> items = new List<ClientUISlotBarCategoryItemModule> {
                CreateRocketLauncher(controller)
            };

            foreach (var launcherRocket in RocketLauncherAmunitionSelectionHandler.Instance.Items.OrderBy(x => x.ID)) {
                controller.Account.Vault.RocketLauncherAmmunitions.TryGetValue(launcherRocket.ID, out int count);

                items.Add(CreateCategoryItem(launcherRocket.Name, "ttip_rocket", true, count, 200, 0, true,
                    controller.Account.CurrentHangar.Selection.Rocket == launcherRocket.ID));
            }

            return new ClientUISlotBarCategoryModule("rocket_launchers", items);

        }

        internal static ClientUISlotBarCategoryItemModule CreateRocketLauncher(PlayerController controller) {



            /*    RocketLauncherAmmunition rocketLauncherAmmunition = controller.Account.CurrentHangar.Selection.RocketLauncher.FromRocketLauncherAmmunitions();
                string selectedRLAmmo = rocketLauncherAmmunition.Name;

                bool loaded = true;
                if (!controller.Account.Vault.RocketLauncherAmmunition.TryGetValue(rocketLauncherAmmunition.ID, out int count)) {
                    loaded = false;
                    selectedRLAmmo = "equipment_weapon_rocketlauncher_hst-2";// "equipment_weapon_rocketlauncher_hst-2";
                } */

            TimeSpan cooldownLeft = TimeSpan.Zero;
            if (DateTime.Now <
                controller.Account.Cooldown.RocketLauncherLastFire
                + TimeSpan.FromMilliseconds(
                    RocketLauncherAmmunition.Cooldown.TotalMilliseconds
                    * controller.BoosterAssembly.Get(BoosterType.ROCKET_LAUNCHER_COOLDOWN))
                ) {
                cooldownLeft = TimeSpan.FromMilliseconds(RocketLauncherAmmunition.Cooldown.TotalMilliseconds * controller.BoosterAssembly.Get(BoosterType.ROCKET_LAUNCHER_COOLDOWN))
                    - (DateTime.Now - controller.Account.Cooldown.RocketLauncherLastFire);
            }

            return new ClientUISlotBarCategoryItemModule(
                    1, RocketLauncherState(controller, out bool loaded),
                    new ClientUISlotBarCategoryItemTimerModule("equipment_weapon_rocketlauncher_hst",
                        new ClientUISlotBarCategoryItemTimerStateModule(ClientUISlotBarCategoryItemTimerStateModule.READY),
                        (long)cooldownLeft.TotalMilliseconds, 90000000, true
                    ),
                    new CooldownTypeModule(CooldownTypeModule.ROCKET_LAUNCHER),
                    (short)(loaded ? 3 : 0), 0, false
                );
        }

        internal static ClientUISlotBarCategoryItemStatusModule RocketLauncherState(PlayerController controller, out bool loaded) {
            string selectedRLAmmo = "equipment_weapon_rocketlauncher_hst-2";
            int count = 0;
            loaded = true;


            ClientUITooltipsCommand tooltips = new ClientUITooltipsCommand(new List<ClientUITooltipModule> {
                new ClientUITooltipModule(
                    ClientUITooltipModule.STANDARD, "ttip_rocketlauncher",
                    new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.LOCALIZED),
                    new List<MessageWildcardReplacementModule> {
                        new MessageWildcardReplacementModule("%TYPE%", "equipment_weapon_rocketlauncher_hst",
                            new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.LOCALIZED)
                        )
                    }
                ),

                new ClientUITooltipModule(
                    ClientUITooltipModule.STANDARD, "ttip_rocketlauncher_loadcount",
                    new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.LOCALIZED),
                    new List<MessageWildcardReplacementModule> {
                        new MessageWildcardReplacementModule("%COUNT%", count.ToString(),
                            new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.PLAIN)
                        ),
                        new MessageWildcardReplacementModule("%TYPE%", selectedRLAmmo,
                            new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.const_3135)
                        )
                    }
                ),
            });

            if (controller.Account.CurrentHangar.Selection.RocketLauncher != 0) {
                RocketLauncherAmmunition rocketLauncherAmmunition = controller.Account.CurrentHangar.Selection.RocketLauncher.FromRocketLauncherAmmunitions();

                selectedRLAmmo = rocketLauncherAmmunition.Name;
                count = controller.Account.CurrentHangar.Selection.RocketLauncherLoadedCount;
                loaded = true;
            }

            return new ClientUISlotBarCategoryItemStatusModule(true, true, "equipment_weapon_rocketlauncher_hst",
                        tooltips,
                        tooltips,
                        false, controller.HangarAssembly.Ship.RocketLauncherSlots * 5,
                        count, ClientUISlotBarCategoryItemStatusModule.BLUE, selectedRLAmmo,
                        true, false, false
                    );
        }

        private static ClientUISlotBarCategoryModule SpecialItemCategory(PlayerController controller) {

            List<ClientUISlotBarCategoryItemModule> items = new List<ClientUISlotBarCategoryItemModule>();
            foreach (var specialItem in SpecialItemSelectionHandler.Instance.Items.OrderBy(x => x.ID)) {
                controller.Account.Vault.SpecialItems.TryGetValue(specialItem.ID, out int count);
                controller.Account.Cooldown.SpecialItemCooldown.TryGetValue(specialItem.ID, out DateTime lastActivation);

                TimeSpan cooldownLeft = TimeSpan.Zero;
                if (DateTime.Now < lastActivation + specialItem.Cooldown) {
                    cooldownLeft = specialItem.Cooldown - (DateTime.Now - lastActivation);
                }

                items.Add(CreateCategoryItem(specialItem.Name, "ttip_explosive", true, count, 50, (long)cooldownLeft.TotalMilliseconds, true, false));
            }

            return new ClientUISlotBarCategoryModule("special_items", items);

        }

        private static ClientUISlotBarCategoryModule MineCategory(PlayerController controller) {

            TimeSpan cooldownLeft = TimeSpan.Zero;
            if (DateTime.Now < controller.Account.Cooldown.LastMine + Mine.Cooldown) {
                cooldownLeft = Mine.Cooldown - (DateTime.Now - controller.Account.Cooldown.LastMine);
            }

            List<ClientUISlotBarCategoryItemModule> items = new List<ClientUISlotBarCategoryItemModule>();
            foreach (var mine in MineSelectionHandler.Instance.Items.OrderBy(x => x.ID)) {
                controller.Account.Vault.Mines.TryGetValue(mine.ID, out int count);

                items.Add(CreateCategoryItem(mine.Name, "ttip_explosive", true, count, 50, (long)cooldownLeft.TotalMilliseconds, true, false));
            }

            return new ClientUISlotBarCategoryModule("mines", items);

        }

        private static ClientUISlotBarCategoryModule CpuCategory(PlayerController controller) {

            List<ClientUISlotBarCategoryItemModule> items = new List<ClientUISlotBarCategoryItemModule>();
            foreach (var extra in ExtraSelectionHandler.Instance.Items.OrderBy(x => x.ID)) {
                if (extra.ID == Extra.ANTI_Z1.ID || extra.ID == Extra.CL04K.ID) {
                    controller.Account.Vault.Extras.TryGetValue(extra.ID, out int count);
                    items.Add(CreateCategoryItem(extra.Name, extra.TTIP, true, count, 0, 0, true, false, 1));
                } else {
                    bool selected =
                           extra.ID == Extra.AROL_X.ID && controller.Account.CurrentHangar.Selection.AutoRocketCpu
                        || extra.ID == Extra.RL_LB_X.ID && controller.Account.CurrentHangar.Selection.AutoRocketLauncherCpu;

                    items.Add(CreateCategoryItem(extra.Name, extra.TTIP, false, 0, 0, 0, true, selected));
                }
            }

            return new ClientUISlotBarCategoryModule("cpus", items);

        }

        private static ClientUISlotBarCategoryModule TechItemCategory(PlayerController controller) {

            List<ClientUISlotBarCategoryItemModule> items = new List<ClientUISlotBarCategoryItemModule>();
            foreach (var tech in TechFactorySelectionHandler.Instance.Items.OrderBy(x => x.ID)) {
                controller.Account.Vault.Techs.TryGetValue(tech.ID, out int count);
                controller.Account.Cooldown.TechCooldown.TryGetValue(tech.ID, out DateTime lastActivation);

                TimeSpan cooldownLeft = TimeSpan.Zero;
                if (DateTime.Now < lastActivation + tech.Cooldown + tech.Duration) {
                    cooldownLeft = (tech.Cooldown + tech.Duration) - (DateTime.Now - lastActivation);
                    if (cooldownLeft > tech.Cooldown) {
                        cooldownLeft -= tech.Cooldown;
                    }
                }

                items.Add(CreateCategoryItem(tech.Name, tech.Name, true, count, 0, (long)cooldownLeft.TotalMilliseconds, true, false, 1, 3));
            }

            return new ClientUISlotBarCategoryModule("tech_items", items);

        }

        private static ClientUISlotBarCategoryModule AbilityCategory(PlayerController controller) {

            List<ClientUISlotBarCategoryItemModule> items = new List<ClientUISlotBarCategoryItemModule>();
            foreach (var ability in controller.HangarAssembly.Ship.Abilities.OrderBy(x => x.ID)) {
                controller.Account.Cooldown.AbilityCooldown.TryGetValue(ability.ID, out DateTime lastActivation);

                TimeSpan cooldownLeft = TimeSpan.Zero;
                if (DateTime.Now < lastActivation + ability.Cooldown + ability.Duration) {
                    cooldownLeft = (ability.Cooldown + ability.Duration) - (DateTime.Now - lastActivation);
                    if (cooldownLeft > ability.Cooldown) {
                        cooldownLeft -= ability.Cooldown; // ability still running
                    }
                }

                items.Add(CreateCategoryItem(ability.Name, ability.Name, false, 0, 0, (long)cooldownLeft.TotalMilliseconds, true, false, 0, 3));
            }

            return new ClientUISlotBarCategoryModule("ship_abilities", items);

        }

        private static ClientUISlotBarCategoryModule DroneFormationCategory(PlayerController controller) {

            List<ClientUISlotBarCategoryItemModule> items = new List<ClientUISlotBarCategoryItemModule>();
            foreach (var formation in DroneFormationSelectionHandler.Instance.Items.OrderBy(x => x.ID)) {
                if (!controller.Account.Vault.DroneFormations.Contains(formation.ID)) {
                    continue;
                }

                items.Add(CreateCategoryItem(formation.Name, formation.Name, false, 0, 0, 0, true,
                    controller.DroneFormationAssembly.DroneFormation.ID == formation.ID, 0, 3));
            }

            return new ClientUISlotBarCategoryModule("drone_formations", items);

        }

        public static ClientUISlotBarCategoryItemStatusModule CreateItemStatus(string pLootId, string pTooltipId, bool pCountable, long pCount, long pMaxCount, bool pAvailable, bool pSelected, short localisation) {

            // item bar tooltip
            List<ClientUITooltipModule> tooltipItemBars = new List<ClientUITooltipModule>();

            // --------------------------------------------------------
            // last argument List (vec<class_721>)
            List<MessageWildcardReplacementModule> vec_721_1 = new List<MessageWildcardReplacementModule>();

            if (localisation == ClientUITooltipTextFormatModule.LOCALIZED) {
                // fill last argument List (class_521 -> vec<721>)
                ClientUITooltipTextFormatModule x_521_1 =
                        new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.const_3135);
                MessageWildcardReplacementModule x_721_1 = new MessageWildcardReplacementModule("%TYPE%", pLootId, x_521_1);
                vec_721_1.Add(x_721_1);

                // text format
                ClientUITooltipTextFormatModule class521_localized_1 =
                        new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.LOCALIZED);

                // create tooltip
                ClientUITooltipModule slotBarItemStatusTooltip_1 =
                        new ClientUITooltipModule(ClientUITooltipModule.STANDARD, pTooltipId, class521_localized_1, vec_721_1);

                // add tooltip to tooltip bar
                tooltipItemBars.Add(slotBarItemStatusTooltip_1);
            } else {


                ClientUITooltipModule slotBarItemStatusTooltip_1 =
                       new ClientUITooltipModule(ClientUITooltipModule.STANDARD, pTooltipId, new ClientUITooltipTextFormatModule(localisation), vec_721_1);
                tooltipItemBars.Add(slotBarItemStatusTooltip_1);

            }

            // --------------------------------------------------------

            if (pCountable) {
                // last argument List (vec<class_721>)
                List<MessageWildcardReplacementModule> vec_721_2 = new List<MessageWildcardReplacementModule>();

                // fill last argument List (class_521 -> vec<721>)
                ClientUITooltipTextFormatModule class521_plain =
                        new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.PLAIN);
                MessageWildcardReplacementModule x_721_2 =
                        new MessageWildcardReplacementModule("%COUNT%", pCount.ToString(), class521_plain);
                vec_721_2.Add(x_721_2);

                // TODO shall we use new one or reuse old one???
                // text format
                ClientUITooltipTextFormatModule class521_localized_2 =
                        new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.LOCALIZED);

                // create tooltip
                ClientUITooltipModule slotBarItemStatusTooltip_2 =
                        new ClientUITooltipModule(ClientUITooltipModule.STANDARD, "ttip_count", class521_localized_2,
                                                  vec_721_2);

                // add tooltip to tooltip bar
                tooltipItemBars.Add(slotBarItemStatusTooltip_2);
            }

            // --------------------------------------------------------


            // last argument List (vec<class_721>)
            List<MessageWildcardReplacementModule> vec_721_3 = new List<MessageWildcardReplacementModule>();

            // text format
            ClientUITooltipTextFormatModule x_521_3 =
                    new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.const_296);

            ClientUITooltipModule slotBarItemStatusTooltip_3 =
                    new ClientUITooltipModule(ClientUITooltipModule.STANDARD, pLootId, x_521_3, vec_721_3);

            // add tooltip to tooltip bar
            tooltipItemBars.Add(slotBarItemStatusTooltip_3);

            // --------------------------------------------------------


            // last argument List (vec<class_721>)
            List<MessageWildcardReplacementModule> vec_721_4 = new List<MessageWildcardReplacementModule>();

            // text format
            ClientUITooltipTextFormatModule class521_localized_4 =
                    new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.LOCALIZED);

            /*
            // create tooltip
            ClientUITooltipModule slotBarItemStatusTooltip_4 =
                    new ClientUITooltipModule(ClientUITooltipModule.STANDARD, "ttip_double_click_to_fire", class521_localized_4, vec_721_4);

            // add tooltip to tooltip bar
            tooltipItemBars.Add(slotBarItemStatusTooltip_4);
            */

            // ========================================================
            // ========================================================

            //slot bar tooltip
            List<ClientUITooltipModule> tooltipSlotBars = new List<ClientUITooltipModule>();

            // last argument List (vec<class_721>)
            List<MessageWildcardReplacementModule> vec_721_5 = new List<MessageWildcardReplacementModule>();

            if (localisation == ClientUITooltipTextFormatModule.LOCALIZED) {
                // fill last argument List (class_521 -> vec<721>)
                ClientUITooltipTextFormatModule x_521_5 =
                    new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.const_3135);
                MessageWildcardReplacementModule x_721_5 = new MessageWildcardReplacementModule("%TYPE%", pLootId, x_521_5);
                vec_721_5.Add(x_721_5);

                ClientUITooltipTextFormatModule class521_localized_5 =
                        new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.LOCALIZED);

                // create tooltip
                ClientUITooltipModule slotBarItemStatusTooltip_5 =
                        new ClientUITooltipModule(ClientUITooltipModule.STANDARD, pTooltipId, class521_localized_5, vec_721_5);

                // add tooltip to tooltip slot bar
                tooltipSlotBars.Add(slotBarItemStatusTooltip_5);
            } else {

                ClientUITooltipModule slotBarItemStatusTooltip_1 =
                      new ClientUITooltipModule(ClientUITooltipModule.STANDARD, pTooltipId, new ClientUITooltipTextFormatModule(localisation), vec_721_1);
                tooltipSlotBars.Add(slotBarItemStatusTooltip_1);

            }

            // --------------------------------------------------------

            if (pCountable) {
                // last argument List (vec<class_721>)
                List<MessageWildcardReplacementModule> vec_721_6 = new List<MessageWildcardReplacementModule>();

                // fill last argument List (class_521 -> vec<721>)
                ClientUITooltipTextFormatModule tf_plain_6 =
                        new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.PLAIN);
                MessageWildcardReplacementModule x_721_6 =
                        new MessageWildcardReplacementModule("%COUNT%", pCount.ToString(), tf_plain_6);
                vec_721_6.Add(x_721_6);

                //text format
                ClientUITooltipTextFormatModule tf_localized_6 =
                        new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.LOCALIZED);

                // create tooltip
                ClientUITooltipModule slotBarItemStatusTooltip_6 =
                        new ClientUITooltipModule(ClientUITooltipModule.STANDARD, "ttip_count", tf_localized_6, vec_721_6);

                // add tooltip to tooltip slot bar
                tooltipSlotBars.Add(slotBarItemStatusTooltip_6);
            }

            // --------------------------------------------------------

            // last argument List (vec<class_721>)
            List<MessageWildcardReplacementModule> vec_721_7 = new List<MessageWildcardReplacementModule>();

            // text format
            ClientUITooltipTextFormatModule tf_234_7 =
                    new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.const_296);

            // create tooltip
            ClientUITooltipModule slotBarItemStatusTooltip_7 =
                    new ClientUITooltipModule(ClientUITooltipModule.STANDARD, pLootId, tf_234_7, vec_721_7);

            // add tooltip to tooltip slot bar
            tooltipSlotBars.Add(slotBarItemStatusTooltip_7);

            // ========================================================

            // create item bar & slot bar tooltip commands
            ClientUITooltipsCommand itemBarStatusTootip = new ClientUITooltipsCommand(tooltipItemBars);
            ClientUITooltipsCommand slotBarStatusTooltip = new ClientUITooltipsCommand(tooltipSlotBars);

            short color = ClientUISlotBarCategoryItemStatusModule.BLUE;
            if (pCountable && pCount * 5 <= pMaxCount) {
                color = ClientUISlotBarCategoryItemStatusModule.RED;
            }

            bool activateAble = !pCountable || pCount > 0;

            return new ClientUISlotBarCategoryItemStatusModule(true, pAvailable, pLootId,
                itemBarStatusTootip, slotBarStatusTooltip, false, pMaxCount, pCount, color,
                pLootId, activateAble, pSelected, false);


            /*  List<ClientUITooltipModule> tooltips = new List<ClientUITooltipModule> { // Wenns mal dazu kommen sollte, so gehört sich das
            new ClientUITooltipModule(ClientUITooltipModule.STANDARD, ttip,
                      new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.LOCALIZED),
                      new List<MessageWildcardReplacementModule> {
                          new MessageWildcardReplacementModule("%TYPE%", itemId,
                              new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.const_3135))
                      }
                  ),

                  new ClientUITooltipModule(ClientUITooltipModule.STANDARD, "ttip_count",
                      new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.LOCALIZED),
                      new List<MessageWildcardReplacementModule> {
                          new MessageWildcardReplacementModule("%COUNT%", count.ToString(),
                              new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.PLAIN)
                          )
                      }
                  ),

                  new ClientUITooltipModule(ClientUITooltipModule.STANDARD, itemId,
                      new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.const_296)
                  )
              };

            ClientUITooltipsCommand slotBarTooltips = new ClientUITooltipsCommand(tooltips);

            if (initiator.ClientConfiguration.UserSettings.gameplaySettingsModule.doubleclickAttack) {
                slotBarTooltips = new ClientUITooltipsCommand(tooltips.ToList());
                tooltips.Add(
                    new ClientUITooltipModule(ClientUITooltipModule.STANDARD, "ttip_double_click_to_fire",
                        new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.LOCALIZED)
                    )
                );
            }

            ClientUITooltipsCommand itemBarTooltips = new ClientUITooltipsCommand(tooltips);

            return new ClientUISlotBarCategoryItemStatusModule(true, count > 0, itemId,
                    itemBarTooltips, slotBarTooltips, false, 1000, count,
                    count > 200 ? ClientUISlotBarCategoryItemStatusModule.BLUE : ClientUISlotBarCategoryItemStatusModule.RED,
                    itemId, count > 0, selected, false
                ); */
        }

        private static ClientUISlotBarCategoryItemModule CreateCategoryItem(string pLootId, string pTooltipId,
                                                             bool pCountable, long pCount, long pMaxCount,
                                                             long pCooldownTime, bool pAvailable,
                                                             bool pSelected, short counterStyle = ClientUISlotBarCategoryItemModule.SELECTION,
                                                             short localisation = ClientUITooltipTextFormatModule.LOCALIZED) {




            ClientUISlotBarCategoryItemStatusModule rocketsCategoryItemStatus = CreateItemStatus(pLootId, pTooltipId,
                pCountable, pCount, pMaxCount, pAvailable, pSelected, localisation);

            // create category timer
            ClientUISlotBarCategoryItemTimerStateModule categoryItemTimerState =
                    new ClientUISlotBarCategoryItemTimerStateModule(ClientUISlotBarCategoryItemTimerStateModule.ACTIVE);
            ClientUISlotBarCategoryItemTimerModule categoryTimerModule =
                    new ClientUISlotBarCategoryItemTimerModule(pLootId, categoryItemTimerState, pCooldownTime, 90000000,
                                                               false);

            // create 5th parameter
            CooldownTypeModule cooldownType = new CooldownTypeModule(CooldownTypeModule.NONE);

            short counterType =
                    pCountable ? counterStyle : ClientUISlotBarCategoryItemModule.NONE;
            // create rockets category item
            return new ClientUISlotBarCategoryItemModule(1, rocketsCategoryItemStatus, categoryTimerModule, cooldownType,
                                                         counterType, ClientUISlotBarCategoryItemModule.SELECTION);

        }


    }
}
