using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EpicOrbit.Server.Data.Models.ViewModels.Account {
    public class AccountDeclareBanView {

        public int ID { get; set; }

        [StringLength(256, MinimumLength = 3)]
        public string Reason { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

    }
}
