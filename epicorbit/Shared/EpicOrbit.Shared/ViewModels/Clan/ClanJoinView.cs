using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Clan {
    public class ClanJoinView {

        public int ClanID { get; set; }

        [StringLength(256, MinimumLength = 0)]
        public string Description { get; set; }

    }
}
