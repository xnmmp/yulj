using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 用于保存和修改Token（GUID）
    /// 在注册的时候保存Token，在登录的时候修改Token
    /// </summary>
    public class TokenTable
    {
        //ID
        public string ID { get; set; }
        //用户姓名
        public string UserName { get; set; }
        //随机生成的Token
        public string GUID { get; set; }
        //登录的时间
        public string loginTime { get; set; }
        //角色
        public string Role { get; set; }
        //备注字段
        public string Remark { get; set; }
    }
}
