using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CRM_trial
    {
        //用户ID
        public string ID { get; set; }
        //管理员ID
        public string UserID { get; set; }
        //到期时间
        public DateTime endTime { get; set; }
        //备注字段
        public string remark1 { get; set; }
        //备注字段1
        public string remark2 { get; set; }
    }
}
