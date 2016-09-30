using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gravity.TrueRandom
{ 
    class StressTest
    {
        static RandomGenerator Ran = new RandomGenerator();
        static void Main(string[] args)
        {
            ///Testing every function with preformance vizualization
            ///Also a cpu stresstest
            SHelp();
        }
        [MTAThread]
        static void Stress(int amount) //Test: 100% CPU in MVS but 20-30% in TaskManager
        {
            ulong score = 0;
            if (amount <= 0) return;
            List<Thread> threads = new List<Thread>();
            bool d = false;
            while (true)
            {
                d = false;
                if (threads.Count <= amount){
                    score++;
                    Thread workerThread = new Thread(new ThreadStart(SubStress));
                    threads.Add(workerThread);
                    workerThread.Start();
                }
                foreach(Thread t in threads)
                {
                    if (!t.IsAlive)
                    {
                        d = true;   
                    }
                }
                if (d) threads.RemoveAt(0);
            }
        }

        [MTAThread]
        static void SubStress()
        {
            Ran.GetRandomString(100, System.Text.Encoding.UTF8);
        }

        static void DebugA()
        {

        }
        static void Debug()
        {
            Console.WriteLine("Command | Action | Description");
            Console.WriteLine("Stress  | Stress | Starts a stress test based on the TrueRandom lib(cpu-bound)");
            Console.WriteLine("DebugA  | DebugA | Tests all function to check that they work (only debug output)");
            Console.WriteLine("DebugM  | Debug  | Shows Debug menu");
            Console.WriteLine("DebugH  | DHelp  | Debug help screen");
            Console.WriteLine("Help    | SHelp  | Shows this screen"); //TODO: Change Text
            Console.Write("What do you want to do? ");
            string command = Console.ReadLine();
        }
        static void SHelp()
        {
            Console.WriteLine("Command | Action | Description");
            Console.WriteLine("Stress  | Stress | Starts a stress test based on the TrueRandom lib(cpu-bound)");
            Console.WriteLine("DebugA  | DebugA | Tests all function to check that they work (only debug output)");
            Console.WriteLine("DebugM  | Debug  | Shows Debug menu");
            Console.WriteLine("Help    | SHelp  | Shows this screen");
            Console.Write("What do you want to do? ");
            string command = Console.ReadLine();
            switch (command)
            {
                case "DebugM":
                    Debug();
                    break;
                case "Stress":
                    Console.Write("Max simlutanius Calculations?");
                    int amount = int.Parse(Console.ReadLine());
                    if (amount != 0) Stress(amount); else SHelp();
                    break;
                case "DebugA":
                    break;
                case "Help":
                    SHelp();
                    break;
                default:
                    SHelp();
                    break;
            }
        }

    }
}
