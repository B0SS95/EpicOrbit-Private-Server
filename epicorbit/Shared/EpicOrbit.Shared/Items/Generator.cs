using EpicOrbit.Shared.Items.Abstracts;

namespace EpicOrbit.Shared.Items {
    public sealed class Generator : ItemBase {

        #region {[ STATIC ]}
        public static Generator G3N1010 { get; } = new Generator(1, "equipment_generator_speed_g3n-1010", 2);
        public static Generator G3N2010 { get; } = new Generator(2, "equipment_generator_speed_g3n-2010", 3);
        public static Generator G3N3210 { get; } = new Generator(3, "equipment_generator_speed_g3n-3210", 4);
        public static Generator G3N3310 { get; } = new Generator(4, "equipment_generator_speed_g3n-3310", 5);
        public static Generator G3N6900 { get; } = new Generator(5, "equipment_generator_speed_g3n-6900", 7);
        public static Generator G3N7900 { get; } = new Generator(6, "equipment_generator_speed_g3n-7900", 10);
        #endregion

        #region {[ PROPERTIES ]}
        public int Speed { get; }
        #endregion

        #region {[ ItemBase implementation ]}
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        public Generator(int id, string name, int speed) {
            ID = id;
            Name = name;
            Speed = speed;
        }
        #endregion

    }
}
