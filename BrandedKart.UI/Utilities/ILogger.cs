

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using RequestContext = System.Web.HttpContext;

namespace BrandedKart.UI.CommonTasks
{
    //Following Singleton Design Pattern (Creational)
    //Sealed to avoid inheritance by nested class
    sealed class Logger
    {
        private Logger()
        {

        }

        private static Lazy<Logger> instance = new Lazy<Logger>(() => new Logger());
        public static Logger GetInstance
        {
            get
            {
                if (instance is null)
                {
                    return new Logger();
                }
                return instance.Value;
            }
        }


        public void LogException(Exception exception)
        {
            var line = Environment.NewLine + Environment.NewLine;
            string ErrorlineNo, Errormsg, exceptionType, exceptionUrl, ErrorLocation;

            ErrorlineNo = exception.StackTrace.Substring(exception.StackTrace.Length - 7, 7);
            ErrorLocation = exception.Message.ToString();
            exceptionType = exception.GetType().ToString();
            Errormsg = exception.GetType().Name.ToString();
            exceptionUrl = RequestContext.Current.Request.Url.ToString();

            try
            {
                string filePath = RequestContext.Current.Server.MapPath("~/ExceptionDetailsFile/");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                filePath = filePath + DateTime.Today.ToString("dd-MM-yy") + ".txt";
                if (!File.Exists(filePath))
                {

                    File.Create(filePath).Dispose();

                }

                //Using calls Dispose which will always Close the stream,
                //and Close will Flush the stream (Incase of exception too)
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + exceptionType + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exceptionUrl + line;
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(line);
                    sw.WriteLine(error);
                    sw.WriteLine("--------------------------------*End*------------------------------------------");
                    sw.WriteLine(line);
                    //sw.Flush();
                    //sw.Close();
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }


        }
    }
}