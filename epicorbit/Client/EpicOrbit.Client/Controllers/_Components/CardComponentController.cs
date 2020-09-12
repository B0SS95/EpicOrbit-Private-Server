using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace EpicOrbit.Client.Controllers._Components {
    public class CardComponentController : ComponentBase {

        [Parameter] protected string Class { get; set; }

        [Parameter] protected string ParentClass { get; set; } = "";

        [Parameter] protected string Type { get; set; } = "secondary";

        [Parameter] protected RenderFragment ChildContent { get; set; }

    }
}
