using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Account {
    public class AccountGameView {

        public int ID { get; set; }
        public string Token { get; set; }
        public int Display { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
