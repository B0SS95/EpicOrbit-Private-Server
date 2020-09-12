using System;
using System.Collections.Generic;
using System.Text;
using EpicOrbit.Server.Data.Models.Modules;

namespace EpicOrbit.Server.Data.Models.Items {
    public class FactionData {

        #region {[ PROPERTIES ]}
        public int LowerBaseMapID { get; }
        public Position LowerBasePosition { get; }

        public int UpperBaseMapID { get; }
        public Position UpperBasePosition { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        public FactionData(int lowerBaseMapID = 0, Position lowerBasePosition = null, int upperBaseMapID = 0, Position upperBasePosition = null) {
            LowerBaseMapID = lowerBaseMapID;
            LowerBasePosition = lowerBasePosition;

            UpperBaseMapID = upperBaseMapID;
            UpperBasePosition = upperBasePosition;
        }
        #endregion

    }
}
