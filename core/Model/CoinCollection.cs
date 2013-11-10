// -----------------------------------------------------------------------
// <copyright file="CoinCollection.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class CoinCollection : Collection<CoinStack>
    {
        private readonly Currency currency;

        private readonly ICoinValidator coinValidator;

        public CoinCollection(Currency currency) 
            : this(currency, new List<CoinStack>())
        {
            
        }

        public CoinCollection(Currency currency, IList<CoinStack> list)
            : this(currency, list, new CoinValidator(new CurrencyValidator(), new DenominationValidatorFactory()))
        {
        }

        public CoinCollection(Currency currency, IList<CoinStack> list, ICoinValidator coinValidator)
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

        protected override void InsertItem(int index, CoinStack item)
        {
            this.Validate(item);
            base.InsertItem(index, item);
        }

        protected void Validate(CoinStack item)
        {
            this.coinValidator.Validate(item.Coin);

            if (!this.currency.Equals(item.Coin.Currency))
            {
                throw new ArgumentOutOfRangeException("item", item.Coin.Currency, string.Format("This collection is a {0}, as such only coins of the same currency may be added.", this.currency));
            }
        }
    }
}