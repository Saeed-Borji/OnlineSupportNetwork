using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using OnlineSupportNetwork.Models;

namespace OnlineSupportNetwork.Controllers
{
    public class NotificationMessageController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post()
        {
            //XMLLoad xmlLoad = new XMLLoad();
            
            string Result = string.Empty;
            //int accounttype = -1;//Acount Type Not Set

            string username = HttpContext.Current.Request.Form["username"];
            string accounttypename = HttpContext.Current.Request.Form["accounttypename"];// $B. 14/04/2022
            string UserInboxPath = string.Empty;
            if (accounttypename == "Field")
            {
                UserInboxPath = AppDomain.CurrentDomain.BaseDirectory + "\\InboxMessage\\fieldexpert\\" + username;
            }
            else if (accounttypename == "Technical")
            {
                UserInboxPath = AppDomain.CurrentDomain.BaseDirectory + "\\Question";
            }// $B. 14/04/2022

            int fCount = 0;
            //string password = HttpContext.Current.Request.Form["password"];
            //string check = string.Empty;// HttpContext.Current.Request.Form["check"];

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\log.txt", username + "__" + UserInboxPath);
            if (!Directory.Exists(UserInboxPath))// != true)
            {
                Result = "Dont have any message";
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\log1.txt", username + "__" + UserInboxPath);
            }
            else
            {
                //fCount = Directory.GetFiles(UserInboxPath+"\\", "*.*", SearchOption.AllDirectories).Length;
                //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(UserInboxPath);
                //fCount = dir.GetFiles().Length;
                fCount = Directory.GetFiles(UserInboxPath, "*.xml", SearchOption.AllDirectories).Length;
                Result = "Have Message";
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\log2.txt", username + "__" + UserInboxPath+"<_>"+fCount);
            }



            if (Result == "Have Message")
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    statusCode = 200,
                    statusMessage = Result.ToString(),
                    resultID = 0,
                    messageCount = fCount,
                });
            else if (Result == "Dont have any message")
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    statusCode = 200,
                    statusMessage = Result.ToString(),
                    resultID = 1,
                    messageCount = fCount,
                });
            else
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    statusCode = 401,
                    statusMessage = "Request Failed" + "_" + Result,
                    resultID = 2,
                    messageCount = fCount,
                });

        }
    }
}
