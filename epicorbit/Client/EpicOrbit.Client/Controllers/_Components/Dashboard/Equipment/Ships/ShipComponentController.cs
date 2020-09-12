using EpicOrbit.Shared.Items;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Equipment.Ships {
    public class ShipComponentController : ComponentBase {

        [Parameter]
        protected Ship[] Ships { get; set; }

        [Parameter]
        protected Faction Company { get; set; }

        [Parameter]
        protected Action<Ship> Activate { get; set; }

        [Parameter]
        protected Action<Ship> Equipment { get; set; }

        [Parameter]
        protected Ship Active { get; set; }

        protected internal Ship Current { get; set; }

        protected internal void SelectShip(Ship ship) {
            if (Current == null || ship.ID != Current.ID) {
                Current = ship;
                StateHasChanged();
            }
        }

    }
}
