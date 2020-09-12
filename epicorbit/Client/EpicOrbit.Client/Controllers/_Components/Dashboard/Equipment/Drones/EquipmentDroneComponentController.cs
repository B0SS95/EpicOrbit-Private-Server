using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.ViewModels.Configuration;
using EpicOrbit.Shared.ViewModels.Vault;
using Microsoft.AspNetCore.Components;
using System;
using EpicOrbit.Shared.Items.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Equipment.Drones {
    public class EquipmentDroneComponentController : ComponentBase {

        [Parameter]
        protected ConfigurationView Configuration { get; set; }

        [Parameter]
        protected VaultView Vault { get; set; }

        protected internal Drone Strongest => Vault.Drones.Max(x => x.Value).FromDrones();
        protected internal List<EquipmentSlotItemController> Items => GetItems();

        protected internal List<EquipmentSlotItemController> GetItems() {
            List<EquipmentSlotItemController> result = new List<EquipmentSlotItemController>();

            foreach (var pair in Vault.DroneDesigns) {
                DroneDesign design = pair.Key.FromDroneDesigns();

                int equiped = Configuration.Drones.Count(x => x.StatsDesignID == pair.Key);
                for (int i = 0; i < pair.Value - equiped; i++) {
                    result.Add(new EquipmentSlotItemController { Type = 0, ID = pair.Key, Name = design.Name });
                }
            }

            foreach (var pair in Vault.Weapons) {
                Weapon weapon = pair.Key.FromWeapons();

                int equiped = Configuration.Weapons.Count(x => x == pair.Key) + Configuration.Drones.Sum(x => x.WeaponItems.Count(y => y == pair.Key));
                for (int i = 0; i < pair.Value - equiped; i++) {
                    result.Add(new EquipmentSlotItemController { Type = 1, ID = pair.Key, Name = weapon.Name });
                }
            }

            foreach (var pair in Vault.Shields) {
                Shield shield = pair.Key.FromShields();

                int equiped = Configuration.Shields.Count(x => x == pair.Key) + Configuration.Drones.Sum(x => x.ShieldItems.Count(y => y == pair.Key));
                for (int i = 0; i < pair.Value - equiped; i++) {
                    result.Add(new EquipmentSlotItemController { Type = 2, ID = pair.Key, Name = shield.Name });
                }
            }

            return result;
        }

        protected internal void InventarClickHandler(int index) {
            List<EquipmentSlotItemController> items = Items;
            if (index >= 0 && index < items.Count) {
                EquipmentSlotItemController item = items[index];
                switch (item.Type) {
                    case 0:
                        foreach (DroneView pair in Configuration.Drones) {
                            if (pair.StatsDesignID == 0) {
                                int equiped = Configuration.Drones.Count(x => x.StatsDesignID == item.ID);
                                if (Vault.DroneDesigns.TryGetValue(item.ID, out int count) && count > equiped) {
                                    pair.StatsDesignID = item.ID;
                                    StateHasChanged();
                                    break;
                                } else { break; }
                            }
                        }
                        break;
                    case 1:
                        foreach (DroneView pair in Configuration.Drones) {
                            if ((pair.WeaponItems.Count + pair.ShieldItems.Count) < pair.DroneID.FromDrones().Slots) {
                                int equiped = Configuration.Weapons.Count(x => x == item.ID) + Configuration.Drones.Sum(x => x.WeaponItems.Count(y => y == item.ID));
                                if (Vault.Weapons.TryGetValue(item.ID, out int count) && count > equiped) {
                                    pair.WeaponItems.Add(item.ID);
                                    StateHasChanged();
                                    break;
                                } else { break; }
                            }
                        }
                        break;
                    case 2:
                        foreach (DroneView pair in Configuration.Drones) {
                            if ((pair.WeaponItems.Count + pair.ShieldItems.Count) < pair.DroneID.FromDrones().Slots) {
                                int equiped = Configuration.Shields.Count(x => x == item.ID) + Configuration.Drones.Sum(x => x.ShieldItems.Count(y => y == item.ID));
                                if (Vault.Shields.TryGetValue(item.ID, out int count) && count > equiped) {
                                    pair.ShieldItems.Add(item.ID);
                                    StateHasChanged();
                                    break;
                                } else { break; }
                            }
                        }
                        break;
                }
            }
        }

    }
}
