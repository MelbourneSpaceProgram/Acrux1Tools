using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acrux1Tools.Models.Fec;
using Microsoft.AspNetCore.Mvc;

namespace Acrux1Tools.Controllers
{
    public class FecController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FecDecode()
        {
            return View();
        }


        public IActionResult FecDecodePost(FecDecodeViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}