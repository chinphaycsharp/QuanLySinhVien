using PagedList;
using QuanLySinhVien.Common;
using QuanLySinhVien.Service;
using QuanLySinhVien.ViewModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySinhVien.Controllers
{
    public class HomeController : BaseController
    {
        private readonly LoginService _LoginService;
        private readonly ViolateEmployeeService _violateEmployeeService;

        public HomeController()
        {
            _LoginService = new LoginService();
            _violateEmployeeService = new ViolateEmployeeService();
        }

        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var students = _violateEmployeeService.GetStudentViolates();
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.StudentName.Contains(searchString)).ToList();
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (TempData["result"] != null)
            {
                ViewBag.Success = TempData["result"];
            }
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetAllAccounts(string currentFilter = null, string searchString = null, int? page = null)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var accounts = _LoginService.GetLogins();
            if (!String.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(s => s.UserName.Contains(searchString)).ToList();
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (TempData["result"] != null)
            {
                ViewBag.Success = TempData["result"];
            }
            return View(accounts.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Logout()
        {
            if (Session[commonConst.user_session] != null)
            {
                Session[commonConst.user_session] = null;
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public ActionResult AddAccount() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAccount(AddLoginViewModel viewModel) 
        {
            if (ModelState.IsValid)
            {
                int result = _LoginService.AddAccount(viewModel);
                if(result > 0)
                {
                    return RedirectToAction("GetAllAccounts", "Home");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditAccount(int id)
        {
            var account = _LoginService.GetLoginById(id);
            EditLoginViewModel result = new EditLoginViewModel()
            {
                Id = account.Id,
                Email = account.Email,
                UserName = account.UserName,
                PassWord = ""
            };
            return View(result);
        }

        [HttpPost]
        public ActionResult EditAccount(EditLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int result = _LoginService.UpdateAccount(viewModel);
                if (result > 0)
                {
                    return RedirectToAction("GetAllAccounts", "Home");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult DeleteAccount(int id)
        {
            int result = _LoginService.DeleteAccount(id);
            if(result > 0)
            {
                return RedirectToAction("GetAllAccounts");
            }
            return View();
        }
    }
}