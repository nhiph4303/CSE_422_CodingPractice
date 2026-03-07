using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPracticeLab1.Problems
{
    internal class IntegerDivision
    {
        public int Divide(int dividend, int divisor)
        {
            // Handle edge case where dividend is 0
            if (dividend == 0) return 0;

            // Handle overflow edge case (32-bit signed integer)
            if (dividend == int.MinValue && divisor == -1)
                return int.MaxValue;

            // Determine the sign of the result
            bool isNegative = (dividend < 0) ^ (divisor < 0); // XOR: true if signs are different

            // Work with absolute values
            long dividendAbs = Math.Abs((long)dividend);
            long divisorAbs = Math.Abs((long)divisor);

            int result = 0;
            while (dividendAbs >= divisorAbs)
            {
                long tempDivisor = divisorAbs, multiple = 1;
                // Try to double the divisor (by left shifting)
                while (dividendAbs >= (tempDivisor << 1))
                {
                    tempDivisor <<= 1; // Double the divisor
                    multiple <<= 1; // Double the multiple
                }
                // Subtract the largest multiple of the divisor from the dividend
                dividendAbs -= tempDivisor;
                result += (int)multiple;
            }

            // If the result is negative, we negate it
            return isNegative ? -result : result;
        }
    }
}
