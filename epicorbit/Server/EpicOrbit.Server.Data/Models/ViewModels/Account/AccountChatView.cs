using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Modules;

namespace EpicOrbit.Server.Data.Models.ViewModels.Account {
    public class AccountChatView {

        public int AccountID { get; set; }

        public string Username { get; set; } // processed

        public string ClanTag { get; set; } // processed

        public bool IsAdministrator { get; set; }

        public bool IsModerator { get; set; }

        public BanHistory ActiveBan { get; set; }

    }
}
