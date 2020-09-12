using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Server.Data.Synchronisation {
    public enum LockType {

        Read,
        UpgradeableRead,
        Write

    }
}
