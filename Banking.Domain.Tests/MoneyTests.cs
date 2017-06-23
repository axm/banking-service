using Base.Types;
using NUnit.Framework;
using System;

namespace Banking.Domain.Tests
{
    [TestFixture]
    [Author("Alex")]
    [Category("Constructor")]
    public class MoneyConstructorTests
    {
        [Test]
        [Category("Constructor_Exception")]
        public void NegativeParameter_ThrowsArgumentOutOfRangeException()
        {
            var negativeAmount = -250.35m;

            Assert.Throws<ArgumentOutOfRangeException>(() => new Money(negativeAmount));
        }

        [DatapointSource]
        public Money[] MoneyAmounts = new[] { new Money(100), new Money(0), new Money(25) };

        [Theory]
        [Category("Constructor_OK")]
        public void ZeroOrPositiveParameter_WorksOK(decimal amount)
        {
            Assume.That(amount >= 0);

            Assert.DoesNotThrow(() => new Money(amount));
        }
    }

    [TestFixture]
    [Author("Alex")]
    [Category("Operator")]
    public class MoneySubtractOperator
    {
        [Test]
        [Category("Operator_Exception")]
        public void Subtraction_IfFirstArgumentSmallerThanSecond_ThrowsInvalidOperationException()
        {
            var param1 = new Money(125m);
            var param2 = new Money(250m);

            Assert.That(param1 < param2);

            Assert.Throws<InvalidOperationException>(() => { var result = param1 - param2; });
        }

        [Theory]
        [Category("Operator_OK")]
        public void Subtraction_IfFirstArgumentIsGreaterThanOrEqualToSecondArgument_OK(Money param1, Money param2)
        {
            Assume.That(param1 >= param2);

            Assert.DoesNotThrow(() => { var result = param1 - param2; });
        }
    }
}
