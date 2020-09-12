using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.ViewModels.Configuration;
using EpicOrbit.Shared.ViewModels.Vault;
using EpicOrbit.Shared.Items.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Equipment.Ships {
    public class EquipmentShipComponentController : ComponentBase {

        [Parameter]
        protected Ship Ship { get; set; }

        [Parameter]
        protected Faction Company { get; set; }

        [Parameter]
        protected ConfigurationView Configuration { get; set; }

        [Parameter]
        protected VaultView Vault { get; set; }

        protected internal List<EquipmentSlotItemController> Weapons => Configuration.Weapons.Select(x => new EquipmentSlotItemController { Name = x.FromWeapons().Name }).ToList();
        protected internal List<EquipmentSlotItemController> Generators => Configuration.Generators.Select(x => new EquipmentSlotItemController { Name = x.FromGenerators().Name })
            .Concat(Configuration.Shields.Select(x => new EquipmentSlotItemController { Name = x.FromShields().Name })).ToList();

        protected internal List<EquipmentSlotItemController> Items => GetItems();

        protected internal List<EquipmentSlotItemController> GetItems() {
            List<EquipmentSlotItemController> result = new List<EquipmentSlotItemController>();
            foreach (var pair in Vault.Weapons) {
                Weapon weapon = pair.Key.FromWeapons();

                int equiped = Configuration.Weapons.Count(x => x == pair.Key) + Configuration.Drones.Sum(x => x.WeaponItems.Count(y => y == pair.Key));
                for (int i = 0; i < pair.Value - equiped; i++) {
                    result.Add(new EquipmentSlotItemController { Type = 0, ID = pair.Key, Name = weapon.Name });
                }
            }

            foreach (var pair in Vault.Generators) {
                Generator generator = pair.Key.FromGenerators();

                int equiped = Configuration.Generators.Count(x => x == pair.Key);
                for (int i = 0; i < pair.Value - equiped; i++) {
                    result.Add(new EquipmentSlotItemController { Type = 1, ID = pair.Key, Name = generator.Name });
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

        protected internal void WeaponClickHandler(int index) {
            if (index >= 0 && index < Configuration.Weapons.Count) {
                Configuration.Weapons.RemoveAt(index);
                StateHasChanged();
            }
        }

        protected internal void GeneratorClickHandler(int index) {
            if (index >= 0 && index < Configuration.Generators.Count) {
                Configuration.Generators.RemoveAt(index);
                StateHasChanged();
            } else if ((index - Configuration.Generators.Count) >= 0
                && (index - Configuration.Generators.Count) < Configuration.Shields.Count) {
                Configuration.Shields.RemoveAt((index - Configuration.Generators.Count));
                StateHasChanged();
            }
        }

        protected internal void InventarClickHandler(int index) {
            List<EquipmentSlotItemController> items = Items;
            if (index >= 0 && index < items.Count) {
                EquipmentSlotItemController item = items[index];
                switch (item.Type) {
                    case 0:
                        if (Configuration.Weapons.Count < Ship.WeaponSlots) {
                            int equiped = Configuration.Weapons.Count(x => x == item.ID) + Configuration.Drones.Sum(x => x.WeaponItems.Count(y => y == item.ID));
                            if (Vault.Weapons.TryGetValue(item.ID, out int count) && count > equiped) {
                                Configuration.Weapons.Add(item.ID);
                                StateHasChanged();
                            }
                        }
                        break;
                    case 1:
                        if (Configuration.Generators.Count + Configuration.Shields.Count < Ship.GeneratorSlots) {
                            int equiped = Configuration.Generators.Count(x => x == item.ID);
                            if (Vault.Generators.TryGetValue(item.ID, out int count) && count > equiped) {
                                Configuration.Generators.Add(item.ID);
                                StateHasChanged();
                            }
                        }
                        break;
                    case 2:
                        if (Configuration.Generators.Count + Configuration.Shields.Count < Ship.GeneratorSlots) {
                            int equiped = Configuration.Shields.Count(x => x == item.ID) + Configuration.Drones.Sum(x => x.ShieldItems.Count(y => y == item.ID));
                            if (Vault.Shields.TryGetValue(item.ID, out int count) && count > equiped) {
                                Configuration.Shields.Add(item.ID);
                                StateHasChanged();
                            }
                        }
                        break;
                }
            }
        }

    }
}
