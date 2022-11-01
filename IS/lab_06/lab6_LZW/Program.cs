using System;
using System.IO;

namespace lab6_LZW
{
    class Program
    {
        static void Main(string[] args)
        {
            string fType = "rar";
            LZWAlgo.Compress("test." + fType, "tmp.txt");
            Console.WriteLine();
            LZWAlgo.Decompress("tmp.txt", "res." + fType);
        }
    }
}
