using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 省市区
    /// </summary>
  public  class Adresskind
    {
        public class cities
        {
            public int id { get; set; }
            public string cityid { get; set; }
            public string city { get; set; }
            public string provinceid { get; set; }
        }
        public class provinces
        {

            public int id { get; set; }
            public string provinceid { get; set; }
            public string province { get; set; }
        }
        public class areass
        {
            public int id { get; set; }
            public string areaid { get; set; }
            public string area { get; set; }
            public string cityid { get; set; }
        }

    }
}
