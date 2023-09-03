using KESKO_task2.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO.MemoryMappedFiles;


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
            char[] message = result.ToCharArray();
            int size = message.Length;
            MemoryMappedFile sharedMemory = MemoryMappedFile.CreateOrOpen("MemoryFile", size * 2 + 4);
            using (MemoryMappedViewAccessor writer = sharedMemory.CreateViewAccessor(0, size * 2 + 4))
            {
                writer.Write(0, size);
                writer.WriteArray<char>(4, message, 0, size);
            }
            Thread.Sleep(5000);
            GetData(model);
            return RedirectToAction("Index");
        }


        public IActionResult GetData(IndexModel model)
        {
            char[] message;
            int size;
            MemoryMappedFile sharedMemory = MemoryMappedFile.OpenExisting("MemoryFile");
            using (MemoryMappedViewAccessor reader = sharedMemory.CreateViewAccessor(0, 4, MemoryMappedFileAccess.Read))
            {
                size = reader.ReadInt32(0);
            }
            using (MemoryMappedViewAccessor reader = sharedMemory.CreateViewAccessor(4, size * 2, MemoryMappedFileAccess.Read))
            {
                message = new char[size];
                reader.ReadArray<char>(0, message, 0, size);
            }
            string charsStr = new string(message);
            HttpContext.Session.SetString("res", charsStr);
            return RedirectToAction("Index");
        }
    }
}