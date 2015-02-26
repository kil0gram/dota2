using dotadata.Helpers;
//using MatchNameSpace;
using DotaMatchHistory;
using Heroes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace dotadata
{
    class Program
    {
        public static string API = "23CEC905617913D3710DC832621110F3";
        
        static void Main(string[] args)
        {
            string apiKey = "23CEC905617913D3710DC832621110F3";
            string matchhistoryUrl = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=";
            string herosUrl = @"https://api.steampowered.com/IEconDOTA2_570/GetHeroes/v0001/?key=";

            List<Match> matchHistory = new List<Match>();
            List<HeroesClass> heroesClassdota = new List<HeroesClass>();

            var heros = GetHeroes(herosUrl, API, heroesClassdota);
            matchHistory = GetMatchHistory(matchhistoryUrl, apiKey, heros);

            Match dota = new Match();
            
            
        }

        public static string ConvertHeroFromID(int id,HeroesClass.HeroesObject heroes)
        {
            string heronamestr = string.Empty;
            foreach(var hero in heroes.result.heroes)
            {
                if(hero.id == id)
                {
                    heronamestr = StringManipulation.UppercaseFirst(hero.name);
                    return heronamestr;
                    break;
                }
            }
            return heronamestr;

        }

        public static List<Match> GetMatchHistory(string uri, string api, HeroesClass.HeroesObject heroes)
        {
            //create a container to store all of matches with everything
            //cleaned up
            //I create it up here because if If I hit a exception
            //I wanted to return the object with whatever it has
            List<Match> _matches = new List<Match>();
            try
            {
                
                var response = string.Empty;
                
                //we format our url to include our api key
                //Uri completeUri = new Uri(string.Format("{0}{1}", uri, API));

                //I'm reading up on how to stylize code, I think
                //this below method of creating a Uri is preferred
                //over the string.Format that I originall used
                Uri getmatchUri = new Uri(uri + api);

                //client used to download the json response
                using (WebClient client = new WebClient())
                {
                    //downloading the json response
                    response = client.DownloadString(getmatchUri);
                } 


                //serializing json data to our class
                //this is when we parse all of the json data into
                //our custom object classes
                MatchRootObject ourResponse = JsonConvert.DeserializeObject<MatchRootObject>(response);
                Console.WriteLine("Total Matches {0}", ourResponse.result.matches.Count.ToString());
                int matchcountInt = 0;
                foreach(var match in ourResponse.result.matches)
                {
                    Console.WriteLine("Match {0} of {1}", matchcountInt, ourResponse.result.matches.Count);
                    //start looking up details on first match
                    //I created this object to store the details for this particular match
                    Match _match = new Match();
                    
                    Console.WriteLine("Match ID: {0}", match.match_id);
                    Console.WriteLine("Lobby Type: {0}", match.lobby_type);
                    Console.WriteLine("MatchSeqNum: {0}", match.match_seq_num);
                    Console.WriteLine("Start Time: {0}", match.start_time);

                    _match.match_id = match.match_id;
                    _match.lobby_type = match.lobby_type;
                    _match.match_seq_num = match.match_seq_num;
                    _match.players = match.players;
                    _match.start_time = match.start_time;

                    //looping through each player in the match
                    Console.WriteLine("Real Players: {0}", match.players.Count.ToString());
                    int playercountInt = 0;
                    foreach(var player in match.players)
                    {
                        Console.WriteLine("Hero ID: {0}", player.hero_id);
                        string name = ConvertHeroFromID(player.hero_id, heroes);
                        Console.WriteLine("*************");
                        Console.WriteLine("Player {0} of {1}", playercountInt);
                        Console.WriteLine("*************");

                        //This was my original way of splitting the text
                        //string splitword = "npc_dota_hero_";
                        //var splitName = StringManipulation.SplitTextByWord(name, splitword);
                        //string cleanName = splitName[1].Replace("_", " ");
                        
                        //I think this is a good way of cleaning up the name?
                        string cleanName =  StringManipulation.UppercaseFirst(name.Split('_')[3]);
                        Console.WriteLine("Name: {0}", cleanName);
                        Console.WriteLine("orig-name: {0}", name);
                        Console.WriteLine("Hero ID: {0}", player.hero_id);
                        Console.WriteLine("Account ID: {0}", player.account_id);
                        Console.WriteLine("=======================================");
                    }

                    _matches.Add(_match);
                    matchcountInt++;
                }
                return _matches;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return _matches;
             
            }
        }

        public static HeroesClass.HeroesObject GetHeroes(string uri, string api,List<HeroesClass> heroes)
        {
            try
            {
                var response = string.Empty;
                //Uri completeUri = new Uri(string.Format("{0}{1}", uri, api));
                //I'm reading up on how to stylize code, I think
                //this below method of creating a Uri is preferred
                //over the string.Format that I originall used
                Uri getmatchUri = new Uri(uri + api);
                using (WebClient client = new WebClient())
                {
                    response = client.DownloadString(getmatchUri);
                }
                
                HeroesClass.HeroesObject ourResponse = JsonConvert.DeserializeObject<HeroesClass.HeroesObject>(response);

                //we clean up the names and stuff here
                List<HeroesClass.Hero> Heroes = new List<HeroesClass.Hero>();

                Console.WriteLine("Count of Heroes {0}", ourResponse.result.heroes.Count);
                int herocountInt = 0;
                foreach (var hero in ourResponse.result.heroes)
                {
                    
                    Console.WriteLine("Hero {0} of {1}", herocountInt, ourResponse.result.heroes.Count);
                    HeroesClass.Hero Hero = new HeroesClass.Hero();
                    Hero.name = StringManipulation.UppercaseFirst(hero.name);
                    Console.WriteLine("{0}", Hero.name);
                    Heroes.Add(Hero);
                    herocountInt++;
                }
                return ourResponse;
                
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
