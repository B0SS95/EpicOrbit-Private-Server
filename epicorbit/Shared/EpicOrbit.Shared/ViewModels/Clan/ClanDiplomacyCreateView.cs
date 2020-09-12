using EpicOrbit.Shared.Enumerables;
using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Clan {
    public class ClanDiplomacyCreateView {

        public int TargetID { get; set; }
        public ClanRelationType Type { get; set; }

        // only if type is not war
        public string Message { get; set; }

    }
}
