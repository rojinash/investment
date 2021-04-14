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
            return View();
        }

        [HttpPost]
        public IActionResult Index(Models.Calculator model)
        {
            if (ModelState.IsValid)
            {
                model.CalculateReturn();
                if (model.RateOfReturn == 0)
                {
                    return View("Error");
                }
                ViewData["TimeAmountArr"] = model.TimeAmountArr;
                ViewData["RateOfReturn"] = model.RateOfReturn;

            }
            return View(model);
            
        }
    }
}
