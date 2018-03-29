using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    //用户基本信息填写
    public class T_User
    {
        //用户ID
        public string ID { get; set; }
        //用户姓名
        public string name { get; set; }
        //角色
        public int role { get; set; }
        //密码
        public string PWD { get; set; }
        //电话
        public string mobile { get; set; }
        //备注
        public string remark { get; set; }
        //状态
        public int status { get; set; }
        //创建时间
        public DateTime createTime { get; set; }
    }
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
