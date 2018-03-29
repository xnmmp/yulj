using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
namespace Service
{
    public class TokenTableService
    {
        //添加数据（在用户登录的时候）
        public bool AddToken(string name, string GUID, string loginTime, string Remark,string Role)
        {
            return TokenTableSQL.AddToken(name, GUID, loginTime, Remark,Role);
        }
        //修改Token（在用户登录的时候）
        public bool UpdataToken(string name, string GUID, string loginTime, string Role,string Remark)
        {

            return TokenTableSQL.UpdataToken(name, GUID, loginTime, Role, Remark);

        }
        //查询
        public TokenTable SelByname(string name)
        {

            return TokenTableSQL.SelByname(name);

        }
    }
}

