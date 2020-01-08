using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppCSharp
{
    class winners : IComparable<winners>
    {
        public pair thePair;
        public Word theWord;
        public winners(pair p, Word w)
        {
            thePair = p;
            theWord = w;
        }

        public int CompareTo(winners other)
        {
            return this.thePair.score.CompareTo(other.thePair.score);
        }
    }
}
