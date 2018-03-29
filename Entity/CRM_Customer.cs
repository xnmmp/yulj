using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entity
{
    /// <summary>
    /// 客户
    /// </summary>
    public class CRM_Customer
    {
        //ID
        public string ID { get; set; }
        //客户名称
        public string C_name { get; set; }
        //客户地址
        public string C_address { get; set; }
        //联系人名称
        public string C_linkname { get; set; }
        //联系人联系方式
        public string C_linkphone { get; set; }
        //省
        public string C_province { get; set; }
        //市
        public string C_city { get; set; }
        //区
        public string C_district { get; set; }
        //客户类型（1.个人 2.企业）
        public int C_kind { get; set; }
        //客户合作方式（1.临时 2.长期 3.战略合作）
        public int C_cooperation { get; set; }
        //客户规模（1.小型 2.中型 3.大型）
        public int C_scale { get; set; }
        //客户行业
        public string C_industry { get; set; }
        //父级ID
        public string C_UserID { get; set; }
        //备注字段(该字段被使用定为：当前添加客户的业务员的ID)
        public string C_remark1 { get; set; }
        //备注字段
        public string C_remark2 { get; set; }
        public city city { get; set; }
    }
    public class city
    {
        public provinces provinces { get; set; }
        public cities cities { get; set; }
        public areass areass { get; set; }
    }
}
