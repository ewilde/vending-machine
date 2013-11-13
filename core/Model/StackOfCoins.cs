// -----------------------------------------------------------------------
// <copyright file="StackOfCoins.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    /// <summary>
    /// A stack of coins all of which are of the same currency and denomination
    /// </summary>
    public class StackOfCoins
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

        public StackOfCoins(Coin coin, int amount)
        {
            this.Coin = coin;
            this.Amount = amount;
        }

        public Coin Remove()
        {
            this.Amount = this.Amount - 1;
            return this.Coin;
        }

        public Coin Add()
        {
            this.Amount = this.Amount + 1;            
            return this.Coin;
        }

        public Coin Add(int amount)
        {
            this.Amount = this.Amount + amount;            
            return this.Coin;
        }

        public override string ToString()
        {
            return string.Format("{0}, amount: {1}", this.Coin, this.Amount);
        }
    }
}