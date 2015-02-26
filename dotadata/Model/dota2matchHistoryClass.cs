using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaMatchHistory
{
    public class Player
    {
        public object account_id { get; set; }
        public int player_slot { get; set; }
        public int hero_id { get; set; }
    }

    public class Match
    {
        public int match_id { get; set; }
        public int match_seq_num { get; set; }
        public int start_time { get; set; }
        public int lobby_type { get; set; }
        public List<Player> players { get; set; }
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
