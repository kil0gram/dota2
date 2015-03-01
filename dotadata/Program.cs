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
        //my steam API key
        public static string API = "23CEC905617913D3710DC832621110F3";

        //steam urls to get json data
        public static string matchhistoryUrl = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=";
        public static string herosUrl = @"https://api.steampowered.com/IEconDOTA2_570/GetHeroes/v0001/?key=";
        public static string matchdetailsUrl = @"https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/?&key=";
        public static string steamaccountUrl = @"http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=";

        static void Main(string[] args)
        {
            string SteamID = "111348541";
            var steamaccount = SteamAccount.GetSteamAccount(steamaccountUrl, API, SteamID);

            
            //Get list of latest heroes with names parsed/cleaned
            List<Heroes.Hero> heros = Heroes.GetHeroes(herosUrl, API);
            
            //get latest 100 matches with brief details (no hero items/abilities/build info)
            List<Match> latest100matches = MatchHistory.GetMatchHistory(matchhistoryUrl, API, heros);

            //Get match details for match id 1277955116
            MatchDetails.MatchDetailsResult matchdetails = MatchDetails.GetMatchDetail(matchdetailsUrl, API, 1277955116, heros);
            

        }

       



        
    }
}
