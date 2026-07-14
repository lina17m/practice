using System.Threading;

namespace task17
{
    public class LongTask : ILongCommand
    {
        public int Steps { get; private set; }
        public bool IsCompleted => Steps <= 0;

        public LongTask(int steps) => Steps = steps;

        public void Execute()
        {
            if (Steps > 0)
            {
                Thread.SpinWait(1000); 
                Steps--;
            }
        }
    }
}
