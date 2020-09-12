using EpicOrbit.Shared.Items;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard {
    public class CompanyComponentController : ComponentBase {

        [Parameter] protected internal Faction Faction { get; set; }
        [Parameter] protected internal bool Big { get; set; }

        protected internal string GetImagePath() {
            return $"./do_img/global/logos/logo_{Faction.Name.ToLower()}{(Big ? "" : "_mini")}.png";
        }

    }
}
