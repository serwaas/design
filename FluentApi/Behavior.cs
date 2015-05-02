using System;
using System.Collections.Generic;
using System.Threading;

namespace FluentTask
{
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
                    action(behavior);
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
                action(this);
            }
            _actions.Clear();
        }

    }
}