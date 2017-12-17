using Services;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controla las paginas relacionadas con el CRUD de países
    /// </summary>
    public class CountryController : Controller
    {
        ConsultasCountry services;

        public CountryController()
        {
            services = new ConsultasCountry();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var country = services.Listar();

            return View(country);
        }

        [HttpPost]
        public ActionResult Create(CountryDto model)
        {
            if (model.CountryID> 0)
            {
                services.Modificar(model);
                ViewBag.Message = "País modificado";
            }
            else
            {
                services.Agregar(model);
                ViewBag.Message = "País agregado";
            }

            var country = services.Listar();
            return View(country);
        }

        public ActionResult Delete(int Id)
        {
            services.Eliminar(Id);

            return View("Create", services.Listar());
        }
    }
}