///////////////////////////////////////////////////////////////////////////////////////////
// Bruce Rhoades - Threading Operation Examples
//
// Code to simulate lenghty operations using multithreading:
//
// 1.) Using the thread class
// 2.) Using a ThreadPool
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace parallel
{
    ///////////////////////////////////////////////////////////////////////////////////////////
    // Class used with the thread pool solution to trigger when the thread is complete and to
    // return data from the thread. Maintains collections of triggers and return values - one 
    // for each thread
    ///////////////////////////////////////////////////////////////////////////////////////////
    class State
    {
        public EventWaitHandle [] eventWaitHandles;
        public string [] results;

        // Constructor to initialize the amount of threads involved
        // Sets up the Reset Events for each thread to an untriggered state
        public State(int threadCount)
        {
            results = new string[threadCount];
            eventWaitHandles = new EventWaitHandle[threadCount];
            for(int i = 0; i < threadCount; i++)
            {
                EventWaitHandle mRH = new ManualResetEvent(false);
                eventWaitHandles[i] = mRH;
            }
        }
    }

    class Threadingous
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        // Method to take timestamps before and after running a (simulated) lengthy operation
        // Uses a StringBuilder to collect results from the operation and display them
        //
        // Creates threads explicitly and launches them to run the operations concurrently
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static void testThreading()
        {
            Console.WriteLine("Threads!");
            DateTime start = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            
            string one = string.Empty;
            string two = string.Empty;
            string three = string.Empty;
            Thread t1 = new Thread(()=> {one = Threadingous.getStr1(); });
            t1.Start();

            // while first thread is running, carry out other operations
            Thread t2 = new Thread(()=> {two = Threadingous.getStr2(); });
            t2.Start();

            // while second thread is running, carry out other operations
            Thread t3 = new Thread(()=> {three = Threadingous.getStr3(); });
            t3.Start();

            // wait for threads to complete by joining to current thread
            t1.Join();
            t2.Join();
            t3.Join();
            sb.Append(one);
            sb.Append(two);
            sb.Append(three);
            Console.WriteLine("Done with Threads - Values are: " + sb);
            DateTime finish = DateTime.Now;
            Console.WriteLine("Total Milliseconds = " + finish.Subtract(start).TotalMilliseconds);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Method to take timestamps before and after running a (simulated) lengthy operation
        // Uses a StringBuilder to collect results from the operation and display them
        //
        // Uses a threadpool instead of managing threads directly
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static void testThreadingPool()
        {
            Console.WriteLine("Diving Into Thread Pool!");
            DateTime start = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            
            // initialize with 3 threads
            State state = new State(3);
            // ThreadPool.SetMaxThreads() Can be used to set the max number of threads used in a large system
            // Once the number of threads used reaches the maximum, processes will need to wait until threads 
            // are released back to the thread pool when thread completes. This happens (rather than the thread
            // shutting down) if work items needing threads still exist. If so, the released thread that was 
            // placed back in the pool will be used, otherwise the newly released thread will get shut down
            //
            //  Kick off the (simulated) lengthy operations using the ThreadPool. State objects tell us when 
            //  the threads complete and the return values from the threads
            ThreadPool.QueueUserWorkItem(getStr1, state);
            ThreadPool.QueueUserWorkItem(getStr2, state);
            ThreadPool.QueueUserWorkItem(getStr3, state);

            // wait until all the thread have completed
            WaitHandle.WaitAll(state.eventWaitHandles);
            
            // apply the results
            sb.Append(state.results[0]);
            sb.Append(state.results[1]);
            sb.Append(state.results[2]);
            Console.WriteLine("Done with Threads - Values are: " + sb);
            DateTime finish = DateTime.Now;
            Console.WriteLine("Total Milliseconds = " + finish.Subtract(start).TotalMilliseconds);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Simulated lengthy operation one. Simply delaying for 5 seconds and returning "1"
        //
        // This uses the state object to populate the result and signal that the operation is complete
        ///////////////////////////////////////////////////////////////////////////////////////////
        static void getStr1(object stateObject)
        {
            State state = stateObject as State;
            if(state != null)
            {
                state.results[0] = getStr1();
                state.eventWaitHandles[0].Set();
            }
        }
        
        ///////////////////////////////////////////////////////////////////////////////////////////
        // Simulated lengthy operation two. Simply delaying for 3 seconds and returning "2"
        //
        // This uses the state object to populate the result and signal that the operation is complete
        ///////////////////////////////////////////////////////////////////////////////////////////
        static void getStr2(object stateObject)
        {
            State state = stateObject as State;
            if(state != null)
            {
                state.results[1] = getStr2();
                state.eventWaitHandles[1].Set();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Simulated lengthy operation three. Simply delaying for 2 seconds and returning "3"
        //
        // This uses the state object to populate the result and signal that the operation is complete
        ///////////////////////////////////////////////////////////////////////////////////////////
        static void getStr3(object stateObject)
        {
            State state = stateObject as State;
            if(state != null)
            {
                state.results[2] = getStr3();
                state.eventWaitHandles[2].Set();
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Simulated lengthy operation one. Simply delaying for 5 seconds and returning "1"
        ///////////////////////////////////////////////////////////////////////////////////////////
        static string getStr1()
        {
            Console.WriteLine("Starting Thread Operation 1");
            string ret = string.Empty;
            Thread.Sleep(5000);
            ret = "1";
            Console.WriteLine("Returning " + ret + " from Thread Operation 1");
            return ret;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Simulated lengthy operation two. Simply delaying for 3 seconds and returning "2"
        ///////////////////////////////////////////////////////////////////////////////////////////
        static string getStr2()
        {
            Console.WriteLine("Starting Thread Operation 2");
            string ret = string.Empty;
            Thread.Sleep(3000);
            ret = "2";
            Console.WriteLine("Returning " + ret + " from Thread Operation 2");
            return ret;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Simulated lengthy operation three. Simply delaying for 2 seconds and returning "3"
        ///////////////////////////////////////////////////////////////////////////////////////////
        static string getStr3()
        {
            Console.WriteLine("Starting Thread Operation 3");
            string ret = string.Empty;
            Thread.Sleep(2000);
            ret = "3";
            Console.WriteLine("Returning " + ret + " from Thread Operation 3");
            return ret;
        }
    }
}