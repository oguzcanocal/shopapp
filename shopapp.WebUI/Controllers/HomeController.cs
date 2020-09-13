using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shopapp.Common.Dto;


namespace shopapp.WebUI.Controllers
{
    public class HomeController : BaseController
    {

        public IActionResult Index()
        {
            ProductListModel model = GetApiResult<ProductListModel>("/home/index");
            return View(model);
        }
    }
}
