// -----------------------------------------------------------------------
// <copyright file="OutOfStockException.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception raised when a <see cref="Product"/> is out of stock.
    /// </summary>
    [Serializable]
    public class OutOfStockException : Exception
    {
        public Product Product { get; set; }

        public OutOfStockException()
        {
        }

        public OutOfStockException(string message)
            : base(message)
        {
        }

        public OutOfStockException(string message, Product product) : this(message)
        {
            this.Product = product;
        }

        public OutOfStockException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected OutOfStockException(
            SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}