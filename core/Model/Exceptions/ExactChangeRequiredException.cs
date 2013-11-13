// -----------------------------------------------------------------------
// <copyright file="ExactChangeRequiredException.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception raised when exact change is required.
    /// </summary>
    [Serializable]
    public class ExactChangeRequiredException : Exception
    {
        public ExactChangeRequiredException()
        {
        }

        public ExactChangeRequiredException(string message)
            : base(message)
        {
        }

        public ExactChangeRequiredException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ExactChangeRequiredException(
            SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}