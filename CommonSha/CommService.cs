using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;
namespace CommonSha
{
    public class CommService
    {
        public static string SearchFIDByID(string ID)
        {
            var Crm_User = LoginSQL.SearchCrmByID(ID);
            if (Crm_User == null)
            {
                return null;
            }
            else
            {
                string FID = Crm_User.T_UserID;
                return FID;
            }
        }
    }
}
