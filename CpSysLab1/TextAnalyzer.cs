using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpSysLab1
{
    static class TextAnalyzer
    {
        public static AnalysisResult Analyze(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException("filePath");

            using(StreamReader reader = new StreamReader(filePath, Encoding.Default))
            {
                int length = 0;
                char character;
                Dictionary<char, double> charFrequencies = new Dictionary<char, double>();
                while (!reader.EndOfStream)
                {
                   character = (char)reader.Read();
                    if (charFrequencies.Keys.Contains(character))
                    {
                        charFrequencies[character]++;
                    }
                    else
                    {
                        charFrequencies.Add(character, 1);
                    }
                    length++;
                }
                charFrequencies = charFrequencies
                    .Select(elem => new KeyValuePair<char, double>(elem.Key, 1.0 * elem.Value / length))
                    .OrderBy(elem => elem.Key)
                    .ToDictionary(elem => elem.Key, elem => elem.Value);
                var averageEntropy = -charFrequencies.Sum(elem => elem.Value * Math.Log(elem.Value, 2f));

                return new AnalysisResult(
                        charFrequencies,
                        averageEntropy,
                        Math.Ceiling(averageEntropy * length / 8)
                    );
            }
        }
    }

    class AnalysisResult
    {
        public double AverageEntropy { get; }
        public double Size { get;}
        public Dictionary<char, double> Frequency { get;}

        public AnalysisResult(Dictionary<char, double> frequency, double entropy, double size)
        {
            AverageEntropy = entropy;
            Size = size;
            Frequency = frequency;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var pair in Frequency)
            {
                stringBuilder.AppendLine(pair.Key + " " + pair.Value);
            }
            stringBuilder.AppendLine("Avarage entropy: " + AverageEntropy);
            stringBuilder.AppendLine("Information size: " + Size);

            return stringBuilder.ToString();
        }
    }
}
