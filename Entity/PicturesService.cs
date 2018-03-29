using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{   /// <summary>
    /// 图片服务器
    ///用于上传图片
    /// </summary>
    public class PicturesService
    {
        //ID
        public string ID { get; set; }
        //上传图片用户的ID
        public string UserName { get; set; }
        //图片的名称
        public string Name { get; set; }
        //图片的类型
        public string Type { get; set; }
        //图片的大小
        public int Size { get; set; }
        //图片的状态
        public int State { get; set; }
        //图片地址路径
        public string Url { get; set; }
        //备注字段1
        public string Remark1 { get; set; }
        //备注字段2
        public string Remark2 { get; set; }
    }
}
