using EpicOrbit.Shared.Items;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Equipment.Ships {
    public class ShipItemComponentController : ComponentBase {

        [Parameter]
        protected Ship Ship { get; set; }

        [Parameter]
        protected Faction Company { get; set; }

        [Parameter]
        protected bool Active { get; set; }

        [Parameter]
        protected Action<Ship> Click { get; set; }

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

            return basePath + "_100x100.png";
        }

    }
}
