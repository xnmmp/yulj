using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entity
{
    /// <summary>
    /// 商机表
    /// </summary>
    public class CRM_business
    {
        //ID
        public string ID { get; set; }
        //商机名称
        public string B_name { get; set; }
        //商机创建日期
        public DateTime B_datetime { get; set; }
        //商机来源(合作伙伴、老客户介绍、平台、公司资源、网络渠道、本人开发）
        public int B_source { get; set; }
        //商机类型(项目施工、服务保养)
        public int B_kind { get; set; }
        //服务类型1
        public string B_servicekind1 { get; set; }
        //服务类型2
        public string B_servicekind2 { get; set; }
        //需求原因
        public string B_needcause { get; set; }
        //需求量
        public string B_neednumber { get; set; }
        //商机录入人
        public string Addbusinessman { get; set; }
        //客户ID
        public string CustomerID { get; set; }
        //跟单人ID
        public string B_documentaryID { get; set; }
        //跟单人名称
        public string B_documentaryname { get; set; }
        //父级ID
        public string B_fistuseerID { get; set; }
        //商机筛选默认为(结果 0：未被筛选，1：派单，2：转单，3：放弃）
        public int B_status { get; set; }

        //(当商机被放弃的时候，放弃原因)
        public string B_giveupcause { get; set; }
        //当被派单时跟单人的职责
        public string B_documentaryneed { get; set; }
        //需求评估（0：未评估 1：已评估）
        public int B_needsassessment { get; set; }
        //方案报价标注（0：未报价  1.已报价）
        public int B_projectQuote { get; set; }
        //派单人名称
        public string B_remark1 { get; set; }
        //创建该商机的业务员
        public string B_remark2 { get; set; }
        //客户名称
        public string C_name { get; set; }
        //客户地址
        public string C_address { get; set; }
        //客户电话
        public string C_linkphone { get; set; }
        //客户联系人
        public string C_linkname { get; set; }
        public Kinds Kinds { get; set; }


    }
    public class Kinds
    {
        public Service_one so { get; set; }
        public Service_two st { get; set; }

    }
}
