using PagedList;
using QuanLySinhVien.Common;
using QuanLySinhVien.Service;
using QuanLySinhVien.ViewModel.Login;
using QuanLySinhVien.ViewModel.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySinhVien.Controllers
{
    public class StudentController : BaseController
    {
        private readonly StudentService _studentService;
        private readonly ViolateEmployeeService _violateEmployeeService;

        public StudentController()
        {
            _studentService = new StudentService();
            _violateEmployeeService = new ViolateEmployeeService ();
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
            var students = _studentService.GetStudents();
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Id.Contains(searchString)).ToList();
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (TempData["result"] != null)
            {
                ViewBag.Success = TempData["result"];
            }
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(AddStudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int result = _studentService.AddStudent(viewModel);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditStudent(string id)
        {
            var account = _studentService.GetStudentById(id);
            return View(account);
        }

        [HttpPost]
        public ActionResult EditStudent(EditStudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int result = _studentService.UpdateStudent(viewModel);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult DeleteStudent(string id)
        {
            int result = _studentService.DeleteStudent(id);
            if (result > 0)
            {
                return RedirectToAction("Index", "Student");
            }
            return View();
        }
    }
}