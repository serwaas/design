using System;
using System.Diagnostics;

namespace PerfLogger
{
    public class PerfLogger : IDisposable
    {
        public Action<long> Action { get; private set; }
        public Stopwatch Timer { get; private set; }

        public static PerfLogger Measure(Action<long> action)
        {
            return new PerfLogger
            {
                Action = action,
                Timer = Stopwatch.StartNew()
            };
        }

        public void Dispose()
        {
            Timer.Stop();
            var workingTime = Timer.ElapsedMilliseconds;
            Action(workingTime);
        }
    }
}