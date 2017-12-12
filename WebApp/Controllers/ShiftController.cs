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
        ConsultasEmployees services = new ConsultasEmployees();


        //TODO: Consultar y resolver como vamos a agregar los horarios de ingreso y salida
        public ActionResult Shifts()
        {
            return View();
        }

        public ActionResult NightShift()
        {
            
            var a = services.ListarPorTurno(2);

            return View("Shifts", a);
        }

        public ActionResult LateShift()
        {

            var a = services.ListarPorTurno(1);
            return View("Shifts", a);
        }

        public ActionResult MorningShift()
        {

            var a = services.ListarPorTurno(0);
            return View("Shifts", a);
        }

        //[HttpPost]
        //public ActionResult ChangeHour()
        //{

        //}      

    }
}