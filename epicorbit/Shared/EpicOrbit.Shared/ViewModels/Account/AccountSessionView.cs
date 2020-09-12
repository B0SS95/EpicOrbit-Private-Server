using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Account {
    public class AccountSessionView {

        public AccountSessionView() { }
        public AccountSessionView(int accountId, string token) {
            AccountID = accountId;
            Token = token;
        }

        public int AccountID { get; set; }

        [StringLength(18, MinimumLength = 18)]
        public string Token { get; set; } = "";

    }
}
