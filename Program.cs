using ModulF.Tage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModulF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChooseChapter();
        }

        static void ChooseChapter() 
        {
            Console.WriteLine("Choose Chapter");
            Console.WriteLine("1 - Erste Schritte");
            Console.WriteLine("2 - Hotel");
            Console.WriteLine("3 - Hotel mit Kindern");
            Console.WriteLine("4 - ");
            Console.WriteLine("5 - ");
            Console.WriteLine("6 - ");
            Console.WriteLine("7 - ");
            Console.WriteLine("");
            Console.WriteLine("7 - Change Text Color");
            Console.WriteLine("8 - Change Background Color");
            Console.WriteLine("0 - Quit");

            Console.WriteLine("F - Flappy Bird");
            Console.WriteLine("----------------------------\n");

            string input = Console.ReadLine();

            switch(input)
            {
                case "1": new _01().Run(); break;
                case "2": new _02().Run(); break;
                case "3": new _03().Run(); break;
                case "4": new _04().Run(); break;
                case "5": new _05().Run(); break;
                case "6": new _06().Run(); break;
                case "7": new _07().Run(); break;
                case "8": ChangeTextColor(); break;
                case "9": ChangeBackgroundColor(); break;

                case "F": new FlappyBird().Start(); break;


                case "0":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("");
                    Console.WriteLine("   ____                 _     ____           ");
                    Console.WriteLine("  / ___| ___   ___   __| |   | __ )  _   _   ");
                    Console.WriteLine(" | |  _ / _ \\ / _ \\ / _` |   |  _ \\ | | | |  ");
                    Console.WriteLine(" | |_| | (_) | (_) | (_| |   | |_) || |_| |  ");
                    Console.WriteLine("  \\____|\\___/ \\___/ \\__,_|   |____/  \\__, |  ");
                    Console.WriteLine("                                       |___/  ");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
                    speechSynthesizer.SetOutputToDefaultAudioDevice();
                    speechSynthesizer.Speak("Good Bye");
                    Thread.Sleep(1000);
                    return;


                default:
                    Console.WriteLine("Wrong Input");
                    ChooseChapter();
                    break;
            }

            ChooseChapter();
        }

        static void ChangeTextColor()
        {
            Console.WriteLine("Choose Text Color");
            Console.WriteLine("1 - Green");
            Console.WriteLine("2 - Red");
            Console.WriteLine("3 - Magenta");
            Console.WriteLine("0 - Default");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "2":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "0":
                    Console.ResetColor();
                    break;
                default:
                    ChangeTextColor();
                    break;
            }
            Console.Clear();
            ChooseChapter();
        }
        static void ChangeBackgroundColor()
        {
            Console.WriteLine("Choose Background Color");
            Console.WriteLine("1 - Green");
            Console.WriteLine("2 - Red");
            Console.WriteLine("3 - Magenta");
            Console.WriteLine("0 - Default");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case "2":
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case "3":
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case "0":
                    Console.ResetColor();
                    break;
                default:
                    ChangeBackgroundColor();
                    break;
            }
            Console.Clear();
            ChooseChapter();
        }


    }
}
