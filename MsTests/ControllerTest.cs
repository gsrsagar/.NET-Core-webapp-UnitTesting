using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SocialMediaLinkedIn.Controllers;
using SocialMediaLinkedIn.Models;
using SocialMediaLinkedIn.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MsTests
{
    [TestClass]
    public class ControllerTest
    {
        HomeController _controller;
        AccountController _accountController;
        EmployeeRepository _Repository;
        IWebHostEnvironment HostingEnvironment;
        private  Employee _employee;
        private readonly IFormFile photo;
        private readonly LoginViewModel loginViewModel;
        private EmployeeEditViewModel _employeeEditViewModel;
        private readonly UserManager<IdentityUser> userManager;
        private IdentityUser _identityUser;
        private  SignInManager<IdentityUser> signInManager;
        private SQLEmployeeRepositorycs SqlRepo;

        public ControllerTest()
        {          
            _Repository = new IEmployeeRepository();
            _controller = new HomeController(_Repository,HostingEnvironment);
            _accountController = new AccountController(userManager,SqlRepo,signInManager);
             _employee = new Employee()
            {
                Id = 1,
                Name = "Sagar",
                Email = "sagar.guvvala@gmail.com",
                Department = Dept.Payroll,
                Photopath = "sdsgabasgsdbasbasdbadbsdb"
            };
            loginViewModel = new LoginViewModel()
            {
                Email = "sagar.guvvala@gmail.com",
                Password = "@Sagarreddy123",
                RememberMe = true
            };
            _identityUser = new IdentityUser()
            {
                Email = "sagar.guvvala@gmail.com",
                UserName = "sagar.guvvala@gmail.com"
            };
            _employeeEditViewModel = new EmployeeEditViewModel()
            {
                Id = 1,
                ExistingPhotopath = "asdawfagagaga_2e9242"
            };
            
        }

        [TestMethod]
        public void Index_ViewResult()
        {   
            int id = _employee.Id;
            ViewResult result = _controller.Index(id);
            ViewResult expected=null;
            ViewModelHomeIndex vi=null;
            //Assert.IsInstanceOfType(expected, (Type)result);
            //var viewResult = Assert.IsInstanceOfType(expected, (Type)result);
            //var viewResult = Assert.AreSame<ViewResult>(result);
            Assert.IsInstanceOfType(vi, (Type)result.ViewData.Model);
            Assert.IsNotNull(result);
           
            
        }
        [TestMethod]
        public void Delete_Employee_ActionResult()
        {
            var result = _controller.Delete(_employee.Id);
            var OkResult = result as OkObjectResult;
            Assert.AreEqual(200,OkResult.StatusCode);
        }
        [TestMethod]
        public void Create_EmployeeCreate_ViewModel()
        {
            Employee emp = _employee;
            var empc = new EmployeeCreateViewModel()
            {
                Name = _employee.Name,
                Email = _employee.Email,
                Department = _employee.Department,
                Photo = { }       
            };
            var result = _controller.Create(empc);
            var okresult = result as OkObjectResult;
            Assert.AreEqual(200,okresult.StatusCode);
        }
        [TestMethod]
        public void Login_model_Validate()
        {
            var result = _accountController.Login(loginViewModel);
            
            Assert.AreEqual(200, okresult.StatusCode);
        }

        /*[Fact]

           public void ListAllUsers_Validate()
           {
               var result = _accountController.List();
               Assert.NotNull(result);
               Assert.IsAssignableFrom<IQueryable<IdentityUser>>(result);
               *//*IQueryable<IdentityUser> listusers = IQueryable<IdentityUser>(result);*//*
               // Assert.NotNull(result);
               //Assert.IsType<IActionResult>(result);
               *//* var model = Assert.IsAssignableFrom<IQueryable>(result);*//*

           }*/


        [TestMethod]
        public void Logout_Validate()
        {
            IAsyncResult result= _accountController.Logout();
            //Assert.IsAssignableFrom<IAsyncResult>(result);
        }
        [TestMethod]
        public void RegisterUser_Validate()
        {
            RegisterViewModel model;
            model = new RegisterViewModel()
            {
                Email = _identityUser.Email,
                Password = _identityUser.PasswordHash,
                ConfirmPassword = _identityUser.PasswordHash
            };
            IAsyncResult result = _accountController.Register(model);
            //Assert.IsAssignableFrom<IAsyncResult>(result);
        }

        [TestMethod]
        public void Register_Get_Validate()
        {
            var result = _accountController.Register();
            //Assert.IsAssignableFrom<IActionResult>(result);
        }

        [TestMethod]
        public void Login_Get_Validate()
        {
            IActionResult result;
            result = _accountController.Login();
           // Assert.NotNull(result);
           // Assert.IsAssignableFrom<IActionResult>(result);
        }
        [TestMethod]
        public void Edit_Get_Validate()
        {
            var result = _controller.Edit(1);
           // Assert.IsAssignableFrom<ViewResult>(result);
        }

        [TestMethod]
        public void Employee_Edit_View_Validate()
        {

            var result = _controller.Edit(_employeeEditViewModel);
           // Assert.IsAssignableFrom<IActionResult>(result);
        }

        [TestMethod]
        public void Details_Validate()
        {
            var result = _controller.Details();
           // Assert.IsAssignableFrom<IActionResult>(result);

        }

        [TestMethod]
        public void Create_Validate()
        {
            var result = _controller.Create();
            //Assert.IsAssignableFrom<ViewResult>(result);

        }


    }
}























/*var result = controller.Index();

           // Assert
           var viewResult = Assert.IsType<ViewResult>(result);
           var model = Assert.IsAssignableFrom<List<Employee>>(
               viewResult.ViewData.Model);
           Assert.Equal(2, model.Count());*/
