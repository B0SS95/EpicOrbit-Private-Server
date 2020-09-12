using EpicOrbit.Shared.Enumerables;

namespace EpicOrbit.Shared.ViewModels.Boost {
    public struct BoostView {

        #region {[ PROPERTIES ]}
        public BoosterType Type { get; }
        public AmountType AmountType { get; }
        public double Amount { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        public BoostView(BoosterType boosterType, double amount, AmountType amountType = AmountType.PERCENT) {
            Type = boosterType;
            Amount = amount;
            AmountType = amountType;
        }
        #endregion

    }
}
