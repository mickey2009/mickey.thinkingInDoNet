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

            for (int i = 0; i < 5; i++)
            {
                File.AppendAllText(path, "\n222222");
            }
            Console.Write(File.ReadAllText(path));
            Console.ReadLine();
        }
    }
}
