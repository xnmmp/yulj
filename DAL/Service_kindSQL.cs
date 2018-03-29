using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entity;
namespace DAL
{
    public class Service_kindSQL
    {
        //查询一级所有数据
        public static List<Service_one> SerOne()
        {
            string sql = @"select ID,Text from Service_one";
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, CommandType.Text);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            List<Service_one> list = new List<Service_one>();
            foreach (DataRow item in dt.Rows)
            {

                Service_one so = new Service_one();
                so.ID = item["ID"].ToString();
                so.Text = item["Text"].ToString();
                list.Add(so);
            }
            return list;
        }
        //通过一级ID的查询出二级信息
        public static List<Service_two> SerTwoFor1(string ID)
        {

            string sql = string.Format(@"select ID,Text,ParentID from Service_two where ParentID='{0}'", ID);
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, CommandType.Text);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            List<Service_two> list = new List<Service_two>();
            foreach (DataRow item in dt.Rows)
            {
                Service_two st = new Service_two();
                st.ID = item["ID"].ToString();
                st.Text = item["Text"].ToString();
                st.ParentID = item["ParentID"].ToString();
                list.Add(st);
            }
            return list;
        }

        //通过第二级的ID查询出第二级的名字
        public static Service_one SelectonenameByid(string id)
        {
            string sql = string.Format(@"select Text,ID from Service_one where ID='{0}'", id);
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, CommandType.Text);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            Service_one so = new Service_one();
            so.Text = dt.Rows[0]["Text"].ToString();
            so.ID = dt.Rows[0]["ID"].ToString();
            return so;
        }
        //通过第一级的ID查询出第一级的名字
        public static Service_two SelecttwonameByid(string id)
        {
            string sql = string.Format(@"select Text,ID from Service_two where ID='{0}'", id);
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, CommandType.Text);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            Service_two st = new Service_two();
            st.Text= dt.Rows[0]["Text"].ToString();
            st.ID = dt.Rows[0]["ID"].ToString();
            return st;
        }

        //查询省
        public static List<provinces> select()
        {

            string sql = "select id,provinceid,province from provinces";
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, CommandType.Text);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            List<provinces> list = new List<provinces>();
            foreach (DataRow item in dt.Rows)
            {
                provinces p = new provinces();
                p.id = int.Parse(item["id"].ToString());
                p.provinceid = item["provinceid"].ToString();
                p.province = item["province"].ToString();
                list.Add(p);
            }
            return list;
        }
        //查询城市
        public static List<cities> selectbycity(string proid)
        {

            string sql = string.Format(@"select id,cityid,city from cities where provinceid='{0}'", proid);
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, CommandType.Text);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            List<cities> cs = new List<cities>();
            foreach (DataRow item in dt.Rows)
            {
                cities c = new cities();
                c.id = int.Parse(item["id"].ToString());
                c.cityid = item["cityid"].ToString();
                c.city = item["city"].ToString();
                cs.Add(c);
            }
            return cs;
        }
        //查询地区
        public static List<areass> selectbyadress(string ciid)
        {

            string sql = string.Format(@"select id,area,areaid from areass where cityid='{0}'", ciid);
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, CommandType.Text);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            List<areass> ars = new List<areass>();
            foreach (DataRow item in dt.Rows)
            {
                areass a = new areass();
                a.id = int.Parse(item["id"].ToString());
                a.area = item["area"].ToString();
                a.areaid = item["areaid"].ToString();
                ars.Add(a);
            }
            return ars;
        }

        //查询provinces
        public static provinces proname(string proid)
        {
            string sql = string.Format(@"select province,provinceid from provinces where provinceid='{0}'", proid);
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, CommandType.Text);
            provinces pro = new provinces();
            if (dt.Rows.Count <= 0)
            {
                pro.province = "";
            }
            else
            {
                pro.province = dt.Rows[0]["province"].ToString();
                pro.provinceid = dt.Rows[0]["provinceid"].ToString();
            }
            return pro;

        }
        //查询citity
        public static cities citname(string citid)
        {
            string sql = string.Format(@"select city,cityid from cities where cityid='{0}'", citid);
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, CommandType.Text);
            cities cit = new cities();
            if (dt.Rows.Count <= 0)
            {
                cit.city = "";
            }
            else
            {
                cit.city = dt.Rows[0]["city"].ToString();
                cit.cityid = dt.Rows[0]["cityid"].ToString();
            }

            return cit;

        }
        //查询areass
        public static areass arename(string areid)
        {
            string sql = string.Format(@"select area,areaid from areass where areaid='{0}'", areid);
            DataTable dt = DbHelperSQL.ExecuteQuery(sql, CommandType.Text);
            areass are = new areass();
            if (dt.Rows.Count <= 0)
            {
                are.area = "";
            }
            else
            {
                are.area = dt.Rows[0]["area"].ToString();
                are.areaid = dt.Rows[0]["areaid"].ToString();
            }
            return are;
        }





    }
}
