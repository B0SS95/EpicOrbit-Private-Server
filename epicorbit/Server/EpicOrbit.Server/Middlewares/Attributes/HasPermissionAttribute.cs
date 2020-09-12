using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Server.Data.Models.Enumerables;
using Microsoft.AspNetCore.Authorization;

namespace EpicOrbit.Server.Middlewares.Attributes {
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
    public class HasPermissionAttribute : AuthorizeAttribute {
        public HasPermissionAttribute(GlobalRole role) : base(role.ToString("G")) { }
    }
}
