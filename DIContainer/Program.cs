using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using DIContainer.Commands;
using Ninject;


namespace DIContainer
{
    public interface IProgram
    {
        void Run();
        string[] GetCommandNames();
    }

    public class Program : IProgram
    {
        private readonly ICommandLineArgs _arguments;
        private readonly ICommand[] _commands;
        private readonly TextWriter _textWriter;


        public Program(ICommandLineArgs arguments, TextWriter textWriter, params ICommand[] commands)
        {
            _arguments = arguments;
            _commands = commands;
            _textWriter = textWriter;

        }

        public string[] GetCommandNames()
        {
            return _commands.Select(command => command.Name).ToArray();
        }

        static void Main(string[] args)
        {
            var container = new StandardKernel();
            container.Bind<ICommand>().To<TimerCommand>();
            container.Bind<ICommand>().To<PrintTimeCommand>();
            container.Bind<ICommand>().To<HelpCommand>();

            container.Bind<IProgram>().To<Program>()
                                      .InSingletonScope();
            
            container.Bind<TextWriter>().ToConstant(Console.Out);

            container.Bind<ICommandLineArgs>().To<CommandLineArgs>()
                                              .WithConstructorArgument(typeof(string[]), args);
            
            container.Get<IProgram>().Run();
        }

        public void Run()
        {
            if (_arguments.Command == null)
            {
                _textWriter.WriteLine("Please specify <command> as the first command line argument");
                return;
            }
            var command = _commands.FirstOrDefault(c => c.Name.Equals(_arguments.Command, StringComparison.InvariantCultureIgnoreCase));
            if (command == null)
                _textWriter.WriteLine("Sorry. Unknown command {0}", _arguments.Command);
            else
                command.Execute();
        }
    }
}
