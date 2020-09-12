using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers.Dashboard {
    public class ShopController : ComponentBase {

        protected internal bool Loading { get; set; } = true;
        protected internal string Padding => "px-lg-5 " + (Loading ? "py-lg-5" : "pb-lg-5");

    }
}
