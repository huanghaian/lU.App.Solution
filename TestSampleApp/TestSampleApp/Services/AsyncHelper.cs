using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestSampleApp.Services
{
    public static class AsyncHelper
    {
        public static readonly TaskFactory _taskFactory = new TaskFactory(CancellationToken.None,TaskCreationOptions.None,TaskContinuationOptions.None,TaskScheduler.Default);
        public static void RunAsync(Func<Task> func)
        {
             _taskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();
        }
        public static TResult RunWithResultAsync<TResult>(Func<Task<TResult>> func)
        {
           return _taskFactory.StartNew(func).Unwrap().GetAwaiter().GetResult();
        }

    }
}
