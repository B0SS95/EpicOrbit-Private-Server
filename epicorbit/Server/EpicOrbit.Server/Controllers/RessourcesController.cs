using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Emulator;
using EpicOrbit.Server.Services;
using EpicOrbit.Shared.Extensions;
using EpicOrbit.Shared.ViewModels;
using EpicOrbit.Shared.ViewModels.Ressources;
using Microsoft.AspNetCore.Mvc;

namespace EpicOrbit.Server.Controllers {

    [Route("api/[controller]")]
    public class RessourcesController : Controller {

        #region {[ FIELDS ]}
        private RessourceProviderManager _ressourceManager;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public RessourcesController(RessourceProviderManager ressourceManager) {
            _ressourceManager = ressourceManager;
        }
        #endregion

        #region {[ FUNCTIONS ]}

        [HttpPost("token")]
        public async Task<IActionResult> RetrieveToken([FromBody] TokenView tokenView) {
            return Ok(new TokenView {
                Token = SecurityExtension.Encrypt(_ressourceManager.Token, tokenView.Token)
            });
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveProviderList() {
            lock (_ressourceManager.ValidProviders) {
                return Ok(new RessourcesProviderView {
                    Providers = _ressourceManager.ValidProviders
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] string provider) {
            _ressourceManager.Add(provider);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] string provider) {
            _ressourceManager.Delete(provider);
            return Ok();
        }

        [HttpGet("detail")]
        public async Task<IActionResult> Get() {
            return Ok(_ressourceManager.Get());
        }
        #endregion

    }

}
