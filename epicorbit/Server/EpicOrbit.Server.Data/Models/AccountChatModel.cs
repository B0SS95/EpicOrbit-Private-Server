using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Abstracts;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Server.Data.Repositories.Attributes;

namespace EpicOrbit.Server.Data.Models {
    public class AccountChatModel : ModelBase {

        public AccountChatModel() { }
        public AccountChatModel(int id) {
            AccountID = id;
        }

        [Index] public int AccountID { get; set; }

        public bool IsAdministrator { get; set; }

        public bool IsModerator { get; set; }

        public List<BanHistory> BanHistory { get; set; } = new List<BanHistory>();

    }
}
