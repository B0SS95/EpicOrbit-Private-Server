using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Services.Implementations {
    public class WebClientWithRange : WebClient {

        private readonly long from;
        private readonly long to;

        public WebClientWithRange(long from, long to) {
            this.from = from;
            this.to = to;
        }

        protected override WebRequest GetWebRequest(Uri address) {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.AddRange(from, to);
            return request;
        }

    }
}
