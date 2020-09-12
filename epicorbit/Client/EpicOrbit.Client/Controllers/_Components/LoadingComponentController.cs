using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace EpicOrbit.Client.Controllers._Components {
    public class LoadingComponentController : ComponentBase {

        [Parameter] protected bool Loading { get; set; }
        [Parameter] protected string Text { get; set; } = "Please wait until the required data is loaded!";
        [Parameter] protected RenderFragment ChildContent { get; set; }

    }
}
