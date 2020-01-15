///////////////////////////////////////////////////////////////////////////////////////////
// Bruce Rhoades - Asynchronous Operation Examples
//
// Code to simulate lenghty operations using asynchronous operations:
//
// 1.) Using straight asynchronous tasks with await and async
// 2.) Using asychronous tasks running in parallel
//
// Using Tasks with asynchronous operations (using async and await) is a bit more 
// flexible and straightforward than threads. They will tell when an operation is completed,
// they can return a value and no explicit thread management is necessary
///////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Threading.Tasks;
using System.Text;

namespace parallel
{
    class Asynchronous
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        // Method to take timestamps before and after running a (simulated) lengthy operation
        // Uses a StringBuilder to collect results from the operation and display them
        //
        // This method runs asynchronously (async keyword in function header) so will not block
        // the calling thread
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static async void testAsynchronously()
        {
            Console.WriteLine("Asynchronous Tasks!");
            DateTime start = DateTime.Now;
            // Tasks used to execute asynchronously on a thread pool and represents some work to be done
            // Advantages of tasks is that they can tell you if the work has completed and if it returns a result
            // Tasks make applications more responsive by offloading the work to another thread from the thread pool
            Task<string> result1, result2, result3;
            StringBuilder sb = new StringBuilder();
            string one = string.Empty;
            string two = string.Empty;
            string three = string.Empty;

            result1 = GetStr1(); 
            result2 = GetStr2(); 
            result3 = GetStr3(); 
 
            // await so that main thread is not blocked - not having 'await' would require this method
            // to be run synchronously (async removed) and main thread (calling method) would be blocked
            // until this method and its tasks complete (similar to testWithParallelTasks())
            await Task.WhenAll(result1, result2, result3);
            sb.Append(result1.Result);
            sb.Append(result2.Result);
            sb.Append(result3.Result);
            Console.WriteLine("Done with Tasks - Values are: " + sb);
            DateTime finish = DateTime.Now;
            Console.WriteLine("Total Milliseconds = " + finish.Subtract(start).TotalMilliseconds);
        }


        ///////////////////////////////////////////////////////////////////////////////////////////
        // Method to take timestamps before and after running a (simulated) lengthy operation
        // Uses a StringBuilder to collect results from the operation and display them
        //
        // This method runs synchronously (no async keyword in function header) so will block
        // the calling thread until the three asynchronous tasks complete. Since this uses
        // Parallel Programming, multiple CPU Cores existing in modern computers can be utilized
        // for enhanced throughput
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static void testWithParallelTasks()
        {
            Console.WriteLine("Parallel Tasks!");
            DateTime start = DateTime.Now;
            // Tasks used to execute asynchronously on a thread pool and represents some work to be done
            // Advantages of tasks is that they can tell you if the work has completed and if it returns a result
            // Tasks make applications more responsive by offloading the work to another thread from the thread pool
            // CPU Bound operations can be parallelized to multiple processors 
            Task<string> result1, result2, result3;
            StringBuilder sb = new StringBuilder();
            string one = string.Empty;
            string two = string.Empty;
            string three = string.Empty;

            // execute operations in paralell, if possible
            // No guarantees are made about the order in which the operations execute or whether they execute 
            // in parallel. This method does not return until each of the provided operations has completed, 
            // regardless of whether completion occurs due to normal or exceptional termination.
            // i.e., Parallel.Invoke does not 'await' and this method is not async
            Parallel.Invoke(() => { result1 = GetStr1(); one = result1.Result; },
                            () => { result2 = GetStr2(); two = result2.Result; },
                            () => { result3 = GetStr3(); three = result3.Result; });
            
            sb.Append(one);
            sb.Append(two);
            sb.Append(three);
            Console.WriteLine("Done with Tasks - Values are: " + sb);
            DateTime finish = DateTime.Now;
            Console.WriteLine("Total Milliseconds = " + finish.Subtract(start).TotalMilliseconds);
        }


        ///////////////////////////////////////////////////////////////////////////////////////////
        // Simulated lengthy operation one. Simply delaying for 5 seconds and returning "1"
        //
        // async modifier to indicate that the method is to run asynchronously as it
        // can run for awhile and we do not want to block the caller's thread
        // use a Task object for a return value in an async operation (wrapping a string in this case)
        ///////////////////////////////////////////////////////////////////////////////////////////
        static async Task<string> GetStr1()
        {
            Console.WriteLine("Starting Asynch Task 1");
            string ret = string.Empty;
            await Task.Delay(5000);
            ret = "1";
            Console.WriteLine("Returning " + ret + " from Asynch Task 1");
            return ret;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Simulated lengthy operation two. Simply delaying for 3 seconds and returning "2"
        //
        // async modifier to indicate that the method is to run asynchronously as it
        // can run for awhile and we do not want to block the caller's thread
        // use a Task object for a return value in an async operation (wrapping a string in this case)
        ///////////////////////////////////////////////////////////////////////////////////////////
        static async Task<string> GetStr2()
        {
            Console.WriteLine("Starting Asynch Task 2");
            string ret = string.Empty;
            await Task.Delay(3000);
            ret = "2";
            Console.WriteLine("Returning " + ret + " from Asynch Task 2");
            return ret;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Simulated lengthy operation three. Simply delaying for 2 seconds and returning "3"
        //
        // async modifier to indicate that the method is to run asynchronously as it
        // can run for awhile and we do not want to block the caller's thread
        // use a Task object for a return value in an async operation (wrapping a string in this case)
        ///////////////////////////////////////////////////////////////////////////////////////////
        static async Task<string> GetStr3()
        {
            Console.WriteLine("Starting Asynch Task 3");
            string ret = string.Empty;
            await Task.Delay(2000);
            ret = "3";
            Console.WriteLine("Returning " + ret + " from Asynch Task 3");
            return ret;
        }
    }
}