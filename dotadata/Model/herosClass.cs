using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes
{
    public class HeroesClass
    {
        public class Hero
        {
            public string name { get; set; }
            public string origname { get; set; }
            public int id { get; set; }
        }

        public class HeroesRoot
        {
            public List<Hero> heroes { get; set; }
            public int count { get; set; }
        }

        public class HeroesObject
        {
            public HeroesRoot result { get; set; }
        }
    }
}
