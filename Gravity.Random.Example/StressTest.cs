using System;
using System.Collections.Generic;
using System.Text;
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
        {                              //Note: compiled higher percentage
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
            for (int i = 0; i < 100; i++)
            {
                Ran.GetRandomString(100, System.Text.Encoding.UTF8);
            }
        }

        static void DebugA()
        {
            RandomGenerator ran = new RandomGenerator();
            RandomUtils utils = new RandomUtils();
            byte[] bytes = ran.GetRandomBytes();
            Task<int> inef = utils.InefficientRandomInt(); //Use this with <Task_Name>.Result; to get the return value
            Console.WriteLine(utils.ByteArrayToHex(bytes));
            Console.WriteLine(utils.ByteArrayToInt(bytes));
            Console.WriteLine(utils.ByteArrayToString(bytes));
            Console.WriteLine(inef.Result);
            Console.WriteLine(utils.EfficientRandomInt());
            Console.WriteLine(ran.GetRandomString(10, Encoding.Unicode));
            SHelp();

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
            //Console.WriteLine("DebugM  | Debug  | Shows Debug menu");
            Console.WriteLine("Help    | SHelp  | Shows this screen");
            Console.WriteLine("More will be added to this screen in later versions");
            Console.Write("What do you want to do? ");
            string command = Console.ReadLine();
            switch (command)
            {
                case "DebugM":
                    Debug();
                    break;
                case "Stress":
                    Console.Write("Max simlutanius Calculations? (doesn't really matter because of windows restrictions)");
                    int amount = int.Parse(Console.ReadLine());
                    if (amount != 0) Stress(amount); else SHelp();
                    break;
                case "DebugA":
                    DebugA();
                    break;
                case "Help":
                    SHelp();
                    break;
                case "Pass":
                    Console.WriteLine(MaskCreate.GenPass(MaskCreate.Mask2, 20));
                    Thread.Sleep(10000);
                    SHelp();
                    break;
                default:
                    SHelp();
                    break;
            }
        }

    }
}
