using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EpicOrbit.Client.Services.Extensions {
    public static class HttpWebRequestExtension {

        public static HttpWebRequest WithAuthentification(this HttpWebRequest request, int id, string token) {
            request.Headers.Add("x-api-id", id.ToString());
            request.Headers.Add("x-api-token", token);
            return request;
        }

        public static HttpWebRequest WithAuthentification(this HttpWebRequest request, ApiClient client) {
            return request.WithAuthentification(client.AccountSessionView.AccountID, client.AccountSessionView.Token);
        }

        public static HttpWebRequest WithBody(this HttpWebRequest request, object body) {
            if (body != null) {
                request.ContentType = "application/json";

                byte[] content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
                request.ContentLength = content.Length;

                using (Stream requestStream = request.GetRequestStream()) {
                    requestStream.Write(content, 0, content.Length);
                }
            }
            return request;
        }

        public static WebExceptionStatus Execute(this HttpWebRequest request, out HttpWebResponse response) {
            response = null;
            try {
                response = (HttpWebResponse)request.GetResponse(); // kein using-context, weil der stream nicht geschlossen werden darf.
                return WebExceptionStatus.Success;
            } catch (WebException e) {
                if (e.Response != null) {
                    response = (HttpWebResponse)e.Response;
                }

                return e.Status;
            }
        }

        public static WebExceptionStatus ExecuteAndDeserialize<T>(this HttpWebRequest request, out HttpStatusCode statusCode, out T responseObject) {
            WebExceptionStatus status = request.Execute(out HttpWebResponse response);

            if (response.TryGetStatusCode(out statusCode)) {
                responseObject = JsonConvert.DeserializeObject<T>(response.GetReponseString());
            } else {
                responseObject = default;
            }

            return status;
        }

        public static WebExceptionStatus ExecuteAndStatus(this HttpWebRequest request, out HttpStatusCode statusCode) {
            WebExceptionStatus status = request.Execute(out HttpWebResponse response);

            response.TryGetStatusCode(out statusCode);
            return status;
        }

        public static bool TryGetStatusCode(this HttpWebResponse response, out HttpStatusCode statusCode) {
            statusCode = response?.StatusCode ?? HttpStatusCode.OK;
            return response != null;
        }

        public static string GetReponseString(this HttpWebResponse response) {
            try {
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8)) {
                    return reader.ReadToEnd();
                }
            } catch {
                return "";
            }
        }

    }
}
