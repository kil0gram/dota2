using dotadata.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotadata.Helpers
{
    public class Common
    {
        /// <summary>Simply loops throw the list of heroes to find the ID given and then returns that heroes information. 
        /// <seealso cref="http://uglyvpn.com/"/>
        /// </summary> 
        public static string ConvertHeroFromID(int id, List<Heroes.Hero> heroes)
        {
            string heronamestr = string.Empty;
            //Console.Write("Finding hero {0}..", id);
            foreach (var hero in heroes)
            {
                if (hero.id == id)
                {
                    heronamestr = StringManipulation.UppercaseFirst(hero.name);
                    return heronamestr;
                }
            }

            return heronamestr;

        }

    }
}
