using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Ressources {
    public class RessourceTableView {

        public byte[] TokenHash { get; set; }
        public Dictionary<string, RessourceTableItemView> Table { get; set; }

    }
}
