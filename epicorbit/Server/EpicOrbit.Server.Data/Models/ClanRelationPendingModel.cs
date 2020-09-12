using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Abstracts;
using EpicOrbit.Server.Data.Repositories.Attributes;
using EpicOrbit.Shared.Enumerables;

namespace EpicOrbit.Server.Data.Models {
    public class ClanRelationPendingModel : ModelBase {

        [CompoundIndex(1)] public int InitiatorID { get; set; }
        [CompoundIndex(1)] public int TargetID { get; set; }

        public string Message { get; set; }
        public ClanRelationType Type { get; set; }

    }
}
