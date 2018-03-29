using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 方案报价
    /// </summary>
    public class CRM_QProgram
    {
        //ID
        public string ID { set; get; }
        //项目报价时间
        public DateTime O_datetime { set; get; }
        //对方人员
        public string O_consumerpeople { set; get; }
        //商谈地点
        public string O_conferaddress { set; get; }
        //商谈内容
        public string O_tokleinfor { set; get; }
        //报价文件
        public object O_fileURL { set; get; }
        //下部计划
        public string O_nextplan { set; get; }
        //客户反馈(1.认可、2.重做、3.部分修改)
        public int O_customerfeedback { set; get; }
        //商机名称
        public string O_businessName { set; get; }
        //商机ID
        public string O_businessID { set; get; }
        //客户名称
        public string O_consumerName { set; get; }
        //客户ID
        public string O_consumerID { set; get; }
        //跟单人ID
        public string O_documentaryID { set; get; }
        //父级ID
        public string O_UserID { get; set; }
        //(备注)
        public string remark1 { set; get; }
        //备注2
        public string remark2 { set; get; }
    }
}
