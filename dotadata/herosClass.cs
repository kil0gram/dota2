using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchNameSpace
{
    public class herosClass
    {
        public class Hero
        {
            public string name { get; set; }
            public int id { get; set; }
        }

        public class Result
        {
            public List<Hero> heroes { get; set; }
            public int count { get; set; }
        }

        public class HeroesObject
        {
            public Result result { get; set; }
        }
    }
}
