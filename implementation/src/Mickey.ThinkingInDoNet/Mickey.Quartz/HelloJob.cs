using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Quartz
{
    public class HelloJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("HelloJob is executing." + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
