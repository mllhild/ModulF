using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulF.Tage
{
    internal class _07 : BaseClass
    {
        internal override void ResolveTasks() {
            Console.WriteLine("---------- 7 ----------------");
            // by Ref val
            // in C#, classes are always by reference, others (primitive, structs) are by value by default

            // delegate
            // used to transform a method into a variable so that it can be passed along

            Console.WriteLine("Reference");
            int number = 5;
            // by value
            number = MultiplyBy2Default(number);
            Console.WriteLine(number + "\n");
            // by forced reference
            number = 5;
            MultiplyBy2Ref(ref number);
            Console.WriteLine(number + "\n");
            
            Console.WriteLine("Delegates");
            // delegate
            Calculate calc = Addition;
            int result = calc.Invoke(5, 4); // does addition
            Console.WriteLine(result + "\n");

            calc = Multiply;
            result = calc.Invoke(5, 4); // does multiply
            // 20
            Console.WriteLine(result + "\n");

            calc += Multiply; 
            result = calc.Invoke(5, 4); // does multiply twice, but only the last one returns is passed to the result
            // 20
            Console.WriteLine(result + "\n");

            result = 5;
            result = calc.Invoke(result, 4); // does multiply twice, but only the last one returns is passed to the result and updates it
            // 20
            Console.WriteLine(result + "\n");


            Console.WriteLine();
            Writer writer = Write1;
            writer += Write2;
            writer += Write3;
            writer += Write1;
            writer.Invoke(); // Invokes methods in sequence they where added


            Console.ReadKey(intercept: true);
        }

        internal int MultiplyBy2Default(int number) {
            number = number * 2;
            return number;
        }
        internal void MultiplyBy2Ref(ref int number)
        {
            number = number * 2;
        }

        internal delegate int Calculate(int num1, int num2);
        internal int Addition(int num1, int num2) { return num1 + num2; }
        internal int Multiply(int num1, int num2) { return num1 * num2; }
        internal void MethodWithDelegate(Calculate calculate) { }

        internal delegate void Writer();
        internal void Write1() { Console.WriteLine(1); }
        internal void Write2() { Console.WriteLine(2); }
        internal void Write3() { Console.WriteLine(3); }
    }
}
