///////////////////////////////////////////////////////////////////////////////////////////
// Bruce Rhoades - Synchronous Operation Example
//
// Code to simulate lenghty operations using a synchronous, single threaded approach
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Threading;

namespace parallel
{
    class Synchronous
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        // Method to take timestamps before and after running a (simulated) lengthy operation
        // Uses a StringBuilder to collect results from the operation and display them
        ///////////////////////////////////////////////////////////////////////////////////////////
        public static void testSynchronously()
        {
            Console.WriteLine("Synchronous Operations!");
            DateTime start = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            string one = string.Empty;
            string two = string.Empty;
            string three = string.Empty;
            one = getStr1();
            two = getStr2();
            three = getStr3();
            sb.Append(one);
            sb.Append(two);
            sb.Append(three);
            Console.WriteLine("Done with Tasks - Values are: " + sb);
            DateTime finish = DateTime.Now;
            Console.WriteLine("Total Milliseconds = " + finish.Subtract(start).TotalMilliseconds);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Simulated lengthy operation one. Simply delaying for 5 seconds and returning "1"
        ///////////////////////////////////////////////////////////////////////////////////////////
        static string getStr1()
        {
            Console.WriteLine("Starting Synch Task 1");
            string ret = string.Empty;
            Thread.Sleep(5000);
            ret = "1";
            Console.WriteLine("Returning " + ret + " from Synch Task 1");
            return ret;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Simulated lengthy operation two. Simply delaying for 3 seconds and returning "2"
        ///////////////////////////////////////////////////////////////////////////////////////////
        static string getStr2()
        {
            Console.WriteLine("Starting Synch Task 2");
            string ret = string.Empty;
            Thread.Sleep(3000);
            ret = "2";
            Console.WriteLine("Returning " + ret + " from Synch Task 2");
            return ret;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Simulated lengthy operation three. Simply delaying for 2 seconds and returning "3"
        ///////////////////////////////////////////////////////////////////////////////////////////
        static string getStr3()
        {
            Console.WriteLine("Starting Synch Task 3");
            string ret = string.Empty;
            Thread.Sleep(2000);
            ret = "3";
            Console.WriteLine("Returning " + ret + " from Synch Task 3");
            return ret;
        }
    }
}
