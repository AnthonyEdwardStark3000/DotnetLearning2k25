namespace ThreadSynchronization
{
    /// <summary>
    /// Semaphore is a synchronization primitive that can be used to control access to a resource pool.
    /// signal mechanism.
    /// </summary>
    public class SemaphoreExample
    {
        private static Semaphore _semaphore = new Semaphore(1, 1);
        public static void Main(string[] args) {
            // writing threads
            for (int i=0;i<15;i++) { 
                new Thread(ThreadWrite).Start();
            }
        }
        public static void ThreadWrite()
        {
            Console.WriteLine("Thread {0} is waiting !!!.", Thread.CurrentThread.ManagedThreadId);
            _semaphore.WaitOne();
            Console.WriteLine("Thread {0} is Writing the semaphore.........", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            Console.WriteLine("Thread {0} has completed.", Thread.CurrentThread.ManagedThreadId);
            _semaphore.Release();
        }
    }
}