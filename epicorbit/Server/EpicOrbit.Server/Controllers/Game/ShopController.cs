using EpicOrbit.Server.Data.Models.Enumerables;
using EpicOrbit.Server.Middlewares.Attributes;
using EpicOrbit.Shared.ViewModel;
using EpicOrbit.Shared.ViewModels.Shop;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Server.Controllers.Game {

    [Route("api/[controller]")]
    public class ShopController : Controller {

        [HttpGet, HasPermission(GlobalRole.USER)]
        public async Task<IActionResult> Get() {
            return Ok(ValidatedView<ShopView>.Valid(new ShopView {
                Categories = new Dictionary<string, ShopCategoryView> {
                    { "Drones", new ShopCategoryView {
                        Items = new Dictionary<string, ShopItemView> {
                            { "", new ShopItemView { Name = "LF-2", Identifier = "weapon_lf-2" } }
                        }
                    } }
                }
            }));
        }

    }

}
