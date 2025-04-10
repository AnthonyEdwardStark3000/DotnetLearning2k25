namespace ThreadSynchronization
{
    public class Lock
    {
        // Locker object
        private static Object _locker = new Object();
        static void Main(string[]args)
        {
            for (int i= 0; i <15;i++) {
                new Thread(PrintThreads).Start();
            }
            Console.ReadKey();
        }
        public static void PrintThreads() {
            lock (_locker)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} starting ...");
                Thread.Sleep(2000); // sleep 2secs
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} completed ...");
            }
        }
    }
}