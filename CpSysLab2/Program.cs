using System;

namespace CpSysLab2
{
    class Program
    {
        static void Main()
        {
            int i = 7, j = 6;

            Console.WriteLine("Product right move:\n");
            var def = ResultShift.RightShift(i, j);
            Console.WriteLine(def);
            Console.WriteLine();

            Console.WriteLine("Binary division:\n");
            var (def2, r, q) = BinaryDivider.Divide(i,j);
            Console.WriteLine(def2);
            Console.WriteLine($"Remainder = {r} Quotient = {q}");
            Console.WriteLine();

            float a = 2.5f, b = 3f;
            Console.WriteLine("Floating number addition:\n");
            var (def3, bin, result) = FloatAdder.Add(a,b);
            Console.WriteLine(def3);
            Console.WriteLine($"Result: {bin} = {result}");
            
            Console.Read();
        }
    }
}
