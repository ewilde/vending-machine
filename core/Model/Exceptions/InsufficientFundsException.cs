// -----------------------------------------------------------------------
// <copyright file="InsufficientFundsException.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception raised when there are not enough funds.
    /// </summary>
    [Serializable]
    public class InsufficientFundsException : VendingMachineException
    {
        private readonly decimal requiredFunds;

        private readonly decimal actualFunds;

        public InsufficientFundsException()
        {
        }

        public InsufficientFundsException(string message)
            : base(message)
        {
        }

        public InsufficientFundsException(string message, decimal requiredFunds, decimal actualFunds)
            : base(message)
        {
            this.requiredFunds = requiredFunds;
            this.actualFunds = actualFunds;
        }

        public InsufficientFundsException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InsufficientFundsException(
            SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public decimal ActualFunds
        {
            get
            {
                return this.actualFunds;
            }
        }

        public decimal RequiredFunds
        {
            get
            {
                return this.requiredFunds;
            }
        }
    }
    
}