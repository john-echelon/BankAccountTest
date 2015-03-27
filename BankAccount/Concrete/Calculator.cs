using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccountBL.Abstract;
namespace BankAccountBL.Concrete
{
    public class Calculator : ICalculator
    {
        public decimal Add(decimal x, decimal y)
        {
            return x + y;
        }

        public decimal Multiply(decimal x, decimal y)
        {
            return x * y;
        }

        public decimal Divide(decimal numerator, decimal denominator)
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException("Denominator cannot be zero.");
            }
            return (numerator / denominator) + (numerator % denominator);
        }

    }
}
