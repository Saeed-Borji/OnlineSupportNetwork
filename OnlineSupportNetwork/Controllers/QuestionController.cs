using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.IO;
using OnlineSupportNetwork.Models;
using System.Threading;

namespace OnlineSupportNetwork.Controllers
{
    public class QuestionController : ApiController
    {
        XMLLoad xmlLoad = new XMLLoad();

        [HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> PostAsync()
        {
            string username = HttpContext.Current.Request.Form["username"];
            string accountname = HttpContext.Current.Request.Form["accountname"];
            string productname = HttpContext.Current.Request.Form["productname"];
            string hardsoft = HttpContext.Current.Request.Form["hardsoft"];
            string productmodel = HttpContext.Current.Request.Form["productmodel"];
            string modulename = HttpContext.Current.Request.Form["modulename"];
            string bankname = HttpContext.Current.Request.Form["bankname"];
            string message = HttpContext.Current.Request.Form["message"];

            string[] param = { username, accountname, productname, hardsoft, productmodel, modulename, bankname, message };
            string Result = string.Empty;// 0 = "" | 1 = "" | 2 = "" | 

            string QuestionPath = AppDomain.CurrentDomain.BaseDirectory + "Question\\" + username + "_Field" ;
            string PicPath = QuestionPath +"\\"+ "pic.jpg";
            string VoicePath = QuestionPath + "\\" + "voice.mp3";

            if (File.Exists(QuestionPath+ "\\Question.xml") ==true)
            {
                Result = "سوال شما توسط کارشناسان در حال بررسی می باشد، لطفا تا دریافت پاسخ شکیبا باشید";
            }
            else
            {
                Directory.CreateDirectory(QuestionPath.ToString());

                try
                {
                    //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\modulename.txt", modulename);
                    //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\bankname.txt", bankname);
                    //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\message.txt", message);

                    string p = QuestionPath.ToString() + "\\Question.xml";
                    xmlLoad.CreateQuestionFile(p, param);
                }
                catch (Exception e)
                {
                    Result = "S.B_"+e.ToString();
                    //throw;
                }
                

                //$.B _ Get Pic \/
                try
                {
                    //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\log2.txt", "hi");

                    string root = HttpContext.Current.Server.MapPath("~/Question/" + username + "_Field");
                    if (!Directory.Exists(root))
                        Directory.CreateDirectory(root);
                    var provider = new MultipartFormDataStreamProvider(root);
                    await Request.Content.ReadAsMultipartAsync(provider);


                    //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\log2.txt", (provider.FileData[0].LocalFileName).ToString() + "_"+(provider.FileData[1].LocalFileName).ToString());

                    //$.B Get Pic \/
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
                    //$.B Get Pic /\

                    //$.B Get Voice \/
                    localFileName = provider.FileData[1].LocalFileName;
                    mainFileName = Guid.NewGuid().ToString("N") + "." + provider.FileData[1].Headers.ContentDisposition.FileName.Replace("\"", string.Empty).Split('.').Last();
                    targetFileName = localFileName.Replace(localFileName.Split('\\').LastOrDefault(), mainFileName);
                    if (File.Exists(targetFileName))
                        File.Delete(targetFileName);
                    File.Move(localFileName, targetFileName);

                    //$.B \/
                    File.Copy(targetFileName, VoicePath);
                    Thread.Sleep(2000);

                    if (File.Exists(targetFileName))
                        File.Delete(targetFileName);
                    //$.B /\
                    //$.B Get Voice /\


                    //DoSomthings(File.ReadAllBytes(targetFileName));
                }
                catch (Exception ex)
                {
                    GC.Collect();
                    //Log.WriteLog(Log.LogState.Error, Log.LogPackages.Web, ex.Message + Environment.NewLine + ex.StackTrace, "RohamFace", true);
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error has been occurred. It will be resolve about an hour later!");
                }
                //$.B _ Get Pic /\
                Result = "سوال شما دریافت شد، منتظر جواب همکاران باشید";
            //$.B _ Get Voice

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
                    statusMessage = "Request Failed",
                    resultID = 1

                });

        }

    }
}
