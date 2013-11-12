namespace VendingMachine.Core
{
    using System.Collections.Generic;

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