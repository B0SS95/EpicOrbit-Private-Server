using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Account {
    public class AccountLoginView {

        [Required, StringLength(25, MinimumLength = 3)]
        public string Username { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

    }
}
