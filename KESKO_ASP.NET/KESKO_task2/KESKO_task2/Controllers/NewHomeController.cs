using KESKO_task2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Pipes;
using System.Diagnostics;
using System.IO;
//using System.Windows.Forms;
using System.Threading;
using System.Security.Principal;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace KESKO_task2.Controllers
{
    public class NewHomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult PassData(IndexModel model)
          //public void PassData(IndexModel model)
        {
            string NumberI = model.NumberI;
            string NumberJ = model.NumberJ;
            string result = (NumberI + " " + NumberJ);
            SendData(result);
            //char[] message = result.ToCharArray();
            //int size = message.Length;
            //GetData(model);
            return RedirectToAction("Index");
        }

        private async Task SendData(string data)
        {
            var tcpListener = new TcpListener(IPAddress.Any, 8888);
            try
            {
                tcpListener.Start();    
                while (true)
                {
                    using var tcpClient = await tcpListener.AcceptTcpClientAsync();
                    var stream = tcpClient.GetStream();
                    byte[] DataToByte = Encoding.UTF8.GetBytes(data); 
                    await stream.WriteAsync(DataToByte);
                }
            }
            finally
            {
                tcpListener.Stop();
            }
        }
    }
}
