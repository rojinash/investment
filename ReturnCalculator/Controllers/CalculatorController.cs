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
            if (!ModelState.IsValid)
            {
                ModelState.Remove("AnnualContribution");
                ModelState.Remove("Inflation");
            }
            if (ModelState.IsValid)
            {
                model.CalculateReturn();
                ViewData["TimeAmountArr"] = model.timeAmountArr;
                ViewData["RateOfReturn"] = model.RateOfReturn;

            }
            return View(model);
        }
    }
}
