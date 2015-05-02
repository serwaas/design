using System;
using System.Collections.Generic;
using System.Threading;

namespace FluentTask
{
	internal class Program
	{
		private static void Main()
		{


		    var behaviour = new Behavior()
		        .Say("Привет мир!")
            				.UntilKeyPressed(b => b
                                .Say("Ля-ля-ля!")
                                .Say("Тру-лю-лю"))
                            .Jump(JumpHeight.High)
                            .UntilKeyPressed(b => b
                                .Say("Aa-a-a-a-aaaaaa!!!")
                                .Say("[набирает воздух в легкие]"))
                            .Say("Ой!")
                           .Delay(TimeSpan.FromSeconds(1))
                            .Say("Кто здесь?!")
                            .Delay(TimeSpan.FromMilliseconds(2000));
            
            behaviour.Execute();


		}
	}

    public class Behavior
    {
        private List<Action<Behavior>> _actions = new List<Action<Behavior>>();

        public Behavior Say(string something)
        {
            _actions.Add(behavior => Console.WriteLine(something));
            return this;
        }

        public Behavior UntilKeyPressed(Action<Behavior> action)
        {
            _actions.Add(b =>
            {
                var behavior = new Behavior();
                while (!Console.KeyAvailable)
                {
                    action.Invoke(behavior);
                    behavior.Execute();
                    Thread.Sleep(500);
                }
                Console.ReadKey(true);
            });
            return this;
        }

        public Behavior Jump(JumpHeight height)
        {
            _actions.Add(behavior =>
            {
                var line = height == JumpHeight.High ? "Высоко прыгнул" : "Низко прыгнул";
                Console.WriteLine(line);
            });
            return this;
        }

        public Behavior Delay(TimeSpan time)
        {
            _actions.Add(behavior =>
            {
                Thread.Sleep(time);
            });
            return this;
        }

        public void Execute()
        {
            foreach (var action in _actions)
            {
                action.Invoke(this);
            }
            _actions.Clear();
        }

    }
}