using System;
using System.Collections.Concurrent;
using System.Threading;

namespace task17
{
    public class ServerThread
    {
        private readonly BlockingCollection<ICommand> _queue; 
        private readonly Thread _thread;                     
        private readonly IScheduler _scheduler;                         
        private volatile bool _stop = false;           

        public ServerThread(BlockingCollection<ICommand> queue, IScheduler scheduler)
        {
            _queue = queue;
            _scheduler = scheduler;
            _thread = new Thread(Run);
        }

        public void Start() => _thread.Start();

        private void Run()
        {
            while (!_stop)
            {
                bool process = false;

                if (_queue.TryTake(out var cmd))
                {
                    Exec(cmd);
                    process = true;
                }

                if (_scheduler.HasCommand())
                {
                    Exec(_scheduler.Select());
                    process = true;
                }

                if (!process) Thread.Sleep(1);
            }
        }

        private void Exec(ICommand cmd)
        {
            try 
            { 
                cmd.Execute(); 
                if (cmd is ILongCommand { IsCompleted: false } longCmd)
                {
                    _scheduler.Add(longCmd);
                }
            }
            catch (Exception ex) { HandleEx(ex, cmd); }
        }

        private void HandleEx(Exception ex, ICommand cmd)
        {
            Console.WriteLine($"Ex: {cmd.GetType().Name} -> {ex.Message}");
        }

        public void Stop() => _stop = true;
        public Thread internalThread => _thread;
    }
}
