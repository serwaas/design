using System;
using System.IO;

namespace DIContainer.Commands
{
    public class PrintTimeCommand : BaseCommand
    {
    
        private readonly TextWriter _textWriter;

        public PrintTimeCommand(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }
        public override void Execute()
        {
            _textWriter.WriteLine(DateTime.Now);
        }
    }
}