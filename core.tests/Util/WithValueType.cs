// -----------------------------------------------------------------------
// <copyright file="WithValueType.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------

namespace VendingMachine.Core.Tests.Util
{
    using System;

    using Machine.Specifications;

    public class WithValueType<TSubject>
    {
        public static TSubject Subject { get; set; }

        public static Exception Exception { get; set; }

        Cleanup cleanup = () =>
            {
                Subject = default(TSubject);
                Exception = null;
            };
    }

    public class WithValueTypeAndResult<TSubject, TResult> : WithValueType<TSubject>
    {
        public static TResult Result { get; set; }

        Cleanup cleanup = () => Result = default(TResult);
    }
}