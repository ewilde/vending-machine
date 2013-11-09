// -----------------------------------------------------------------------
// <copyright file="WithSubjectAndResult.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core.Tests.Util
{
    using System;

    using Machine.Specifications;

    public class WithSubject<TSubject> : Machine.Fakes.WithSubject<TSubject> where TSubject : class
    {
        public static Exception Exception { get; set; }

    }

    public class WithSubjectAndResult<TSubject, TResult> : WithSubject<TSubject> where TSubject : class
    {
        public static TResult Result { get; set; }
        
        
        private Cleanup cleanup = () =>
            {
                Subject = null;
                Result = default(TResult);
                Exception = null;
            };
    }
}