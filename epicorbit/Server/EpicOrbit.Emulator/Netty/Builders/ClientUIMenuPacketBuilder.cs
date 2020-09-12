using EpicOrbit.Emulator.Game.Objects;
using EpicOrbit.Emulator.Netty.Commands;
using System.Collections.Generic;

namespace EpicOrbit.Emulator.Netty.Builders {
    public static class ClientUIMenuPacketBuilder {

        public struct Window {

            public Window(string value, WindowStateChangedRequest state) {
                Value = value;
                State = state;
            }

            public string Value { get; set; }
            public WindowStateChangedRequest State { get; set; }

        }

        private static List<ClientUIMenuBarItemModule> GenerateTopRight(Dictionary<string, string> pair) {
            List<ClientUIMenuBarItemModule> pairMenu = new List<ClientUIMenuBarItemModule>();
            foreach (var x in pair) {
                pairMenu.Add(new ClientUIMenuBarItemModule(x.Key, true,
                    new ClientUITooltipsCommand(new List<ClientUITooltipModule>() {
                        new ClientUITooltipModule(0, x.Value, new ClientUITooltipTextFormatModule(ClientUITooltipTextFormatModule.LOCALIZED), new List<MessageWildcardReplacementModule>())
                })));
            }
            return pairMenu;
        }

        private static List<ClientUIMenuBarItemModule> GenerateTopLeft(Dictionary<string, Window> pair) {
            List<ClientUIMenuBarItemModule> pairMenu = new List<ClientUIMenuBarItemModule>();
            foreach (var x in pair) {
                pairMenu.Add(new class_990(
                    x.Value.State.maximized, 
                    x.Value.State.height, 
                    true, 
                    x.Value.State.y, 
                    x.Value.State.x,
                    new ClientUITooltipsCommand(
                        new List<ClientUITooltipModule>() {
                            new ClientUITooltipModule(
                                0, 
                                x.Value.Value, 
                                new ClientUITooltipTextFormatModule(
                                    ClientUITooltipTextFormatModule.LOCALIZED
                                ), 
                                new List<MessageWildcardReplacementModule>()
                            )
                        }
                    ), 
                    x.Value.Value, 
                    x.Value.State.width, 
                    null, 
                    x.Key)
                );
            }
            return pairMenu;
        }

        private static Dictionary<string, string> _topRight = new Dictionary<string, string>() {
            { "fullscreen", "title_fullscreen_btn" },
            { "settings", "title_settings" },
            { "logout", "title_logout" }
        };

        private static List<ClientUIMenuBarItemModule> _topRightMenuItems = null;
        private static ClientUIMenuBarModule GetTopRightMenu(UserClientConfiguration configuration) {
            if (_topRightMenuItems == null) {
                _topRightMenuItems = GenerateTopRight(_topRight);
            }

            return new ClientUIMenuBarModule(ClientUIMenuBarModule.GENERIC_FEATURE_BAR,
                _topRightMenuItems, configuration.SlotbarPositions.genericFeatureBarPosition, configuration.SlotbarPositions.genericFeatureBarLayout);
        }

        private static Dictionary<string, Window> _topLeft = new Dictionary<string, Window>() {
             { "user", new Window("title_user", new WindowStateChangedRequest(param4: 212, param5: 92)) },
             { "ship", new Window("title_ship", new WindowStateChangedRequest(param4: 212, param5: 92)) },
           //  { "ship_warp", new Window("ttip_shipWarp_btn", new WindowStateChangedRequest()) },
             { "chat", new Window("title_chat", new WindowStateChangedRequest(param4: 451, param5: 219, param3: 97)) },
             { "group", new Window("title_group", new WindowStateChangedRequest()) },
             { "minimap", new Window("title_map", new WindowStateChangedRequest()) },
             { "spacemap", new Window("title_spacemap", new WindowStateChangedRequest()) },
             { "log", new Window("title_log", new WindowStateChangedRequest(param4: 230, param5: 159)) },
             { "pet", new Window("title_pet", new WindowStateChangedRequest(param4: 260, param5: 130)) },
             { "contacts", new Window("title_contacts", new WindowStateChangedRequest(param4: 356, param5: 350)) },
        };

        private static ClientUIMenuBarModule GetTopLeftMenu(UserClientConfiguration configuration, Dictionary<string, Window> extra) {

            Dictionary<string, Window> topLeftMenu = new Dictionary<string, Window>(_topLeft); // create copy

            if (!configuration.UserSettings.displaySettingsModule.displayChat) {
                topLeftMenu.Remove("chat");
            }

            if (extra != null && extra.Count > 0) {
                foreach (var pair in extra) {
                    topLeftMenu[pair.Key] = pair.Value;
                }
            }

            foreach (var windowState in configuration.WindowSettings) {
                if (topLeftMenu.TryGetValue(windowState.Key, out Window window)) {
                    topLeftMenu[windowState.Key] = new Window(window.Value, windowState.Value);
                }
            }

            return new ClientUIMenuBarModule(ClientUIMenuBarModule.GAME_FEATURE_BAR,
                GenerateTopLeft(topLeftMenu), configuration.SlotbarPositions.gameFeatureBarPosition,
                configuration.SlotbarPositions.gameFeatureBarLayout);

        }

        internal static void AddMenuBarItem(string key, Window value) {
            _topLeft[key] = value;
        }

        internal static void RemoveMenuBarItem(string key) {
            _topLeft.Remove(key);
        }

        private static object _lock = new object();
        private static ClientUIMenuBarModule _keineAhnungWofuer = new ClientUIMenuBarModule(ClientUIMenuBarModule.NOT_ASSIGNED, new List<ClientUIMenuBarItemModule>(), "", "");
        public static ClientUIMenuBarsCommand Generate(UserClientConfiguration configuration, Dictionary<string, Window> extra) {
            lock (_lock) {
                return new ClientUIMenuBarsCommand(new List<ClientUIMenuBarModule>() {
                    GetTopLeftMenu(configuration, extra),
                    GetTopRightMenu(configuration),
                    _keineAhnungWofuer
                });
            }
        }

    }
}
