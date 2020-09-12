using EpicOrbit.Shared.Items;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Equipment.Ships {
    public class EquipmentShipOverviewComponentController : ComponentBase {

        [Parameter]
        protected Ship Ship { get; set; }

        [Parameter]
        protected Faction Company { get; set; }

        protected internal string GetImagePath() {
            string basePath = "./do_img/global/items";

            string last = "";
            foreach (string part in Ship.Name.Split('_')) {
                last = part;
                basePath += "/" + part;
            }


            if (last.EndsWith("aegis") || last.EndsWith("spearhead") || last.EndsWith("citadel")
                || last.EndsWith("a-veteran") || last.EndsWith("a-elite")
                || last.EndsWith("s-veteran") || last.EndsWith("s-elite")
                || last.EndsWith("c-veteran") || last.EndsWith("c-elite")) {
                basePath += "-" + Company.Name.ToLower();
            }

            return basePath + "_top.png";
        }

    }
}
