using System;
using System.IO;

namespace JsonTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = File.ReadAllText(@"C:\Users\mdazam\Downloads\dataPartition_0.txt");
            var unescapedText = System.Text.RegularExpressions.Regex.Unescape(text);

        }
    }
}
