using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace ModulF.Tage
{
    internal class _01 : BaseClass
    {
        internal override void ResolveTasks()
        {
            Console.Clear();
            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.SetOutputToDefaultAudioDevice();
            speechSynthesizer.Speak("Hallo Student");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Install Visual Studio 2022");
            Console.ResetColor();
            Console.WriteLine("Project -> Konsole -> .Net Enviorment -> Disable \"Top Level Statements\" ");

            
            Console.ReadKey();
        }
    }
}
