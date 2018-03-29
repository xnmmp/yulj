using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
namespace Service
{
    public class Service_kindService
    {
        //一二级联查
        public Result ServiceOneSerchTwo(int index, string childId)
        {
            Result result = new Result();
            var pro = new object();
            switch (index)
            {
                case 0:
                    {
                        pro = Service_kindSQL.SerOne();
                        if (pro == null)
                        {
                            result.state = 2;
                            result.msg = "查询不到类型！";

                        }
                        else
                        {
                            result.state = 1;
                            result.data = pro;
                        }
                    }; break;
                case 1:
                    {
                        if (childId == "" || childId == null)
                        {
                            result.state = 3;
                            result.msg = "没有选择服务类型！";
                        }
                        else
                        {
                            pro = Service_kindSQL.SerTwoFor1(childId);
                            if (pro == null)
                            {
                                result.state = 2;
                                result.msg = "查询不到类型！";
                            }
                            else
                            {
                                result.state = 1;
                                result.data = pro;
                            }
                        }

                    }; break;
            }
            return result;
        }

        //省市区三级联查
        public Result AdressSearch(int index, string childId)
        {
            Result result = new Result();
            var pro = new object();
            switch (index)
            {
                case 0:
                    {
                        pro = Service_kindSQL.select();
                        if (pro == null)
                        {
                            result.state = 2;
                            result.msg = "查询不到省！";

                        }
                        else
                        {
                            result.state = 1;
                            result.data = pro;
                        }
                        //表示需要获取所有的省份
                    }; break;
                case 1:
                    {
                        if (childId == "" || childId == null)
                        {
                            result.state = 3;
                            result.msg = "没有选择地址！";
                        }
                        else
                        {
                            //表示通过省份Id（provinceid）查所有城市
                            pro = Service_kindSQL.selectbycity(childId);
                            if (pro == null)
                            {
                                result.state = 2;
                                result.msg = "查询不到市！";
                            }
                            else
                            {
                                result.state = 1;
                                result.data = pro;
                            }
                        }
                    }; break;
                case 2:
                    {
                        if (childId == "" || childId == null)
                        {
                            result.state = 3;
                            result.msg = "没有选择地址！";
                        }
                        //表示通过省份Id（provinceid）查所有城市
                        else
                        {
                            pro = Service_kindSQL.selectbyadress(childId);
                            if (pro == null)
                            {
                                result.state = 2;
                                result.msg = "查询不到区！";
                            }
                            else
                            {
                                result.state = 1;
                                result.data = pro;
                            }
                        }

                    }; break;
            }
            return result;
        }

        ////通过第一二级的ID查询出第一二级的名字
        //public Result SelecttwonameByid(string OneID, string TwoID)
        //{

        //    Result result = new Result();
        //    string twoname = Service_kindSQL.SelecttwonameByid(TwoID);
        //    string onename = Service_kindSQL.SelectonenameByid(OneID);
           
        //    if (twoname != null && onename != null)
        //    {
        //        result.state = 1;
        //        ReturnOneTwo ro = new ReturnOneTwo();
        //        ro.TwoName = twoname;
        //        ro.OneName = onename;
        //        result.data = ro;
        //    }
        //    else if (onename!= null&&twoname== null)
        //    {
        //          result.state = 3;
        //          ReturnOneTwo ro = new ReturnOneTwo();
        //          ro.TwoName = "无";
        //          ro.OneName = onename;
        //    }
        //    else
        //    {
        //        result.state = 2;
        //        result.msg = "服务类型已经给删除";
        //    }
        //    return result;
        //}




    }
}
