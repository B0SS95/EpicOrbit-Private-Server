using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Clan {
    public class ClanCreateView {

        [StringLength(24, MinimumLength = 4)]
        public string Name { get; set; }

        [StringLength(4, MinimumLength = 1)]
        public string Tag { get; set; }

        [StringLength(256, MinimumLength = 0)]
        public string Description { get; set; }

    }
}
