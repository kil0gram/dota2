using dotadata.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotadata.Helpers
{
    public class Common
    {
        //<summary>This should be the only value that you need to change, obtain a API
        //key from steam and replace below
        //</summary>
        public static string API = "23CEC905617913D3710DC832621110F3";

        //steam urls to get json data
        public static string matchhistoryUrl = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=";
        public static string herosUrl = @"https://api.steampowered.com/IEconDOTA2_570/GetHeroes/v0001/?key=";
        public static string matchdetailsUrl = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?&key=";
        public static string steamaccountUrl = @"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=";

        /// <summary>Simply loops throw the list of heroes to find the ID given and then returns that heroes information. 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
        public static string ConvertHeroFromID(int id, List<Heroes.Hero> heroes)
        {
            string heronamestr = string.Empty;
            //Console.Write("Finding hero {0}..", id);
            foreach (var hero in heroes)
            {
                if (hero.id == id)
                {
                    heronamestr = StringManipulation.UppercaseFirst(hero.name);
                    return heronamestr;
                }
            }

            return heronamestr;

        }

    }
}
