using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
  public  class Service_two
    {
      //ID
      public string ID { get; set; }
      //图片
      public string Images { get; set; }
      //名字
      public string Text { get; set; }
      //文本描述
      public string InforText { get; set; }
      //父级ID
      public string ParentID { get; set; }
      //前后对比（前）
      public string beforeWork { get; set; }
      //前后对比（后）
      public string afterWork { get; set; }

      //评估
      public string serveAssessment { get; set; }
    }
}
