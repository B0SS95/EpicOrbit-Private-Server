using EpicOrbit.Shared.Items.Abstracts;

namespace EpicOrbit.Shared.Items {
    public sealed class Shield : ItemBase {

        #region {[ STATIC ]}
        public static Shield A01 { get; } = new Shield(1, "equipment_generator_shield_sg3n-a01", 1000, 0.4, 0.04);
        public static Shield A02 { get; } = new Shield(2, "equipment_generator_shield_sg3n-a02", 2000, 0.5, 0.04);
        public static Shield A03 { get; } = new Shield(3, "equipment_generator_shield_sg3n-a03", 5000, 0.6, 0.04);
        public static Shield B01 { get; } = new Shield(4, "equipment_generator_shield_sg3n-b01", 4000, 0.7, 0.04);
        public static Shield B02 { get; } = new Shield(5, "equipment_generator_shield_sg3n-b02", 10000, 0.8, 0.04);

        public static Shield FS01 { get; } = new Shield(6, "equipment_generator_shield_fs-01", 3200, 0.7, 0.0425);
        public static Shield FS02 { get; } = new Shield(7, "equipment_generator_shield_fs-02", 5600, 0.75, 0.0425);
        public static Shield FS03 { get; } = new Shield(8, "equipment_generator_shield_fs-03", 8000, 0.8, 0.0425);
        #endregion

        #region {[ PROPERTIES ]}
        public int Strength { get; }
        public double Absorption { get; }
        public double Regeneration { get; }
        #endregion

        #region {[ ItemBase implementation ]}
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        public Shield(int id, string name, int strength, double absorption, double regeneration) {
            ID = id;
            Name = name;
            Strength = strength;
            Absorption = absorption;
            Regeneration = regeneration;
        }
        #endregion

    }
}
