using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.ThinkingInDoNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"F:\git\mickey.thinkingInDoNet\implementation\src\Mickey.ThinkingInDoNet\Mickey.ThinkingInDoNet\test.txt";
            var test = File.ReadAllText(path);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(test.Replace("1", "55"));
                sw.Flush();
            }
            Console.Write(File.ReadAllText(path));

            Console.ReadLine();
        }
    }
}
