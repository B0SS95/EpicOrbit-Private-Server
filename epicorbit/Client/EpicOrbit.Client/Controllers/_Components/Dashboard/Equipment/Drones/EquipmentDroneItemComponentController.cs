using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Items.Extensions;
using EpicOrbit.Shared.ViewModels.Configuration;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Equipment.Drones {
    public class EquipmentDroneItemComponentController : ComponentBase {

        [Parameter]
        protected DroneView Pair { get; set; }

        [Parameter]
        protected Action Refresh { get; set; }

        protected internal Drone Drone => Pair.DroneID.FromDrones();
        protected List<EquipmentSlotItemController> Slots => Pair.WeaponItems
            .Select(x => new EquipmentSlotItemController { Name = x.FromWeapons().Name })
            .Concat(Pair.ShieldItems.Select(x => new EquipmentSlotItemController { Name = x.FromShields().Name })).ToList();

        protected internal List<EquipmentSlotItemController> Designs => Pair.StatsDesignID == 0 ?
            new List<EquipmentSlotItemController>() :
            new List<EquipmentSlotItemController> {
                new EquipmentSlotItemController { Name = Pair.StatsDesignID.FromDroneDesigns().Name }
            };

        protected internal string GetImagePath() {
            string basePath = "./do_img/global/items";

            string last = "";
            foreach (string part in Drone.Name.Split('_')) {
                last = part;
                basePath += "/" + part;
            }

            return basePath + "-" + (Drone.Level - 1) + "_100x100.png";
        }

        protected internal void ItemClicked(int index) {
            if (index >= 0 && index < Pair.WeaponItems.Count) {
                Pair.WeaponItems.RemoveAt(index);
                Refresh();
            } else if ((index - Pair.WeaponItems.Count) >= 0
                && (index - Pair.WeaponItems.Count) < Pair.ShieldItems.Count) {
                Pair.ShieldItems.RemoveAt((index - Pair.WeaponItems.Count));
                Refresh();
            }
        }

        protected internal void DesignClicked(int index) {
            if (index == 0 && Pair.StatsDesignID != 0) {
                Pair.StatsDesignID = 0;
                Refresh();
            }
        }

    }
}
