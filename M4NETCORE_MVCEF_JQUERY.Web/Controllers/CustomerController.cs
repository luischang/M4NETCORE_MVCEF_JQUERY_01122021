using M4NETCORE_MVCEF_JQUERY.Web.Models;
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

        public async Task<IActionResult> Eliminar(int idCliente)
        {
            var exito = await CustomerRepo.Delete(idCliente);
            return Json(exito);
        }

        public async Task<IActionResult> Obtener(int idCliente)
        {
            var customer = await CustomerRepo.GetCustomerById(idCliente);
            return Json(customer);
        }

        public async Task<IActionResult> Listado()
        {
            var customers = await CustomerRepo.GetCustomersAsync();
            return PartialView(customers);
        }

        [HttpPost]
        public async Task<IActionResult> Grabar(int idCliente, string nombres, string apellidos, string pais,
                                                string ciudad, string telefono)
        {
            bool exito = true;

            var customer = new Customer()
            {
                FirstName = nombres,
                LastName = apellidos,
                Country = pais,
                City = ciudad,
                Phone = telefono
            };

            if (idCliente == -1)
            {
                //Es un nuevo cliente
                exito = await CustomerRepo.Insert(customer);
            }
            else
            {
                //Es una actualización
                customer.Id = idCliente;
                exito = await CustomerRepo.Update(customer);
            }

            return Json(exito);
        }



    }
}
