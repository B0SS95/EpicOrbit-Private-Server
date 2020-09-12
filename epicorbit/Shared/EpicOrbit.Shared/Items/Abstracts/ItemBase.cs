using EpicOrbit.Shared.Items.Interfaces;

namespace EpicOrbit.Shared.Items.Abstracts {
    public abstract class ItemBase : IIndentifyable {

        public abstract int ID { get; }
        public abstract string Name { get; }

    }
}
