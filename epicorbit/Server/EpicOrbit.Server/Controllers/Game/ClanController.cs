using EpicOrbit.Emulator.Services;
using EpicOrbit.Server.Data.Models.Enumerables;
using EpicOrbit.Server.Extensions;
using EpicOrbit.Server.Middlewares.Attributes;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels;
using EpicOrbit.Shared.ViewModels.Account;
using EpicOrbit.Shared.ViewModels.Clan;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Server.Controllers.Game {

    [Route("api/[controller]")]
    public class ClanController : Controller {

        [HttpGet, HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> Get([FromQuery] string query, [FromQuery] int offset, [FromQuery] int count) {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.RetrieveClans(accountSessionView.AccountID, (query ?? "").ToLower(), offset, count));
            }
            return Ok(ValidatedView<EnumerableResultView<ClanView>>.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpGet("current"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> GetCurrent() {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.RetrieveClanViewFromMember(accountSessionView.AccountID));
            }
            return Ok(ValidatedView<ClanView>.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpGet("current/members"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> GetMembers() {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.RetrieveClanMembersFromMember(accountSessionView.AccountID));
            }
            return Ok(ValidatedView<List<AccountClanView>>.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpGet("current/members/self"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> GetSelf() {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.RetrieveSelf(accountSessionView.AccountID));
            }
            return Ok(ValidatedView<AccountClanView>.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpGet("current/members/pending"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> GetPending() {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.RetrievePendingFromMember(accountSessionView.AccountID));
            }
            return Ok(ValidatedView<List<AccountClanView>>.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpPut("current/members/pending/{id}"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> AcceptJoinRequest(int id) {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.AcceptJoinRequest(accountSessionView.AccountID, id));
            }
            return Ok(ValidatedView.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpDelete("current/members/pending/{id}"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> RejectJoinRequest(int id) {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.RejectJoinRequest(accountSessionView.AccountID, id));
            }
            return Ok(ValidatedView.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpPut("leave"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> Leave() {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.Leave(accountSessionView.AccountID));
            }
            return Ok(ValidatedView.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpPost, HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> Post([FromBody] ClanCreateView clanCreateView) {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.CreateClan(accountSessionView.AccountID, clanCreateView));
            }
            return Ok(ValidatedView.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpDelete("request/{clanId}"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> RevokeRequest(int clanId) {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.RevokeJoinRequest(accountSessionView.AccountID, clanId));
            }
            return Ok(ValidatedView.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpPost("request"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> CreateRequest([FromBody] ClanJoinView clanJoinView) {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.CreateJoinRequest(accountSessionView.AccountID, clanJoinView));
            }
            return Ok(ValidatedView.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpPut("current/members/role/{id}/{role}"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> AssingRole(int id, int role) {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.AssignRole(accountSessionView.AccountID, id, (ClanRole)role));
            }
            return Ok(ValidatedView.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpPost("current"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> Edit([FromBody] ClanCreateView clanUpdateView) {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.Edit(accountSessionView.AccountID, clanUpdateView));
            }
            return Ok(ValidatedView.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpGet("current/diplomacies"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> GetDiplomacies() {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.RetrieveDiplomacies(accountSessionView.AccountID));
            }
            return Ok(ValidatedView<List<ClanDiplomacyView>>.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpGet("current/diplomacies/pending"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> GetPendingDiplomacies() {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.RetrievePendingDiplomacies(accountSessionView.AccountID));
            }
            return Ok(ValidatedView<List<ClanDiplomacyView>>.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpPost("current/diplomacies"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> CreateDiplomacy([FromBody] ClanDiplomacyCreateView clanDiplomacyCreateView) {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await ClanService.CreateDiplomacy(accountSessionView.AccountID, clanDiplomacyCreateView));
            }
            return Ok(ValidatedView.Invalid(ErrorCode.OPERATION_FAILED));
        }

    }
}
