using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.Items.Abstracts;
using EpicOrbit.Shared.ViewModels.Boost;

namespace EpicOrbit.Shared.Items {
    public sealed class DroneDesign : ItemBase {

        #region {[ STATIC ]}
        public static DroneDesign NONE { get; } = new DroneDesign(0, "drone_designs_none", new BoostView[0], new BoostView[0]);

        public static DroneDesign HAVOC { get; } = new DroneDesign(1, "drone_designs_havoc", new BoostView[0], new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.1)
        });

        public static DroneDesign HERCULES { get; } = new DroneDesign(2, "drone_designs_hercules", new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.15)
        }, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.15),
            new BoostView(BoosterType.HITPOINTS, 1.2)
        });

        /*  public static DroneDesign SPARTAN { get; } = new DroneDesign(3, "drone_designs_spartan", new Boost[] {
              new Boost(BoosterType.DAMAGE, 1.1), // is falsch aber yolo, bei einer full dmg confi macht es keinen Unterschied
              new Boost(BoosterType.HITPOINTS, 1.01)
          }, new Boost[] {
              new Boost(BoosterType.DAMAGE, 1.1),
              new Boost(BoosterType.SHIELD, 1.1),
              new Boost(BoosterType.HITPOINTS, 1.15)
          }); */
        #endregion

        #region {[ PROPERTIES ]}
        public BoostView[] Single { get; }
        public BoostView[] Full { get; }
        #endregion

        #region {[ ItemBase implementation ]}
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        private DroneDesign(int id, string name, BoostView[] single, BoostView[] full) {
            ID = id;

            Single = single ?? new BoostView[0];
            Full = full ?? new BoostView[0];

            Name = name;
        }
        #endregion

    }
}
