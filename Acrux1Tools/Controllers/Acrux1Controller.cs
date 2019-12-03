using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acrux1Tools.Web.Models.Acrux1;
using Microsoft.AspNetCore.Mvc;

namespace Acrux1Tools.Web.Controllers
{
    public class Acrux1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Decode()
        {
            return View();
        }

        [HttpPost]
        [ActionName(nameof(Decode))]
        public IActionResult DecodePost(DecodeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Error = "Input errors";
                return View(viewModel);
            }

            viewModel.Error = null;



            return View(viewModel);
        }
    }
}