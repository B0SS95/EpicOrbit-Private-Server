using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Clan {
    public class ClanView {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Description { get; set; }

        public int Rank { get; set; }
        public int Points { get; set; }
        public bool Pending { get; set; }

        public int MembersCount { get; set; }
        public string LeaderUsername { get; set; }

        public DateTime CreationDate { get; set; }

    }
}
