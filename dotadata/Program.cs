using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using dotadata.Model;
using dotadata.Helpers;


namespace dotadata
{
    class Program
    {
       
        static void Main(string[] args)
        {
            string SteamID = "111348541";
            var steamaccount = SteamAccount.GetSteamAccount(SteamID);
            
            //Get list of latest heroes with names parsed/cleaned
            List<Heroes.Hero> heros = Heroes.GetHeroes();
            
            //get latest 100 matches with brief details (no hero items/abilities/build info)
            List<Match> latest100matches = MatchHistory.GetMatchHistory(heros);

            //Get match details for match id 1277955116
            MatchDetails.MatchDetailsResult matchdetails = MatchDetails.GetMatchDetail(1277955116, heros);
            
        }
       
    }
}
