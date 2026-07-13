using System;
using System.Threading;

namespace task14
{
    public class DefiniteIntegral
    {
        private static double _result = 0;

        private static void AtomicAdd(double value)
        {
            double initialVal, computVal;
            do
            {
                initialVal = _result;
                computVal = initialVal + value;
            } while (initialVal != Interlocked.CompareExchange(ref _result, computVal, initialVal));
        }

         public static double SolveSingleThread(double a, double b, Func<double, double> function, double step)
        {
            int n = (int)Math.Max(1, Math.Ceiling((b - a) / step));
            double h = (b - a) / n;
            double sum = (function(a) + function(b)) / 2.0;
            for (int i = 1; i < n; i++)
            {
                sum += function(a + i * h);
            }
            return sum * h;
        }


        public static double Solve(double a, double b, Func<double, double> function, double step, int threadsNumber)
        {
            _result = 0;

            using (Barrier barrier = new Barrier(threadsNumber + 1))
            {
                double section = b - a;
                double sectionSize = section / threadsNumber;

                for (int i = 0; i < threadsNumber; i++)
                {
                    int index = i; 
                    Thread t = new Thread(() =>
                    {
                        double start = a + index * sectionSize;
                        double end = (index == threadsNumber - 1) ? b : start + sectionSize;

                        double localRes = CalculateSection(start, end, function, step);

                        AtomicAdd(localRes);
                        barrier.SignalAndWait();
                    });
                    t.Start();
                }
                barrier.SignalAndWait();
            }
            return _result;
        }

        private static double CalculateSection(double a, double b, Func<double, double> f, double step)
        {
            if (a >= b) return 0;

            int n = (int)Math.Max(1, Math.Ceiling((b - a) / step));
            double h = (b - a) / n; 

            double sum = (f(a) + f(b)) / 2.0;
            for (int i = 1; i < n; i++)
            {
                sum += f(a + i * h);
            }

            return sum * h;
        }
    }
}
