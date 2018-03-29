using Entity;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace CRM.Controllers
{
    public class PicturesServiceController : Controller
    {
        //
        // GET: /TEST/


        SAuthoriza sau = new SAuthoriza();
        PicturesServiceser pts = new PicturesServiceser();

        #region //图片加载公共
        public string PicPunlic(string Username, string Token, string show)
        {
            // StringBuilder url_picture = new StringBuilder();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
            Result result = new Result();
            EAuthentication ecu = sau.Authentication(Username, Token);
            if (!ecu.state)
            {
                result.msg = "未登录";
                result.state = -1;
                //json.Serialize(result, sb);
                //return sb.ToString();
            }
            else
            {
                int maxSize = 1024 * 1024 * 20;
                string regx = "application/octet-stream|image/gif|image/jpeg|image/jpeg|image/bmp|image/png|image/pjpeg|image/x-png";
                string outereg = ".jpg|.png|.gif|.bmp|.doc|.docx|.txt";
                string upload_product_image_filepath = ConfigurationManager.AppSettings["upload_product_image_filepath"];
                string upload_product_host_ip = ConfigurationManager.AppSettings["upload_product_host_ip"];
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    int fileSize = file.ContentLength;
                    var rm = new Random();
                    var tm = DateTime.Now.ToString("yyyMMdd");
                    var fileOuter = Path.GetExtension(file.FileName);
                    string fileName = tm + Guid.NewGuid().ToString("N") + rm.Next(10000, 99999) + fileOuter;
                    string mimeType = file.ContentType.ToLower();
                    //var index = regx.IndexOf(mimeType);
                    //var oindex = outereg.IndexOf(fileOuter);
                    //if (index < 0 || oindex < 0)
                    //{
                    //    result.state = -1;
                    //    result.msg = "文件类型错误！";

                    //}
                    //else
                    //{
                    //if (index == 0)
                    //{
                    //    maxSize *= 5;

                    //}
                    if (fileSize > maxSize)
                    {
                        result.state = 0;
                        result.msg = "文件太大";

                    }
                    else
                    {
                        //todo
                        result.state = 1;
                        PicturesService ps = new PicturesService();
                        ps.ID = Guid.NewGuid().ToString("N");
                        ps.Name = file.FileName;
                        ps.Size = fileSize;
                        ps.State = 1;
                        ps.UserName = ecu.name;
                        ps.Type = fileOuter.Substring(1, fileOuter.Length - 1);
                        ps.Remark1 = "";//备注字段
                        ps.Remark2 = "";
                        ps.Url = upload_product_host_ip + upload_product_image_filepath + fileName;
                        pts.AddPictures(ps);
                        result.data = ps;
                        System.IO.Stream fileContent = file.InputStream;
                        file.SaveAs(Server.MapPath(upload_product_image_filepath + fileName));
                        // url_picture.Append(fileName);
                        //if (i < Request.Files.Count - 1) url_picture.Append(",");
                    }
                    //}
                }
            }
            json.Serialize(result, sb);
            return sb.ToString();
        }
        #endregion




        #region//删除图片
        public ActionResult DeletePictures(string id, string Username, string Token)
        {
            //id = "8f450cd2f037469182aa4d4247f70e0b";
            //Username = "pppp";
            //Token = "2d9982288a074e00be11540706afcdc6";
            //Token = "1d60776e668045bdbe4a14827e636a51";
            //Username = "pppp";
            //id = "5dd4c3f100324289bce0f4b4522088dd";
            Result result = new Result();
            if (id == null || id == "")
            {
                result.state = 3;
                result.msg = "传值失败！";
            }
            EAuthentication ecu = sau.Authentication(Username, Token);
            if (!ecu.state)
            {
                result.msg = "未登录";
                result.state = -1;
            }
            else
            {
                result = pts.Deletepic(id, Username, 1);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion





        #region //查询图片
        public ActionResult SelAll(string Username, string Token, string idone, string idtwo, string idthree)
        {
            //Username = "pppxxx";
            //Token = "ae9698bae70a415b83a01d743b477230";
            //idone = "580ad81bf2a6436c8478f48884284865";
            //idtwo = "";
            //idthree = "";
            Result result = new Result();
            if (idone == null && idtwo == null && idthree == null)
            {
                result.state = 3;
                result.msg = "没有找到文件！";
            }
            else if (idone == "" && idtwo == "" && idthree == "")
            {
                result.state = 3;
                result.msg = "没有找到文件！";
            }
            else
            {
                EAuthentication ecu = sau.Authentication(Username, Token);
                if (!ecu.state)
                {
                    result.msg = "未登录";
                    result.state = -1;
                }
                else
                {
                    result = pts.SelAll(idone, idtwo, idthree);
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}
