using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Entity
{
    public class CRM_projectassess
    {
        //ID
        public string ID { get; set; }
        //评估日期
        public DateTime A_datetime { get; set; }
        //工期要求（宽松、严格、苛刻）
        public int A_durationneed { get; set; }
        //施工要求（宽松、严格、苛刻）
        public int A_constructneed { get; set; }
        //验收要求（宽松、严格、苛刻）
        public int A_acceptanceneed { get; set; }
        //违约赔偿（无、宽松、严格、苛刻）
        public int A_defaultmakeup { get; set; }
        //客户资质（没有实力、一般、有实力）
        public int A_aptitudes { get; set; }
        //资金回笼（快、账期一个月内、慢）
        public int A_fundsbackfront { get; set; }
        //项目规模（超大型、大型、中型、小型）
        public int A_projectscale { get; set; }
        //预算（<1万，1万-5万，5-10，>10）
        public int A_budget { get; set; }
        //商机质量（按分值自动计算得出）
        public int A_businessmass { get; set; }
        //竞争对手（无，不清楚，不强，强）
        public int A_compete { get; set; }
        //客户关键人（没有，一点，可以，搞定）
        public int A_consumerkey { get; set; }
        //回扣要求（没有，有）
        public int A_kickbackneed { get; set; }
        //客户关注点（价格、质量、品牌、性价比）
        public int A_consumerFocus { get; set; }
        //竞争力（弱、一般、强）
        public int A_competitiveness { get; set; }
        //项目阻力（无，有）
        public int A_projectdrag { get; set; }
        //成单率(按分值自动计算得出)
        public int A_overorder { get; set; }
        //结论自动得出（放弃，、普通商机、重点商机、鸭子商机）
        public int A_conclusion { get; set; }
        //商机名称
        public string A_businessName { get; set; }
        //商机ID
        public string A_businessID { get; set; }
        //客户名称
        public string A_customerName { get; set; }
        //客户ID
        public string A_customerID { get; set; }
        //跟单人ID
        public string A_documentaryID { get; set; }
        //父级ID
        public string UserID { get; set; }
        //备注
        public string A_remark1 { get; set; }
        //备注
        public string A_remark2 { get; set; }
    }
}
