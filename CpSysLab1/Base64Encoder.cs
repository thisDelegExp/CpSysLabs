using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CpSysLab1
{
    static class Base64Encoder
    {
        public static void Encode(string inputPath , string outputPath)
        {
            string alphabet = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            int value = 0;
            using(BinaryReader binaryReader = new BinaryReader(File.Open(inputPath, FileMode.Open), Encoding.UTF8))
            {
                using (StreamWriter streamWriter = new StreamWriter(File.OpenWrite(outputPath)))
                {
                    int mod = (int)(binaryReader.BaseStream.Length % 3);
                    for (int i = 0; i < binaryReader.BaseStream.Length - mod; i += 3)
                    {
                        value = (((binaryReader.ReadByte() << 8) + binaryReader.ReadByte()) << 8) + binaryReader.ReadByte();
                        streamWriter.Write($"{alphabet[(value >> 18) & 0x3F]}{alphabet[(value >> 12) & 0x3F]}{alphabet[(value >> 6) & 0x3F]}{alphabet[value & 0x3F]}");
                    }
                    if (mod == 2)
                    {
                        value = ((binaryReader.ReadByte() << 8) + binaryReader.ReadByte()) << 2;
                        streamWriter.Write($"{alphabet[(value >> 12) & 0x3F]}{alphabet[(value >> 6) & 0x3F]}{alphabet[value & 0x3F]}=");
                    }
                    if (mod == 1)
                    {
                        value = binaryReader.ReadByte() << 4;
                        streamWriter.Write($"{alphabet[(value >> 6) & 0x3F]}{alphabet[value & 0x3F]}==");
                    }
                }
            }
        }
    }
}
