using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Emulator.Services;
using EpicOrbit.Server.Data.Models.Enumerables;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;

namespace EpicOrbit.Server.Middlewares.Authorization {
    public class RoleHandler : AuthorizationHandler<RoleRequired> {

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequired requirement) {
            if (context.User.Identities.Count() < 2) {
                context.Fail();
                return;
            }

            Dictionary<string, string> claims = context.User.Identities.ElementAt(1).Claims
                .ToDictionary(x => x.Type, x => x.Value);

            if (!int.TryParse(claims["x-api-id"], out int id)) {
                context.Fail();
                return;
            }

            ValidatedView<GlobalRole> validatedAuthenticateView = await AccountService
                .Authenticate(new AccountSessionView(id, claims["x-api-token"]));
            if (validatedAuthenticateView.IsValid && validatedAuthenticateView.Object >= requirement.Role) {
                context.Succeed(requirement);
                return;
            }

            context.Fail();
        }

    }
}
