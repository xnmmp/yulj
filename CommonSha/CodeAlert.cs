using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CommonSha
{
    public class CodeAlert
    {
        //public int SSM(string phonenum, string code)
        //{
        //    try
        //    {
        //        DateTime dt = DateTime.Now;
        //        string time = dt.ToString();
        //        Encoding myEncoding = Encoding.GetEncoding("UTF-8");
        //        string param = "action=send&userid=1427&account=duke&password=123456&mobile=" + phonenum + "&content=【度科网络】来自与CRM的提醒" + code + "&sendTime=" + time + "&mobilenumber=2&countnumber=2&telephonenumber=0";
        //        byte[] postBytes = myEncoding.GetBytes(param);
        //        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://115.238.168.178:8888/sms.aspx");
        //        req.Method = "POST";
        //        req.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
        //        req.ContentLength = postBytes.Length;
        //        using (Stream reqStream = req.GetRequestStream())
        //        {
        //            reqStream.Write(postBytes, 0, postBytes.Length);
        //        }
        //        System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
        //        using (WebResponse wr = req.GetResponse())
        //        {
        //            StreamReader sr = new StreamReader(wr.GetResponseStream(), System.Text.Encoding.UTF8);
        //            System.IO.StreamReader xmlStreamReader = sr;
        //            xmlDoc.Load(xmlStreamReader);
        //        }
        //        if (xmlDoc == null)
        //        {
        //            return 1;
        //        }
        //        else
        //        {
        //            String message = xmlDoc.GetElementsByTagName("message").Item(0).InnerText.ToString();
        //            if (message == "ok")
        //            {
        //                return 2;
        //            }
        //            else
        //            {
        //                return 3;
        //            }
        //        }
        //    }
        //    catch (System.Net.WebException WebExcp)
        //    {
        //        return 4;
        //    }
        //}



        public int SSM(string phonenum)
        {
            string ret = null;
            int ss;
            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            //ip格式如下，不带https://
            bool isInit = api.init("app.cloopen.com", "8883");
            api.setAccount("8a216da85c34774c015c439abd9e05e1", "ea958002662d4785ac5f58f6b5c9de5a");
            api.setAppId("8a216da85c34774c015c439abe7305e7");

            try
            {
                if (isInit)
                {
                    Dictionary<string, object> retData = api.SendTemplateSMS(phonenum, "178910", null);
                    ret = getDictionaryData(retData);
                }
                else
                {
                    ret = "初始化失败";
                }
            }
            catch (Exception exc)
            {
                ret = exc.Message;
            }
            finally
            {
                if (ret.Contains("0000"))
                {
                    ss = 2;
                }
                else
                {
                    ss = 1;
                }
            }
            return ss;
        }



        private static string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (KeyValuePair<string, object> item in data)
            {
                if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    ret += item.Key.ToString() + "={";
                    ret += getDictionaryData((Dictionary<string, object>)item.Value);
                    ret += "};";
                }
                else
                {
                    ret += item.Key.ToString() + "=" + (item.Value == null ? "null" : item.Value.ToString()) + ";";
                }
            }
            return ret;
        }
    }
}
