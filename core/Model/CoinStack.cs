// -----------------------------------------------------------------------
// <copyright file="CoinStack.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    public class CoinStack
    {
        public Coin Coin { get; private set; }

        public int Amount { get; private set; }

        public decimal Total
        {
            get
            {
                return this.Coin * this.Amount;
            }
        }

        public CoinStack(Coin coin, int amount)
        {
            this.Coin = coin;
            this.Amount = amount;
        }
    }
}