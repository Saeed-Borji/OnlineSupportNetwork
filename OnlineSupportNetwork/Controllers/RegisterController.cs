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
using System.Threading;

namespace OnlineSupportNetwork.Controllers
{
    public class RegisterController : ApiController
    {
        XMLLoad xmlLoad = new XMLLoad();
        string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "\\DataBase1.xml";

        [HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> PostAsync()
        {
                     
            string username = HttpContext.Current.Request.Form["username"];
            string accounttype = HttpContext.Current.Request.Form["accounttype"];
            string password = HttpContext.Current.Request.Form["password"];
            string repetpassword = HttpContext.Current.Request.Form["repetpassword"];
            string accountnumber = HttpContext.Current.Request.Form["accountnumber"];
            string bankname = HttpContext.Current.Request.Form["bankname"];
            string accountname = HttpContext.Current.Request.Form["accountname"];
            string check = HttpContext.Current.Request.Form["checked"];

            string[] param = { username, accounttype, password, repetpassword , accountnumber, bankname, accountname , check};
            string Result = string.Empty;// 0 = "" | 1 = "" | 2 = "" | 
            string PicPath = AppDomain.CurrentDomain.BaseDirectory + "\\Register\\ReciptPic\\" + username + "_" + accounttype + ".jpg";

            if (File.Exists(PicPath) == true)
            {
                Result = "Awaiting review of user documents";
            }
            else
            {
                
                //$.B Write Register Data on xml file \/
                if (File.Exists(xmlPath.ToString()) != true)
                {
                    //Log.LogString.Add("product.xml = Not Exist _ Create product.xml file");

                    //xmlLoad.CreateFile(xmlPath.ToString(), username, accounttype, password, accountnumber, bankname, accountname);
                    xmlLoad.CreateFile(xmlPath.ToString(), param);

                    if (File.Exists(xmlPath) == true)
                    {
                        Result = "Register New User Successful";
                    }
                    else
                    {
                        Result = "Create Database.xml fail";
                    }

                }
                else if (File.Exists(xmlPath) == true)
                {
                    xmlLoad.LoadFile(xmlPath.ToString(), "/User/" + username.ToString());

                    if (xmlLoad.username.ToString() == username.ToString())//change parameter of user
                    {
                        if (xmlLoad.accounttype.ToString() == accounttype.ToString())// || xmlLoad.accounttype.ToString() == "Both")//$.B 13/04/2020
                        {
                            Result = "Duplicate Username and Accounttype";
                        }
                        else
                        {
                            if (xmlLoad.accounttype.ToString() == "Both")
                            {
                                Result = "Duplicate Username and Accounttype";
                            }
                            else
                            {
                                //$.B 13/04/2020
                                //param[1] = "Both";
                                //xmlLoad.SaveFile(xmlPath.ToString(), "/User/" + username.ToString(), param);
                                //Result = "Register Old User Successful _ Both";
                                Result = "Can Not Register Duplicate Username";
                            }

                        }
                        
                    }
                    else//add new user and params
                    {
                        xmlLoad.AddClient(xmlPath.ToString(), "/User/" + username.ToString(), param);
                        Result = "Register New User Successful";
                    }

                }

                //$.B Write Register Data on xml file /\

                //$.B \/ Get Pic
                if (Result == "Duplicate Username and Accounttype")
                {
                    Result = "Duplicate Username and Accounttype";
                }
                else
                {
                    try
                    {

                        string root = HttpContext.Current.Server.MapPath("~/Register/ReciptPic");
                        if (!Directory.Exists(root))
                            Directory.CreateDirectory(root);
                        var provider = new MultipartFormDataStreamProvider(root);
                        await Request.Content.ReadAsMultipartAsync(provider);

                        string localFileName = provider.FileData[0].LocalFileName;
                        string mainFileName = Guid.NewGuid().ToString("N") + "." + provider.FileData[0].Headers.ContentDisposition.FileName.Replace("\"", string.Empty).Split('.').Last();
                        string targetFileName = localFileName.Replace(localFileName.Split('\\').LastOrDefault(), mainFileName);
                        if (File.Exists(targetFileName))
                            File.Delete(targetFileName);
                        File.Move(localFileName, targetFileName);


                        //$.B \/
                        File.Copy(targetFileName, PicPath);
                        Thread.Sleep(2000);

                        if (File.Exists(targetFileName))
                            File.Delete(targetFileName);
                        //$.B /\

                        //DoSomthings(File.ReadAllBytes(targetFileName));
                    }
                    catch (Exception ex)
                    {
                        GC.Collect();
                        //Log.WriteLog(Log.LogState.Error, Log.LogPackages.Web, ex.Message + Environment.NewLine + ex.StackTrace, "RohamFace", true);
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error has been occurred. It will be resolve about an hour later!");
                    }
                }
                //$.B /\ Get Pic

            }


            if (Result.ToString() != "")
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    statusCode = 200,
                    statusMessage = Result.ToString(),
                    resultID = 0

                });
            else
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    statusCode = 401,
                    statusMessage = "Request Failed" + Result.ToString(),
                    resultID = 1

                });
            
        }
    }
    public class userdata
    {
        public string username;
        public string accounttype;
        public string password;
        public string repetpassword;
        public string accountnumber;
        public string accountname;
        public string check;
    }
}
