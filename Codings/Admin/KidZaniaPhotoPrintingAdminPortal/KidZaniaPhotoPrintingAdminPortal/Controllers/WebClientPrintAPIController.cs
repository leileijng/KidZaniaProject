using Neodynamic.SDK.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidZaniaPhotoPrintingAdminPortal.Controllers
{
    public class WebClientPrintAPIController : Controller
    {
        [AllowAnonymous]
        public void ProcessRequest()
        {
            //get session ID
            string sessionID = (HttpContext.Request["sid"] != null ? HttpContext.Request["sid"] : null);

            //get Query String
            string queryString = HttpContext.Request.Url.Query;

            try
            {
                //Determine and get the Type of Request 
                RequestType prType = WebClientPrint.GetProcessRequestType(queryString);

                if (prType == RequestType.GenPrintScript ||
                    prType == RequestType.GenWcppDetectScript)
                {
                    //Let WebClientPrint to generate the requested script
                    byte[] script = WebClientPrint.GenerateScript(Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme), queryString);

                    HttpContext.Response.ContentType = "text/javascript";
                    HttpContext.Response.BinaryWrite(script);
                    HttpContext.Response.End();
                }
                else if (prType == RequestType.ClientSetWcppVersion)
                {
                    //This request is a ping from the WCPP utility
                    //so store the session ID indicating it has the WCPP installed
                    //also store the WCPP Version if available
                    string wcppVersion = HttpContext.Request["wcppVer"];
                    if (string.IsNullOrEmpty(wcppVersion))
                        wcppVersion = "1.0.0.0";

                    HttpContext.Application.Set(sessionID + "wcppInstalled", wcppVersion);
                }
                else if (prType == RequestType.ClientSetInstalledPrinters)
                {
                    //WCPP Utility is sending the installed printers at client side
                    //so store this info with the specified session ID
                    string printers = HttpContext.Request["printers"];
                    if (string.IsNullOrEmpty(printers) == false)
                        printers = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(printers));

                    HttpContext.Application.Set(sessionID + "printers", printers);

                }
                else if (prType == RequestType.ClientSetInstalledPrintersInfo)
                {
                    //WCPP Utility is sending the client installed printers with detailed info
                    //so store this info with the specified session ID
                    //Printers Info is in JSON format
                    string printersInfo = HttpContext.Request.Params["printersInfoContent"];

                    if (string.IsNullOrEmpty(printersInfo) == false)
                        printersInfo = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(printersInfo));

                    HttpContext.Application.Set(sessionID + "printersInfo", printersInfo);


                }
                else if (prType == RequestType.ClientGetWcppVersion)
                {
                    //return the WCPP version for the specified sid if any
                    bool sidWcppVersion = (HttpContext.Application.Get(sessionID + "wcppInstalled") != null);

                    HttpContext.Response.ContentType = "text/plain";
                    HttpContext.Response.Write((sidWcppVersion ? HttpContext.Application.Get(sessionID + "wcppInstalled") : ""));
                    HttpContext.Response.End();

                }
                else if (prType == RequestType.ClientGetInstalledPrinters)
                {
                    //return the installed printers for the specified sid if any
                    bool sidHasPrinters = (HttpContext.Application.Get(sessionID + "printers") != null);

                    HttpContext.Response.ContentType = "text/plain";
                    HttpContext.Response.Write((sidHasPrinters ? HttpContext.Application.Get(sessionID + "printers") : ""));
                    HttpContext.Response.End();
                }
                else if (prType == RequestType.ClientGetInstalledPrintersInfo)
                {
                    //return the installed printers with detailed info for the specified Session ID (sid) if any
                    bool sidHasPrinters = (HttpContext.Application[sessionID + "printersInfo"] != null);

                    HttpContext.Response.ContentType = "text/plain";
                    HttpContext.Response.Write(sidHasPrinters ? HttpContext.Application[sessionID + "printersInfo"] : "");

                }

            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                HttpContext.Response.ContentType = "text/plain";
                HttpContext.Response.Write(ex.Message + " - StackTrace: " + ex.StackTrace);
                HttpContext.Response.End();
            }
            
        }
    }
}