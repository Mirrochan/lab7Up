﻿using Lab_7.Interfaces;
using Lab_7.Mocks;
using Lab_7.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab_7.Controllers
{
    public class AddNewEmployeeController : Controller
    {
        private readonly IEmployees _employeesService;

        public AddNewEmployeeController(IEmployees employeesService)
        {
            _employeesService = employeesService;
        }

      
        public IActionResult List()
        {
            var employees = _employeesService.employees; 
            return View(employees);
        }

      
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(Employees newEmployee)
        {
            var employeeList = (_employeesService as MockEmployees).employees;
            if (ModelState.IsValid)
            {
                newEmployee.Id = employeeList.Any() ? employeeList.Max(x => x.Id) + 1 : 1;
               
                (_employeesService as MockEmployees)?.AddEmployee(newEmployee);

                
                return RedirectToAction("List", "AllEmployees");
            }
            return View(newEmployee);
        }

    }
}
