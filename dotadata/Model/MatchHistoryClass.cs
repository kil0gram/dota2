﻿using dotadata.Helpers;
using dotadata.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotadata.Model
{
    public class MatchHistory
    {
        /// <summary>Gets the latest (upto) 100 matches (so far, atleast for me during time of writing this 02-28-15). 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
        public static List<Match> GetMatchHistory()
        {
            //to do
            //create a player class to hold more information regarding the individual 
            //players including abilites/build info

            //we get a list of the latest heroes
            List<Heroes.Hero> heroes = Heroes.GetHeroes(false);

            //create a container to store all of matches with everything
            //cleaned up
            //I create it up here because if If I hit a exception
            //I wanted to return the object with whatever it has
            List<Match> _matches = new List<Match>();

            //download the resposne
            string response = GetWebResponse.DownloadSteamAPIString(Common.matchhistoryUrl, Common.API);


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
                    string name = Common.ConvertHeroFromID(player.hero_id, heroes);
                    player.name = name;
                    player.steamvanityname = SteamAccount.GetSteamAccount(player.account_id).personaname;

                    Console.WriteLine("             Name: {0}", name);
                    Console.WriteLine("             Hero ID: {0}", player.hero_id);
                    Console.WriteLine("             Account ID: {0}", player.account_id);
                    Console.WriteLine("             Vanity Name: {0}", player.steamvanityname);

                    playercountInt++;
                }

                Console.WriteLine("**************************");
                _matches.Add(_match);
                matchcountInt++;
            }
            return _matches;


        }

        /// <summary>Used to get the matches in the order which they were recorded (i.e. sorted ascending by match_seq_num).
        /// This means that the first match on the first page of results returned by the call will be the very first public mm-match recorded in the stats. 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
        public static List<dotadata.Model.MatchDetails.MatchDetailsResult> GetMatchHistoryBySeqNum(int matchseqnumb, int requestedmatches, List<ItemsClass.Item> DotaItems)
        {
            //to do
            //create a player class to hold more information regarding the individual 
            //players including abilites/build info

            //we get a list of the latest heroes
            List<Heroes.Hero> heroes = Heroes.GetHeroes(false);

            //create a container to store all of matches with everything
            //cleaned up
            //I create it up here because if If I hit a exception
            //I wanted to return the object with whatever it has

            List<MatchDetails.MatchDetailsResult> matchlist = new List<MatchDetails.MatchDetailsResult>();

            //download the resposne
            string response = GetWebResponse.DownloadSteamAPIString(Common.matchhistorybyseqUrl, Common.API + "&start_at_match_seq_num=" + matchseqnumb + "&matches_requested=" + requestedmatches);


            //serializing json data to our class
            //this is when we parse all of the json data into
            //our custom object classes
            MatchRootObject ourResponse = JsonConvert.DeserializeObject<MatchRootObject>(response);
            foreach (var match in ourResponse.result.matches)
            {
                var m = MatchDetails.GetMatchDetail(match.match_id, DotaItems);
                matchlist.Add(m);
            }
            return matchlist;

        }
    }

    public class Player
    {
        public string account_id { get; set; }
        public int player_slot { get; set; }
        public int hero_id { get; set; }
        public string name { get; set; }
        public string steamvanityname { get; set; }
    }

    public class Match
    {
        public int match_id { get; set; }
        public int match_seq_num { get; set; }
        public int start_time { get; set; }
        public DateTime StartTime { get; set; }
        public int lobby_type { get; set; }
        public string LobbyType { get; set; }
        public List<Player> players { get; set; }
        public int humanplayers { get; set; }
    }

    public class Result
    {
        public int status { get; set; }
        public int num_results { get; set; }
        public int total_results { get; set; }
        public int results_remaining { get; set; }
        public List<Match> matches { get; set; }
    }

    public class MatchRootObject
    {
        public Result result { get; set; }
    }

}
