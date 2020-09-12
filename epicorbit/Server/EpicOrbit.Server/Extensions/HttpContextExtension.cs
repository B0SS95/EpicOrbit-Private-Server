using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Emulator.Services;
using EpicOrbit.Server.Data.Models.Enumerables;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels.Account;
using Microsoft.AspNetCore.Http;

namespace EpicOrbit.Server.Extensions {
    public static class HttpContextExtension {

        public static bool TryGetCurrentSession(this HttpContext context, out AccountSessionView accountSession) {
            accountSession = null;

            if (context.Request.Headers.ContainsKey("x-api-id") && context.Request.Headers.ContainsKey("x-api-token")
                && int.TryParse(context.Request.Headers["x-api-id"], out int id)) {
                accountSession = new AccountSessionView(id, context.Request.Headers["x-api-token"]);
            }

            return accountSession != null;
        }

        public static async Task<GlobalRole> GetCurrentUserRole(this HttpContext context) {
            if (!TryGetCurrentSession(context, out AccountSessionView accountSession)) {
                return GlobalRole.NONE;
            }

            ValidatedView<GlobalRole> validatedAuthenticateView =
                await AccountService.Authenticate(accountSession);
            if (!validatedAuthenticateView.IsValid) {
                return GlobalRole.NONE;
            }

            return validatedAuthenticateView.Object;
        }

    }
}
