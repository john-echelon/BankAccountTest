using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountBL.Abstract
{
    public interface ICalculator
    {
        decimal Add(decimal x, decimal y);
        decimal Multiply(decimal x, decimal y);
        decimal Divide(decimal numerator, decimal denominator);
    }
}
