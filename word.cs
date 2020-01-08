using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppCSharp
{
    class Word
    {
        public string name;
        public int[] letterArray;
        public int distinctLetterCount;
        public int score;

        public Word(string input)
        {
            name = input;
            letterArray = CreateLetterArray(name);
            distinctLetterCount = DistinctLetterCount(letterArray);
            if (name.Length == 4)
                score = 1;
            if (name.Length > 4)
                score = name.Length;
            if (distinctLetterCount == 7) //pangram bonus
                score += 7;
            putScoreinLetterArray();//this is for central letter considerations later, making sure that we pick the one with the highest score
        }
        private void putScoreinLetterArray()
        {
            for (int x = 0; x < letterArray.Length; x++)
            {
                if (letterArray[x] != 0)
                {
                    letterArray[x] = score;
                }
            }
        }
        public bool filterWord(int[] input)//this function verifies that the input array does not contain letters where the word object does not
        {
            for(int x = 0; x < letterArray.Length; x++)
            {
                if(letterArray[x] == 0 && input[x] != 0)
                {
                    return false;
                }
            }
            return true;
        }
        private int[] CreateLetterArray(string input)
        {
            int[] alphabet = new int[26];

            foreach (char c in input.ToCharArray())
            {
                alphabet[c - 97] = 1;//making a zero based index off of ascii, turns out chars are ints.
            }

            return alphabet;
        }
        private int DistinctLetterCount(int[] input)
        {
            int sum = 0;
            foreach (int i in input)
            {
                sum += i;
            }
            return sum;
        }
    }
}
