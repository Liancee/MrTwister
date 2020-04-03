using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrTwister
{
    class Twister
    {
        private static readonly string[] ListOfWords = File.ReadAllLines(@"E:\vsProjects\MrTwister\woerterliste.txt");

        public List<string> Twist(string inputString)
        {
            var returnList = new List<string>();
            var wordList = inputString.Split(' ').ToList();
            foreach (var input in wordList)
            {
                if (input.Length > 3)
                {
                    var result = Twisting(input);
                    while (input == result)
                        result = Twisting(input);
                    returnList.Add(result); //Theoretisch kann der input jetzt nie der output sein
                }
                else returnList.Add(input);
            }
            return returnList;
        }
        private string Twisting(string input)
        {
            var rnd = new Random();
            var word = input.ToList();
            string returnWord = word.First().ToString();
            word.Remove(word.First());
            var lastLetter = word.Last();
            word.Remove(word.Last());
            for (var i = 0; i < word.Count; i = 0)
            {
                var letter = word[rnd.Next(word.Count)];
                returnWord += letter;
                word.Remove(letter);
            }
            return returnWord += lastLetter;
            
        }
        public List<string> Detwist(string twistedWord)
        {
            if (twistedWord.Length > 3)
            {
                var filteredList = ListOfWords.Where(x => x.StartsWith(twistedWord.First().ToString()) && x.EndsWith(twistedWord.Last().ToString()) && x.Length == twistedWord.Length).ToList();
                var result = new List<string>();
                foreach (var word in filteredList)
                {
                    var wordcopy = word.Substring(1, word.Length - 2); //derzeitiges Wort aus der Liste ohne Anfangs,- Endbuchstaben
                    var twistedWordcopy = twistedWord.Substring(1, twistedWord.Length - 2); //derzeitiges verdrehtes Wort ohne Anfangs,- Endbuchstaben
                    if (string.Join("", wordcopy.OrderBy(c => c)).Equals(string.Join("", twistedWordcopy.OrderBy(c => c))))
                        result.Add(word);

                    //for (int i = 1; i < twistedWord.Length; i++)
                    //{
                    //    if (wordcopy.Contains(twistedWord[i]))

                        //        wordcopy = wordcopy.Remove(wordcopy.LastIndexOf(twistedWord[i]));
                        //    wordcopy.re
                        //}
                        //if (wordcopy.Length == 0) result.Add(word);
                }
                return result;
            }
            return new List<string>() { "Makes absolutely no sense to enttwist a word with less than 4 letters." }; //macht das Sinn? Wird das benutzt?
        }
    }
}
