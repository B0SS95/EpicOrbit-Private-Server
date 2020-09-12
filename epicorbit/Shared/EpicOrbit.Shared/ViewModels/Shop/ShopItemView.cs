using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.ViewModels.Shop {
    public class ShopItemView {

        public string Identifier { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public int PriceUridium { get; set; }
        public int PriceCredits { get; set; }
        public double Discount { get; set; }

    }
}
