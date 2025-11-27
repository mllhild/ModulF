using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModulF
{
    abstract internal class BaseClass
    {
        internal void Run()
        {
            Start();
            ResolveTasks();
            End();
        }

        internal void Start()
        {
            Console.WriteLine("-------------- Start -------------");
        }
        internal void End()
        {
            Console.WriteLine("-------------- End -------------");
        }

        abstract internal void ResolveTasks();
    }
}
