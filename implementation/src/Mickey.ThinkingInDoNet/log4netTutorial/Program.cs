using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log4netTutorial
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("test");

        static void Main(string[] args)
        {
            //参考文档： https://csharp.today/log4net-tutorial-great-library-for-logging/
            log.Info("Application is working");
            Console.ReadLine();
        }
    }
}
