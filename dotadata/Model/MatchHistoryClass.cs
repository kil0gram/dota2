using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaMatch
{
    public class Player
    {
        public object account_id { get; set; }
        public int player_slot { get; set; }
        public int hero_id { get; set; }
        public string name { get; set; }
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

    public class MatchDetails
    {
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
