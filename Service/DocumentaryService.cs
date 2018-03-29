using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;
using System.Transactions;
using CommonSha;
namespace Service
{
    public class DocumentaryService
    {
        //用户查看个人用户详情
        public Result searchCRMUserInfor(string T_accountnumber)
        {
            Result result = new Result();
            CRM_User cu = DocumentarySQL.searchCRMUserInfor(T_accountnumber);
            if (cu == null)
            {
                result.state = 2;
                result.msg = "用户不存在！";
            }
            else
            {
                if (cu.T_photoUrl == null)
                {
                    cu.T_photoUrl = "";
                }
                else
                {
                    string url = cu.T_photoUrl.ToString();
                    var ids = url.Split(',');
                    string one = ids[0];
                    string two = "";
                    string three = "";
                    if (ids.Length == 2)
                    {
                        two = ids[1];
                    }
                    else if (ids.Length == 3)
                    {
                        two = ids[1];
                        three = ids[2];
                    }
                    PicturesServiceser pse = new PicturesServiceser();
                    var picrlist = pse.SelAll(one, two, three);
                    cu.T_photoUrl = picrlist;
                }
                result.state = 1;
                cu.T_pwd = null;
                result.data = cu;
            }
            return result;
        }
        //员工修改个人信息
        public Result UpdatauserInfor(CRM_User cu)
        {
            Result result = new Result();
            if (DocumentarySQL.UpdatauserInfor(cu))
            {
                result.state = 1;
                result.msg = "修改信息成功!";
            }
            else
            {
                result.state = 2;
                result.msg = "修改信息失败！";
            }
            return result;
        }
        //显示所有状态为1的用户
        public Result SearchUserBystatusStatus(string UserID)
        {
            Result result = new Result();
            var list = DocumentarySQL.SearchUserBystatusStatus(UserID);
            if (list == null)
            {
                result.state = 3;
                result.msg = "查询用户为空！";
            }
            else
            {
                result.data = list;
                result.state = 1;
            }
            return result;
        }
        //通过用户ID查询出用户的名字
        public string SearchUserNameByID(string ID)
        {
            Result result = new Result();
            string T_name = DocumentarySQL.SearchUserNameByID(ID);
            if (T_name == null)
            {
                return null;
            }
            return T_name;
        }


        // //在用户修改个人密码中，通过账号查找出密码
        public Result SearchIsPwdPRO(string T_accountnumber, string pwd)
        {
            Result result = new Result();
            string userPwd = DocumentarySQL.SearchIsPwdPRO(T_accountnumber);
            if (userPwd == null)
            {
                result.state = 3;
                result.msg = "网络异常请重新登录！";
            }
            else
            {
                string spwd = encrypt.Sha256(pwd);
                if (spwd == userPwd)
                {
                    result.state = 1;
                }
                else
                {
                    result.state = 2;
                    result.msg = "密码输入错误！";
                }
            }
            return result;
        }
        //修改密码
        public Result UpdataPWD(string T_accountnumber, string pwd, string role, string oldPwd)
        {
            Result result = new Result();
            string guid = Guid.NewGuid().ToString("N");
            pwd = encrypt.Sha256(pwd);
            oldPwd = encrypt.Sha256(oldPwd);
            string userPwd = DocumentarySQL.SearchIsPwdPRO(T_accountnumber);
            if (userPwd == oldPwd)
            {
                using (TransactionScope sc = new TransactionScope())
                {
                    if (DocumentarySQL.UpdataPWD(T_accountnumber, pwd))
                    {
                        if (TokenTableSQL.UpdataToken(T_accountnumber, guid, DateTime.Now.ToString(), role, ""))
                        {
                            sc.Complete();
                            result.state = 1;
                            result.guid = guid;
                            result.msg = "密码修改成功！";
                        }
                        else
                        {
                            result.state = 2;
                            result.msg = "密码修改失败！";
                        }
                    }
                    else
                    {
                        result.state = 2;
                        result.msg = "密码修改失败！";
                    }
                }
            }
            else
            {
                result.state = 2;
                result.msg = "密码修改失败！";
            }
            return result;
        }
    }
}
