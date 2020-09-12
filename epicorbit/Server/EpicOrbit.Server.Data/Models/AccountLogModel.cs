using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Abstracts;
using EpicOrbit.Server.Data.Repositories.Attributes;

namespace EpicOrbit.Server.Data.Models {
    public class AccountLogModel : ModelBase {

        // expire after 7 days
        [Index(false)] public int AccountID { get; set; }
        public string Message { get; set; }
        [Index(false, 604800)] public override DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
