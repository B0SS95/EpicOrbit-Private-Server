using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Server.Data.Implementations;
using EpicOrbit.Server.Data.Models.Abstracts;
using EpicOrbit.Server.Data.Models.Enumerables;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Server.Data.Repositories.Attributes;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Server.Data.Models {
    public class AccountModel : ModelBase {

        public AccountModel() { }
        public AccountModel(string username, string password, string email, int factionId) {
            Username = username;
            Password = password.ComputeHash(Salt);
            Email = email;
            FactionID = factionId;
        }

        [Index] public int ID { get; set; } = RandomGenerator.UniqueIdentifier();
        [Index] public string Username { get; set; }
        [Index(false)] public string Email { get; set; }

        public int ActiveShipID { get; set; } = Ship.PHOENIX.ID;
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; } = Encoding.UTF8.GetBytes(RandomGenerator.String(6));
        public string EmailVerificationToken { get; set; } = RandomGenerator.String(12);
        public DateTime EmailVerificationDate { get; set; } = DateTime.MinValue;
        public GlobalRole Role { get; set; } = GlobalRole.USER;

        [Index(false)] public DateTime PremiumDue { get; set; } = DateTime.Now.AddDays(2);
        [Index(false)] public int FactionID { get; set; }
        [Index(false)] public int PlayerKills { get; set; }
        [Index(false)] public int NPCKills { get; set; }

        [Index(false)] public long Points => (PlayerKills * 6 + NPCKills * 2 + Experience / 100000 + Honor / 1000) - (Deaths * 2 + OwnCompanyKills * 7);
        [Index(false)] public int RankID { get; set; } = 1;

        public long Experience { get; set; }
        public long Honor { get; set; }
        public long Uridium { get; set; } = 10000000;
        public long Credits { get; set; } = 10000000;
        public int OwnCompanyKills { get; set; }
        public int Deaths { get; set; }

        public List<BanHistory> BanHistory { get; set; } = new List<BanHistory>();
        public string UserClientConfiguration { get; set; } = "{\"WindowSettings\":{\"ship\":{\"x\":98,\"y\":29,\"maximized\":true,\"itemId\":\"ship\",\"height\":92,\"width\":212,\"ID\":16656},\"minimap\":{\"x\":98,\"y\":97,\"maximized\":true,\"itemId\":\"minimap\",\"height\":232,\"width\":325,\"ID\":16656},\"chat\":{\"x\":0,\"y\":96,\"maximized\":true,\"itemId\":\"chat\",\"height\":217,\"width\":442,\"ID\":16656},\"log\":{\"x\":98,\"y\":8,\"maximized\":false,\"itemId\":\"log\",\"height\":121,\"width\":241,\"ID\":16656},\"user\":{\"x\":98,\"y\":17,\"maximized\":true,\"itemId\":\"user\",\"height\":92,\"width\":212,\"ID\":16656}},\"UserSettings\":{\"displaySettingsModule\":{\"displayResources\":true,\"name_42\":1,\"displayPlayerName\":true,\"displaySetting3DqualityTextures\":1,\"name_13\":1,\"displaySetting3DqualityLights\":1,\"displayChat\":true,\"var_2596\":false,\"var_1379\":true,\"displaySetting3DqualityEffects\":1,\"showNotOwnedItems\":true,\"preloadUserShips\":false,\"var_1948\":true,\"displayHitpointBubbles\":true,\"displaySetting3DsizeTextures\":1,\"displayBoxes\":true,\"proActionBarKeyboardInputEnabled\":true,\"displayDrones\":true,\"displaySetting3DqualityAntialias\":1,\"notSet\":false,\"var_3069\":true,\"displayCargoboxes\":true,\"displayNotifications\":true,\"proActionBarEnabled\":true,\"var_1406\":true,\"proActionBarAutohideEnabled\":true,\"var_3558\":true,\"name_161\":true,\"var_55\":false,\"var_3236\":true,\"displayPenaltyCargoboxes\":true,\"displaySetting3DtextureFiltering\":1,\"ID\":20579},\"windowSettingsModule\":{\"hideAllWindows\":false,\"barState\":\"23,1|24,1|\",\"minimapScaleFactor\":7,\"ID\":12640},\"gameplaySettingsModule\":{\"quickslotStopAttack\":true,\"doubleclickAttack\":true,\"autoChangeAmmo\":true,\"autoRefinement\":true,\"var_5281\":true,\"autoBuyGreenBootyKeys\":true,\"showBattlerayNotifications\":true,\"notSet\":false,\"autoStart\":true,\"autoBoost\":true,\"ID\":8750},\"audioSettingsModule\":{\"playCombatMusic\":false,\"var_957\":0,\"var_2006\":false,\"sound\":0,\"music\":0,\"ID\":12126},\"var_3182\":{\"var_2218\":false,\"var_658\":false,\"var_736\":false,\"var_3368\":false,\"var_4737\":false,\"var_1718\":false,\"ID\":32617},\"qualitySettingsModule\":{\"qualityAttack\":0,\"qualityCustomized\":false,\"notSet\":false,\"qualityCollectables\":0,\"qualityShip\":0,\"qualityEffect\":0,\"qualityPresetting\":0,\"qualityExplosion\":0,\"qualityEngine\":0,\"qualityBackground\":0,\"qualityPOIzone\":0,\"ID\":4314},\"ID\":6163},\"KeyBindings\":[{\"charCode\":0,\"keyCodes\":[49],\"parameter\":0,\"actionType\":7,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[50],\"parameter\":1,\"actionType\":7,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[51],\"parameter\":2,\"actionType\":7,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[52],\"parameter\":3,\"actionType\":7,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[53],\"parameter\":4,\"actionType\":7,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[54],\"parameter\":5,\"actionType\":7,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[55],\"parameter\":6,\"actionType\":7,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[56],\"parameter\":7,\"actionType\":7,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[57],\"parameter\":8,\"actionType\":7,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[48],\"parameter\":9,\"actionType\":7,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[-1],\"parameter\":0,\"actionType\":8,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[-1],\"parameter\":1,\"actionType\":8,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[-1],\"parameter\":2,\"actionType\":8,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[-1],\"parameter\":3,\"actionType\":8,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[-1],\"parameter\":4,\"actionType\":8,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[-1],\"parameter\":5,\"actionType\":8,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[-1],\"parameter\":6,\"actionType\":8,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[-1],\"parameter\":7,\"actionType\":8,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[-1],\"parameter\":8,\"actionType\":8,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[74],\"parameter\":0,\"actionType\":0,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[67],\"parameter\":0,\"actionType\":1,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[17],\"parameter\":0,\"actionType\":2,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[32],\"parameter\":0,\"actionType\":3,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[69],\"parameter\":0,\"actionType\":4,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[82],\"parameter\":0,\"actionType\":5,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[],\"parameter\":0,\"actionType\":13,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[],\"parameter\":0,\"actionType\":6,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[72],\"parameter\":0,\"actionType\":9,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[70],\"parameter\":0,\"actionType\":10,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[187],\"parameter\":0,\"actionType\":11,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[189],\"parameter\":0,\"actionType\":12,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[],\"parameter\":0,\"actionType\":14,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[],\"parameter\":0,\"actionType\":15,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[-1],\"parameter\":9,\"actionType\":8,\"ID\":8383},{\"charCode\":0,\"keyCodes\":[],\"parameter\":0,\"actionType\":16,\"ID\":8383}],\"SlotbarPositions\":{\"standardSlotBarLayout\":\"0\",\"genericFeatureBarLayout\":\"0\",\"barState\":\"23,1|24,1|\",\"var_505\":false,\"proActionBarPosition\":\"50,97.5268817204301|0,0\",\"premiumSlotBarLayout\":\"0\",\"premiumSlotBarPosition\":\"50,93.33333333333331|0,0\",\"genericFeatureBarPosition\":\"99.88907376594565,-0.1088139281828074\",\"categoryBarPosition\":\"50,84.94623655913979\",\"minimapScaleFactor\":7,\"proActionBarLayout\":\"0\",\"gameFeatureBarLayout\":\"0\",\"gameFeatureBarPosition\":\"-0.06734006734006734,-0.1088139281828074\",\"name_124\":\"\",\"standardSlotBarPosition\":\"50,89.13978494623656|0,0\",\"ID\":25141},\"StandardSlotBar\":[{\"slotId\":1,\"var_2176\":\"ammunition_laser_ucb-100\",\"ID\":17494},{\"slotId\":2,\"var_2176\":\"ammunition_laser_rsb-75\",\"ID\":17494},{\"slotId\":3,\"var_2176\":\"ammunition_laser_cbo-100\",\"ID\":17494},{\"slotId\":4,\"var_2176\":\"ammunition_laser_pib-100\",\"ID\":17494}],\"PremiumSlotBar\":[],\"ProActionBar\":[]}";
        public int DisplayPreference { get; set; } = 0;

    }
}
