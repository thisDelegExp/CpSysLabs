using System;
using System.Collections.Generic;
using System.Text;

namespace CpSysLab2
{
    public static class ResultShift
    {
        public static string RightShift (int multiplierValue, int multiplicandValue) {
            string multiplicand = multiplicandValue.ToBinaryString();
            string multiplier = multiplierValue.ToBinaryString();
            string product = new string('0', 64), result;
            string def = string.Empty;
            for (int i = 0; i < 32; i++)
            {
                def += $" Iteration {i + 1}:\r\n Product: {product}\r\n Multiplier: {multiplier}\r\n\r\n";

                if (multiplier[31] == '1')
                {
                    result = SumBinaryStrings(product.Substring(0, 32), multiplicand);
                    product = product.Remove(0, 32);
                    product = result + product;
                }

                product = InternalRightMove(product);
                multiplier = InternalRightMove(multiplier);
            }
            def += "Result(Product):\r\n" + product;

            return def;
        }

        private static string SumBinaryStrings(string productLeftPart, string multiplicand)
        {
            var result = new StringBuilder(new string('0', 32));
            bool remainder = false;
            for (int i = 0; i < 32; i++)
            {
                if (productLeftPart[31 - i] == '1')
                {
                    if (multiplicand[31 - i] == '1')
                    {
                        result[31 - i] = remainder ? '1' : '0';
                        remainder = true;
                    }
                    else
                    {
                        result[31 - i] = remainder ? '0' : '1';
                    }
                }
                else
                {
                    if (multiplicand[31 - i] == '1')
                    {
                        result[31 - i] = remainder ? '0' : '1';
                    }
                    else
                    {
                        result[31 - i] = remainder ? '1' : '0';
                        remainder = false;
                    }
                }
            }
            if (remainder) {
                throw new InvalidOperationException("Register overflow");
            }
            return result.ToString();
        }

        private static string InternalRightMove(string bitString)
        {
            return $"0{bitString.Remove(bitString.Length - 1)}";
        }

        private static string ToBinaryString(this int number)
        {
            var baseConverted =  Convert.ToString(number, 2);
            return  baseConverted.Insert(0, new string('0', 32 - baseConverted.Length));
        }
    }
}
