using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Web;
namespace Service
{
    public class SAuthoriza
    {


        TokenTableService tokents = new TokenTableService();
        public EAuthentication Authentication(string Username, string Token)
        {

            EAuthentication eau = new EAuthentication();
            if (Username == null || Username == "" )
            {
                eau.state = false;
            }
            else
            {
                TokenTable tkt = tokents.SelByname(Username);
                if (tkt == null || tkt.GUID != Token)
                {
                    eau.state = false;
                }
                else
                {
                    eau.state = true;
                    eau.name = Username;
                    eau.token = Token;
                    eau.loginTime = tkt.loginTime;
                    eau.Role = tkt.Role;
                }
            }
            return eau;
        }
    }
}
