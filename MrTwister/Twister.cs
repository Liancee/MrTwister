using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MrTwister
{
    class Twister
    {
        string[] ListOfWords = null;
        public Twister()
        {
            ListOfWords = woerterliste.Woerterliste.Split('\n');
        }
        public List<string> Twist(List<string> wordList)
        {
            var returnList = new List<string>();
            foreach (var input in wordList)
            {
                if (input.Length > 3 && Auxiliary_methods.ContainsDifferentLetters(input)) //Erst ab 4 Buchstaben macht das Twisten Sinn und da unten geguckt wird ob der input gleich dem result ist, müssen Fälle ausgeschlossen werden wo immer das gleiche entsteht, Bsp.: "dwwwwd" kann getwistet auch immer nur dwwwwd sein...
                {
                    var result = Twisting(input);
                    while (input == result) //Solange twisten bis was unterschiedliches rauskommt, Endlosschleife hoffentlich nicht mehr möglich dank if Abfrage darüber ("dwwwwd" Beispiel)
                        result = Twisting(input);
                    returnList.Add(result); //Theoretisch kann der input jetzt nie der output sein
                }
                else returnList.Add(input);
            }
            return returnList;
        }
        /// <summary>
        /// Returns the input string in a new random order, except for the first and last letter.
        /// <see cref="Random"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string Twisting(string input)
        {
            var rnd = new Random(); //Eine Objekt der Random Klasse wird instanziiert, um später für die zufällige Anordnung verwendet zu werden
            var word = input.ToList(); //Hereinkommendes Wort wird zu einer Liste um die Remove Methode für bestimmte Charakter benutzen zu können
            string returnWord = word.First().ToString(); //Returnvalue wird schonmal der Anfangsbuchstabe hinzugefügt, da dieser nicht betroffen sein wird
            word.Remove(word.First()); //Erster Buchstabe wird entfernt, damit mein Wortpool korrekt ist
            var lastLetter = word.Last(); //Letzter Buchstabe wird vermerkt, damit er am Ende dem Returnvalue hinzugefügt werden kann
            word.Remove(word.Last()); //Letzter Buchstabe wird entfernt, damit der Wortpool mit zu vertauschenden Buchstaben korrekt ist
            for (var i = 0; i < word.Count; i = 0) //Ist eigentlich nur eine ziemlich schlechte While SChleife (while (word.Count > 0) aber irgendwie fand ich die Idee witzig i immer wieder 0 zu setzen
            {
                var letter = word[rnd.Next(word.Count)]; //Wählt aus dem Wortpool einen zufälligen Buchstaben aus,
                returnWord += letter; //packt diesen an das Returnvalue
                word.Remove(letter); //und löscht ihn dann aus dem Wortpool
            } //Springt dann in den nächsten Schleifendurchlauf, bis nur noch ein Buchstabe da ist
            return returnWord += lastLetter; //Fügt den letzten Buchstaben wieder ran und et voilà das Wort ist getwistet
        }
        public List<string> Detwist(string input)
        {
            string twistedWord = input.ToUpper(); //Der input wird in Großbuchstaben zwischengespeichert, damit case-insensitive nach dem Wort geprüft werden kann

            //Die Liste mit Wörter gegen die geprüft wird, wird nach Anfangs,- Endbuchstabe und Länge, des getwisteten Wortes gefiltert und Zwischengespeichert
            var filteredList = ListOfWords.Where(x => x.ToUpper().StartsWith(twistedWord.First().ToString()) && x.ToUpper().EndsWith(twistedWord.Last().ToString()) && x.Length == twistedWord.Length).ToList();
            var result = new List<string>();
            foreach (var word in filteredList)
            {
                //Für jedes Wort in der gefilterten Liste wird von a nach z sortiert geguckt ob sie gleich sind, wenn ja, ist es eine gültige Permutation und wird der Resultliste hinzugefügt
                if (Enumerable.SequenceEqual(word.ToUpper().OrderBy(c => c), twistedWord.ToUpper().OrderBy(c => c)))
                    result.Add(word);
            }
            return result.Distinct().ToList(); //Da Woerter (Bsp.: weiterer) in der Liste gegen die geprüft wird mehrmals vorkommen (können), wird nochmal Distinct benutzt
        }
    }
}
