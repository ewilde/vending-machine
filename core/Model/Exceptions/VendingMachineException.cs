// -----------------------------------------------------------------------
// <copyright file="VendingMachineException.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class VendingMachineException : ApplicationException
    {
        public VendingMachineException()
        {
        }

        public VendingMachineException(string message)
            : base(message)
        {
        }

        public VendingMachineException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected VendingMachineException(
            SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}