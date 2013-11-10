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

    public class CoinCollection : Collection<Coin>
    {
        private readonly Currency currency;

        private readonly ICoinValidator coinValidator;

        public CoinCollection(Currency currency) 
            : this(currency, new List<Coin>())
        {
            
        }

        public CoinCollection(Currency currency, IList<Coin> list)
            : this(currency, list, new CoinValidator(new CurrencyValidator(), new DenominationValidatorFactory()))
        {
        }

        public CoinCollection(Currency currency, IList<Coin> list, ICoinValidator coinValidator) : base(list)
        {
            this.currency = currency;
            this.coinValidator = coinValidator;
        }

        protected override void InsertItem(int index, Coin item)
        {
            this.Validate(item);
            base.InsertItem(index, item);
        }

        protected void Validate(Coin item)
        {
            this.coinValidator.Validate(item);

            if (!this.currency.Equals(item.Currency))
            {
                throw new ArgumentOutOfRangeException("item", item.Currency, string.Format("This collection is a {0}, as such only coins of the same currency may be added.", this.currency));
            }
        }
    }
}