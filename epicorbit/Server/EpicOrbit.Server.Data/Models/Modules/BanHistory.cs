using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Abstracts;

namespace EpicOrbit.Server.Data.Models.Modules {
    public class BanHistory : ModelBase {

        public DateTime Until { get; set; }
        public string Reason { get; set; }

    }
}
