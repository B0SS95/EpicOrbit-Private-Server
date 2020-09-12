using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Emulator;
using EpicOrbit.Emulator.Game;
using EpicOrbit.Emulator.Services;
using EpicOrbit.Server.Data.Models.Enumerables;
using EpicOrbit.Server.Extensions;
using EpicOrbit.Server.Middlewares.Attributes;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels.Account;
using EpicOrbit.Shared.ViewModels.Vault;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EpicOrbit.Server.Controllers.Game {

    [Route("api/[controller]")]
    public class AccountController : Controller {

        [HttpGet, HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> Get() {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await AccountService.RetrieveAccountOverview(accountSessionView.AccountID));
            }
            return Ok(ValidatedView<AccountOverview>.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpGet("vault"), HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> GetVault() {
            if (HttpContext.TryGetCurrentSession(out AccountSessionView accountSessionView)) {
                return Ok(await AccountService.RetrieveVault(accountSessionView.AccountID));
            }
            return Ok(ValidatedView<VaultView>.Invalid(ErrorCode.OPERATION_FAILED));
        }

        [HttpGet("online")]
        public async Task<IActionResult> GetOnlinePlayers() {
            return Ok(ValidatedView<AccountPlayersOnline>.Valid(
                new AccountPlayersOnline {
                    Active = GameContext.IsRunning,
                    Count = GameManager.Players.Count
                }));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountLoginView login) {
            return Ok(await AccountService.Login(login));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountRegisterView register) {
            return Ok(await AccountService.Register(register));
        }

    }

}
