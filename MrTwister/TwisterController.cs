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

            var twister = new Twister();
            Console.WriteLine("Gimme the word/sentence that's supposed to be twisted");
            //Nur Wörter, Sätze mit normalen Zeichen akzeptieren
            var twistedInput = twister.Twist(Console.ReadLine());
            foreach (var twistedWord in twistedInput)
            {
                //EnttwisterCounter = 0;
                //Attempted.Clear();
                if(twistedWord.Count() < 4 && twistedInput.Count == 1)
                {
                    //WriteInColor(,$"[{twistedWord}]" ConsoleColor.Green);
                    WriteInColor($"<Green [{twistedWord}]\\> <Red hasn't more than 3 characters, which means neither twisting nor detwisting makes sense here. Go ahead and restart to try again hitting ur keyboard four times in a row or just exit, gl!\\>");
                    System.Threading.Thread.Sleep(3000);
                    RestartOrExitDialog();
                }
                if (twistedWord.Count() < 4 && twistedInput.Count > 1)
                {
                    //WriteInColor($"[{twistedWord}]", ConsoleColor.Green);
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
                        WriteInColor($"Press <DarkYellow [1]\\> to show next twisted word, <DarkYellow [2]\\> to try enttwisting, <Yellow [3]\\> to restart or <Yellow [4]\\> to exit.");
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
                                        Console.WriteLine($"Couldn't detwist <Green [{twistedWord}]\\>. Continueing with next word");
                                        break; //soll hier ins nächste twistedWord returnen
                                    case 1:
                                        Console.WriteLine($"The word was: <Green [{entwistedWordPossibilities.First()}]\\>");
                                        break; //Soll im while loop bleiben um nach nächster Anweisung zu warten, vlt mir var weDidSth?
                                    default:
                                        Console.WriteLine("The word was one of these:\n");
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
                        Console.WriteLine("Press <DarkYellow [1]\\> to try enttwisting, <Yellow [2]\\> to restart or <Yellow [3]\\> to exit.");
                        var readKey = Console.ReadKey();

                        switch (readKey.Key)
                        {
                            case ConsoleKey.D1:
                                Console.Clear();
                                var entwistedWordPossibilities = twister.Detwist(twistedWord);
                                switch (entwistedWordPossibilities.Count)
                                {
                                    case 0:
                                        Console.WriteLine($"Couldn't detwist <Green [{twistedWord}]\\>.");
                                        // Muss hier in die restart/exit switch springen
                                        RestartOrExitDialog();
                                        return;
                                    case 1:
                                        Console.WriteLine($"The word was: <Green [{entwistedWordPossibilities.First()}]\\>");
                                        // Muss hier in die restart/exit switch springen
                                        RestartOrExitDialog();
                                        return;
                                    default:
                                        Console.WriteLine("The word was one of these:\n");
                                        foreach (var possibleword in entwistedWordPossibilities)
                                            Console.WriteLine(possibleword);
                                        // Muss hier in die restart/exit switch springen
                                        RestartOrExitDialog();
                                        return;
                                }
                                //if (entwistedWordPossibilities.Count < 2)
                                //{
                                //    Console.WriteLine(entwistedWordPossibilities.First());

                                //    while (true)
                                //    {
                                //        Console.WriteLine("Press [1] to restart or [2] to exit.");
                                //        readKey = Console.ReadKey();

                                //        switch (readKey.Key)
                                //        {
                                //            case ConsoleKey.D1:
                                //                Console.Clear();
                                //                StartTwister();
                                //                break;
                                //            case ConsoleKey.D2:
                                //                Console.Clear();
                                //                Environment.Exit(0);
                                //                break;
                                //        }
                                //    }
                                //}
                                //Console.WriteLine("The word was one of these:\n");
                                //foreach (var possibleword in entwistedWordPossibilities)
                                //    Console.WriteLine(possibleword);
                                //while (true)
                                //{
                                //    Console.WriteLine("Press [1] to restart or [2] to exit.");
                                //    readKey = Console.ReadKey();

                                //    switch (readKey.Key)
                                //    {
                                //        case ConsoleKey.D1:
                                //            Console.Clear();
                                //            StartTwister();
                                //            break;
                                //        case ConsoleKey.D2:
                                //            Console.Clear();
                                //            Environment.Exit(0);
                                //            break;
                                //    }
                                //}
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

                    //Console.WriteLine("Press [1] to try enttwisting, [2] to restart or [3] to exit.");

                }
                //Console.WriteLine(Enttwister(twistedWord, filteredList));
            }
        }
        private void WriteInColor(string output)
        {
            string endSymbol = @"\>";
            if (output.IndexOf('<') == -1)
                Console.WriteLine(output);
            else
            {
                Console.Write(output.Substring(0, output.IndexOf('<')));
                int endIndexOfColor = output.IndexOf(" ", output.IndexOf('<')) - 1;
                var color = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), output.Substring(output.IndexOf('<') + 1, endIndexOfColor - output.IndexOf('<') + 1));
                //ConsoleColor.TryParse(output.Substring(output.IndexOf('<') + 1, endIndexOfColor), out ConsoleColor color);
                Console.ForegroundColor = color;
                int endIndexOfColoredOutputString = output.IndexOf("\\>") - 1;
                Console.Write(output.Substring(endIndexOfColor + 2, endIndexOfColoredOutputString - endIndexOfColor - 1));
                Console.ForegroundColor = ConsoleColor.White;
                WriteInColor(output.Substring(endIndexOfColoredOutputString + 3));
            }
        }
        private void RestartOrExitDialog()
        {
            while (true)
            {
                Console.WriteLine("Press [1] to restart or [2] to exit.");
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
        //private void NextOrRestartOrExitDialog()
        //{
        //    while (true)
        //    {
        //        Console.WriteLine("Press [1] to show next twisted word, [2] to restart or [3] to exit.");

        //        var readKey = Console.ReadKey();

        //        switch (readKey.Key)
        //        {
        //            case ConsoleKey.D1:

        //            case ConsoleKey.D2:
        //                Console.Clear();
        //                StartTwister();
        //                break;
        //            case ConsoleKey.D3:
        //                Console.Clear();
        //                Environment.Exit(0);
        //                break;
        //        }
        //    }
        //}
    }
}
