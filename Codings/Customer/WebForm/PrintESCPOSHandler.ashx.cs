using Neodynamic.SDK.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForm
{
    /// <summary>
    /// Summary description for PrintESCPOSHandler
    /// </summary>
    public class PrintESCPOSHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (WebClientPrint.ProcessPrintJob(context.Request.Url.Query))
            {
                //bool useDefaultPrinter = (context.Request["useDefaultPrinter"] == "checked");
                string printerName = context.Server.UrlDecode(context.Request["printerName"]);

                bool useDefaultPrinter = true;

                string summary = context.Server.UrlDecode(context.Request["summary"]);
                //Create ESC/POS commands for sample receipt

                string[] summary_array = summary.Split('|');
                string usercode = summary_array[0];
                string amt = summary_array[1];
                string dc = summary_array[2];
                string ec = summary_array[3];
                string a5 = summary_array[4];
                string mg = summary_array[5];
                string kc = summary_array[6];
                string lr = summary_array[7];
                string s_today = DateTime.Now.ToString("MM/dd/yyyy h:mm tt"); // As String
                string GS = "0x1D"; //GS byte in hex notation
                string ESC = "0x1B"; //ESC byte in hex notation
                string NewLine = "0x0A"; //LF byte in hex notation
                string cmds = ESC + "@"; //Initializes the printer (ESC @)
                cmds += ESC + "!" + "0x76"; //Emphasized + Double-height + Double-width mode selected (ESC ! (8 + 16 + 32)) 56 dec => 38 hex
                cmds += "       " + usercode; //text to print
                cmds += NewLine + NewLine;
                cmds += ESC + "!" + "0x33"; //Smaller size
                cmds += "     KidZania Singapore";
                cmds += NewLine + NewLine;
                cmds += ESC + "!" + "0x00"; //Character font A selected (ESC ! 0)
                cmds += "------------------------------------------";
                cmds += NewLine;
                cmds += "        Photo Collection Ticket";
                cmds += NewLine;
                cmds += "------------------------------------------";
                cmds += NewLine;
                cmds += "     Date: " + s_today;
                cmds += NewLine + NewLine;
                if (dc != "0")
                {
                    cmds += "     Digital copy: " + dc;
                    cmds += NewLine;
                }
                if (ec != "0")
                {
                    cmds += "     Establishment Card: " + ec;
                    cmds += NewLine;
                }
                if (a5 != "0")
                {
                    cmds += "     Hardcopy w/Folder: " + a5;
                    cmds += NewLine;
                }
                if (mg != "0")
                {
                    cmds += "     Magnet: " + mg;
                    cmds += NewLine;
                }
                if (kc != "0")
                {
                    cmds += "     Keychains: " + kc;
                    cmds += NewLine;
                }
                if (lr != "0")
                {
                    cmds += "     Leatherette: " + lr;
                    cmds += NewLine;
                }
                cmds += "------------------------------------------";
                cmds += NewLine;
                cmds += "                              Total Amount";
                cmds += NewLine;
                cmds += "                              $" + amt;
                cmds += NewLine + NewLine;
                cmds += "    All prices are inclusive of 7% GST    ";
                cmds += NewLine + NewLine;
                cmds += NewLine + NewLine;
                if (dc != "0")
                {
                    cmds += "    Your digital copy will be sent to     ";
                    cmds += "              your email.                 ";
                    cmds += NewLine;
                    cmds += " You can download them at the below link: ";
                    cmds += "       https://tinyurl.com/qkhbtdq        ";
                    cmds += NewLine;
                    cmds += "  Code is valid for 7 days from date of   ";
                    cmds += "               purchased.                 ";
                    cmds += NewLine + NewLine;
                    cmds += "  For further assistance please email at  ";
                    cmds += "         moments@kidzania.com.sg          ";
                }
                cmds += NewLine + NewLine;
                cmds += NewLine + NewLine;
                cmds += NewLine + NewLine;
                cmds += NewLine + NewLine;
                cmds += NewLine + NewLine;
                cmds += "0x1D0x560x00";
                cmds += NewLine + NewLine;

                //Set license info...
                WebClientPrint.LicenseOwner = "..."; //Removed by Chris. Only show in production side.
                WebClientPrint.LicenseKey = "..."; //Removed by Chris. Only show in production side.

                //Create a ClientPrintJob and send it back to the client!
                ClientPrintJob cpj = new ClientPrintJob();
                //set  ESCPOS commands to print...
                cpj.PrinterCommands = cmds + cmds; //print twice
                cpj.FormatHexValues = true;

                //set client printer...
                if (useDefaultPrinter || printerName == "null")
                    cpj.ClientPrinter = new DefaultPrinter();
                else
                    cpj.ClientPrinter = new InstalledPrinter(printerName);

                //send it... to thermal printer           
                context.Response.ContentType = "application/octet-stream";
                context.Response.BinaryWrite(cpj.GetContent());
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}