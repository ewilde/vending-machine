namespace VendingMachine.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// A collection of coins, all of the same <see cref="Currency"/>.
    /// </summary>
    public class CoinPurse : MoneyHopper{
        public CoinPurse(Currency currency)
            : base(currency)
        {
        }

        public CoinPurse(Currency currency, IList<StackOfCoins> list)
            : base(currency, list)
        {
        }

        public CoinPurse(Currency currency, IList<StackOfCoins> list, ICoinValidator coinValidator)
            : base(currency, list, coinValidator)
        {
        }
    }
}