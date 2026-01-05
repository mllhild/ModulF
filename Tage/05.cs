using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ModulF.Tage
{
    internal class _05 : BaseClass
    {
        internal override void ResolveTasks() { 
        

        }
    }

    /// <summary>
    /// Dies ist eine Klasse
    /// </summary>
    public class Calculator
    {
        public int Id { get; set; }

        private string name;
        public string Name { get { return name; } set { name = value; } }

        public int Age { get; set; }

        public Calculator() : this(0,"",0){ }

        public Calculator(int id) : this(id, "", 0) { }

        public Calculator(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        /// <summary>
        /// Diese Method addiert zwei belibige ganz zahlen
        /// </summary>
        /// <param name="a">Erste Zahl</param>
        /// <param name="b"></param>
        /// <returns>Einen Int</returns>
        public int AddNumbers(int a, int b) { 
            int result = (int)Math.Round(AddNumbers((double)a, (double)b ));
            return result;
        }

        /// <summary>
        /// Diese Method addiert zwei belibige zahlen
        /// </summary>
        /// <param name="a">Erste Zahl</param>
        /// <param name="b"></param>
        /// <returns>Einen Double</returns>
        public double AddNumbers(double a, double b)
        {
            double result = a + b;
            return result;
        }
    }
}
