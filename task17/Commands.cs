using System;
using System.Collections.Concurrent;
using System.Threading;

namespace task17
{
    public class HardStop : ICommand
    {
        private ServerThread _thread;
        public HardStop(ServerThread thread) => _thread = thread;

        public void Execute()
        {
            if (Thread.CurrentThread != _thread.internalThread) throw new InvalidOperationException();
            _thread.Stop();
        }
    }

    public class SoftStop : ICommand
    {
        private ServerThread _thread;
        private BlockingCollection<ICommand> _queue;

        public SoftStop(ServerThread thread, BlockingCollection<ICommand> queue)
        {
            _thread = thread;
            _queue = queue;
        }

        public void Execute()
        {
            if (Thread.CurrentThread != _thread.internalThread) throw new InvalidOperationException();
            
            _queue.CompleteAdding();
        }
    }
}
