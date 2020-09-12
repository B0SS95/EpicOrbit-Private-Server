using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Account {
    public class AccountRegisterView {

        [Required, StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Range(1, 3)]
        public int Faction { get; set; }

    }
}
