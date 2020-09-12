using EpicOrbit.Emulator.Netty.Commands;
using System.Collections.Generic;
using System.Linq;

namespace EpicOrbit.Emulator.Game.Objects {
    public class UserClientConfiguration {

        public static UserClientConfiguration Instance() {
            return new UserClientConfiguration() {
                WindowSettings = new Dictionary<string, WindowStateChangedRequest>(),
                KeyBindings = new List<UserKeyBindingsModule>(),
                PremiumSlotBar = new List<ClientUISlotBarItemModule>(),
                SlotbarPositions = new SaveBarsSettingsRequest(),
                ProActionBar = new List<ClientUISlotBarItemModule>(),
                StandardSlotBar = new List<ClientUISlotBarItemModule>(),
                UserSettings = new UserSettingsCommand() {
                    audioSettingsModule = new AudioSettingsModule(),
                    displaySettingsModule = new DisplaySettingsCommand(),
                    gameplaySettingsModule = new GameplaySettingsModule(),
                    qualitySettingsModule = new QualitySettingsModule(),
                    var_3182 = new class_704(),
                    windowSettingsModule = new WindowSettingsModule()
                }
            };
        }

        public Dictionary<string, WindowStateChangedRequest> WindowSettings { get; set; }
        public UserSettingsCommand UserSettings { get; set; }
        public List<UserKeyBindingsModule> KeyBindings { get; set; }
        public SaveBarsSettingsRequest SlotbarPositions { get; set; }
        public List<ClientUISlotBarItemModule> StandardSlotBar { get; set; }
        public List<ClientUISlotBarItemModule> PremiumSlotBar { get; set; }
        public List<ClientUISlotBarItemModule> ProActionBar { get; set; }

        public void Set(List<ClientUISlotBarItemModule> slotbar, int index, string itemId) {
            var slot = slotbar.Where(x => x.slotId == index).FirstOrDefault();
            if (slot == null) {
                slotbar.Add(new ClientUISlotBarItemModule(index, itemId));
            } else {
                slot.var_2176 = itemId;
            }
        }

        public bool Get(List<ClientUISlotBarItemModule> slotbar, int index, out string itemId) {
            itemId = null;
            var slot = slotbar.Where(x => x.slotId == index).FirstOrDefault();
            if (slot != null) {
                itemId = slot.var_2176;
                return true;
            }
            return false;
        }

        public void Remove(List<ClientUISlotBarItemModule> slotbar, int index) {
            slotbar.RemoveAll(x => x.slotId == index);
        }

    }
}
