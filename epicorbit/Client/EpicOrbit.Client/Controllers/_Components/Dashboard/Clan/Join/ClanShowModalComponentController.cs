using EpicOrbit.Shared.ViewModels.Clan;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Clan.Join {
    public class ClanShowModalComponentController : ComponentBase {

        [Parameter] protected ClanView Clan { get; set; }

    }
}
