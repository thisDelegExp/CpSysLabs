using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpSysLab1
{
    class Program
    {
        private static readonly string[] sampleTexts = new [] {"AngularArchitectureOverview.txt", "TeenageMutantNinjaTurtlesThemeSong.txt", "BattleOfWaterloo.txt" };
        private static readonly string textDirectory = @"..\..\SampleText\";
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            foreach (var fileName in sampleTexts)
            {
                Console.WriteLine($"{fileName} \n {TextAnalyzer.Analyze(textDirectory+fileName)} ");
            }

            Console.ReadKey();
        }
    }
}
