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
    public class HomeController : Controller
    {
        private readonly LoginService _LoginService;

        public HomeController(LoginService LoginService)
        {
            _LoginService = LoginService;
        }

        public ActionResult GetAllAccounts(string currentFilter, string searchString, int? page)
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
            return RedirectToAction("Index", "login");
        }

        public ActionResult AddAccount() 
        {
            return View();
        }

        public ActionResult AddAccount(AddLoginViewModel viewModel) 
        {
            if (ModelState.IsValid)
            {
                int result = _LoginService.AddAccount(viewModel);
                if(result > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult EditAccount(int id)
        {
            var account = _LoginService.GetLoginById(id);
            return View(account);
        }

        public ActionResult EditAccount(EditLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int result = _LoginService.UpdateAccount(viewModel);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}