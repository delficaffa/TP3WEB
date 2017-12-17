using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;
using Services.Dtos;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Contol de las paginas del CRUD de empleados.
    /// </summary>
    public class EmployeeController : Controller
    {
        ConsultasEmployees services;
        ConsultasCountry countryServices;
        ConsultasHorarios horarioServices;


        public EmployeeController()
        {
            services = new ConsultasEmployees();
            countryServices = new ConsultasCountry();
            horarioServices = new ConsultasHorarios();
        }


        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Employees()
        {
            return View(services.Listar());
        }

        [HttpGet]
        public ActionResult Create()
        {
            var emp = new EmployeeDto();
            ViewBag.Countries = countryServices.Listar();
            return View(emp);
        }

        [HttpPost]
        public ActionResult Create(EmployeeDto model)
        {
            if (model.Id > 0)
            {
                services.Modificar(model);
            }
            else
            {
                services.Agregar(model);
            }
            return View("Employees", services.Listar());
        }

        [HttpGet]
        public ActionResult Modify(int Id)
        {
            var emp = services.Listar().FirstOrDefault(c => c.Id == Id);
            if (emp != null)
            {
                ViewBag.Countries = countryServices.Listar();
                return View("Create", emp);
            }
            else
            {
                return View("Employees", services.Listar());
            }
        }

        public ActionResult Delete(int Id)
        {
            services.Eliminar(Id);

            return View("Employees", services.Listar());
        }

        public ActionResult Salary(int id)
        {
            var list = GetModel(id, DateTime.Now.Month, DateTime.Now.Year);
            return View(list);
        }

        [HttpPost]
        public ActionResult Salary(int id, string date)
        {
            var dateSplit = date.Split('-');
            var month = int.Parse(dateSplit[1]);
            var year = int.Parse(dateSplit[0]);
            var a = GetModel(id, month, year);
            return View(a);
        }

        public List<TurnModel> GetModel(int id, int month, int year)
        {
            var emp = services.GetById(id);
            var list = new List<TurnModel>();

            foreach (var item in horarioServices.ListarHorariosDeEmpleado(id, month, year))
            {
                var n = new TurnModel();
                n.EmployeeId = emp.Id;
                n.Name = emp.Name + " " + emp.Surname;
                n.CheckIn = item.StartlHour;
                n.CheckOut = item.FinishHour;
                n.Price = emp.Price;
                list.Add(n);
            }

            if (list.Count == 0)
            {
                list.Add(new TurnModel
                {
                    EmployeeId = emp.Id,
                    Name = emp.Name + " " + emp.Surname,
                    CheckIn = null,
                    CheckOut = null,
                    Price = emp.Price
                });
            }
            return list;
        }
    }
}