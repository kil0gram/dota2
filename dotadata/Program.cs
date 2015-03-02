using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using dotadata.Model;
using dotadata.Helpers;
using System.IO;


namespace dotadata
{
    class Program
    {
       
        static void Main(string[] args)
        {
            
            //lets parse items.txt and store this data into
            //memory so we can make the associatation
            //for match details
            string[] itemstext = File.ReadAllLines(Common.items_txt_path);
            List<ItemsClass.Item> items = ItemsClass.ParseItemsText(itemstext);

            //Get match details for match id 1277955116.
            MatchDetails.MatchDetailsResult matchdetails = MatchDetails.GetMatchDetail(1277955116,items);

            //Gets details regarding a steam account using
            //account id.
            string SteamID = "111348541";
            var steamaccount = SteamAccount.GetSteamAccount(SteamID);
            
            //Get list of latest heroes with names parsed/cleaned.
            List<Heroes.Hero> heros = Heroes.GetHeroes(true);
            
            //get latest 100 matches with brief details (no hero items/abilities/build info).
            List<Match> latest100matches = MatchHistory.GetMatchHistory();

         
            
        }
       
    }
}
