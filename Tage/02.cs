using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulF.Tage
{
    internal class _02 : BaseClass
    {
        internal override void ResolveTasks()
        {
            Console.Clear();
            string input = "";
            Console.WriteLine("Anzahl der Personen: ");
            input = Console.ReadLine().Trim();
            if (!Int32.TryParse(input, out int personen))
            {
                Console.WriteLine("Input Error");
                ResolveTasks();
            }

            Console.WriteLine("Aufenhaltsdauer: ");
            input = Console.ReadLine().Trim();
            if (!Int32.TryParse(input, out int dauer))
            {
                Console.WriteLine("Input Error");
                ResolveTasks();
            }

            double netto = 70 * personen * dauer;
            double mwst = 0.19d;
            double gesamtpreis = netto * (1 + mwst);

            Console.WriteLine("Gesamtpreis: " + gesamtpreis.ToString());
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
        }
    }
}
