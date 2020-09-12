using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Abstracts;
using EpicOrbit.Server.Data.Repositories.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace EpicOrbit.Server.Data.Models {
    public class AccountCooldownModel : ModelBase {

        public AccountCooldownModel() { }
        public AccountCooldownModel(int id) {
            AccountID = id;
        }

        [Index] public int AccountID { get; set; }

        public DateTime LastMine { get; set; }

        public DateTime RocketLauncherLastFire { get; set; }

        public DateTime InfectionUntil { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, DateTime> TechCooldown { get; set; } = new Dictionary<int, DateTime>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, DateTime> AbilityCooldown { get; set; } = new Dictionary<int, DateTime>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, DateTime> SpecialItemCooldown { get; set; } = new Dictionary<int, DateTime>();

    }
}
