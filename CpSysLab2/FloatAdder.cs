using System;

namespace CpSysLab2
{
    public class FloatAdder
    {
        public static (string def, string binAnsw, float answ) Add(float aValue, float bValue)
        {
            uint aTemporal = BitConverter.ToUInt32(BitConverter.GetBytes(aValue), 0);
            uint bTemporal = BitConverter.ToUInt32(BitConverter.GetBytes(bValue), 0);
            
            uint aSign = aTemporal >> 31;
            uint bSign = bTemporal >> 31;
            uint aExponent = (aTemporal >> 23) & 0xFF;
            uint bExponent = (bTemporal >> 23) & 0xFF;
            uint aMantissa = (aTemporal & 0x7FFFFF) + 8388608;
            uint bMantissa = (bTemporal & 0x7FFFFF) + 8388608;

            string def = $"{aValue} + {bValue}\n\n";
            def += $"A = {Convert.ToString(aTemporal, 2)}\n";
            def += $"B = {Convert.ToString(bTemporal, 2)}\n\n";
            def += $"A: sign = {aSign}  exponent = {Convert.ToString(aExponent, 2)}  mantissa = {Convert.ToString(aMantissa, 2)}\n";
            def += $"B: sign = {bSign}  exponent = {Convert.ToString(bExponent, 2)}  mantissa = {Convert.ToString(bMantissa, 2)}\n";
            
            if (((aExponent<<23) + aMantissa) < ((bExponent<<23) + bMantissa))
            {
                uint temp = aTemporal;
                aTemporal = bTemporal;
                bTemporal = temp;
                temp = aExponent;
                aExponent = bExponent;
                bExponent = temp;
                temp = aSign;
                aSign = bSign;
                bSign = temp;
                temp = aMantissa;
                aMantissa = bMantissa;
                bMantissa = temp;

                def += "\nValue swap: |A| < |B|\n";
                def += $"A: sign = {aSign}  exponent = {Convert.ToString(aExponent, 2)}  mantissa = {Convert.ToString(aMantissa, 2)}\n";
                def += $"B: sign = {bSign} exponent = {Convert.ToString(bExponent, 2)}  mantissa = {Convert.ToString(bMantissa, 2)}\n";
            }
            
            uint cExponent = aExponent;
            
            bMantissa >>= (int)(aExponent - bExponent);
            def += $"\nRight shift B mantissa by the exponent difference {aExponent - bExponent}\n";
            def += $"B: sign = {bSign}  exponent = {Convert.ToString(bExponent, 2)}  mantissa = {Convert.ToString(bMantissa, 2)}\n";
            
            uint mantissa;

            if (aSign == bSign)
                mantissa = aMantissa + bMantissa;
            else
                mantissa = aMantissa - bMantissa;

            def += $"\nCompute {(aSign==bSign?"sum":"difference")} of the mantissas\n";
            def+=  $"C: sign = {aSign}  exponent = {Convert.ToString(cExponent, 2)}  mantissa = {Convert.ToString(mantissa, 2)}\n";
            

            if (mantissa >> 23 != 1)
            {
                int length;
                uint tmp = mantissa;
                for (length = 0; tmp != 0; tmp >>= 1, length++) ;

                cExponent += (uint)(length - 24);
                if (length > 24)
                    mantissa >>= (length - 24);
                else
                    mantissa <<= (24 - length);

                def += "\nNormalize the result\n";
                def += $"C: sign = {aSign}  exponent = {Convert.ToString(cExponent, 2)}  mantissa = {Convert.ToString(mantissa, 2)}\n";
            }

            uint result = (((aSign << 8) + cExponent)<<23) + (mantissa& 0x7FFFFF);
            float floatResult = BitConverter.ToSingle(BitConverter.GetBytes(result), 0);

            return (def, Convert.ToString(result, 2), floatResult);
        }
    }
}
