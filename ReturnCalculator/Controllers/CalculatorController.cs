using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReturnCalculator.Controllers
{
    public class CalculatorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            ViewBag.Message = "Yo is this actuall working?";
            return View();
        }

        [HttpPost]
        public IActionResult Index(Models.Calculator model)
        {
            model.CalculateReturn();
            return View(model);
        }
    }
}
