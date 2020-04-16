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
            Console.WriteLine("Give me the word/sentence that is supposed to be twisted");
            var wordList = Regex.Replace(Console.ReadLine(), @"(\s+)", " ").Split(' ').Where(x => x != string.Empty || x != "").ToList();
            if (wordList.Count == 1 && wordList[0] == string.Empty)
            {
                Auxiliary_methods.WriteInColor("<Red Try again and hit your keyboard this time, gl!\\>\n");
                //alten Twister disposen?
                StartTwister();
            }
            var twistedInput = twister.Twist(wordList);
            var inputSanitizingRegEx = new Regex(@"^[a-zA-Z]+$");
            string twistedWord = string.Empty;
            //foreach (var twistedWord in twistedInput)
            for (int posInTwistedInput = 0; posInTwistedInput < twistedInput.Count; posInTwistedInput++)
            {
                twistedWord = twistedInput[posInTwistedInput];
                if (!inputSanitizingRegEx.IsMatch(twistedWord))
                {
                    if (twistedInput.Count == 1 || posInTwistedInput == twistedInput.Count - 1)
                    {
                        if (twistedWord.Length > 3)
                            Auxiliary_methods.WriteInColor($"<Red Unfortunately your input\\><Green [{wordList.ElementAt(twistedInput.IndexOf(twistedWord))}]\\> <Red had numbers or other non valid characters, which makes detwisting impossible. At least here is your twisted word\\> <Green [{twistedWord}]\\><Red .\\>");
                        else Auxiliary_methods.WriteInColor($"<Red Unfortunately your input\\><Green [{wordList.ElementAt(twistedInput.IndexOf(twistedWord))}]\\> <Red had numbers or other non valid characters, which makes detwisting impossible. Even twisting makes no sense since you did not manage to hit your keyboard at least four times.. I guess you have to get along with only your input, I am terribly sorry.\\>");
                        System.Threading.Thread.Sleep(3000);
                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                        RestartOrExitDialog();
                    }
                    if (twistedWord.Length > 3)
                        Auxiliary_methods.WriteInColor($"<Red Unfortunately your input\\><Green [{wordList.ElementAt(twistedInput.IndexOf(twistedWord))}]\\> <Red had numbers or other non valid characters, which makes detwisting impossible. At least here is your twisted word\\> <Green [{twistedWord}]\\><Red . We will continue with the next word.\\>");
                    else Auxiliary_methods.WriteInColor($"<Red Unfortunately your input\\><Green [{wordList.ElementAt(twistedInput.IndexOf(twistedWord))}]\\> <Red had numbers or other non valid characters, which makes detwisting impossible. Even twisting makes no sense since you did not manage to hit your keyboard at least four times.. I guess you have to get along with only your input, I am terribly sorry. We will continue with the next word.\\>");
                    System.Threading.Thread.Sleep(3000);
                    Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                    continue;
                }

                if (twistedWord.Length < 4)
                {
                    if (posInTwistedInput == twistedInput.Count - 1)
                    {
                        Auxiliary_methods.WriteInColor($"<Green [{twistedWord}]\\> <Red has not more than three characters, which means neither twisting nor detwisting makes sense here. Go ahead and restart to try again hitting your keyboard four times in a row or just exit, gl!\\>\n");
                        System.Threading.Thread.Sleep(3000);
                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                        RestartOrExitDialog();
                    }
                    if (twistedInput.Count > 1)
                    {
                        Auxiliary_methods.WriteInColor($"<Green [{twistedWord}]\\> <Red has not more than three characters, which means neither twisting nor detwisting makes sense here. We will continue with the next word.\\>");
                        System.Threading.Thread.Sleep(3000);
                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                        continue;
                    }
                }

                if (!Auxiliary_methods.ContainsDifferentLetters(twistedWord))
                {
                    if (posInTwistedInput == twistedInput.Count - 1)
                    {
                        Auxiliary_methods.WriteInColor($"<Green [{twistedWord}]\\> <Red since every letter is the same except the first and the last, there is no point in doing anything here. Restart and find some different button this time, gl!\\>\n");
                        System.Threading.Thread.Sleep(3000);
                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                        RestartOrExitDialog();
                    }
                    if (twistedInput.Count > 1)
                    {
                        Auxiliary_methods.WriteInColor($"<Green [{twistedWord}]\\> <Red since every letter is the same except the first and the last, there is no point in doing anything here. We will continue with the next word.\\>");
                        System.Threading.Thread.Sleep(3000);
                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                        continue;
                    }
                }
                
                Console.Write("Twisted word: ");
                Auxiliary_methods.WriteInColor($"<Green [{twistedWord}]\\>");
                if (twistedInput.Count > 1 && posInTwistedInput != twistedInput.Count - 1)
                {
                    Auxiliary_methods.WriteInColor($"Press <DarkYellow [1]\\> to show next twisted word, <DarkYellow [2]\\> to try enttwisting, <DarkYellow [3]\\> to restart or <DarkYellow [4]\\> to exit.");
                    var validKey = false;
                    while (!validKey)
                    {
                        var readKey = Console.ReadKey(true);
                        
                        switch (readKey.Key)
                        {
                            case ConsoleKey.D1:
                                validKey = true;
                                Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                                break; //soll hier ins nächste twistedWord returnen
                            case ConsoleKey.D2:
                                validKey = true;
                                var entwistedWordPossibilities = twister.Detwist(twistedWord);
                                switch (entwistedWordPossibilities.Count)
                                {
                                    case 0:
                                        Auxiliary_methods.WriteInColor($"Couldn't detwist <Green [{twistedWord}]\\>.");
                                        System.Threading.Thread.Sleep(1500);
                                        Console.WriteLine("Continueing with next word..");
                                        System.Threading.Thread.Sleep(1500);
                                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                                        break; //soll hier ins nächste twistedWord returnen
                                    case 1:
                                        Auxiliary_methods.WriteInColor($"The word was: <Green [{entwistedWordPossibilities.First()}]\\>");
                                        System.Threading.Thread.Sleep(1500);
                                        Console.WriteLine("Continueing with next word..");
                                        System.Threading.Thread.Sleep(1500);
                                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                                        break;
                                    default:
                                        Auxiliary_methods.WriteInColor("The word was one of these:\n");
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
                                validKey = true;
                                StartTwister(); //Hier soll er die momentane Session komplett killen zum restarten --> ok ist doch egal, da es sowieso immer irgendwann mit exit gekilled wird
                                break;
                            case ConsoleKey.D4:
                                Console.Clear();
                                validKey = true;
                                Environment.Exit(0); //Jo der Exit halt
                                break;
                        }
                    }
                }
                else //hier kommt er nur her wenn es das letzte Wort der Liste ist und über 3 Buchstaben hat
                {
                    Auxiliary_methods.WriteInColor("Press <DarkYellow [1]\\> to try enttwisting, <DarkYellow [2]\\> to restart or <DarkYellow [3]\\> to exit.");
                    bool validKey = false;
                    while (!validKey)
                    {
                        var readKey = Console.ReadKey(true);
                        
                        switch (readKey.Key)
                        {
                            case ConsoleKey.D1:
                                var entwistedWordPossibilities = twister.Detwist(twistedWord);
                                switch (entwistedWordPossibilities.Count)
                                {
                                    case 0:
                                        Auxiliary_methods.WriteInColor($"Couldn't detwist <Green [{twistedWord}]\\>.");
                                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                                        RestartOrExitDialog();
                                        return;
                                    case 1:
                                        Auxiliary_methods.WriteInColor($"The word was: <Green [{entwistedWordPossibilities.First()}]\\>");
                                        Console.WriteLine("\n-------------------------------------------------------------------------------\n");
                                        RestartOrExitDialog();
                                        return;
                                    default:
                                        Auxiliary_methods.WriteInColor("The word was one of these:\n");
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
        
        private void RestartOrExitDialog()
        {
            Auxiliary_methods.WriteInColor("Press <DarkYellow [1]\\> to restart or <DarkYellow [2]\\> to exit.");
            bool validKey = false;
            while (!validKey)
            {
                var readKey = Console.ReadKey(true);
                
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
