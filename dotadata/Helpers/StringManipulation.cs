using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotadata.Helpers
{
    public class StringManipulation
    {
        public static List<string> SplitTextByWord(string text, string splitTerm)
        {
            List<string> splitItems = new List<string>();
            if (string.IsNullOrEmpty(text)) return splitItems;
            if (string.IsNullOrEmpty(splitTerm))
            {
                splitItems.Add(text);
                return splitItems;
            }
            int nextPos = 0;
            int curPos = 0;
            while (nextPos > -1)
            {
                nextPos = text.IndexOf(splitTerm, curPos);
                if (nextPos != -1)
                {
                    splitItems.Add(text.Substring(curPos, nextPos - curPos));
                    curPos = nextPos + splitTerm.Length;
                }
            }
            splitItems.Add(text.Substring(curPos, text.Length - curPos));

            return splitItems;
        }

        public static string UppercaseFirst(string s)
        {
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}
