using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Abstracts;
using EpicOrbit.Server.Data.Repositories.Attributes;
using EpicOrbit.Shared.Items;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace EpicOrbit.Server.Data.Models {
    public class AccountVaultModel : ModelBase {

        public AccountVaultModel() { }
        public AccountVaultModel(int id) {
            AccountID = id;

            Ships = new HashSet<int> { Ship.PHOENIX.ID, Ship.CYBORG.ID };
            DroneFormations = new HashSet<int> { DroneFormation.DEFAULT.ID };
            Drones = new Dictionary<int, int> {
                { 0, Drone.IRIS_LEVEL_6.ID },
                { 1, Drone.IRIS_LEVEL_6.ID },
                { 2, Drone.IRIS_LEVEL_6.ID },
                { 3, Drone.IRIS_LEVEL_6.ID },
                { 4, Drone.IRIS_LEVEL_6.ID },
                { 5, Drone.IRIS_LEVEL_6.ID },
                { 6, Drone.IRIS_LEVEL_6.ID },
                { 7, Drone.IRIS_LEVEL_6.ID }
            };
            Generators = new Dictionary<int, int> { { Generator.G3N7900.ID, 15 } };
            Shields = new Dictionary<int, int> { { Shield.B02.ID, 15 } };
            Weapons = new Dictionary<int, int> { { Weapon.LF4.ID, 5 }, { Weapon.LF3.ID, 15 } };
            Ammunitions = new Dictionary<int, int> {
                { Ammuninition.LCB_10.ID, 1000000  }, { Ammuninition.MCB_25.ID, 1000000 },
                { Ammuninition.MCB_50.ID, 100000 }, { Ammuninition.UCB_100.ID, 10000 },
                { Ammuninition.SAB_50.ID, 50000 }, { Ammuninition.CBO_100.ID, 5000 },
                { Ammuninition.RSB_75.ID, 2500 }
            };
            RocketAmmunitions = new Dictionary<int, int> {
                { RocketAmmunition.R310.ID, 1000000 }, { RocketAmmunition.PLT_2026.ID, 1000000 },
                { RocketAmmunition.PLT_2021.ID, 1000 }, { RocketAmmunition.PLT_3030.ID, 500 },
                { RocketAmmunition.PLD_8.ID, 100 }, { RocketAmmunition.DCR_250.ID, 10 },
                { RocketAmmunition.R_IC3.ID, 10 }
            };
            RocketLauncherAmmunitions = new Dictionary<int, int> {
                { RocketLauncherAmmunition.ECO_10.ID, 5000 }, { RocketLauncherAmmunition.HSTRM_01.ID, 500 },
                { RocketLauncherAmmunition.UBR_100.ID, 500 }, { RocketLauncherAmmunition.SAR_01.ID, 2500 },
                { RocketLauncherAmmunition.SAR_02.ID, 500 }, { RocketLauncherAmmunition.CBR.ID, 250 }
            };
            Extras = new Dictionary<int, int> {
                { Extra.AROL_X.ID, 1 }, { Extra.RL_LB_X.ID, 1 },
                { Extra.CL04K.ID, 10 }
            };
            SpecialItems = new Dictionary<int, int> {
                { SpecialItem.EMP_01.ID, 25 }, { SpecialItem.ISH_01.ID, 25 },
                { SpecialItem.SMB_01.ID, 25 }
            };
            Mines = new Dictionary<int, int> {
                { Mine.ACM_01.ID, 50 }, { Mine.DDM_01.ID, 10 },
                { Mine.EMPM_01.ID, 10 }, { Mine.SABM_01.ID, 10 },
                { Mine.SLM_01.ID, 10 }
            };
            Techs = new Dictionary<int, int> {
                { TechFactory.BATTLE_REPAIR_BOT.ID, 5 }, { TechFactory.CHAIN_IMPULSE.ID, 5 },
                { TechFactory.ENERGY_LEECH.ID, 5 }, { TechFactory.PRECISION_TARGETER.ID, 5 },
                { TechFactory.SHIELD_BACKUP.ID, 5 }
            };
        }

        [Index] public int AccountID { get; set; }

        public HashSet<int> Ships { get; set; } = new HashSet<int>();
        public HashSet<int> DroneFormations { get; set; } = new HashSet<int>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int> Drones { get; set; } = new Dictionary<int, int>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int> DroneDesigns { get; set; } = new Dictionary<int, int>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int> Generators { get; set; } = new Dictionary<int, int>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int> Shields { get; set; } = new Dictionary<int, int>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int> Weapons { get; set; } = new Dictionary<int, int>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int> Techs { get; set; } = new Dictionary<int, int>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, TimeSpan> Booster { get; set; } = new Dictionary<int, TimeSpan>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int> Ammunitions { get; set; } = new Dictionary<int, int>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int> RocketAmmunitions { get; set; } = new Dictionary<int, int>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int> RocketLauncherAmmunitions { get; set; } = new Dictionary<int, int>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int> Mines { get; set; } = new Dictionary<int, int>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int> SpecialItems { get; set; } = new Dictionary<int, int>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int> Extras { get; set; } = new Dictionary<int, int>();

    }
}
