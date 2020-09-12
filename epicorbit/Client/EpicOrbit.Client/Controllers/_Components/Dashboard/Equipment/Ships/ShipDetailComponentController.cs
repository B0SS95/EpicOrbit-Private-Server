using EpicOrbit.Shared.Items;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Equipment.Ships {
    public class ShipDetailComponentController : ComponentBase {

        [Parameter]
        protected Ship Ship { get; set; }

        [Parameter]
        protected bool Active { get; set; }

        [Parameter]
        protected Faction Company { get; set; }

        [Parameter]
        protected Action<Ship> Activate { get; set; }

        [Parameter]
        protected Action<Ship> Equipment { get; set; }

    }
}
