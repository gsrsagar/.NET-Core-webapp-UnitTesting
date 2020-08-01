using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SocialMediaLinkedIn.Models;
using SocialMediaLinkedIn.ViewModels;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialMediaLinkedIn.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
   
        private readonly EmployeeRepository _emp;
        private readonly IWebHostEnvironment hostingEnvironment;

        public HomeController(EmployeeRepository emp , IWebHostEnvironment hostingEnvironment)
        {
            _emp = emp;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult Index(int? id)
        {
            Employee employee = _emp.getById(id??1);
            if(employee==null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.GetValueOrDefault());
            }
            
            ViewModelHomeIndex viewModelHomeIndex = new ViewModelHomeIndex()
            {
                Employee = employee,
                PageTitle = "EmployeeDetails"

            };
            /*Employee model = _emp.getById(2);
            ViewBag.Employee = model;
            ViewBag.PageTitle = "Employee Details";*/
            return View(viewModelHomeIndex);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Employee employee = _emp.DeleteEmployee(id);
            return RedirectToAction("Details");
        }




        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                string uniqueFilename = null;
                if(model.Photo!=null)
                {
                    string uploadsFolder= Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFilename= Guid.NewGuid().ToString()+" _ "+ model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));               
                }
                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    Photopath = uniqueFilename
                };
                _emp.EmpSave(newEmployee);
                return RedirectToAction("index", new { id = newEmployee.Id });
            }
            return View();
        }







        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _emp.getById(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotopath = employee.Photopath

            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                Employee employee = _emp.getById(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if(model.Photo!=null)
                {
                    string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotopath);
                    System.IO.File.Delete(filePath);
               
                }
                employee.Photopath = ProcessUploaderFile(model); // To update the updated photo in the model calling common method for edit and create for photopath property
             
                Employee UpdatedEmployee = _emp.UpdateEmployee(employee);
                return RedirectToAction("index", new { id = UpdatedEmployee.Id });
            }
            return View(model);
        }

        private string ProcessUploaderFile(EmployeeCreateViewModel model)
        {
            string uniqueFilename = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFilename = Guid.NewGuid().ToString() + " _ " + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFilename);
                using (var fileStream =new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFilename;
        }


        [HttpGet]
        public ViewResult Create()
        {
            
            return View();
        }



        public ViewResult Details()
        {
            var model = _emp.GetEmployees();
            return View(model);
        }
    }
}
