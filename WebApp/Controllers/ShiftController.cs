using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controla las paginas relacionadas con los turnos.
    /// </summary>
    public class ShiftController : Controller
    {
        Consultas services = new Consultas();


        //TODO: Consultar y resolver como vamos a agregar los horarios de ingreso y salida
        public ActionResult Shifts()
        {
            return View();
        }

        public ActionResult NightShift()
        {

            //TODO:LISTAR DEBERIA SER POR TURNOS
            var a = services.Listar();

            return View("Shifts", a);
        }

        public ActionResult LateShift()
        {

            var a = services.Listar();
            return View("Shifts", a);
        }

        public ActionResult MorningShift()
        {

            var a = services.Listar();
            return View("Shifts", a);
        }

              
    }
}