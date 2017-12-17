using Services;
using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controla las paginas relacionadas con los turnos.
    /// </summary>
    public class ShiftController : Controller
    {
        ConsultasEmployees employeeServices;
        ConsultasHorarios horarioServices;

        public ShiftController()
        {
            employeeServices = new ConsultasEmployees();
            horarioServices = new ConsultasHorarios();
        }

        
        public ActionResult Shifts()
        {
            return View();
        }

        public ActionResult NightShift()
        {
            var list = new List<TurnModel>();
            var listEmp = employeeServices.ListarPorTurno(2);
            var a = horarioServices.ListarPorTurno(2);
            foreach (var e in listEmp)
            {
                var model = new TurnModel();
                model.EmployeeId = e.Id;
                model.Name = e.Name + " " + e.Surname;
                if (a.FirstOrDefault(c => c.EmployeeId == e.Id) != null)
                {
                    model.CheckIn = a.FirstOrDefault(c => c.EmployeeId == e.Id).StartlHour;
                    model.CheckOut = a.FirstOrDefault(c => c.EmployeeId == e.Id).FinishHour;
                }
                else
                {
                    model.CheckIn = null;
                    model.CheckOut = null;
                }
                list.Add(model);
            }

            return View("Shifts", list);
        }

        public ActionResult LateShift()
        {
            var list = new List<TurnModel>();
            var listEmp = employeeServices.ListarPorTurno(1);
            var a = horarioServices.ListarPorTurno(1);

            foreach (var e in listEmp)
            {
                var model = new TurnModel();
                model.EmployeeId = e.Id;
                model.Name = e.Name + " " + e.Surname;
                if(a.FirstOrDefault(c => c.EmployeeId == e.Id) != null)
                {
                    model.CheckIn = a.FirstOrDefault(c => c.EmployeeId == e.Id).StartlHour;
                    model.CheckOut = a.FirstOrDefault(c => c.EmployeeId == e.Id).FinishHour;
                } else
                {
                    model.CheckIn = null;
                    model.CheckOut = null;
                }
                list.Add(model);

            }
            return View("Shifts", list);
        }

        public ActionResult MorningShift()
        {
            var list = new List<TurnModel>();
            var listEmp = employeeServices.ListarPorTurno(0);
            var a = horarioServices.ListarPorTurno(0);
            foreach (var e in listEmp)
            {
                var model = new TurnModel();
                model.EmployeeId = e.Id;
                model.Name = e.Name + " " + e.Surname;
                if (a.FirstOrDefault(c => c.EmployeeId == e.Id) != null)
                {
                    model.CheckIn = a.FirstOrDefault(c => c.EmployeeId == e.Id).StartlHour;
                    model.CheckOut = a.FirstOrDefault(c => c.EmployeeId == e.Id).FinishHour;
                } else
                {
                    model.CheckIn = null;
                    model.CheckOut = null;
                }
                list.Add(model);
            }
            return View("Shifts", list);
        }

        
        public ActionResult NewHourIn(int Id)
        {
            var h = horarioServices.GetToday(Id);
            if (h == null)
            {
                h = new HorariosDto
                {
                    Id = 0,
                    EmployeeId = Id,
                    StartlHour = new DateTime()
                };
            }
            return View(h);
        }

        [HttpPost]
        public ActionResult NewHourIn(int Id, int employeeId, DateTime checkIn, DateTime? checkOut)
        {

            var model = new HorariosDto
            {
                EmployeeId = employeeId,
                StartlHour = checkIn,
                FinishHour = checkOut
            };

            horarioServices.Add(model);

            return View("Shifts");
        }
    }
}