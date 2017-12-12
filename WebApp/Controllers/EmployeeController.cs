﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;
using Services.Dtos;

namespace WebApp.Controllers
{
    /// <summary>
    /// Contol de las paginas del CRUD de empleados.
    /// </summary>
    public class EmployeeController : Controller
    {
        ConsultasEmployees services;
        ConsultasCountry countryServices;


        public EmployeeController()
        {
            services = new ConsultasEmployees();
            countryServices = new ConsultasCountry();
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
    }
}