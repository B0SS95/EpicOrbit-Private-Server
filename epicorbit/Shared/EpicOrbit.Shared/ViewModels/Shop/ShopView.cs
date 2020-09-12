using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Shop {
    public class ShopView {

        public double Discount { get; set; }
        public Dictionary<string, ShopCategoryView> Categories { get; set; }

    }
}
