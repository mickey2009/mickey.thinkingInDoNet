using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Log4net
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("test");

        static void Main(string[] args)
        {
            //1.配置文件从app.config中提取出来
            //2.使用可变的文件名称来存储日志
            //3.定义输出格式
            //<layout type="log4net.Layout.PatternLayout">
            // < conversionPattern value = "%newline%date%newline%level%newline%logger%newline%message%newline" />
            //</ layout >
            log.Debug("Application is working");
            log.Fatal("Application is working");
            log.Info("Application is working");
            log.Warn("Application is working");
            log.Error("Application is working");
            Console.ReadLine();
        }
    }
}
