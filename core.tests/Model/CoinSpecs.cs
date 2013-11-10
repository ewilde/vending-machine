// -----------------------------------------------------------------------
// <copyright file="CoinSpecs.cs">
// Copyright (c) 2013.
// </copyright>
// -----------------------------------------------------------------------
namespace VendingMachine.Core.Tests
{
    using Machine.Specifications;

    using VendingMachine.Core.Tests.Util;

    [Subject(typeof(Coin), "Equality")]
    public class when_comparing_two_object_that_are_equal : WithResult<bool>
    {
        static Coin Subject1;
        static Coin Subject2;

        Establish context = () =>
            {
                Subject1 = new Coin(Currency.GBP, 2.0m);
                Subject2 = new Coin(Currency.GBP, 2.0m);
            };

        It should_produce_the_same_hashcode = () => 
            Subject1.GetHashCode().ShouldEqual(Subject2.GetHashCode());

        It should_be_equal_to_each_other = () => 
            Subject1.Equals(Subject2).ShouldBeTrue();
    }
}