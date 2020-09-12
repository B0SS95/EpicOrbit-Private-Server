using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Items;
using EpicOrbit.Server.Data.Models.ViewModels.Cooldown;
using EpicOrbit.Server.Data.ViewModels.Hangar;
using EpicOrbit.Shared.Implementations;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Items.Extensions;
using EpicOrbit.Shared.ViewModels.Clan;
using EpicOrbit.Shared.ViewModels.Vault;

namespace EpicOrbit.Server.Data.Models.ViewModels.Account {
    public class AccountView {

        public int ID { get; set; }
        public string Username { get; set; }

        public DateTime PremiumDue { get; set; }
        public int FactionID { get; set; }
        public int PlayerKills { get; set; }
        public int NPCKills { get; set; }

        public long Points => (PlayerKills * 6 + NPCKills * 2 + Experience / 100000 + Honor / 1000) - (Deaths * 2 + OwnCompanyKills * 7);
        public int RankID { get; set; }

        public long Experience { get; set; }
        public long Honor { get; set; }
        public long Uridium { get; set; }
        public long Credits { get; set; }
        public int OwnCompanyKills { get; set; }
        public int Deaths { get; set; }

        public int DisplayPreference { get; set; }
        public string UserClientConfiguration { get; set; }
        public int GGRings { get; set; }

        public ClanOverview Clan { get; set; }
        public HangarView CurrentHangar { get; set; }
        public VaultView Vault { get; set; }
        public CooldownView Cooldown { get; set; }


        public Faction Faction => FactionID.FromFactions();
        public Rank Rank => ItemsExtension<Rank>.Lookup(RankID);
        public bool IsPremium => PremiumDue >= DateTime.Now;
        public int Level => Calculator.Level(Experience);

    }
}
