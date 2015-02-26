using dotadata.Helpers;
//using MatchNameSpace;
using DotaMatchHistory;
using Heroes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using dotadata.Model;

namespace dotadata
{
    class Program
    {
        //my steam API key
        public static string API = "23CEC905617913D3710DC832621110F3";

        //steam urls to get json data
        public static string matchhistoryUrl = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=";
        public static string herosUrl = @"https://api.steampowered.com/IEconDOTA2_570/GetHeroes/v0001/?key=";
        public static string matchdetailsUrl = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?&key=";

        static void Main(string[] args)
        {
            //Get list of latest heroes with names parsed/cleaned
            List<HeroesClass.Hero> heros = GetHeroes(herosUrl, API);

            //get latest 100 matches with brief details (no hero items/abilities/build info)
            List<Match> matchHistory = GetMatchHistory(matchhistoryUrl, API, heros);
            
        }

        public static string ConvertHeroFromID(int id, List<HeroesClass.Hero> heroes)
        {
            string heronamestr = string.Empty;
            //Console.Write("Finding hero {0}..", id);
            foreach(var hero in heroes)
            {
                if(hero.id == id)
                {
                    heronamestr = StringManipulation.UppercaseFirst(hero.name);
                    //Console.WriteLine("..found {0}", heronamestr);
                    return heronamestr;
                }
            }
            
            return heronamestr;

        }

        public static List<Match> GetMatchHistory(string uri, string api, List<HeroesClass.Hero> heroes)
        {
            //to do
            //create a player class to hold more information regarding the individual 
            //players including abilites/build info


            //create a container to store all of matches with everything
            //cleaned up
            //I create it up here because if If I hit a exception
            //I wanted to return the object with whatever it has
            List<Match> _matches = new List<Match>();

            //download the resposne
            string response = GetWebResponse.DownloadSteamAPIString(uri, api);


            //serializing json data to our class
            //this is when we parse all of the json data into
            //our custom object classes
            MatchRootObject ourResponse = JsonConvert.DeserializeObject<MatchRootObject>(response);
            Console.WriteLine("Total Matches {0}", ourResponse.result.matches.Count.ToString());
            int matchcountInt = 0;
            foreach (var match in ourResponse.result.matches)
            {
                Console.WriteLine(" Match {0} of {1}", matchcountInt, ourResponse.result.matches.Count);
                //start looking up details on first match
                //I created this object to store the details for this particular match
                Match _match = new Match();

                Console.WriteLine("     Match ID: {0}", match.match_id);
                Console.WriteLine("     Lobby Type: {0} ({1})", LobbyTypes.GetLobbyType(match.lobby_type), match.lobby_type);
                Console.WriteLine("     MatchSeqNum: {0}", match.match_seq_num);
                

                _match.match_id = match.match_id;
                _match.lobby_type = match.lobby_type;
                _match.LobbyType = LobbyTypes.GetLobbyType(match.lobby_type);
                _match.match_seq_num = match.match_seq_num;
                _match.players = match.players;
                _match.start_time = match.start_time;
                _match.StartTime = StringManipulation.UnixTimeStampToDateTime(match.start_time);
                Console.WriteLine("     Start Time: {0}", _match.StartTime);

                //looping through each player in the match
                Console.WriteLine("     Real Players: {0}", match.players.Count.ToString());
                int playercountInt = 1;
                foreach (var player in match.players)
                {
                    Console.WriteLine("         Player {0} of {1}", playercountInt, match.players.Count);
                    string name = ConvertHeroFromID(player.hero_id, heroes);
                    player.name = name;
                    Console.WriteLine("             Name: {0}", name);
                    Console.WriteLine("             Hero ID: {0}", player.hero_id);
                    Console.WriteLine("             Account ID: {0}", player.account_id);

                    playercountInt++;
                }

                Console.WriteLine("**************************");
                _matches.Add(_match);
                matchcountInt++;
            }
            return _matches;


        }

        

        //public static List<Match> GetMatchDetail(string uri, string api, List<HeroesClass.Hero> heroes)
        //{
        //    //to do
        //    //get match details
        //    //uri is: https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?match_id=27110133&key=<key>
        //string o = uri + api + "&match_id=" + matchid;
        //        string response = GetWebResponse(uri, api);
                
               
        //}
        public static List<HeroesClass.Hero> GetHeroes(string uri, string api)
        {
                string response = string.Empty;
                response = GetWebResponse.DownloadSteamAPIString(uri, api);
                
                HeroesClass.HeroesObject ourResponse = JsonConvert.DeserializeObject<HeroesClass.HeroesObject>(response);

                //we clean up the names and stuff here
                List<HeroesClass.Hero> Heroes = new List<HeroesClass.Hero>();

                Console.WriteLine("Count of Heroes {0}", ourResponse.result.heroes.Count);
                int herocountInt = 1;

            //hero id 0 is private profile i think?
                HeroesClass.Hero Hero = new HeroesClass.Hero();
                Hero.id = 0;
                Hero.name = "Npc_dota_hero_Private Profile";
                Console.WriteLine("Hero orig-name: {0}", Hero.name);
                Heroes.Add(Hero);

                foreach (var hero in ourResponse.result.heroes)
                {
                    Console.WriteLine("Hero {0} of {1}", herocountInt, ourResponse.result.heroes.Count);
                    Console.WriteLine("Hero orig-name: {0}", hero.name);
                    Hero = new HeroesClass.Hero();

                    Hero.name = StringManipulation.UppercaseFirst(hero.name.Replace("npc_dota_hero_","").Replace("_"," "));
                    Hero.id = hero.id;
                    Console.WriteLine("{0}", Hero.name);
                    Heroes.Add(Hero);
                    herocountInt++;
                }
                
                return Heroes;
            
        }

        public void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        
    }
}
