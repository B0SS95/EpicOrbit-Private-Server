using EpicOrbit.Server.Data.Implementations;

namespace EpicOrbit.Emulator.Game.Objects.Abstracts {
    public abstract class AddressableObjectBase {

        public int ID { get; set; }

        public AddressableObjectBase() {
            ID = RandomGenerator.Identifier();
        }

        ~AddressableObjectBase() {
            RandomGenerator.Remove(ID);
        }

    }
}
