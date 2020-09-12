using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Implementations;
using EpicOrbit.Server.Data.Models.Abstracts;
using EpicOrbit.Server.Data.Repositories.Attributes;

namespace EpicOrbit.Server.Data.Models {
    public class AccountSessionModel : ModelBase {

        [Index(false)] public int AccountID { get; set; }

        public string Token { get; set; } = RandomGenerator.String(18);

        [Index(false, 86400)] public override DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
