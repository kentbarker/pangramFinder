using System;
using System.Collections.Generic;

namespace ConsoleAppCSharp
{
    class Program
    {
        private static Word temp = new Word("aerating");

        static void Main(string[] args)
        {
            List<Word> words = createWords();
            List<Word> candidateWords = findCandidates(words);
            List<Word> wordFamily = new List<Word>();
            List<winners> scoredList = new List<winners>();
            

            pair maxPair = new pair(0,0), currentPair;
            Word maxWord = new Word("untreated");

            int progress = 0;
            //build a list of words from the master list that only contain letters from the candidate word
            foreach (Word candidate in candidateWords)
            {
                foreach(Word word in words)
                {
                    //compare to the candidate word to filter it
                    //if it matches add it to a temp list
                    if (candidate.filterWord(word.letterArray))
                    {
                        wordFamily.Add(word);
                    }
                }
                //central letter considerations andsum up the score for each of those words
                currentPair = findCentralLetter(wordFamily);



                scoredList.Add(new winners(currentPair, candidate));
                //store the current max word score + the word + central letter
                if(currentPair.score > maxPair.score)
                {
                    maxWord = candidate;
                    maxPair = currentPair;

                }
                progress++;
                if (progress % 1000 == 0)
                    Console.WriteLine("current word {0}, current score {1}", candidate.name, currentPair.score);
                 
                wordFamily.Clear();
            }
            Console.WriteLine("max word? {0}, central letter {1}",maxWord.name, maxPair.index);//zero based indexing, a = 0, b = 1, ...
            scoredList.Sort();
            Console.WriteLine("after sort");
            foreach(winners current in scoredList)
            {
                Console.WriteLine("word {0}, score {1}, central letter {2}", current.theWord.name, current.thePair.score, current.thePair.index);
            }
            Console.WriteLine("after write");
            Console.ReadKey();
        }

        static pair findCentralLetter(List<Word> wordFamily)//returns the index in the word array that should be the central letter to maximize score
        {
            int index = 0;
            int maxScore = 0;
            int currentScore = 0;

            for(int x = 0; x < temp.letterArray.Length; x++)
            {
                foreach(Word word in wordFamily)
                {
                    currentScore += word.letterArray[x];//this is either 0 or the score for the whole word
                }
                if(currentScore > maxScore)
                {
                    maxScore = currentScore;
                    index = x;
                }
                currentScore = 0;
            }
            return new pair(index, maxScore);
        }
        static List<Word> findCandidates(List<Word> words)
        {
            List<Word> candidateWords = new List<Word>();

            foreach (Word word in words)
            {
                if (word.distinctLetterCount == 7)
                {
                    candidateWords.Add(word);
                }
            }
            candidateWords.TrimExcess();
            return candidateWords;
        }
        static List<Word> createWords()
        {
            string[] rawWords = System.IO.File.ReadAllLines(@"C:\Users\kebarker.REDMOND\Downloads\WordList.txt");
            List<Word> words = new List<Word>();

            //throw away words less than 4 letters 
            rawWords = Array.FindAll(rawWords, StringLongerThan3);
            //remove words that contain s
            rawWords = Array.FindAll(rawWords, containsS);

            foreach (string current in rawWords)
            {
                words.Add(new Word(current));
            }

            return words;
        }
        static void PrintWords(List<Word> words, string message)
        {
            Console.WriteLine(message);
            int x = 0;
            foreach(Word word in words)
            {
                if (x > 10)
                    return;
                Console.WriteLine(word.name);
                x++;
            }
        }
        static bool containsS(string input) => !input.Contains('s');
        static bool StringLongerThan3(string input) => input.Length > 3;
    }
}
