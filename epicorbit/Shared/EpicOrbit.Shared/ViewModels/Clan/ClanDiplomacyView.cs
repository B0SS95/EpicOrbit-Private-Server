using EpicOrbit.Shared.Enumerables;
using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Clan {
    public class ClanDiplomacyView {

        // anhanddessen kann gesehen werden ob, bei abwartend, wir die initiatoren sind oder nicht
        public int InitiatorID { get; set; }

        public ClanView Clan { get; set; }
        public ClanRelationType Type { get; set; }

        public string Message { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
