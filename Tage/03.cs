using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModulF.Tage
{
    internal class _03 : BaseClass
    {
        int aufenhaltsdauer;
        int anzahlDerPersonen;
        Saison saison;
        ZimmerKategorie zimmerKategorie;
        Kundendkategorie kundendkategorie;
        int anzahlKinder;
        List<int> kinderAlter = new List<int>();

        internal override void ResolveTasks() {

            Console.Clear();
            Willkommen();
        }

        void Willkommen() { 
            Console.Clear();
            Console.WriteLine("Willkommen\n");
            //Thread.Sleep(1000);
            Eingabe_AufenhaltsDauer();
        }

        void Eingabe_AufenhaltsDauer() {
            Console.WriteLine("\n------------------------------\n");
            Console.WriteLine("Eingabe_AufenhaltsDauer:\n");
            string input = Console.ReadLine().Trim();
            if (Int32.TryParse(input, out aufenhaltsdauer))
            {
                Console.WriteLine("Input Successful\n");
                Eingabe_AnzahlderPersonen();
            }
            else 
            { 
                Console.WriteLine("Input Error:\n");
                Console.Beep(10000, 500);
                Eingabe_AufenhaltsDauer();
            }
        }

        void Eingabe_AnzahlderPersonen()
        {
            Console.WriteLine("\n------------------------------\n");
            Console.WriteLine("Eingabe_AnzahlderPersonen:\n");
            string input = Console.ReadLine().Trim();
            if (Int32.TryParse(input, out anzahlDerPersonen))
            {
                Console.WriteLine("Input Successful\n");
                Eingabe_Season();
            }
            else
            {
                Console.WriteLine("Input Error:\n");
                Console.Beep(10000, 500);
                Eingabe_AnzahlderPersonen();
            }
        }
        void Eingabe_Season()
        {
            Console.WriteLine("\n------------------------------\n");
            Console.WriteLine("Eingabe_Season:\n");
            string input = Console.ReadLine().Trim();
            if (Int32.TryParse(input, out int output) && Enum.IsDefined(typeof(Saison), output))
            {
                saison = (Saison)output;
                Console.WriteLine("Input Successful\n");
                Eingabe_Zimmerkategorie();
            }
            else
            {
                Console.WriteLine("Input Error:\n");
                Console.Beep(10000, 500);
                Eingabe_Season();
            }
        }
        void Eingabe_Zimmerkategorie()
        {
            Console.WriteLine("\n------------------------------\n");
            Console.WriteLine("Eingabe_Zimmerkategorie:\n");
            string input = Console.ReadLine().Trim();
            if (Int32.TryParse(input, out int output) && Enum.IsDefined(typeof(ZimmerKategorie), output))
            {
                zimmerKategorie = (ZimmerKategorie)output;
                Eingabe_Kundenkategorie();
            }
            else
            {
                Console.WriteLine("Input Error:\n");
                Console.Beep(10000, 500);
                Eingabe_Zimmerkategorie();
            }
        }
        void Eingabe_Kundenkategorie()
        {
            Console.WriteLine("\n------------------------------\n");
            Console.WriteLine("Eingabe_Kundenkategorie:\n");
            string input = Console.ReadLine().Trim();
            if (Int32.TryParse(input, out int output) && Enum.IsDefined(typeof(Kundendkategorie), output))
            {
                kundendkategorie = (Kundendkategorie)output;
                Console.WriteLine("Input Successful\n");
                Eingabe_KinderAnzahl();
            }
            else
            {
                Console.WriteLine("Input Error:\n");
                Console.Beep(10000, 500);
                Eingabe_Kundenkategorie();
            }
        }

        void Eingabe_KinderAnzahl()
        {
            Console.WriteLine("\n------------------------------\n");
            Console.WriteLine("Eingabe_KinderAnzahl:\n");
            string input = Console.ReadLine().Trim();
            if (Int32.TryParse(input, out anzahlKinder))
            {
                Console.WriteLine("Input Successful\n");
                Eingabe_KinderAlter();
            }
            else
            {
                Console.WriteLine("Input Error:\n");
                Console.Beep(10000, 500);
                Eingabe_KinderAnzahl();
            }
        }

        void Eingabe_KinderAlter()
        {
            Console.WriteLine("\n------------------------------\n");
            Console.WriteLine("Eingabe_KinderAlter:\n");
            int counter = 1;
            while (counter <= anzahlKinder)
            {
                Console.WriteLine($"Kind {counter} alter:\n");
                string input = Console.ReadLine().Trim();
                if (Int32.TryParse(input, out int alter) && alter >= 0 && alter < 18)
                {
                    Console.WriteLine("Input Successful\n");
                    kinderAlter.Add(alter);
                    counter++;
                }
                else
                {
                    Console.WriteLine("Input Error\n");
                    Console.Beep(10000, 500);
                }
            }
            Berechnung();
        }

        void Berechnung()
        {
            double zimmerPreis = zimmerKategorie.GetHashCode()*10d;
            double kundernRabatt = kundendkategorie.GetHashCode()*0.1d;
            double totalePersonenAnzahl = 0d;
            totalePersonenAnzahl += anzahlDerPersonen;
            foreach(int alter in kinderAlter) 
            {
                if (alter < 7)
                    totalePersonenAnzahl += 0.0;
                else if(alter >= 7 && alter <= 11)
                    totalePersonenAnzahl += 0.3;
                else
                    totalePersonenAnzahl += 0.7;
            }
            double netto = totalePersonenAnzahl * zimmerPreis * kundernRabatt * aufenhaltsdauer;
            if (aufenhaltsdauer == 1) //Zuschlag
                netto += 100;
            double mwst = 0.19d;
            double brutto = netto * (1+mwst);
            Ausgabe(brutto);
        }

        void Ausgabe(double brutto) 
        {
            Console.WriteLine($"\nFinal Price: {brutto:N2} $ \n");
            Console.ReadKey();
        }




        enum Saison
        {
            Frühling = 1,
            Sommer = 2,
            Herbsts = 3,
            Winter = 4
        }

        enum ZimmerKategorie
        {
            Standard = 1,
            Komfort = 2,
            Suite = 3
        }
        enum Kundendkategorie
        {
            Stammkunde = 1,
            Firmenkunde = 2,
            Reisebüro = 3
        }
    }
}
