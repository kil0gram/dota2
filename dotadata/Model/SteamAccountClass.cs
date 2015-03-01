using dotadata.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotadata.Model
{
    /// <summary>SteamAccount user object that holds info like vanity name and avatar links. 
    /// <seealso cref="http://uglyvpn.com/"/>
    /// </summary> 
    public class SteamAccount
    {

        /// <summary>Gets the Steam account details for a particular user ID, requires "dotadata.Model.SteamAccount". 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
        public static SteamAccount.Player GetSteamAccount(string SteamID)
        {
            string response = string.Empty;
            var steamaccount = new SteamAccount.RootObject();
            response = GetWebResponse.DownloadSteamAPIString(Common.steamaccountUrl, (Common.API + "&steamids=" + StringManipulation.SteamIDConverter(SteamID)));

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

        public class Player
        {
            public string steamid { get; set; }
            public int communityvisibilitystate { get; set; }
            public int profilestate { get; set; }
            public string personaname { get; set; }
            public int lastlogoff { get; set; }
            public string profileurl { get; set; }
            public string avatar { get; set; }
            public string avatarmedium { get; set; }
            public string avatarfull { get; set; }
            public int personastate { get; set; }
        }

        public class Response
        {
            public List<Player> players { get; set; }
        }

        public class RootObject
        {
            public Response response { get; set; }
        }
    }
}
