using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CRM_Pay
    {
        //用户ID
        public string ID { get; set; }
        //管理员ID
        public string UserID { get; set; }
        //付费类型（A套餐，B套餐。。。。）
        public DateTime comboName { get; set; }
        //付费类型名称（超级管理员自取 一年，两年。。。）
        public string comboTimeName { get; set; }
        //付费后能使用的时间（按天计算 1，2，4，5，6，7，8，9。。。。）
        public int combodatatime { get; set; }
        //套餐金额
        public string money { get; set; }
        //到期时间
        public DateTime endtime { get; set; }
        //备注字段
        public string remark1 { get; set; }
        //备注字段
        public string remark2 { get; set; }
    }
}
