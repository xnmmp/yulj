using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data;
using System.Data.SqlClient;
namespace DAL
{

    /// <summary>
    /// 图片服务器
    /// 用来上传图片把图片保存在数据中
    /// </summary>
    public class PicturesServiceSQL
    {
        //添加上传图片到数据库
        public static bool AddPictures(PicturesService ps)
        {
            string sql = string.Format(@"INSERT INTO [dbo].[PicturesService]
           ([ID]
           ,[UserName]
           ,[Name]
           ,[Type]
           ,[Size]
           ,[State]
           ,[Url]
           ,[Remark1]
           ,[Remark2])
     VALUES
           ('{0}'
           ,'{1}'
           ,'{2}'           
           ,'{3}'
           ,{4}
           ,{5}
           ,'{6}'
           ,'{7}'
           ,'{8}')", ps.ID, ps.UserName, ps.Name, ps.Type, ps.Size, ps.State, ps.Url, ps.Remark1, ps.Remark2);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }

        //修改图片
        public static bool updataPictrues(PicturesService ps)
        {
            string sql = string.Format(@"UPDATE [dbo].[PicturesService]
   SET [UserName] ='{1}'
      ,[Name] = '{2}'
      ,[Type] = '{3}'
      ,[Size] = {4}
      ,[State] = {5}
      ,[Url] = '{6}'
      ,[Remark1] ='{7}'
      ,[Remark2] ='{8}'
 WHERE ID='{0}'", ps.ID, ps.UserName, ps.Name, ps.Type, ps.Size, ps.State, ps.Url, ps.Remark1, ps.Remark2);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }
        //查询

        public static List<PicturesService> SelAll(string idone, string idtwo, string idthree)
        {
            string sql = "SearchPicturesByIDPRO";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@IDone",idone),
            new SqlParameter("@IDtwo",idtwo),
            new SqlParameter("@IDthree",idthree)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            List<PicturesService> list = new List<PicturesService>();
            foreach (DataRow item in dt.Rows)
            {
                PicturesService ps = new PicturesService();
                ps.ID = item["ID"].ToString();
                ps.UserName = item["UserName"].ToString();
                ps.Name = item["Name"].ToString();
                ps.Type = item["Type"].ToString();
                ps.Size = int.Parse(item["Size"].ToString());
                ps.State = int.Parse(item["State"].ToString());
                ps.Url = item["Url"].ToString();
                ps.Remark1 = item["Remark1"].ToString();
                ps.Remark2 = item["Remark2"].ToString();
                list.Add(ps);
            }
            return list;
        }
        //删除
        public static bool Deletepic(string id)
        {
            string sql = string.Format(@"DELETE FROM [dbo].[PicturesService]
      WHERE ID='{0}'", id);
            return DbHelperSQL.ExecuteNonQuery(sql, CommandType.Text) > 0;
        }

        //通过ID查询出图片名称和路径
        public static PicturesService Searchpic(string id)
        {
            string sql = "SearchpicinforPRO";
            SqlParameter[] parm = new SqlParameter[] { 
            new SqlParameter("@ID",id)
            };
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, parm, CommandType.StoredProcedure);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            PicturesService ps = new PicturesService();
            ps.UserName = dt.Rows[0]["UserName"].ToString();
            ps.Url = dt.Rows[0]["Url"].ToString();
            return ps;
        }
    }
}
