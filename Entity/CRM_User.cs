using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    /// <summary>
    /// CRM用户表
    /// 
    /// </summary>
    public class CRM_User
    {
        //用户ID
        public string ID { get; set; }
        //用户名称
        public string T_name { get; set; }
        //部门
        public string T_demartment { get; set; }
        //职务
        public string T_duct { get; set; }
        //电话
        public string T_phone { get; set; }
        //员工工号
        public string T_jobnumber { get; set; }
        //帐号
        public string T_accountnumber { get; set; }
        //密码
        public string T_pwd { get; set; }
        //出生年月
        public string T_Birth { get; set; }
        //性别
        public string T_Sex { get; set; }
        //籍贯
        public string T_nativeplace { get; set; }
        //入职时间
        public string T_Entrytime { get; set; }
        //现在居住地址
        public string T_Nowhous { get; set; }
        //身份证号码
        public string T_IDcard { get; set; }
        //照片地址
        public object T_photoUrl { get; set; }
        //该用户的状态
        public int T_status { get; set; }
        //管理员ID
        public string T_UserID { get; set; }
        //用户角色（1.管理员 2.子帐号）
        public int T_UserRole { get; set; }
        //用户权限（1.管理员 2.总经理 3.员工）
        public int T_role { get; set; }
        //使用类型（1.试用   2.付费）
        public int T_Usekind { get; set; }
        //备注字段
        public string T_remark { get; set; }
        //备注字段
        public string T_remark1 { get; set; }

    }
}
