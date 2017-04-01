using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MyNewService
{
    public partial class MyNewService : ServiceBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("test");

        public MyNewService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            new System.Threading.Timer(p =>
            {
                log.Info("当前时间:" + DateTime.Now);
            }, null, 0, 1000);
        }

        protected override void OnStop()
        {
        }
    }
}
