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
            var list = GetListModel(2);

            return View("Shifts", list);
        }

        public ActionResult LateShift()
        {
            var list = GetListModel(1);
            return View("Shifts", list);
        }

        public ActionResult MorningShift()
        {
            var list = GetListModel(0);
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
                    StartlHour = DateTime.Now
                };
            }
            return View(h);
        }

        [HttpPost]
        public ActionResult NewHourIn(int Id, int employeeId,DateTime date, DateTime checkIn, DateTime? checkOut)
        {
            var cIn = new DateTime(date.Year, date.Month, date.Day, checkIn.Hour, checkIn.Minute, checkIn.Second );
            DateTime? cOut;
            if (checkOut != null)
            {
                cOut = new DateTime(date.Year, date.Month, date.Day, checkOut.Value.Hour, checkOut.Value.Minute, checkOut.Value.Second);
            }
            else
            {
                cOut = checkOut;
            }

            var model = new HorariosDto
            {
                EmployeeId = employeeId,
                StartlHour = cIn,
                FinishHour = cOut
            };

            horarioServices.Add(model);

            return View("Shifts");
        }

        public List<TurnModel> GetListModel (int turn)
        {
            var list = new List<TurnModel>();
            var listEmp = employeeServices.ListarPorTurno(turn);
            var a = horarioServices.ListarPorTurno(turn);
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

            return list;
        }
    }
}