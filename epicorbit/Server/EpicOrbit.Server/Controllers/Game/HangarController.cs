using EpicOrbit.Emulator.Services;
using EpicOrbit.Server.Data.Models.Enumerables;
using EpicOrbit.Server.Data.ViewModels.Hangar;
using EpicOrbit.Server.Extensions;
using EpicOrbit.Server.Middlewares.Attributes;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels.Account;
using EpicOrbit.Shared.ViewModels.Hangar;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Server.Controllers.Game {

    [Route("api/[controller]")]
    public class HangarController : Controller {

        [HttpGet, HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> Get() {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await HangarService.RetrieveHangarOverviews(accountSessionView.AccountID));
            }
            return Ok(ValidatedView<List<HangarOverview>>.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpGet("{id}"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> Get(int id) {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await HangarService.RetrieveHangarDetailView(accountSessionView.AccountID, id));
            }
            return Ok(ValidatedView<HangarDetailView>.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpGet("activate/{id}"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> Activate(int id) {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await HangarService.ActivateHangar(accountSessionView.AccountID, id));
            }
            return Ok(ValidatedView.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpPost, HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> Post([FromBody] HangarDetailView hangarDetailView) {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await HangarService.UpdateHangar(accountSessionView.AccountID, hangarDetailView));
            }
            return Ok(ValidatedView.Invalid(ErrorCode.OPERATION_FAILED));
        }

    }
}
