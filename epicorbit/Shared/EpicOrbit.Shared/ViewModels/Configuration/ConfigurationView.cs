using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Items.Extensions;
using EpicOrbit.Shared.ViewModels.Vault;
using Newtonsoft.Json;

namespace EpicOrbit.Shared.ViewModels.Configuration {
    public class ConfigurationView {

        public List<DroneView> Drones { get; set; }
        public List<int> Weapons { get; set; }
        public List<int> Generators { get; set; }
        public List<int> Shields { get; set; }

        [JsonIgnore] public int MaxHitpoints { get; set; }
        [JsonIgnore] public int Shield { get; set; }
        [JsonIgnore] public double Absorption { get; set; }
        [JsonIgnore] public double Regeneration { get; set; }
        [JsonIgnore] public int Damage { get; set; }
        [JsonIgnore] public int Speed { get; set; }
        [JsonIgnore] public bool LaserEquipped => LaserEquippedCount > 0;
        [JsonIgnore] public int LaserEquippedCount { get; set; }

        #region {[ CHECK - HELPER ]}
        private void CheckShip(IGameLogger logger, int accountId, VaultView vault, Ship ship) {
            // check ship
            if (!vault.Ships.Contains(ship.ID)) {
                throw logger.LogError(new Exception($"Check for player {accountId} on hangar with ship {ship.ID} resulted in a assigned ship which is not even owned!"));
            }

            // check amount of items packed into the ship
            if (Weapons.Count > ship.WeaponSlots) {
                Weapons = Weapons.Take(ship.WeaponSlots).ToList();
            }

            if (Generators.Count + Shields.Count > ship.GeneratorSlots) {
                int diff = Math.Abs((Generators.Count + Shields.Count) - ship.GeneratorSlots);

                int generatorsToRemove = Math.Min(diff, Generators.Count);
                int shieldsToRemove = Math.Min(diff - generatorsToRemove, Shields.Count);

                Generators = Generators.Take(Generators.Count - generatorsToRemove).ToList();
                Shields = Shields.Take(Shields.Count - shieldsToRemove).ToList();

                if (diff != 0) {
                    logger.LogWarning($"Check for player {accountId} on hangar with ship {ship.ID} resulted with a problematic difference of {diff}");
                }
            }
        }

        private void CheckDrones(IGameLogger logger, int accountId, VaultView vault, Ship ship) {
            if (Drones == null) {
                Drones = new List<DroneView>();
            }

            // check drones, items and upgrades
            List<DroneView> tempDrones = Drones.ToList();
            foreach (var dronePair in Drones) {
                int newDroneId = vault.Drones[dronePair.Position];
                if (!vault.Drones.ContainsKey(dronePair.Position)) {
                    logger.LogInformation($"Check for player {accountId} on hangar with ship {ship.ID} resulted with a drone [Position: {dronePair.Position}, ID: {dronePair.DroneID}] equipped which is not even owned!");
                    tempDrones.RemoveAll(x => x.Position == dronePair.Position);
                } else if (newDroneId != dronePair.DroneID) {
                    int index = tempDrones.FindIndex(x => x.Position == dronePair.Position);
                    if ((dronePair.DroneID / 10) == (newDroneId / 10)) { // check same class, if true -> Drone lvl changed!
                        tempDrones[index] = new DroneView(newDroneId, dronePair.StatsDesignID, dronePair.VisualDesignID, dronePair.Position, dronePair.WeaponItems, dronePair.ShieldItems);
                    } else {
                        if (ItemsExtension<Drone>.Lookup(newDroneId).Slots < ItemsExtension<Drone>.Lookup(dronePair.DroneID).Slots) {
                            int diff = Math.Abs(dronePair.ShieldItems.Count + dronePair.WeaponItems.Count - ItemsExtension<Drone>.Lookup(newDroneId).Slots);

                            int weaponsToRemove = Math.Min(diff, dronePair.WeaponItems.Count);
                            int shieldsToRemove = Math.Min(diff - weaponsToRemove, dronePair.ShieldItems.Count);

                            tempDrones[index] = new DroneView(newDroneId, dronePair.StatsDesignID, dronePair.VisualDesignID, dronePair.Position,
                                dronePair.WeaponItems.Take(dronePair.WeaponItems.Count - weaponsToRemove).ToList(),
                                dronePair.ShieldItems.Take(dronePair.ShieldItems.Count - shieldsToRemove).ToList());
                        } else {
                            tempDrones[index] = new DroneView(newDroneId, dronePair.StatsDesignID, dronePair.VisualDesignID, dronePair.Position, dronePair.WeaponItems, dronePair.ShieldItems);
                        }
                    }
                }
            }

            // add new drones, which are not present in the hangar
            if (tempDrones.Count < vault.Drones.Count) {
                foreach (var drone in vault.Drones) {
                    int count = tempDrones.Count(x => x.Position == drone.Key);
                    if (count > 1) { // saftiges problem
                        logger.LogWarning($"Check for player {accountId} on hangar with ship {ship.ID} resulted with multiple drones at the same position {drone.Key}!");
                    }

                    if (count <= 0) {
                        tempDrones.Add(new DroneView(drone.Value, DroneDesign.NONE.ID, 0, drone.Key, new List<int>(), new List<int>()));
                    }
                }
            }

            // Check designs
            foreach (var design in tempDrones.GroupBy(x => x.StatsDesignID)) {
                if (design.Key != 0 && (!vault.DroneDesigns.TryGetValue(design.Key, out int count) || count < design.Count())) {
                    int toRemove = design.Count() - count;
                    for (int i = 0; i < tempDrones.Count; i++) {
                        if (tempDrones[i].StatsDesignID == design.Key) {
                            if (toRemove-- == 0) {
                                break;
                            }

                            var tempDrone = tempDrones[i];

                            tempDrones[i] = new DroneView(tempDrone.DroneID, DroneDesign.NONE.ID, tempDrone.VisualDesignID,
                                tempDrone.Position, tempDrone.WeaponItems, tempDrone.ShieldItems);
                        }
                    }
                }
            }

            Drones = tempDrones;
        }

        private void CheckAvailableWeapons(IGameLogger logger, int accountId, VaultView vault, Ship ship) {
            if (Weapons == null) {
                Weapons = new List<int>();
            }

            var tempWeaponsGroup = Weapons.Union(Drones.SelectMany(x => x.WeaponItems)).GroupBy(x => x).ToList();
            foreach (var grouping in tempWeaponsGroup) {
                if (!vault.Weapons.ContainsKey(grouping.Key)) {
                    logger.LogWarning($"Check for player {accountId} on hangar with ship {ship.ID} resulted with equipped weapons id:{grouping.Key} which are not owned!");
                    Weapons.RemoveAll(x => x == grouping.Key);
                } else if (grouping.Count() > vault.Weapons[grouping.Key]) {
                    int diff = grouping.Count() - vault.Weapons[grouping.Key];

                    Weapons.RemoveAll(x => x == grouping.Key && diff-- > 0);
                    foreach (var drone in Drones) {
                        if (diff <= 0) {
                            break;
                        }

                        drone.WeaponItems.RemoveAll(x => x == grouping.Key && diff-- > 0);
                    }

                    if (diff != 0) {
                        logger.LogWarning($"Check for player {accountId} on hangar with ship {ship.ID} resulted with a problematic difference of {diff} (weapon check)");
                    }
                }
            }
        }

        private void CheckAvailableShields(IGameLogger logger, int accountId, VaultView vault, Ship ship) {
            if (Shields == null) {
                Shields = new List<int>();
            }

            var tempShieldsGroup = Shields.Union(Drones.SelectMany(x => x.ShieldItems)).GroupBy(x => x).ToList();
            foreach (var grouping in tempShieldsGroup) {
                if (!vault.Shields.ContainsKey(grouping.Key)) {
                    logger.LogWarning($"Check for player {accountId} on hangar with ship {ship.ID} resulted with equipped shields id:{grouping.Key} which are not owned!");
                    Shields.RemoveAll(x => x == grouping.Key);
                } else if (grouping.Count() > vault.Shields[grouping.Key]) {
                    int diff = grouping.Count() - vault.Shields[grouping.Key];

                    Shields.RemoveAll(x => x == grouping.Key && diff-- > 0);
                    foreach (var drone in Drones) {
                        if (diff <= 0) {
                            break;
                        }

                        drone.ShieldItems.RemoveAll(x => x == grouping.Key && diff-- > 0);
                    }

                    if (diff != 0) {
                        logger.LogWarning($"Check for player {accountId} on hangar with ship {ship.ID} resulted with a problematic difference of {diff} (shield check)");
                    }
                }
            }

        }

        private void CheckAvailableGenerators(IGameLogger logger, int accountId, VaultView vault, Ship ship) {
            if (Generators == null) {
                Generators = new List<int>();
            }

            var tempGeneratorsGroup = Generators.GroupBy(x => x).ToList();
            foreach (var grouping in tempGeneratorsGroup) {
                if (!vault.Generators.ContainsKey(grouping.Key)) {
                    logger.LogWarning($"Check for player {accountId} on hangar with ship {ship.ID} resulted with equipped generators id:{grouping.Key} which are not owned!");
                    Generators.RemoveAll(x => x == grouping.Key);
                } else if (grouping.Count() > vault.Generators[grouping.Key]) {
                    int diff = grouping.Count() - vault.Generators[grouping.Key];

                    Generators.RemoveAll(x => x == grouping.Key && diff-- > 0);

                    if (diff != 0) {
                        logger.LogWarning($"Check for player {accountId} on hangar with ship {ship.ID} resulted with a problematic difference of {diff} (generator check)");
                    }
                }
            }
        }
        #endregion

        #region {[ CALCULATE - HELPER ]}
        private void CalculateDamage(Ship ship) {
            double damage = 0;

            foreach (var weapon in Weapons.GroupBy(x => x)) {
                int weaponCount = weapon.Count();
                damage += weapon.Key.FromWeapons().Damage * weaponCount;
                LaserEquippedCount += weaponCount;
            }

            foreach (double amount in ship.Boosts.Where(x => x.Type == BoosterType.DAMAGE).Select(x => x.Amount)) {
                damage *= amount;
            }

            bool isFullDroneDesign = Drones.GroupBy(x => x.StatsDesignID).Count() == 1;

            foreach (var dronePair in Drones) {
                double inner_damage = 0;
                foreach (var weapon in dronePair.WeaponItems.GroupBy(x => x)) {
                    int weaponCount = weapon.Count();
                    inner_damage += weapon.Key.FromWeapons().Damage * weaponCount;
                    LaserEquippedCount += weaponCount;
                }
                inner_damage *= dronePair.DroneID.FromDrones().DamageBoost;

                if (dronePair.StatsDesignID != DroneDesign.NONE.ID) {
                    DroneDesign design = dronePair.StatsDesignID.FromDroneDesigns();
                    if (isFullDroneDesign) {
                        foreach (double amount in design.Full.Where(x => x.Type == BoosterType.DAMAGE).Select(x => x.Amount)) {
                            inner_damage *= amount;
                        }
                    } else {
                        foreach (double amount in design.Single.Where(x => x.Type == BoosterType.DAMAGE).Select(x => x.Amount)) {
                            inner_damage *= amount;
                        }
                    }
                }

                damage += inner_damage;
            }

            Damage = (int)damage;
        }

        private void CalculateShield(Ship ship) {
            double shield = 0;
            double absorptionSum = 0;
            double regenerationSum = 0;
            int count = 0;

            foreach (var sgen in Shields.GroupBy(x => x)) {
                shield += sgen.Key.FromShields().Strength * sgen.Count();
                absorptionSum += sgen.Key.FromShields().Absorption * sgen.Count();
                regenerationSum += sgen.Key.FromShields().Regeneration * sgen.Count();
                count += sgen.Count();
            }

            foreach (double amount in ship.Boosts.Where(x => x.Type == BoosterType.SHIELD).Select(x => x.Amount)) {
                shield *= amount;
            }

            bool isFullDroneDesign = Drones.GroupBy(x => x.StatsDesignID).Count() == 1;

            foreach (var dronePair in Drones) {
                double inner_shield = 0;
                foreach (var sgen in dronePair.ShieldItems.GroupBy(x => x)) {
                    inner_shield += sgen.Key.FromShields().Strength * sgen.Count();
                    absorptionSum += sgen.Key.FromShields().Absorption * sgen.Count();
                    regenerationSum += sgen.Key.FromShields().Regeneration * sgen.Count();
                    count += sgen.Count();
                }
                inner_shield *= dronePair.DroneID.FromDrones().ShieldBoost;

                if (dronePair.StatsDesignID != DroneDesign.NONE.ID) {
                    DroneDesign design = dronePair.StatsDesignID.FromDroneDesigns();
                    if (isFullDroneDesign) {
                        foreach (double amount in design.Full.Where(x => x.Type == BoosterType.SHIELD).Select(x => x.Amount)) {
                            inner_shield *= amount;
                        }
                    } else {
                        foreach (double amount in design.Single.Where(x => x.Type == BoosterType.SHIELD).Select(x => x.Amount)) {
                            inner_shield *= amount;
                        }
                    }
                }

                shield += inner_shield;
            }

            Shield = (int)shield;

            if (count > 0) {
                Absorption = absorptionSum / count;
                Regeneration = regenerationSum / count;
            }
        }

        private void CalculateSpeed(Ship ship) {
            int speed = 0;

            foreach (var gen in Generators.GroupBy(x => x)) {
                speed += gen.Key.FromGenerators().Speed * gen.Count();
            }

            Speed = ship.Speed + speed;
        }

        private void CalculateHitpoints(Ship ship) {
            double maxHP = ship.Hitpoints;
            foreach (double amount in ship.Boosts.Where(x => x.Type == BoosterType.HITPOINTS).Select(x => x.Amount)) {
                maxHP *= amount;
            }

            bool isFullDroneDesign = Drones.GroupBy(x => x.StatsDesignID).Count() == 1;

            if (isFullDroneDesign) {
                maxHP *= Math.Max(1, Drones.First().StatsDesignID.FromDroneDesigns().Full.Where(x => x.Type == BoosterType.HITPOINTS).Sum(x => x.Amount));
            } else {
                maxHP *= Math.Max(1, Drones.Sum(x => x.StatsDesignID.FromDroneDesigns().Single.Where(y => y.Type == BoosterType.HITPOINTS).Sum(y => y.Amount)));
            }

            MaxHitpoints = (int)maxHP;
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public void Check(IGameLogger logger, int accountId, VaultView vault, Ship ship) {
            CheckShip(logger, accountId, vault, ship);
            CheckDrones(logger, accountId, vault, ship);
            CheckAvailableWeapons(logger, accountId, vault, ship);
            CheckAvailableShields(logger, accountId, vault, ship);
            CheckAvailableGenerators(logger, accountId, vault, ship);
        }

        public void Calculate(Ship ship) {
            CalculateHitpoints(ship);
            CalculateDamage(ship);
            CalculateShield(ship);
            CalculateSpeed(ship);
        }
        #endregion

    }
}
