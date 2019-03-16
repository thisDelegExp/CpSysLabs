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

            string testFilePath = @"..\..\SampleText\EncodingTest.txt";
            string customEncodedPath = @"..\..\SampleText\CustomBase64.txt";
            Base64Encoder.Encode(testFilePath, customEncodedPath);

            string customEncoding = File.ReadAllText(customEncodedPath);
            string base64Encoding = Convert.ToBase64String(File.ReadAllBytes(testFilePath));
            Console.WriteLine($"Custom base64: {customEncoding } \n\nConvert.Base64: {base64Encoding}\n");
            Console.WriteLine($"Base64 check: {base64Encoding.Equals(customEncoding)}");

            Console.ReadKey();
        }
    }
}
