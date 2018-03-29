using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CRM_visitLoad
    {
        //ID
        public string ID { get; set; }
        //商机名称
        public string CV_businessName { get; set; }
        //客户名称
        public string CV_consumerName { get; set; }
        //拜访时间
        public DateTime CV_datetime { get; set; }
        //合同签订日期
        public string P_startdatetime { get; set; }
        //结论
        public string Over { get; set; }

        ///因为显示的内容和方案报价一样，所有方案报价也用这个类
        ///需求评估也来蹭一脚
        ///合同也来蹭一脚
    }
}
