using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Abstracts;
using EpicOrbit.Server.Data.Repositories.Attributes;
using EpicOrbit.Shared.Enumerables;

namespace EpicOrbit.Server.Data.Models {
    public class ClanMemberModel : ModelBase {

        [Index(false)] public int ClanID { get; set; }
        [Index] public int AccountID { get; set; }

        public ClanRole Role { get; set; }

    }
}
