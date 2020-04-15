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
                if (input.Length > 3)
                {
                    var result = Twisting(input);
                    while (input == result) //dwwwwd gleiche buchstaben in der Mitte rip
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
            var word = input.ToList(); //Ich mache das hereinkommende Wort zu einer Liste um die Remove Methode für bestimmte Charakter benutzen zu können
            string returnWord = word.First().ToString(); //Meiner Returnvalue wird schonmal der Anfangsbuchstabe hinzugefügt, da dieser nicht betroffen sein wird
            word.Remove(word.First()); //Erster Buchstabe wird entfernt, damit mein Wortpool korrekt ist
            var lastLetter = word.Last(); //Der letzte Buchstabe wird vermerkt, damit er am Ende zu dem Returnvalue hinzugefügt werden kann
            word.Remove(word.Last()); //Letzter Buchstabe wird entfernt, damit mein Wortpool mit zu vertauschenden Buchstaben korrekt ist
            for (var i = 0; i < word.Count; i = 0) //Ist eigentlich nur eine ziemlich schlechte While SChleife ich weiß :D (while (word.Count > 0) aber irgendwie fand ich die Idee witzig i immer wieder 0 zu setzen
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
            if (twistedWord.Length > 3) //Das Wort muss mindestens eine Länge von 4 besitzen, da Anfangs,- und Endbuchstabe immer gleich sind
            {
                //Die 
                var filteredList = ListOfWords.Where(x => x.ToUpper().StartsWith(twistedWord.First().ToString()) && x.ToUpper().EndsWith(twistedWord.Last().ToString()) && x.Length == twistedWord.Length).ToList();
                var result = new List<string>();
                foreach (var word in filteredList)
                {
                    //var wordcopy = word.Substring(1, word.Length - 2); //derzeitiges Wort aus der Liste ohne Anfangs,- Endbuchstaben
                    //var twistedWordcopy = twistedWord.Substring(1, twistedWord.Length - 2).ToUpper(); //derzeitiges verdrehtes Wort ohne Anfangs,- Endbuchstaben
                    //if (string.Join("", wordcopy.OrderBy(c => c)).Equals(string.Join("", twistedWordcopy.OrderBy(c => c))))
                    if (Enumerable.SequenceEqual(word.ToUpper().OrderBy(c => c), twistedWord.ToUpper().OrderBy(c => c)))
                        result.Add(word);
                }
                return result;
            }
            return new List<string>() { "Makes absolutely no sense to enttwist a word with less than 4 letters." }; //macht das Sinn? Wird das benutzt? Glaube der Fall tritt nicht mehr auf
        }
    }
}
