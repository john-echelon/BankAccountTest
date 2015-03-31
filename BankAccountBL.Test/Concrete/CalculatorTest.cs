using System;
using BankAccountBL.Concrete;
using NUnit.Framework;
namespace BankAccountBL.Test.Concrete
{
    [TestFixture]
    public class CalculatorTest
    {
        [Test]
        ///
        /// Note: A recommended method Naming Convention is: <MethodNameUnderTest>_<Scenario>_<ExpectBehavior>
        /// A Unit test usually comprises three main actions:
        /// [Arrange] objects, creating and setting them up as necessary.
        /// [Act] on a object.
        /// [Assert] that something is expected.
        public void Add_GivenTwoNumbers_SumOfTwoNumbers()
        {
            //Arrange
            var calc = new Calculator();
            Decimal expected = 150.35m;
            //Act
            var actual = calc.Add(100, 50.35m);
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Divide_GivenTwoNumbers_QuotientOfDivisorDividend()
        {
            //Arrange
            var calc = new Calculator();
            Decimal expected = 1.6667m;

            //Act
            var actual = calc.Divide(5m, 3m);

            //Assert
            Assert.That(Math.Abs(expected - actual), Is.LessThan(0.0001M));
        }

        [Test]
        public void Divide_GivenTwoNumberWhereDivisorIsZero_ThrowDivideByZeroException()
        {
            //Arrange
            var calc = new Calculator();
            
            //Act Assert
            Exception ex = Assert.Throws<DivideByZeroException>(()=> calc.Divide(9, 0));
            Assert.That(ex.Message, Is.EqualTo("Denominator cannot be zero."));
        }
    }
}
