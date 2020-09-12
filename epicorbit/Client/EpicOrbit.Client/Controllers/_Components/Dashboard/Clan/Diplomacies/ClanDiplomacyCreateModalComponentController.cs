using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.ViewModels.Clan;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Clan.Diplomacies {
    public class ClanDiplomacyCreateModalComponentController : ComponentBase {

        [Parameter] protected ClanView Current { get; set; }

        protected ClanView Selected { get; set; }
        protected ClanRelationType Relation { get; set; }
        protected string Message { get; set; }

        protected void Select(ClanRelationType type) {
            Relation = type;
        }

        protected void Select(ClanView clan) {
            Selected = clan;
            StateHasChanged();
        }

        protected void Confirm() {

        }

    }
}
