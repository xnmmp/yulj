using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CRM_visit
    {
        //ID
        public string ID { get; set; }
        //拜访时间
        public DateTime CV_datetime { get; set; }
        //客户拜访方式（1.上门，2.电话）
        public int  CV_ways { get; set; }
        //对方人员
        public string CV_oppositepeople { get; set; }
        //拜访目的1、熟悉客户建立感情 2、了解客户需求3、现场勘查诊断 4、方案论证 5、报价论证6、商定合同 7、挖掘商机需求 8.其他
        public int CV_purpose { get; set; }
        //商谈内容
        public string CV_talkdetails { get; set; }
        //客户反馈态度（1.认可，2.观望，3.冷漠）
        public int  CV_feedback { get; set; }
        //下部计划
        public string CV_lowerplan { get; set; }
        //上传文件
        public object CV_fileurl { get; set; }
        //跟单人ID（当前操作人的ID）
        public string CV_documentaryID { get; set; }
        //商机名称
        public string CV_businessName { get; set; }
        //商机ID
        public string CV_businessID { get; set; }
        //客户名称
        public string CV_consumerName { get; set; }
        //客户ID
        public string CV_consumerID { get; set; }
        //当前操作人的父级ID
        public string CV_UserID { get; set; }
        //备注(为有用的字段  备注)
        public string CV_remark1 { get; set; }
        //备注字段2
        public string CV_remark2 { get; set; }
    }
}
