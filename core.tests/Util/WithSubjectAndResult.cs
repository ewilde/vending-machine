// -----------------------------------------------------------------------
// <copyright file="WithSubjectAndResult.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core.Tests.Util
{
    using System;

    using Machine.Specifications;
    
    public class WithSubject<TSubject1, TSubject2> : Machine.Fakes.WithSubject<TSubject1> where TSubject1 : class where TSubject2 : class
    {
        private static TSubject2 subject2;

        public static Exception Exception { get; set; }

        public static TSubject2 Subject2
        {
            get
            {
                if (subject2 == null)
                {
                    subject2 = _specificationController.An<TSubject2>();
                }

                return subject2;
            }
        }

        Cleanup cleanup = () =>
            {
                subject2 = null;
            };
    }

    public class WithSubject<TSubject> : Machine.Fakes.WithSubject<TSubject> where TSubject : class
    {
        public static Exception Exception { get; set; }

    }

    public class WithSubjectAndResult<TSubject, TResult> : WithSubject<TSubject> where TSubject : class
    {
        public static TResult Result { get; set; }
        
        Cleanup cleanup = () =>
            {
                Subject = null;
                Result = default(TResult);
                Exception = null;
            };
    }

    public class WithSubjectAndResult<TSubject1, TSubject2, TResult> : WithSubject<TSubject1, TSubject2> where TSubject1 : class where TSubject2 : class
    {        
        public static TResult Result { get; set; }
        
        Cleanup cleanup = () =>
            {
                Subject = null;
                Result = default(TResult);
                Exception = null;
            };
    }

    public class WithResult<TResult> 
    {        
        public static TResult Result { get; set; }

        public static Exception Exception { get; set; }

        Cleanup cleanup = () =>
            {
                Result = default(TResult);
                Exception = null;
            };
    }
}