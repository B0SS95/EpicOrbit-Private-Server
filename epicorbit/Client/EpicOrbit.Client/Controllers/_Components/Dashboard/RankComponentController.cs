using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard {
    public class RankComponentController : ComponentBase {

        [Parameter] protected internal int RankID { get; set; }

        protected internal string GetImagePath() {
            return $"./do_img/global/ranks/rank_{RankID}.png";
        }

    }
}
