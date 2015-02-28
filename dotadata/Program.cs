using Heroes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using dotadata.Model;
using dotadata.Helpers;
using DotaMatchHistory;

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
        public static string steamaccountUrl = @"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=";

        static void Main(string[] args)
        {
            //string steamid64 = "76561197992765754";
            //string steamid32 = "32500026";
            
            //string convertedid64 = StringManipulation.SteamIDConverter(steamid32);
            //string convertedid32 = StringManipulation.SteamIDConverter(steamid64);


            string SteamID = "111348541";
            var steamaccount = GetSteamAccount(steamaccountUrl, API, SteamID);

            //Get list of latest heroes with names parsed/cleaned
            List<HeroesClass.Hero> heros = GetHeroes(herosUrl, API);

            //Get match details for match id 1277955116
            MatchDetails.MatchDetailsResult matchdetails = GetMatchDetail(matchdetailsUrl, API, 1277955116, heros);

            //get latest 100 matches with brief details (no hero items/abilities/build info)
            List<Match> matchHistory = GetMatchHistory(matchhistoryUrl, API, heros);

        }

        /// <summary>Simply loops throw the list of heroes to find the ID given and then returns that heroes information. 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
        public static string ConvertHeroFromID(int id, List<HeroesClass.Hero> heroes)
        {
            string heronamestr = string.Empty;
            //Console.Write("Finding hero {0}..", id);
            foreach(var hero in heroes)
            {
                if(hero.id == id)
                {
                    heronamestr = StringManipulation.UppercaseFirst(hero.name);
                    return heronamestr;
                }
            }
            
            return heronamestr;

        }

        /// <summary>Gets the latest (upto) 100 matches (so far, atleast for me during time of writing this 02-28-15). 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
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

        /// <summary>Gets match details for a single match, this includes player builds and details. Requires "MatchClass". 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
        public static MatchDetails.MatchDetailsResult GetMatchDetail(string uri, string api, int matchid, List<HeroesClass.Hero> heroes)
        {
            //to do
            //get match details
            //uri is: https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?match_id=27110133&key=<key>
            string o = uri + api + "&match_id=" + matchid;
            string response = GetWebResponse.DownloadSteamAPIString(uri, api + "&match_id=" + matchid);
            
            MatchDetails.MatchDetailsRootObject detail = JsonConvert.DeserializeObject<MatchDetails.MatchDetailsRootObject>(response);
            MatchDetails.MatchDetailsResult match = detail.result;

            match.StartTime = StringManipulation.UnixTimeStampToDateTime(detail.result.start_time);

            Console.WriteLine("Match ID: {0}", match.match_id);
            Console.WriteLine("Match SeqNum: {0}", match.match_seq_num);
            Console.WriteLine("Real Players: {0}", match.human_players);
            Console.WriteLine("Start Time: {0}", match.StartTime);

            foreach (var player in detail.result.players)
            {
                Console.WriteLine("Account ID: {0}", player.account_id);
                player.name = ConvertHeroFromID(player.hero_id, heroes);
                player.steamid64 = StringManipulation.SteamIDConverter(player.account_id);
                player.steamid32 = StringManipulation.SteamIDConverter64to32(player.steamid64);
                
                var steamaccount = GetSteamAccount(steamaccountUrl, API, player.account_id);
                player.steamvanityname = steamaccount.personaname;
                Console.WriteLine("Vanity Name: {0}", player.steamvanityname);

                Console.WriteLine(" {0}", player.name);
                Console.WriteLine("     K/D/A: {0}/{1}/{2}", player.kills, player.deaths, player.assists);
                Console.WriteLine("     CS: {0}/{1}", player.last_hits, player.denies);
                Console.WriteLine("\tGold");
                Console.WriteLine(" \tGPM: {0}g", player.gold_per_min);
                Console.WriteLine(" \tGoldSpent: {0}g", player.gold_spent);
                Console.WriteLine(" \tEnd of game: {0}g", player.gold);
                

            }

            return match;
            
            
        }

        /// <summary>Gets the Steam account details for a particular user ID, requires "SteamAccount". 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
        public static SteamAccount.Player GetSteamAccount(string uri, string api, string SteamID)
        {
            string response = string.Empty;
            var steamaccount = new SteamAccount.RootObject();
            response = GetWebResponse.DownloadSteamAPIString(uri, (api + "&steamids=" + StringManipulation.SteamIDConverter(SteamID)));

            SteamAccount.RootObject ourResponse = JsonConvert.DeserializeObject<SteamAccount.RootObject>(response);
            SteamAccount.Player Player = new SteamAccount.Player();

            if (ourResponse.response.players.Count == 0)
            {
                return Player;
            }
            else
            {
                //only 1 player should return?
                Player = ourResponse.response.players[0];
                return Player;
            }


        }

        /// <summary>Gets the latest list of heroes from Steam, requires "HerosClass". 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
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
                    Hero.origname = hero.name;
                    Console.WriteLine("{0}", Hero.name);
                    Heroes.Add(Hero);
                    herocountInt++;
                }
                
                return Heroes;
            
        }

        
    }
}
