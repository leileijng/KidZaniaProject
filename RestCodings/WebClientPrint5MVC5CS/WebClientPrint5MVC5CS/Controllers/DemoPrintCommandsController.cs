﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Neodynamic.SDK.Web;

namespace WCPMVCCS.Controllers
{
    public class DemoPrintCommandsController : Controller
    {
        // GET: DemoPrintCommands
        public ActionResult Index()
        {
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme), Url.Action("PrintCommands", "DemoPrintCommands", null, HttpContext.Request.Url.Scheme), HttpContext.Session.SessionID);

            return View();
        }

        //sid: user session id who is requesting a ClientPrintJob
        [AllowAnonymous]
        public void PrintCommands(string sid)
        {
            if (WebClientPrint.ProcessPrintJob(System.Web.HttpContext.Current.Request.Url.Query))
            {
                HttpApplicationStateBase app = HttpContext.Application;

                //Create a ClientPrintJob obj that will be processed at the client side by the WCPP
                ClientPrintJob cpj = new ClientPrintJob();

                //get printer commands for this user id
                object printerCommands = app[sid + PRINTER_COMMANDS];
                if (printerCommands != null)
                {
                    cpj.PrinterCommands = printerCommands.ToString();
                    cpj.FormatHexValues = true;
                }

                //get printer settings for this user id
                int printerTypeId = (int)app[sid + PRINTER_ID];

                if (printerTypeId == 0) //use default printer
                {
                    cpj.ClientPrinter = new DefaultPrinter();
                }
                else if (printerTypeId == 1) //show print dialog
                {
                    cpj.ClientPrinter = new UserSelectedPrinter();
                }
                else if (printerTypeId == 2) //use specified installed printer
                {
                    cpj.ClientPrinter = new InstalledPrinter(app[sid + INSTALLED_PRINTER_NAME].ToString());
                }
                else if (printerTypeId == 3) //use IP-Ethernet printer
                {
                    cpj.ClientPrinter = new NetworkPrinter(app[sid + NET_PRINTER_HOST].ToString(), int.Parse(app[sid + NET_PRINTER_PORT].ToString()));
                }
                else if (printerTypeId == 4) //use Parallel Port printer
                {
                    cpj.ClientPrinter = new ParallelPortPrinter(app[sid + PARALLEL_PORT].ToString());
                }
                else if (printerTypeId == 5) //use Serial Port printer
                {
                    cpj.ClientPrinter = new SerialPortPrinter(app[sid + SERIAL_PORT].ToString(),
                                                              int.Parse(app[sid + SERIAL_PORT_BAUDS].ToString()),
                                                              (SerialPortParity)Enum.Parse(typeof(SerialPortParity), app[sid + SERIAL_PORT_PARITY].ToString()),
                                                              (SerialPortStopBits)Enum.Parse(typeof(SerialPortStopBits), app[sid + SERIAL_PORT_STOP_BITS].ToString()),
                                                              int.Parse(app[sid + SERIAL_PORT_DATA_BITS].ToString()),
                                                              (SerialPortHandshake)Enum.Parse(typeof(SerialPortHandshake), app[sid + SERIAL_PORT_FLOW_CONTROL].ToString()));
                }

                //Send ClientPrintJob back to the client
                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.BinaryWrite(cpj.GetContent());
                System.Web.HttpContext.Current.Response.End();

            }

        }


        const string PRINTER_ID = "-PID";
        const string INSTALLED_PRINTER_NAME = "-InstalledPrinterName";
        const string NET_PRINTER_HOST = "-NetPrinterHost";
        const string NET_PRINTER_PORT = "-NetPrinterPort";
        const string PARALLEL_PORT = "-ParallelPort";
        const string SERIAL_PORT = "-SerialPort";
        const string SERIAL_PORT_BAUDS = "-SerialPortBauds";
        const string SERIAL_PORT_DATA_BITS = "-SerialPortDataBits";
        const string SERIAL_PORT_STOP_BITS = "-SerialPortStopBits";
        const string SERIAL_PORT_PARITY = "-SerialPortParity";
        const string SERIAL_PORT_FLOW_CONTROL = "-SerialPortFlowControl";
        const string PRINTER_COMMANDS = "-PrinterCommands";

        [AllowAnonymous]
        [HttpPost]
        public void ClientPrinterSettings(string sid,
                                             string pid,
                                             string installedPrinterName,
                                             string netPrinterHost,
                                             string netPrinterPort,
                                             string parallelPort,
                                             string serialPort,
                                             string serialPortBauds,
                                             string serialPortDataBits,
                                             string serialPortStopBits,
                                             string serialPortParity,
                                             string serialPortFlowControl,
                                             string printerCommands)
        {
            try
            {
                HttpApplicationStateBase app = HttpContext.Application;

                //save settings in the global Application obj

                //save the type of printer selected by the user
                int i = int.Parse(pid);
                app[sid + PRINTER_ID] = i;

                if (i == 2)
                {
                    app[sid + INSTALLED_PRINTER_NAME] = installedPrinterName;
                }
                else if (i == 3)
                {
                    app[sid + NET_PRINTER_HOST] = netPrinterHost;
                    app[sid + NET_PRINTER_PORT] = netPrinterPort;
                }
                else if (i == 4)
                {
                    app[sid + PARALLEL_PORT] = parallelPort;
                }
                else if (i == 5)
                {
                    app[sid + SERIAL_PORT] = serialPort;
                    app[sid + SERIAL_PORT_BAUDS] = serialPortBauds;
                    app[sid + SERIAL_PORT_DATA_BITS] = serialPortDataBits;
                    app[sid + SERIAL_PORT_FLOW_CONTROL] = serialPortFlowControl;
                    app[sid + SERIAL_PORT_PARITY] = serialPortParity;
                    app[sid + SERIAL_PORT_STOP_BITS] = serialPortStopBits;
                }

                //save the printer commands specified by the user
                app[sid + PRINTER_COMMANDS] = printerCommands;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}