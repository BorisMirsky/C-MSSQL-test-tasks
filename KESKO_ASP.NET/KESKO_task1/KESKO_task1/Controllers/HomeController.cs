using KESKO_task1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KESKO_task1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // ----------------------------- Task1 -------------------------------
        public IActionResult RunProcess()
        {
            Process.Start("notepad.exe");
            return RedirectToAction("Index");
        }

    }
}