// -----------------------------------------------------------------------
// <copyright file="Coin.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    /// <summary>
    /// Represents a physical coin, with a denomination and a currency
    /// </summary>
    public struct Coin
    {
        private readonly Currency currency;
        private readonly decimal denomination;

        public Coin(Currency currency, decimal denomination) : this()
        {
            this.denomination = denomination;
            this.currency = currency;
        }

        public decimal Denomination
        {
            get
            {
                return this.denomination;
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

        public override string ToString()
        {
            return string.Format("Currency: {0}, denomination: {1}", this.currency, this.denomination);
        }

        public static implicit operator decimal(Coin coin)
        {
            return coin.denomination;
        }
    }
}