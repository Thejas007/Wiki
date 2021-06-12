using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace ForceStopConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Scheduler starting ...!");
            // Grab the Scheduler instance from the Factory
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();

            Console.WriteLine("Scheduler started...!");

            // some sleep to show what's happening
            await Task.Delay(TimeSpan.FromSeconds(5));

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithRepeatCount(2).WithIntervalInSeconds(20))
                .Build();

            Console.WriteLine("Job scheduled...!");

            // Tell quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);           

            await Task.Delay(TimeSpan.FromSeconds(10));
            Console.WriteLine("Scheduler stopping");
            await scheduler.Shutdown(true);
            Console.WriteLine("Scheduler stopped");
        }

        public class HelloJob : IJob
        {
            public async Task Execute(IJobExecutionContext context)
            {
                await Console.Out.WriteLineAsync("Greetings from HelloJob!");
                Console.WriteLine("Running...!");
                Console.WriteLine("Waiting...!");
                await Task.Delay(TimeSpan.FromSeconds(10));
                Console.WriteLine("Completed...!");
            }
        }
    }
}
