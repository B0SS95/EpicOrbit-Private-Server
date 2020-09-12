using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EpicOrbit.Client.Controllers._API {

    [Route("api/[controller]")]
    public class UpdateController : Controller {

        private static bool _rendered = true;

        [HttpGet]
        public async Task<IActionResult> Index() {
            if (_rendered) {
                _rendered = false;
                return StatusCode(500);
            }
            return Ok();
        }

    }
}
