using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace EpicOrbit.Client.Services {
    public class ComponentService {

        #region {[ EVENTS ]}
        public event Action<IComponent> OnShow;
        #endregion

        #region {[ FUNCTIONS ]}
        public void Show(IComponent component) {
            OnShow?.Invoke(component);
        }
        #endregion

    }
}
