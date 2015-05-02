using System;
using System.IO;
using System.Threading;

namespace DIContainer.Commands
{
    public class TimerCommand : BaseCommand 
    {
        private readonly ICommandLineArgs _arguments;
        private readonly TextWriter _textWriter;

        public TimerCommand(ICommandLineArgs arguments, TextWriter textWriter )
        {
            _arguments = arguments;
            _textWriter = textWriter;
        }

        public override void Execute()
        {
            var timeout = TimeSpan.FromMilliseconds(_arguments.GetInt(0));
            _textWriter.WriteLine("Waiting for " + timeout);
            Thread.Sleep(timeout);
            _textWriter.WriteLine("Done!");
        }
    }
}