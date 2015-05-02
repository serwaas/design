using System;
using System.Diagnostics;
using System.Linq;

namespace PerfLogger
{
	class Program
	{
		static void Main(string[] args)
		{
            var sum = 0.0;
            using (PerfLogger.Measure(t => Console.WriteLine("for: {0}", t)))
                for (var i = 0; i < 100000000; i++) sum += i;

            using (PerfLogger.Measure(t => Console.WriteLine("linq: {0}", t)))
                sum -= Enumerable.Range(0, 100000000).Sum(i => (double)i);
            Console.WriteLine("Sum = " + sum);
            
		}

	    public class PerfLogger: IDisposable
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
}
