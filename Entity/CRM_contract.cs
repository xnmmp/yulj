using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// 合同
    /// </summary>
    public class CRM_contract
    {
        //ID
        public string ID { get; set; }
        //当前操作人
        public string P_cookiename { get; set; }
        //签单时间
        public DateTime P_datetime { get; set; }
        //合同开始时间
        public string P_startdatetime { get; set; }
        //合同结束时间
        public string P_enddatetime { get; set; }
        //合同类型1
        public int P_kindone { get; set; }
        //合同类型2
        public int P_kindtwo { get; set; }
        //合同类型3
        public int P_kindthree { get; set; }
        //甲方合同名称
        public string P_name { get; set; }
        //合同金额
        public string P_money { get; set; }
        //地址1
        public object P_URL1 { get; set; }
        //地址2
        public object P_URL2 { get; set; }
        //商机名称
        public string P_businessName { get; set; }
        //商机ID
        public string P_businessID { get; set; }
        //客户名称
        public string P_customerName { get; set; }
        //客户ID
        public string P_customerID { get; set; }
        //跟单人ID
        public string P_documentaryID { get; set; }
        //父级ID
        public string UserID { get; set; }
        //备注1(现在用于显示商机日期)
        public string remark1 { get; set; }
        //备注2
        public string remark2 { get; set; }
        //备注3
        public string remark3 { get; set; }
    }
}
