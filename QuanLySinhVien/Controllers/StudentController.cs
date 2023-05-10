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
    public class StudentController : Controller
    {
        private readonly StudentService _studentService;
        private readonly ViolateEmployeeService _violateEmployeeService;

        public StudentController(StudentService studentService, ViolateEmployeeService violateEmployeeService)
        {
            _studentService = studentService;
            _violateEmployeeService = violateEmployeeService;
        }

        public ActionResult StudentViolate(string currentFilter, string searchString, int? page)
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
                students = students.Where(s => s.idStudent.Contains(searchString)).ToList();
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            if (TempData["result"] != null)
            {
                ViewBag.Success = TempData["result"];
            }
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AddStudent()
        {
            return View();
        }

        public ActionResult AddStudent(AddStudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int result = _studentService.AddStudent(viewModel);
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

        public ActionResult EditStudent(string id)
        {
            var account = _studentService.GetStudentById(id);
            return View(account);
        }

        public ActionResult EditStudent(EditStudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int result = _studentService.UpdateStudent(viewModel);
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
        public ActionResult DeleteStudent(string id)
        {
            int result = _studentService.DeleteStudent(id);
            if (result > 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}