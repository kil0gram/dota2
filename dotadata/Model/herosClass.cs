using dotadata.Helpers;
using dotadata.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotadata.Model
{
    public class Heroes
    {

        /// <summary>Gets the latest list of heroes from Steam, requires "HerosClass". 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
        public static List<Heroes.Hero> GetHeroes(string uri, string api)
        {
            string response = string.Empty;
            response = GetWebResponse.DownloadSteamAPIString(uri, api);

            Heroes.HeroesObject ourResponse = JsonConvert.DeserializeObject<HeroesObject>(response);

            //we clean up the names and stuff here
            List<Heroes.Hero> Heroes = new List<Heroes.Hero>();

            Console.WriteLine("Count of Heroes {0}", ourResponse.result.heroes.Count);
            int herocountInt = 1;

            //hero id 0 is private profile i think?
            Heroes.Hero Hero = new Heroes.Hero();
            Hero.id = 0;
            Hero.name = "Npc_dota_hero_Private Profile";
            Console.WriteLine("Hero orig-name: {0}", Hero.name);
            Heroes.Add(Hero);

            foreach (var hero in ourResponse.result.heroes)
            {
                Console.WriteLine("Hero {0} of {1}", herocountInt, ourResponse.result.heroes.Count);
                Console.WriteLine("Hero orig-name: {0}", hero.name);
                Hero = new Heroes.Hero();

                Hero.name = StringManipulation.UppercaseFirst(hero.name.Replace("npc_dota_hero_", "").Replace("_", " "));
                Hero.id = hero.id;
                Hero.origname = hero.name;
                Console.WriteLine("{0}", Hero.name);
                Heroes.Add(Hero);
                herocountInt++;
            }

            return Heroes;

        }

        public class Hero
        {
            public string name { get; set; }
            public string origname { get; set; }
            public int id { get; set; }
        }

        public class HeroesRoot
        {
            public List<Hero> heroes { get; set; }
            public int count { get; set; }
        }

        public class HeroesObject
        {
            public HeroesRoot result { get; set; }
        }
    }
}
