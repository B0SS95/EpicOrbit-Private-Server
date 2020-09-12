using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Abstracts;
using EpicOrbit.Server.Data.Repositories.Attributes;

namespace EpicOrbit.Server.Data.Models {
    public class ClanLogModel : ModelBase {

        // Log messages expire after 15 days
        [Index(false)] public int ClanID { get; set; }
        public string Message { get; set; }
        [Index(false, 1296000)] public override DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
