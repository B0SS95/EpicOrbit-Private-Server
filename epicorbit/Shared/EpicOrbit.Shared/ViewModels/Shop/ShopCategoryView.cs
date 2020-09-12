using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Shop {
    public class ShopCategoryView {

        public double Discount { get; set; }
        public Dictionary<string, ShopItemView> Items { get; set; }

    }
}
