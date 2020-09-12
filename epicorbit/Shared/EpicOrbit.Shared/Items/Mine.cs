using EpicOrbit.Shared.Items.Abstracts;
using System;

namespace EpicOrbit.Shared.Items {
    public sealed class Mine : ItemBase {

        #region {[ STATIC ]}
        public static TimeSpan Cooldown = TimeSpan.FromSeconds(30);

        public static Mine ACM_01 { get; } = new Mine(1, "ammunition_mine_acm-01");
        public static Mine EMPM_01 { get; } = new Mine(2, "ammunition_mine_empm-01");
        public static Mine SABM_01 { get; } = new Mine(3, "ammunition_mine_sabm-01");
        public static Mine DDM_01 { get; } = new Mine(4, "ammunition_mine_ddm-01");
        public static Mine SLM_01 { get; } = new Mine(7, "ammunition_mine_slm-01");
        public static Mine IM_01 { get; } = new Mine(5, "ammunition_mine_im-01");
        #endregion

        #region {[ ItemBase implementation ]}
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        private Mine(int id, string name) {
            ID = id;
            Name = name;
        }
        #endregion

    }
}
