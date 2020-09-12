using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EpicOrbit.Client.Services.Extensions {
    public static class JsonConvertExtension {

        public static T DeserializeJsonSafe<T>(this string json) {
            try {
                return JsonConvert.DeserializeObject<T>(json);
            } catch (Exception e) {
                ClientContext.Logger.LogError(e);
            }
            return default;
        }

    }
}
