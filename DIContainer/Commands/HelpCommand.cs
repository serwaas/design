using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer.Commands
{
    public class HelpCommand : BaseCommand
    {
        private Lazy<IProgram> _program;
        private readonly TextWriter _textWriter;

        public HelpCommand(Lazy<IProgram> program, TextWriter textWriter)
        {
            _program = program;
            _textWriter = textWriter;
        }

        public override void Execute()
        {
            var commandNames = _program.Value.GetCommandNames();
            foreach (var commandName in commandNames)
            {
                _textWriter.WriteLine(commandName);
            }

        }
    }
}
