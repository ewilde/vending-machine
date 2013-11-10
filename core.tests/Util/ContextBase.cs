// -----------------------------------------------------------------------
// <copyright file="ContextBase.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core.Tests.Util
{
    using System;

    using Machine.Fakes;

    public class ContextBase
    {
        public static IFakeAccessor FakeAccessor { get; set; }

        public static TSubject Subject<TSubject>()
        {
            if (FakeAccessor == null)
            {
                throw new Exception("Please assign FakeAccessor before calling Subject()");
            }

            return (TSubject)FakeAccessor.GetType().GetProperty("Subject").GetGetMethod().Invoke(FakeAccessor, null);
        }
    }
}