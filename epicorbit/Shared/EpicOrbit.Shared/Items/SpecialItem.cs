using EpicOrbit.Shared.Items.Abstracts;
using System;

namespace EpicOrbit.Shared.Items {
    public sealed class SpecialItem : ItemBase {

        #region {[ STATIC ]}
        public static SpecialItem SMB_01 { get; } = new SpecialItem(1, "ammunition_mine_smb-01", TimeSpan.FromSeconds(30));
        public static SpecialItem ISH_01 { get; } = new SpecialItem(2, "equipment_extra_cpu_ish-01", TimeSpan.FromSeconds(30));
        public static SpecialItem EMP_01 { get; } = new SpecialItem(3, "ammunition_specialammo_emp-01", TimeSpan.FromSeconds(30));
        #endregion

        #region {[ PROPERTIES ]}
        public TimeSpan Cooldown { get; }
        #endregion

        #region {[ ItemBase implementation ]}
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        private SpecialItem(int id, string name, TimeSpan cooldown) {
            ID = id;
            Name = name;
            Cooldown = cooldown;
        }
        #endregion

    }
}
