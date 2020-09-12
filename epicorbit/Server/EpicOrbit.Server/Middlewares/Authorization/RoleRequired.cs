using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Server.Data.Models.Enumerables;
using Microsoft.AspNetCore.Authorization;

namespace EpicOrbit.Server.Middlewares.Authorization {
    public class RoleRequired : IAuthorizationRequirement {

        public GlobalRole Role { get; }

        public RoleRequired(GlobalRole role) {
            Role = role;
        }

    }
}
