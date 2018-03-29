using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EAuthentication
    {
        //名称
        public string name { get; set; }
        //token
        public string token { get; set; }
        //Role
        public string Role { get; set; }
        //state
        public bool state { get; set; }
        //上次登录时间
        public string loginTime { get; set; }
    }
}
