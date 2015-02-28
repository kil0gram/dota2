using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotadata.Model
{
    public class LobbyTypes
    {
        /// <summary>Converts the input int into a recognizable name
        /// -1 : Invalid
        /// 0 : Public Matching Making
        /// 1 : Practice
        /// 2 : Tournament
        /// 3 : Tutorial
        /// 4 : Co-Op with bots
        /// 5 : Team Match
        /// 6 : Solo Queue
        /// 7 : Ranked Public Matchmaking
        /// 8 : 1v1. 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
        public static string GetLobbyType(int lobbytypeInt)
        {
            switch (lobbytypeInt)
            {
                case -1:
                    return "Invalid";
                case 0:
                    return "Public Matchmaking";
                    break;
                case 1:
                    return"Practice";
                    break;
                case 2:
                    return "Tournament";
                    break;
                case 3:
                    return "Tutorial";
                    break;
                case 4:
                    return "Co-Op with Bots";
                    break;
                case 5:
                    return "Team Match";
                    break;
                case 6:
                    return "Solo Queue";
                case 7:
                    return "Ranked Public Matchmaking";
                case 8:
                    return "1v1 Practice Matchmaking";
                default:
                    return "Invalid Match Type";
                    break;
            }
        }
    }
}
