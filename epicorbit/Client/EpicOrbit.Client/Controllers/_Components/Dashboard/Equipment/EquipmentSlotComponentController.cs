using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Controllers._Components.Dashboard.Equipment {
    public class EquipmentSlotComponentController : ComponentBase {

        [Parameter]
        protected int Index { get; set; }

        [Parameter]
        protected List<EquipmentSlotItemController> Items { get; set; }

        [Parameter]
        protected Action<int> ClickHandler { get; set; }

        protected internal bool GetImagePath(out string path) {
            path = null;
            if (Items.Count - 1 < Index || Items[Index].Name == null) {
                return false;
            }

            EquipmentSlotItemController item = Items[Index];
            path = "./do_img/global/items";

            string last = "";
            foreach (string part in item.Name.Split('_')) {
                last = part;
                path += "/" + part;
            }

            path += "_30x30.png";
            return true;
        }

        protected internal void MouseEvent(UIMouseEventArgs mouseEventArgs) {
            if (mouseEventArgs.Button == 2) {
                ClickHandler(Index);
            }
        }

    }
}
