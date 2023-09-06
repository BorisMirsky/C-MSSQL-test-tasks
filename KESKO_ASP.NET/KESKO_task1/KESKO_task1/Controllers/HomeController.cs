using KESKO_task1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using IWshRuntimeLibrary;


namespace KESKO_task1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RunProcess()
        {

            WshShell shell = new WshShell();
            shell.Run("notepad", 1, true);
            return RedirectToAction("Index");
        }

    }
}