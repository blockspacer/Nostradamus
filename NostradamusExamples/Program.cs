﻿using FlatBuffers.Schema;
using Nostradamus.Networking;
using Nostradamus.Tests;
using System;

namespace Nostradamus.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            //var t1 = new ServerSimulatorTest();
            //t1.TestExampleScene();

            //var t2 = new ClientSimulatorTest();
            //t2.TestExampleScene();

            SimpleServerExample.Run();

            //ThreadPool.QueueUserWorkItem(s =>
            //{
            //    Thread.Sleep(3000);
            //    SimpleClientExample.Run();
            //});

            //StepByStepExample.Run();

            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }
    }
}
