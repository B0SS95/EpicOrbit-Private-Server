using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.Implementations;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Items.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Account {
    public class AccountClanView {

        public int ID { get; set; }
        public string Username { get; set; }

        public int ActiveShipID { get; set; }
        public int FactionID { get; set; }
        public int RankID { get; set; }

        public long Experience { get; set; }

        public ClanRole Role { get; set; }
        public DateTime JoinDate { get; set; }

        public string Message { get; set; }
        public bool Online { get; set; }
        public string Map { get; set; }
        public (int X, int Y) Position { get; set; }

        public Ship Ship => ActiveShipID.FromShips();
        public Faction Faction => FactionID.FromFactions();
        public int Level => Calculator.Level(Experience);

    }
}
