using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Abstracts;
using EpicOrbit.Server.Data.Repositories.Attributes;

namespace EpicOrbit.Server.Data.Models {
    public class ClanMemberPendingModel : ModelBase {

        [CompoundIndex(1)] public int ClanID { get; set; }
        [CompoundIndex(1)] public int AccountID { get; set; }

        public string Message { get; set; }

    }
}
