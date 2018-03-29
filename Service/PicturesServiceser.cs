using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
using System.IO;
namespace Service
{
    public class PicturesServiceser
    {
        //添加上传图片到数据库
        public bool AddPictures(PicturesService ps)
        {
            return PicturesServiceSQL.AddPictures(ps);
        }
        //修改图片
        public bool updataPictrues(PicturesService ps)
        {

            return PicturesServiceSQL.updataPictrues(ps);
        }
        //查询

        public Result SelAll(string idone, string idtwo, string idthree)
        {
            Result result = new Result();
            var list = PicturesServiceSQL.SelAll(idone, idtwo, idthree);
            if (list == null)
            {
                result.state = 2;  //-------未搜索到地址
            }
            else
            {
                result.state = 1;
                result.data = list;
            }

            return result;
        }
        //删除
        public Result Deletepic(string id, string Username, int role)
        {
            Result result = new Result();
            var list = PicturesServiceSQL.Searchpic(id);
            if (list == null)
            {
                result.msg = "删除失败！";
                result.state = 3;
            }
            //else if (Username != list.UserName)
            //{
            //    result.msg = "删除异常！";
            //    result.state = 4;
            //}
            else
            {
                string name = list.Url;
                int start = name.LastIndexOf("/");
                name = name.Substring(start + 1, name.Length - start - 1);
                string sstxt = "";
                if (role == 1)
                {
                    sstxt = System.Web.HttpContext.Current.Server.MapPath("~/updata/") + name;
                }
                else if (role == 2)
                {
                    sstxt = System.Web.HttpContext.Current.Server.MapPath("~/ERP_load/") + name;
                }
                if (File.Exists(sstxt))
                {
                    //存在则删除
                    if (PicturesServiceSQL.Deletepic(id))
                    {
                        File.Delete(sstxt);
                        result.msg = "删除成功！";
                        result.state = 1;
                    }
                    else
                    {
                        result.msg = "删除失败！";
                        result.state = 2;
                    }
                }
                else
                {
                    result.msg = "删除失败！";
                    result.state = 2;
                }
            }
            return result;
        }
    }
}
