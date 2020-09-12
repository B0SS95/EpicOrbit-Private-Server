using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace EpicOrbit.Client.Controllers._Components {
    public class InputGroupComponentController : ComponentBase {

        [Parameter] protected string Icon { get; set; }

        [Parameter] protected string Class { get; set; }

        [Parameter] protected RenderFragment ChildContent { get; set; }

    }
}
