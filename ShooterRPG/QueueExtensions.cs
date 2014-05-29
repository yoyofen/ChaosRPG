namespace ShooterRPG
{
    using System.Collections.Generic;

    static class QueueExtensions
    {
        public static void EnqueueList<T>(this Queue<T> queue, List<T> list)
        {
            foreach (T item in list)
            {
                queue.Enqueue(item);
            }
        }

        public static bool IsEmpty<T>(this Queue<T> queue)
        {
            return queue.Count == 0;
        }
    }
}