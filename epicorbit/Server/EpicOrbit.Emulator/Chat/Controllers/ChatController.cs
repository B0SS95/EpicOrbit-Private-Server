namespace EpicOrbit.Emulator.Chat.Controllers {
    public class ChatController {

        #region {[ INSTANCE ]}
        public static ChatController Instance {
            get {
                if (_instance == null) {
                    _instance = new ChatController();
                }
                return _instance;
            }
        }
        private static ChatController _instance;
        #endregion

        #region {[ PROPERTIES ]}
        public int Messages { get; set; }
        public int MessagesSent { get; set; }
        public int MessagesReceived { get; set; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        #endregion

    }
}
