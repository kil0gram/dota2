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
        {//http://www.dotnetperls.com/uppercase-first-letter
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

     

        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {//https://stackoverflow.com/questions/249760/how-to-convert-unix-timestamp-to-datetime-and-vice-versa
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static string SteamIDConverter(string SteamAccountID)
        {//Found a cool way in PHP to convert steam ID so I adopted the method, credit
            //goes to original author, page here: https://gist.github.com/almirsarajcic/4664387

            //this is our converted id object which will be returned
            string converted_id = string.Empty;

            //if the length of the SteamID is 17 characters
            //we will assume it is a 64 id
            if (SteamAccountID.Length == 17)
            {
                //we remove the prefix of 765
                //then do the math
                decimal steamidDec = Convert.ToDecimal(SteamAccountID.Substring(3));
                converted_id = (steamidDec - 61197960265728).ToString();
            }
            else
            {
                decimal steamidDec = Convert.ToDecimal(SteamAccountID);
                converted_id = "765" + (steamidDec + 61197960265728).ToString();
            }

            return converted_id;
        }

        public static string SteamIDConverter32to64(string SteamID)
        {
            decimal steamidDec = Convert.ToDecimal(SteamID);
            string converted_id = "765" + (steamidDec + 61197960265728).ToString();
            
            return converted_id;
        }
        public static string SteamIDConverter64to32(string SteamID)
        {
            decimal steamidDec = Convert.ToDecimal(SteamID.Substring(3));
            string converted_id = (steamidDec - 61197960265728).ToString();

            return converted_id;
        }
    }
}
