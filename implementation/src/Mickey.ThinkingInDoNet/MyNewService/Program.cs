using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyNewService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// https://msdn.microsoft.com/en-us/library/zt39148a(v=vs.110).aspx
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MyNewService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
