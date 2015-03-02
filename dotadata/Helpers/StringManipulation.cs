using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace dotadata.Helpers
{
    public class StringManipulation
    {
        /// <summary>Split a string by a word. 
        /// <para>Split a string by the input word.</para>
        /// </summary>
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

        /// <summary>Capitalizes only the first character of the word. 
        /// <seealso cref="http://www.dotnetperls.com/uppercase-first-letter"/>
        /// </summary> 
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        /// <summary>UnixTimeStampToDateTime is a part of the Dota 2 helper class. 
        /// <para>Converst UTC time to legable local time.</para>
        /// <seealso cref="https://stackoverflow.com/questions/249760/how-to-convert-unix-timestamp-to-datetime-and-vice-versa"/>
        /// </summary> 
        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>SteamIDConverter will convert the input account id to its opposite. 
        /// <para>If SteamAccountID is a 32bit number, this method will convert it to a 64bit and vice versa. <see cref="System.Console.WriteLine(System.String)"/> for information about output statements.</para>
        /// <seealso cref="https://gist.github.com/almirsarajcic/4664387"/>
        /// </summary> 
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

        /// <summary>SteamIDConverter32to64 will convert a 32bit Steam account ID to a 64bit account ID. 
        /// <para>If SteamAccountID is a 32bit number, this method will convert it to a 64bit.
        /// 32bit account id input = 32500026
        /// 64bit account id output = 76561197992765754</para>
        /// <seealso cref="https://gist.github.com/almirsarajcic/4664387"/>
        /// </summary> 
        public static string SteamIDConverter32to64(string SteamID)
        {
            decimal steamidDec = Convert.ToDecimal(SteamID);
            string converted_id = "765" + (steamidDec + 61197960265728).ToString();
            
            return converted_id;
        }

        /// <summary>SteamIDConverter32to64 will convert a 64bit Steam account ID to a 32bit account ID. 
        /// <para>If SteamAccountID is a 64bit number, this method will convert it to a 32bit.
        /// 32bit account id input = 76561197992765754
        /// 64bit account id output = 32500026</para>
        /// <seealso cref="https://gist.github.com/almirsarajcic/4664387"/>
        /// </summary> 
        public static string SteamIDConverter64to32(string SteamID)
        {
            decimal steamidDec = Convert.ToDecimal(SteamID.Substring(3));
            string converted_id = (steamidDec - 61197960265728).ToString();

            return converted_id;
        }

        /// <summary>Convert our object into json string. 
        /// </summary> 
        public static string ObjectToJson(object itemsobject)
        {
            //take our structured object and convert
            //it over to json format.
            var json = new JavaScriptSerializer();
            var jsonserialized = json.Serialize(itemsobject);

            return jsonserialized.ToString();
        }
    }
}
