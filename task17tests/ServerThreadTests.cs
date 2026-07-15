using Xunit;
using System.Collections.Concurrent;
using System.Threading;
using System;
using task17;

namespace task17
{
    public class ServerThreadTests
    {
        private class TestCommand : ICommand
        {
            public int ExecutedCount = 0;
            public void Execute() => Interlocked.Increment(ref ExecutedCount);
        }

        [Fact]
        public void HardStop_StopsImmediately()
        {
            var queue = new BlockingCollection<ICommand>();
            var server = new ServerThread(queue, new Scheduler());
            var testCmd = new TestCommand();

            queue.Add(new HardStop(server));
            queue.Add(testCmd);

            server.Start();
            server.internalThread.Join(1000);

            Assert.Equal(0, testCmd.ExecutedCount);
        }

        [Fact]
        public void SoftStop_ProcessesRemainingCommands()
        {
            var queue = new BlockingCollection<ICommand>();
            var server = new ServerThread(queue, new Scheduler());
            var testCmd = new TestCommand();

            queue.Add(new SoftStop(server, queue));
            queue.Add(testCmd);
            queue.Add(testCmd);

            server.Start();
            server.internalThread.Join(1000);

            Assert.Equal(2, testCmd.ExecutedCount);
        }

        [Fact]
        public void Commands_ShouldThrow_WhenCalledFromOtherThread()
        {
            var queue = new BlockingCollection<ICommand>();
            var server = new ServerThread(queue, new Scheduler());
            var hardStop = new HardStop(server);

            Assert.Throws<InvalidOperationException>(() => hardStop.Execute());
        }

        [Fact]
        public void LongTasks_AreProcessedByScheduler()
        {
            var queue = new BlockingCollection<ICommand>();
            var sched = new Scheduler();
            var server = new ServerThread(queue, sched);
            
            var longTask = new LongTask(5); 
            queue.Add(longTask);

            server.Start();
            Thread.Sleep(200); 

            Assert.True(longTask.IsCompleted);
            server.Stop();
        }
    }
}
