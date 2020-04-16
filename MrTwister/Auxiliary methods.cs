using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrTwister
{
    public static class Auxiliary_methods
    {
        public static bool ContainsDifferentLetters(string input) => !(input.ToUpper().ToList().GetRange(1, input.Length - 2).Distinct().Count() == 1); //Gibt vom Zweiten bis zum Vorletzten Buchstaben alle unterschiedlichen Buchstaben zurück und guckt ob nur ein Buchstabe zurück kam, was heißt, dass der Inhalt aus nur gleichen Buchstaben bestand
        public static void WriteInColor(string output)
        {
            string endSymbol = @"\>";
            int startIndexOfStartColoredOutputString = output.IndexOf('<');
            if (startIndexOfStartColoredOutputString == -1)
                Console.WriteLine(output);
            else
            {
                Console.Write(output.Substring(0, startIndexOfStartColoredOutputString));
                int endIndexOfColor = output.IndexOf(" ", startIndexOfStartColoredOutputString) - 1;
                var color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), output.Substring(startIndexOfStartColoredOutputString + 1, endIndexOfColor - startIndexOfStartColoredOutputString + 1));
                Console.ForegroundColor = color;
                int endIndexOfColoredOutputString = output.IndexOf(endSymbol) - 1;
                Console.Write(output.Substring(endIndexOfColor + 2, endIndexOfColoredOutputString - endIndexOfColor - 1));
                Console.ForegroundColor = ConsoleColor.White;
                WriteInColor(output.Substring(endIndexOfColoredOutputString + 3));
            }
        }
    }
}
