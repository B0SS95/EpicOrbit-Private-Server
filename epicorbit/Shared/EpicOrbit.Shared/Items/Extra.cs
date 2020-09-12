using EpicOrbit.Shared.Items.Abstracts;

namespace EpicOrbit.Shared.Items {
    public sealed class Extra : ItemBase {

        #region {[ STATIC ]}
        public static Extra CL04K { get; } = new Extra(1, "equipment_extra_cpu_cl04k-xl", "ttip_cloak_cpu_category");
        public static Extra ANTI_Z1 { get; } = new Extra(2, "equipment_extra_cpu_anti-z1", "ttip_antiz1_cpu_category");
        public static Extra AROL_X { get; } = new Extra(3, "equipment_extra_cpu_arol-x", "ttip_arol_cpu");
        public static Extra RL_LB_X { get; } = new Extra(4, "equipment_extra_cpu_rllb-x", "ttip_rllb_cpu");
        #endregion

        #region {[ PROPERTIES ]}
        public string TTIP { get; }
        #endregion

        #region {[ ItemBase implementation ]}
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        private Extra(int id, string name, string ttip) {
            ID = id;
            Name = name;
            TTIP = ttip;
        }
        #endregion

    }
}
