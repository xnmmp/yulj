using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Result
    {
        //返回提示
        public string msg { get; set; }
        //返回状态
        public int state { get; set; }
        //返回数据
        public object data { get; set; }
        public int rows { get; set; }
        public int page { get; set; }
        //登录时返回的token
        public string guid { get; set; }
        //返回总数
        public int count { get; set; }
        //用于返回账号名字
        public object accountAndname { get; set; }
        //用于返回项目信息
        public object Project { get; set; }
        ////用户名称
        //public string name { get; set; }
        ////用户ID
        //public string CRMuserid { get; set; }
        ////父级ID
        //public string UserID { get; set; }
        ////用户角色
        //public string Role { get; set; }
    }
}
