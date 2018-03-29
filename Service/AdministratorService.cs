using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;
using CommonSha;
namespace Service
{
    public class AdministratorService
    {
        //注册的时候查询账号不能重复名称
        public int Isrepetition(string T_accountnumber)
        {
            return AdministratorSQL.Isrepetition(T_accountnumber);
        }


        TokenTableService toks = new TokenTableService();
        //管理员添加子账号
        public Result A_Addson(CRM_User cu)
        {
            Result result = new Result();
            if (Isrepetition(cu.T_accountnumber) == 2)
            {
                result.state = 2;
                result.msg = "该用户已被注册";
            }
            else
            {
                if (AdministratorSQL.Addson(cu))
                {
                    string Tokenrole = cu.T_role.ToString();
                    if (toks.AddToken(cu.T_accountnumber, "", "", "", Tokenrole))
                    {
                        result.state = 1;
                        result.msg = "添加成功子账号成功！";
                    }

                }
                else
                {
                    result.state = 2;
                    result.msg = "添加成功子账号失败！";
                }
            }

            return result;
        }
        //管理员查看自己的子账号
        public Result SearchUserByUserid(int page, int rows, string strWhere)
        {
            Result result = new Result();
            string tblName = "CRM_User";  //-----表名
            string strGetFields = "ID,T_name,T_accountnumber,T_UserID,T_role,T_demartment,T_duct,T_phone,T_jobnumber,T_Birth,T_nativeplace,T_Entrytime,T_Nowhous,T_IDcard,T_photoUrl,T_remark,T_remark1";//-----查询字段
            string fldName = "ID";
            int doCount = 0;
            int OrderType = 1;
            List<CRM_User> culist = AdministratorSQL.SearchUserByUserid(page, rows, tblName, strWhere, strGetFields, fldName, doCount, OrderType);
            int count = CRM_businessSQL.BussinessScreenCount(tblName, strWhere);
            if (culist == null)
            {
                result.state = 2;
                result.msg = "未找到数据！";
            }
            else
            {
                result.state = 1;
                result.count = count;
                result.data = culist;
            }
            return result;
        }

        //管理员查询子账号详情
        public Result SearchUserByUseridinfor(string T_accountnumber)
        {
            DocumentaryService doc = new DocumentaryService();
            Result result = new Result();
            result = doc.searchCRMUserInfor(T_accountnumber);
            return result;
        }

        ////管理员修改子账号信息
        public Result UpdataSoninfor(CRM_User cu)
        {
            Result result = new Result();
            if (AdministratorSQL.UpdataSoninfor(cu))
            {
                result.state = 1;
                result.msg = "管理员对" + cu.T_name + "信息修改成功！";
            }
            else
            {
                result.state = 2;
                result.msg = "修改失败！";
            }
            return result;
        }


        //通过账号查到密码
        public string searchPwdByusername(string name)
        {
            string ss = DocumentarySQL.SearchIsPwdPRO(name);
            if (ss == null)
            {
                return null;
            }

            return ss;
        }

    }
}
