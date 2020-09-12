using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.ViewModels.Configuration;
using EpicOrbit.Shared.ViewModels.Hangar;
using EpicOrbit.Shared.ViewModels.Vault;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Equipment {
    public class EquipmentComponentController : ComponentBase {

        [Parameter]
        protected VaultView Vault { get; set; }

        [Parameter]
        protected Ship Ship { get; set; }

        [Parameter]
        protected Faction Company { get; set; }

        [Parameter]
        protected HangarDetailView Hangar { get; set; }

        [Parameter]
        protected Action CloseHandler { get; set; }

        [Parameter]
        protected Action SaveHandler { get; set; }

        protected int SelectedConfiguration { get; set; } = 1;
        protected ConfigurationView Configuration => SelectedConfiguration == 1 ? Hangar.Configuration_2 : Hangar.Configuration_1;

        protected string ButtonNormal { get; } = "btn btn-icon btn-2 btn-default mr-0";
        protected string ButtonActive { get; } = "btn btn-icon btn-2 btn-default mr-0 active";
        protected int View { get; set; }

        protected void ChangeView(int view) {
            View = view;
            StateHasChanged();
        }

        protected void ChangeConfiguration(int configuration) {
            SelectedConfiguration = configuration;
            StateHasChanged();
        }


    }
}
