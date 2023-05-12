using PagedList;
using QuanLySinhVien.Common;
using QuanLySinhVien.Service;
using QuanLySinhVien.ViewModel.Student;
using QuanLySinhVien.ViewModel.Violate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySinhVien.Controllers
{
    public class ViolateStudentController : BaseController
    {
        private readonly StudentService _StudentService;
        private readonly ViolateEmployeeService _violateEmployeeService;

        public ViolateStudentController()
        {
            _StudentService = new StudentService();
            _violateEmployeeService = new ViolateEmployeeService();
        }
        // GET: Student
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

        [HttpGet]
        public ActionResult AddViolateStudent()
        {
            SetViewBag_Supplier();
            return View();
        }

        [HttpPost]
        public ActionResult AddViolateStudent(AddViolateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int result = _violateEmployeeService.AddViolate(viewModel);
                if (result > 0)
                {
                    return RedirectToAction("Index", "ViolateStudent");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult EditViolateStudent(int id)
        {
            var violateEmployee = _violateEmployeeService.GetViolateById(id);
            SetViewBag_Supplier(violateEmployee.idStudent);
            return View(violateEmployee);
        }

        [HttpPost]
        public ActionResult EditViolateStudent(EditViolateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                int result = _violateEmployeeService.UpdateViolate(viewModel);
                if (result > 0)
                {
                    return RedirectToAction("Index", "ViolateStudent");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }

        [HttpDelete]
        public ActionResult DeleteEditViolateStudent(int id)
        {
            int result = _violateEmployeeService.DeleteViolate(id);
            if (result > 0)
            {
                return RedirectToAction("Index", "ViolateStudent");
            }
            return View();
        }
        public void SetViewBag_Supplier(string selectedId = null)
        {
            ViewBag.idStudent = new SelectList(_StudentService.GetStudents(), "Id", "Name", selectedId);
        }
    }
}