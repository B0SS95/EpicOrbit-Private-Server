using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace EpicOrbit.Server.Middlewares.Authentication {
    public class TokenizedAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions> {

        private static readonly string[] Keys = new[] {
            "x-api-id", "x-api-token"
        };

        public TokenizedAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, IDataProtectionProvider dataProtection, ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync() {
            List<Claim> claimCollection = new List<Claim>();

            foreach (string key in Keys) {
                if (Context.Request.Headers.TryGetValue(key, out StringValues value)) {
                    claimCollection.Add(new Claim(key, value.FirstOrDefault()));
                } else {
                    break;
                }
            }

            if (claimCollection.Count >= Keys.Length) {
                Context.User.AddIdentity(new ClaimsIdentity(claimCollection));
            }

            return Task.Factory.StartNew(AuthenticateResult.NoResult);
        }

    }
}
