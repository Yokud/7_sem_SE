using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomNumbers
{
    internal class LCGenerator
    {
        int curElem = 1;
        int a, c, m;

        public LCGenerator(int a, int c, int m) 
        {
            this.a = a;
            this.c = c;
            this.m = m;
        }

        public int Seed
        {
            get => curElem;
            set => curElem = value;
        }

        public IEnumerable<int> GetRandomSequence(int count, int requiredDigits)
        {
            List<int> res = new List<int>();

            int requiredDigitsDivider = (int)Math.Pow(10, requiredDigits);
            int minAppendValue = requiredDigitsDivider / 10 - 1;
            int addedElements = 0;

            for (int i = 0; i < count; i++)
            {
                curElem = (curElem * a + c) % m;

                if (curElem % requiredDigitsDivider >= minAppendValue)
                    res.Add(curElem);
                else
                    addedElements--;
            }

            return res;
        }
    }
}
