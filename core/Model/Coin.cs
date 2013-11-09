// -----------------------------------------------------------------------
// <copyright file="Coin.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    public struct Coin
    {
        private readonly Currency currency;
        private readonly decimal amount;

        public Coin(Currency currency, decimal amount) : this()
        {
            this.amount = amount;
            this.currency = currency;
        }

        public decimal Amount
        {
            get
            {
                return this.amount;
            }            
        }

        public Currency Currency
        {
            get
            {
                return this.currency;
            }            
        }

        public bool Equals(Coin other)
        {
            return this.currency == other.currency;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Coin && this.Equals((Coin)obj);
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + this.currency.GetHashCode();
            
            return hash;
        }
    }
}