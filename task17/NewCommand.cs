using System;

namespace task17
{
    public class NewCommand : ILongCommand
    {
        private readonly int _id;
        private int _counter = 0;

        public bool IsCompleted => _counter >= 3;

        public NewCommand(int id) => _id = id;

        public void Execute()
        {
            _counter++;
            Console.WriteLine($"Поток {_id} вызов {_counter}");
        }
    }
}
