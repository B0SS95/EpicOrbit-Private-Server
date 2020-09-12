using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace EpicOrbit.Client.Controllers._Components {
    public class CenteredComponentController : ComponentBase {

        [Parameter]
        protected int Size { get; set; }

        [Parameter]
        protected RenderFragment ChildContent { get; set; }

        [Parameter]
        protected string Class { get; set; }

    }
}
