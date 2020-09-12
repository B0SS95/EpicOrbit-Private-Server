using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Implementations;
using EpicOrbit.Server.Data.Models.Abstracts;
using EpicOrbit.Server.Data.Repositories.Attributes;

namespace EpicOrbit.Server.Data.Models {
    public class ClanModel : ModelBase {

        [Index] public int ID { get; set; } = RandomGenerator.UniqueIdentifier();
        [Index] public string Tag { get; set; }
        [Index(false)] public string Name { get; set; }

        [Index(false)] public int Points { get; set; }
        [Index(false)] public int Rank { get; set; }

        public string Description { get; set; }

    }
}
