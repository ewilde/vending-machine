// -----------------------------------------------------------------------
// <copyright file="OutOfStockException.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class OutOfStockException : Exception
    {
        public OutOfStockException()
        {
        }

        public OutOfStockException(string message)
            : base(message)
        {
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