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
        public static string items_txt_path = @"D:\dota\items.txt";
        

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

            foreach (var hero in heroes)
            {
                if (hero.id == id)
                {
                    heronamestr = StringManipulation.UppercaseFirst(hero.name);
                    break;
                }
            }

            return heronamestr;

        }

        /// <summary>Simply loops throw the list of DotaItems to find the ID given and then returns that items name. 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
        public static string ConvertItemIDtoName(string id, List<ItemsClass.Item> DotaItems)
        {
            string itemname = string.Empty;
            foreach (var item in DotaItems)
            {
                if (item.id == id)
                {
                    itemname = StringManipulation.UppercaseFirst(item.name);
                    break;
                }
            }

            return itemname;
        }

    }
}
