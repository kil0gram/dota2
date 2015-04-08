using dotadata.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotadata.Model
{
    public class MatchDetails
    {

        /// <summary>Gets match details for a single match, this includes player builds and details. Requires "MatchClass". 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
        public static MatchDetails.MatchDetailsResult GetMatchDetail(int matchid, List<ItemsClass.Item> DotaItems)
        {
            //to do
            //get match details
            //uri is: https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?match_id=27110133&key=<key>

            //we get a list of the latest heroes
            List<Heroes.Hero> heroes = Heroes.GetHeroes(false);

            //get list of abilities
            //parsing abilites list and storing
            //in memory.
            var abilitiestext = File.ReadAllLines(Common.abilities_txt_path);
            var abilities = AbilitiesClass.ParseAbilityText(abilitiestext);

            string response = GetWebResponse.DownloadSteamAPIString(Common.matchdetailsUrl, Common.API + "&match_id=" + matchid);

            MatchDetails.MatchDetailsRootObject detail = JsonConvert.DeserializeObject<MatchDetails.MatchDetailsRootObject>(response);
            MatchDetails.MatchDetailsResult match = detail.result;

            match.StartTime = StringManipulation.UnixTimeStampToDateTime(detail.result.start_time);

            Console.WriteLine("Match ID: {0}", match.match_id);
            Console.WriteLine("Match SeqNum: {0}", match.match_seq_num);
            Console.WriteLine("Real Players: {0}", match.human_players);
            Console.WriteLine("Start Time: {0}", match.StartTime);
            match.Lobbytype = LobbyTypes.GetLobbyType(match.lobby_type);

            foreach (var player in detail.result.players)
            {
                Console.WriteLine("Account ID: {0}", player.account_id);
                player.name = Common.ConvertHeroFromID(player.hero_id, heroes);
                player.steamid64 = StringManipulation.SteamIDConverter(player.account_id);
                player.steamid32 = StringManipulation.SteamIDConverter64to32(player.steamid64);

                //getting item names based on the id number
                player.Item0 = Common.ConvertItemIDtoName(player.item_0.ToString(), DotaItems);
                player.Item1 = Common.ConvertItemIDtoName(player.item_1.ToString(), DotaItems).Replace("item ","");
                player.Item2 = Common.ConvertItemIDtoName(player.item_2.ToString(), DotaItems);
                player.Item3 = Common.ConvertItemIDtoName(player.item_3.ToString(), DotaItems);
                player.Item4 = Common.ConvertItemIDtoName(player.item_4.ToString(), DotaItems);
                player.Item5 = Common.ConvertItemIDtoName(player.item_5.ToString(), DotaItems);

                var steamaccount = SteamAccount.GetSteamAccount(player.account_id);
                player.steamvanityname = steamaccount.personaname;
                Console.WriteLine("Vanity Name: {0}", player.steamvanityname);
                Console.WriteLine(" {0}", player.name);
                Console.WriteLine("     K/D/A: {0}/{1}/{2}", player.kills, player.deaths, player.assists);
                Console.WriteLine("     CS: {0}/{1}", player.last_hits, player.denies);
                Console.WriteLine("\tGold");
                Console.WriteLine(" \tGPM: {0}g", player.gold_per_min);
                Console.WriteLine(" \tGoldSpent: {0}g", player.gold_spent);
                Console.WriteLine(" \tEnd of game: {0}g", player.gold);

                Console.WriteLine("Items");
                Console.WriteLine(" Slot 0: {0}", player.Item0);
                Console.WriteLine(" Slot 1: {0}", player.Item1);
                Console.WriteLine(" Slot 2: {0}", player.Item2);
                Console.WriteLine(" Slot 3: {0}", player.Item3);
                Console.WriteLine(" Slot 4: {0}", player.Item4);
                Console.WriteLine(" Slot 5: {0}", player.Item5);

                //ability output
                //In some scenarios a user might play the game
                //but not upgrade any abilities or he/she 
                //might get kicked out before they get a chance.
                //In this type of situation, we must check for null
                //before continuing.
                if (player.ability_upgrades != null)
                {
                    Console.WriteLine("Ability Upgrade Path");
                    foreach (var ability in player.ability_upgrades)
                    {
                        //clean up the object a bit and put
                        //id where it should be.
                        ability.id = ability.ability;

                        //map the id to a readable name.
                        ability.name = Common.ConvertAbilityIDtoName(ability.ability, abilities);

                        //add the upgrade seconds to the original start
                        //time to get the upgrade time.
                        ability.upgradetime = match.StartTime.AddSeconds(ability.time);

                        //output to screen
                        Console.WriteLine(" {0} upgraded at {1} @ {2}", ability.name, ability.level, ability.upgradetime);
                    }
          
                }
                else
                {
                    Console.WriteLine("Ability Upgrade Path");
                    Console.WriteLine(" No abilities data");
                }
                

            }

            return match;


        }

        public class AbilityUpgrade
        {
            public string ability { get; set; }
            public string name { get; set; }
            public int time { get; set; }
            public DateTime upgradetime { get; set; }
            public int level { get; set; }
            public string id { get; set; }
        }

            public class Player
            {
                public string account_id { get; set; }
                public string name { get; set; }
                public string steamvanityname { get; set; }
                public string steamid64 { get; set; }
                public string steamid32 { get; set; }
                public int player_slot { get; set; }
                public int hero_id { get; set; }
                public int item_0 { get; set; }
                public string Item0 { get; set; }
                public string Item1 { get; set; }
                public string Item2 { get; set; }
                public string Item3 { get; set; }
                public string Item4 { get; set; }
                public string Item5 { get; set; }
                public int item_1 { get; set; }
                public int item_2 { get; set; }
                public int item_3 { get; set; }
                public int item_4 { get; set; }
                public int item_5 { get; set; }
                public int kills { get; set; }
                public int deaths { get; set; }
                public int assists { get; set; }
                public int leaver_status { get; set; }
                public int gold { get; set; }
                public int last_hits { get; set; }
                public int denies { get; set; }
                public int gold_per_min { get; set; }
                public int xp_per_min { get; set; }
                public int gold_spent { get; set; }
                public int hero_damage { get; set; }
                public int tower_damage { get; set; }
                public int hero_healing { get; set; }
                public int level { get; set; }
                public List<AbilityUpgrade> ability_upgrades { get; set; }
            }

            public class MatchDetailsResult
            {
                public List<Player> players { get; set; }
                public bool radiant_win { get; set; }
                public int duration { get; set; }
                public int start_time { get; set; }
                public DateTime StartTime { get; set; }
                public int match_id { get; set; }
                public int match_seq_num { get; set; }
                public int tower_status_radiant { get; set; }
                public int tower_status_dire { get; set; }
                public int barracks_status_radiant { get; set; }
                public int barracks_status_dire { get; set; }
                public int cluster { get; set; }
                public int first_blood_time { get; set; }
                public int lobby_type { get; set; }
                public string Lobbytype { get; set; }
                public int human_players { get; set; }
                public int leagueid { get; set; }
                public int positive_votes { get; set; }
                public int negative_votes { get; set; }
                public int game_mode { get; set; }
            }

            public class MatchDetailsRootObject
            {
                public MatchDetailsResult result { get; set; }
            }

    }
}
