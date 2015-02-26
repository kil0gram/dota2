using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotadata.Model
{
    public class LobbyTypes
    {
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
