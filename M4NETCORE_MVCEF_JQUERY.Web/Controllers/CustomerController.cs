using M4NETCORE_MVCEF_JQUERY.Web.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M4NETCORE_MVCEF_JQUERY.Web.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Listado()
        {
            var customers = await CustomerRepo.GetCustomersAsync();
            return PartialView(customers);
        }
    }
}
