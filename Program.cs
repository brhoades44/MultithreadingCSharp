///////////////////////////////////////////////////////////////////////////////////////////
// Bruce Rhoades - Multithreading and Asynchronous Operation Examples
//
// This sample applciation illustrates the enhanced performance of running a lengthy
// operation using a number of approaches. It uses a menuing system to allow the user to
// select from 5 solutions to the problem (showing the amount of time taken in milliseconds):
//
// 1.) It shows the slow performance of running it through a normal, synchronouse 
//     (single-threaded) approach. 
// 2.) A multithreaded solution
// 3.) A multithreaded solution using a ThreadPool
// 4.) A solution involving asynchronous operations running in parallel
// 5.) A solution involving asynchounous operations that allow control fall back to an 
//     invoking thread
//
// Cheers!
///////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Threading;

namespace parallel
{
    class Program
    {
        ///////////////////////////////////////////////////////////////////////////////////////////
        // Method to prompt the user for a process selection. Repeats until the user quits while
        // alerting when the user makes an improper selection
        ///////////////////////////////////////////////////////////////////////////////////////////
        static string getProcessSelection()
        {
            string processSelection = string.Empty;
            while(processSelection != "q")
            {
                Console.WriteLine("\nEnter a value between 1 and 5 (or q to quit) corresponding to your process selection:"); 
                Console.WriteLine("1. Synchronous, Single Threaded");
                Console.WriteLine("2. Multithreaded");
                Console.WriteLine("3. Multithreaded with ThreadPool");
                Console.WriteLine("4. Parallel Tasks");
                Console.WriteLine("5. Asynchronous Tasks");
                processSelection = Console.ReadLine(); 
                if((processSelection != "1") && (processSelection != "2") && (processSelection != "3") &&  
                   (processSelection != "4") && (processSelection != "5"))
                {
                    if(processSelection != "q") // q
                        Console.WriteLine("\nINVALID SELECTION\n");
                    continue;
                }
                else 
                {
                    break;
                }
            }
            return processSelection;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////
        // Main driver function to invoke methods implementing solutions to the problem given the
        // user selection.
        ///////////////////////////////////////////////////////////////////////////////////////////
        static void Main(string[] args)
        {
            string processSelection = string.Empty;
            while(processSelection != "q")
            {
                processSelection = getProcessSelection();
                Console.WriteLine();
                if(processSelection != "q")
                {
                    Console.WriteLine("------------------------------------");
                    switch(processSelection)
                    {
                        case "1":
                            Synchronous.testSynchronously();
                            break;
                        case "2":
                            Threadingous.testThreading();
                            break;
                        case "3":
                            Threadingous.testThreadingPool();
                            break;
                        case "4":
                            Asynchronous.testWithParallelTasks();
                            break;
                        case "5":
                            Asynchronous.testAsynchronously();
                            Console.WriteLine("Main function sleeping for 8 seconds...");
                            Thread.Sleep(8000);
                            break;
                    }
                }
            }
        }        
    }
}
