using KESKO_task2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net.Sockets;


namespace KESKO_task2.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PassData(IndexModel model)
        {
            string NumberI = model.NumberI;
            string NumberJ = model.NumberJ;
            string result = (NumberI + " " + NumberJ);
            SendData(result);
            return RedirectToAction("Index");
        }

        public IActionResult SendData(string data)
        {
            using TcpClient tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 8888);
            var stream = tcpClient.GetStream();
            byte[] data1 = Encoding.UTF8.GetBytes(data + '\n');
            stream.Write(data1);
            // get data back
            string SessionValue;
            byte[] data2 = new byte[1024];  
            int bytes = stream.Read(data2);
            SessionValue = Encoding.UTF8.GetString(data2, 0, bytes);
            HttpContext.Session.SetString("Result", SessionValue);
            return RedirectToAction("Index");
        }
    }
}