using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrTwister
{
    class TwisterController
    {
        public void StartTwister()
        {
            #region First Idea for a color pattern
            IDictionary<string, ConsoleColor> colorDic = new Dictionary<string, ConsoleColor>()
            {
                { "#Black", ConsoleColor.Black },
                { "#DarkBlue", ConsoleColor.DarkBlue },
                { "#DarkGreen", ConsoleColor.DarkGreen },
                //DarkCyan = 3,
                //DarkRed = 4,
                //DarkMagenta = 5,
                //DarkYellow = 6,
                //Gray = 7,
                //DarkGray = 8,
                //Blue = 9,
                //Green = 10,
                //Cyan = 11,
                //Red = 12,
                //Magenta = 13,
                //Yellow = 14,
                //White = 15
            };

            //Func<string> button1 = () => { 
            //    WriteInColor("[1]", ConsoleColor.Yellow); 
            //    return string.Empty;
            //};
            //Action button2 = () => WriteInColor("[2]", ConsoleColor.Yellow);
            //Action button3 = () => WriteInColor("[3]", ConsoleColor.Yellow);
            //Action button4 WriteInColor("[1]", ConsoleColor.Yellow);
            #endregion

            var twister = new Twister();
            Console.WriteLine("Gimme the word/sentence that's supposed to be twisted");
            //Nur Wörter, Sätze mit normalen Zeichen akzeptieren
            var twistedInput = twister.Twist(Console.ReadLine());
            foreach (var twistedWord in twistedInput)
            {
                if(twistedWord.Count() < 4 && twistedInput.Count == 1)
                {
                    WriteInColor($"<Green [{twistedWord}]\\> <Red hasn't more than 3 characters, which means neither twisting nor detwisting makes sense here. Go ahead and restart to try again hitting ur keyboard four times in a row or just exit, gl!\\>");
                    System.Threading.Thread.Sleep(3000);
                    RestartOrExitDialog();
                }
                if (twistedWord.Count() < 4 && twistedInput.Count > 1)
                {
                    WriteInColor($"<Green [{twistedWord}]\\> <Red hasn't more than 3 characters, which means neither twisting nor detwisting makes sense here. We will continue with the next word.\\>");
                    System.Threading.Thread.Sleep(3000);
                    continue;
                }
                Console.Write("Twisted word: ");
                WriteInColor($"<Green [{twistedWord}]\\>");
                if (twistedInput.Count > 1 && twistedWord != twistedInput.Last()) // Muss nach den oberen if's eigentlich nicht mehr auf Länge abgefragt werden aber w.e
                {
                    var weDidSth = 0;
                    while (weDidSth == 0)
                    {
                        WriteInColor($"Press <DarkYellow [1]\\> to show next twisted word, <DarkYellow [2]\\> to try enttwisting, <DarkYellow [3]\\> to restart or <DarkYellow [4]\\> to exit.");
                        var readKey = Console.ReadKey();

                        switch (readKey.Key)
                        {
                            case ConsoleKey.D1:
                                weDidSth++; //sinnlos weil return?
                                break; //soll hier ins nächste twistedWord returnen
                            case ConsoleKey.D2:
                                weDidSth++;
                                var entwistedWordPossibilities = twister.Detwist(twistedWord);
                                switch (entwistedWordPossibilities.Count)
                                {
                                    case 0:
                                        WriteInColor($"Couldn't detwist <Green [{twistedWord}]\\>. Continueing with next word");
                                        break; //soll hier ins nächste twistedWord returnen
                                    case 1:
                                        WriteInColor($"The word was: <Green [{entwistedWordPossibilities.First()}]\\>");
                                        break; //Soll im while loop bleiben um nach nächster Anweisung zu warten, vlt mir var weDidSth?
                                    default:
                                        WriteInColor("The word was one of these:\n");
                                        foreach (var possibleword in entwistedWordPossibilities)
                                            Console.WriteLine(possibleword);
                                        break; //Soll im while loop bleiben um nach nächster Anweisung zu warten, vlt mir var weDidSth?
                                }
                                break;
                            case ConsoleKey.D3:
                                Console.Clear();
                                weDidSth++;
                                StartTwister(); //Hier soll er die momentane Session komplett killen zum restarten --> ok ist doch egal, da es sowieso immer irgendwann mit exit gekilled wird
                                break;
                            case ConsoleKey.D4:
                                Console.Clear();
                                weDidSth++;
                                Environment.Exit(0); //Jo der Exit halt
                                break;
                        }
                    }
                }
                else
                {
                    bool validKey = false;
                    while (!validKey)
                    {
                        WriteInColor("Press <DarkYellow [1]\\> to try enttwisting, <DarkYellow [2]\\> to restart or <DarkYellow [3]\\> to exit.");
                        var readKey = Console.ReadKey();

                        switch (readKey.Key)
                        {
                            case ConsoleKey.D1:
                                Console.Clear();
                                var entwistedWordPossibilities = twister.Detwist(twistedWord);
                                switch (entwistedWordPossibilities.Count)
                                {
                                    case 0:
                                        WriteInColor($"Couldn't detwist <Green [{twistedWord}]\\>.");
                                        RestartOrExitDialog();
                                        return;
                                    case 1:
                                        WriteInColor($"The word was: <Green [{entwistedWordPossibilities.First()}]\\>");
                                        RestartOrExitDialog();
                                        return;
                                    default:
                                        WriteInColor("The word was one of these:\n");
                                        foreach (var possibleword in entwistedWordPossibilities)
                                            Console.WriteLine(possibleword);
                                        RestartOrExitDialog();
                                        return;
                                }
                            case ConsoleKey.D2:
                                Console.Clear();
                                StartTwister();
                                break;
                            case ConsoleKey.D3:
                                Console.Clear();
                                Environment.Exit(0);
                                break;
                        }
                    }
                }
            }
        }
        private void WriteInColor(string output)
        {
            string endSymbol = @"\>";
            int startIndexOfStartColoredOutputString = output.IndexOf('<');
            if (startIndexOfStartColoredOutputString == - 1)
                Console.WriteLine(output);
            else
            {
                Console.Write(output.Substring(0, startIndexOfStartColoredOutputString));
                int endIndexOfColor = output.IndexOf(" ", startIndexOfStartColoredOutputString) - 1;
                var color = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), output.Substring(startIndexOfStartColoredOutputString + 1, endIndexOfColor - startIndexOfStartColoredOutputString + 1));
                Console.ForegroundColor = color;
                int endIndexOfColoredOutputString = output.IndexOf(endSymbol) - 1;
                Console.Write(output.Substring(endIndexOfColor + 2, endIndexOfColoredOutputString - endIndexOfColor - 1));
                Console.ForegroundColor = ConsoleColor.White;
                WriteInColor(output.Substring(endIndexOfColoredOutputString + 3));
            }
        }
        private void RestartOrExitDialog()
        {
            while (true)
            {
                WriteInColor("Press <DarkYellow [1]\\> to restart or <DarkYellow [2]\\> to exit.");
                var readKey = Console.ReadKey();

                switch (readKey.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        StartTwister();
                        return;
                    case ConsoleKey.D2:
                        Console.Clear();
                        Environment.Exit(0);
                        return;
                }
            }
        }
    }
}
