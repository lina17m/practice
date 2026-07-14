using System.Collections.Generic;

namespace task17;

public class Scheduler : IScheduler
{
    private readonly Queue<ICommand> _tasks = new();
    public bool HasCommand() => _tasks.Count > 0;
    public void Add(ICommand cmd) => _tasks.Enqueue(cmd);
    public ICommand Select() => _tasks.Dequeue();
}
