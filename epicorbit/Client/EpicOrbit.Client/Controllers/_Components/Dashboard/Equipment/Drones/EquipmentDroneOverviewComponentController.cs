using EpicOrbit.Shared.Items;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Equipment.Drones {
    public class EquipmentDroneOverviewComponentController : ComponentBase {

        [Parameter]
        protected Drone Drone { get; set; }

        protected internal string GetImagePath() {
            string basePath = "./do_img/global/items";

            string last = "";
            foreach (string part in Drone.Name.Split('_')) {
                last = part;
                basePath += "/" + part;
            }

            return basePath + "-" + (Drone.Level - 1) + "_top.png";
        }
    }
}