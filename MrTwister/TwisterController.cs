using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var wordList = Console.ReadLine().Split(' ').ToList();
            if (wordList == null)
            {
                Console.WriteLine("Go ahead and try hitting your keyboard this time, gl!");
                StartTwister();
            }
            var twistedInput = twister.Twist(wordList);
            var inputSanitizingRegEx = new Regex(@"^[a-zA-Z]+$");
            foreach (var twistedWord in twistedInput)
            {
                if (!inputSanitizingRegEx.IsMatch(twistedWord))
                {
                    if (twistedInput.Count == 1)
                    {
                        if (twistedWord.Length > 3)
                            WriteInColor($"<Red Unfortunately your input\\><Green [{wordList.ElementAt(twistedInput.IndexOf(twistedWord))}]\\> <Red had numbers or other non valid characters, which makes detwisting impossible. At least here is your twisted word\\> <Green [{twistedWord}]\\><Red .\\>");
                        else WriteInColor($"<Red Unfortunately your input\\><Green [{wordList.ElementAt(twistedInput.IndexOf(twistedWord))}]\\> <Red had numbers or other non valid characters, which makes detwisting impossible. Even twisting makes no sense since you did not manage to hit your keyboard at least 4 times.. I guess you have to get along with only your input, I am terribly sorry.\\>");
                        System.Threading.Thread.Sleep(3000);
                        RestartOrExitDialog();
                    }
                    if (twistedWord.Length > 3)
                        WriteInColor($"<Red Unfortunately your input\\><Green [{wordList.ElementAt(twistedInput.IndexOf(twistedWord))}]\\> <Red had numbers or other non valid characters, which makes detwisting impossible. At least here is your twisted word\\> <Green [{twistedWord}]\\><Red . We will continue with the next word.\\>");
                    else WriteInColor($"<Red Unfortunately your input\\><Green [{wordList.ElementAt(twistedInput.IndexOf(twistedWord))}]\\> <Red had numbers or other non valid characters, which makes detwisting impossible. Even twisting makes no sense since you did not manage to hit your keyboard at least 4 times.. I guess you have to get along with only your input, I am terribly sorry. We will continue with the next word.\\>");
                    System.Threading.Thread.Sleep(3000);
                    Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                    continue;
                }
                if (twistedWord.Count() < 4 && twistedInput.Count == 1)
                {
                    WriteInColor($"<Green [{twistedWord}]\\> <Red hasn't more than 3 characters, which means neither twisting nor detwisting makes sense here. Go ahead and restart to try again hitting your keyboard four times in a row or just exit, gl!\\>");
                    System.Threading.Thread.Sleep(3000);
                    RestartOrExitDialog(); 
                }
                if (twistedWord.Count() < 4 && twistedInput.Count > 1)
                {
                    WriteInColor($"<Green [{twistedWord}]\\> <Red hasn't more than 3 characters, which means neither twisting nor detwisting makes sense here. We will continue with the next word.\\>");
                    System.Threading.Thread.Sleep(3000);
                    Console.WriteLine("\n-------------------------------------------------------------------------------\n");
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
                                weDidSth++;
                                Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                                break; //soll hier ins nächste twistedWord returnen
                            case ConsoleKey.D2:
                                weDidSth++;
                                var entwistedWordPossibilities = twister.Detwist(twistedWord);
                                switch (entwistedWordPossibilities.Count)
                                {
                                    case 0:
                                        WriteInColor($"Couldn't detwist <Green [{twistedWord}]\\>.");
                                        System.Threading.Thread.Sleep(1500);
                                        Console.WriteLine("Continueing with next word..");
                                        System.Threading.Thread.Sleep(1500);
                                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                                        break; //soll hier ins nächste twistedWord returnen
                                    case 1:
                                        WriteInColor($"The word was: <Green [{entwistedWordPossibilities.First()}]\\>");
                                        System.Threading.Thread.Sleep(1500);
                                        Console.WriteLine("Continueing with next word..");
                                        System.Threading.Thread.Sleep(1500);
                                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                                        break;
                                    default:
                                        WriteInColor("The word was one of these:\n");
                                        foreach (var possibleword in entwistedWordPossibilities)
                                            Console.WriteLine(possibleword);
                                        System.Threading.Thread.Sleep(1500);
                                        Console.WriteLine("Continueing with next word..");
                                        System.Threading.Thread.Sleep(1500);
                                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                                        break;
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
                                var entwistedWordPossibilities = twister.Detwist(twistedWord);
                                switch (entwistedWordPossibilities.Count)
                                {
                                    case 0:
                                        WriteInColor($"Couldn't detwist <Green [{twistedWord}]\\>.");
                                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                                        RestartOrExitDialog();
                                        return;
                                    case 1:
                                        WriteInColor($"The word was: <Green [{entwistedWordPossibilities.First()}]\\>");
                                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                                        RestartOrExitDialog();
                                        return;
                                    default:
                                        WriteInColor("The word was one of these:\n");
                                        foreach (var possibleword in entwistedWordPossibilities)
                                            Console.WriteLine(possibleword);
                                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
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
