using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    //供应商信息填写
   public class T_Supplier
    {
       //用户ID
       public string ID { get; set; }
       //用户名称
       public string name { get; set; }
       //省
       public string province { get; set; }
       //市
       public string city { get; set; }
       //区
       public string district { get; set; }
       //地址
       public string address { get; set; }
       //联系人
       public string contactor { get; set; }
       public string phone { get; set; }
       //email邮箱
       public string email { get; set; }
       //传真
       public string fax { get; set; }
       //网址
       public string url { get; set; }
       //邮编
       public string postcode { get; set; }
        //企业营业执照
       public string licenseno { get; set; }
        //税务号
       public string taxcode { get; set; }
        //公司性质
       public int? kind { get; set; }
       //公司规模
       public int? scale { get; set; }
       //公司利润
       public int? profit { get; set; }
       //公司信用
       public string credit { get; set; }
       //QQ
       public int QQ { get; set; }

    }
}
