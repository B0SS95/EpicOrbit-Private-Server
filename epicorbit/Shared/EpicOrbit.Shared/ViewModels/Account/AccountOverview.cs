using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Shared.Implementations;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Items.Extensions;

namespace EpicOrbit.Shared.ViewModels.Account {
    public class AccountOverview {

        public int ID { get; set; }
        public string Username { get; set; }

        public int ActiveShipID { get; set; }
        public int FactionID { get; set; }
        public int RankID { get; set; }

        public int PlayerKills { get; set; }
        public int NPCKills { get; set; }
        public long Experience { get; set; }
        public long Honor { get; set; }

        public long Uridium { get; set; } = 10000000;
        public long Credits { get; set; } = 10000000;
        public int OwnCompanyKills { get; set; }
        public int Deaths { get; set; }

        public int DisplayPreference { get; set; }
        public Faction Faction => FactionID.FromFactions();
        public long Points => (PlayerKills * 6 + NPCKills * 2 + Experience / 100000 + Honor / 1000) - (Deaths * 2 + OwnCompanyKills * 7);
        public int Level => Calculator.Level(Experience);

    }
}
