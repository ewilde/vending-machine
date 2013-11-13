// -----------------------------------------------------------------------
// <copyright file="MoneyHopper.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// A money hopper is a collection of stacks of coins. The money hopper is used
    /// by the <see cref="VendingMachineApplication"/> to store <see cref="StackOfCoins">stacks of coins</see>
    /// all of the same currency.
    /// </summary>
    public class MoneyHopper : Collection<StackOfCoins>
    {
        private readonly Currency currency;

        private readonly ICoinValidator coinValidator;

        public MoneyHopper(Currency currency) 
            : this(currency, new List<StackOfCoins>())
        {
            
        }

        public MoneyHopper(Currency currency, IList<StackOfCoins> list)
            : this(currency, list, new CoinValidator(new CurrencyValidator(), new DenominationValidatorFactory()))
        {
        }

        public MoneyHopper(Currency currency, IList<StackOfCoins> list, ICoinValidator coinValidator)
            : base(list)
        {
            this.currency = currency;
            this.coinValidator = coinValidator;
        }

        public decimal Total
        {
            get
            {
                return this.Sum(item => item.Total);
            }
        }

        public Currency Currency
        {
            get
            {
                return this.currency;
            }
        }

        public StackOfCoins this[Coin index]
        {
            get
            {
                var stack = this.GetStack(index);
                if (stack == null)
                {
                    throw new IndexOutOfRangeException(string.Format("Index [{0}] not found.", index));
                }

                return stack;
            }
        }

        public void Add(Coin coin)
        {
            var stack = this.GetStack(coin);
            if (stack == null)
            {
                this.Add(new StackOfCoins(coin, 1));
            }
            else
            {
                this[coin].Add();
            }
        }

        public bool Contains(Coin value)
        {
            return this.GetStack(value) != null;
        }

        protected override void InsertItem(int index, StackOfCoins item)
        {
            this.Validate(item);

            if (!this.Contains(item.Coin))
            {
                base.InsertItem(index, item);
            }
            else
            {
                this[item.Coin].Add(item.Amount);
            }
        }

        protected void Validate(StackOfCoins item)
        {
            this.coinValidator.Validate(item.Coin);

            if (!this.Currency.Equals(item.Coin.Currency))
            {
                throw new ArgumentOutOfRangeException("item", item.Coin.Currency, string.Format("This collection is a {0}, as such only coins of the same currency may be added.", this.Currency));
            }
        }

        private StackOfCoins GetStack(Coin index)
        {
            return this.FirstOrDefault(item => item.Coin == index);
        }
    }
}