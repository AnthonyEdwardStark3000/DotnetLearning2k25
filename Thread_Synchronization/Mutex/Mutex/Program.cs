using System.Threading;

namespace ThreadSynchronization
{
    public class MutexExample
    {
        private static Mutex _mutex = new Mutex();
        static void Main(string[] args)
        {
            for (int i = 0; i < 15; i++)
            {
                new Thread(ThreadProcess).Start();
            }
            Console.ReadKey();
        }

        public static void ThreadProcess()
        {
            Console.WriteLine("Thread {0} is waiting", Thread.CurrentThread.ManagedThreadId);
            _mutex.WaitOne(); // Wait until it is safe to enter
            Console.WriteLine("Thread {0} STARTS WRITING", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(100);
            Console.WriteLine("Thread {0} has finished writing", Thread.CurrentThread.ManagedThreadId);
            _mutex.ReleaseMutex(); // Release the Mutex
        }
    }
}