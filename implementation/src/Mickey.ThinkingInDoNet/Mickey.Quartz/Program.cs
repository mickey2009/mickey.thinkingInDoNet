using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.Quartz
{
    class Program
    {
        static void Main(string[] args)
        {
            TestConfig();
        }

        public static void TestSimpleTrigger()
        {
            //https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/index.html
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("myTrigger", "group1")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(5)
                  .RepeatForever())
              .Build();

            sched.ScheduleJob(job, trigger);
        }

        /// <summary>
        ///   The ‘/’ character can be used to specify increments to values
        ///   The ‘?’ character is allowed for the day-of-month and day-of-week fields
        ///   Wild-cards (the ‘*’ character) can be used to say “every” possible value of this field
        /// </summary>
        public static void TestCronTrigger()
        {
            //https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/crontriggers.html
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("myTrigger", "group1")
              .StartNow()
              .WithCronSchedule("0 0 0/1 * * ?")
              .Build();

            sched.ScheduleJob(job, trigger);
        }

        public static void TestConfig()
        {
            //https://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/crontriggers.html
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();
        }
    }
}
