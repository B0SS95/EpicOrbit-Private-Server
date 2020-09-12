using Newtonsoft.Json.Linq;

namespace EpicOrbit.Emulator.Chat.Infinicast.Messages {
    public class APlayStringMessage {

        public string Payload { get; set; }
        public JObject DecodedData { get; set; }
        public bool Binary { get; set; } = true;

        public JObject GetDataAsDecodedJson() {
            if (DecodedData == null) {
                DecodedData = JObject.Parse(Payload);
            }
            return DecodedData;
        }

        public void SetDataAsString(string str) {
            Payload = str;
            DecodedData = null;
        }

        public void SetDataAsJson(JObject json) {
            DecodedData = json;
            Payload = null;
        }

        public string GetDataAsString() {
            if (Payload != null) {
                return Payload;
            }
            if (DecodedData != null) {
                Payload = DecodedData.ToString();
            }
            return Payload;
        }

    }
}
