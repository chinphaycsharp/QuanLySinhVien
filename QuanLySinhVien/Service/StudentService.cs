using QuanLySinhVien.Models;
using QuanLySinhVien.ViewModel.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Service
{
    public class StudentService
    {
        private readonly QuanLySinhVienDbContext _dbContext;

        public StudentService()
        {
            _dbContext = new QuanLySinhVienDbContext();
        }

        public List<Student> GetStudents()
        {
            var students = _dbContext.Students.ToList();
            return students;
        }

        public int AddStudent(AddStudentViewModel viewModel)
        {
            try
            {
                string[] date = new string[3];
                if(viewModel.BirthDayStr != null)
                {
                    date = viewModel.BirthDayStr.Split('/');
                }
                Student student = new Student()
                {
                    Id = viewModel.Id,
                    Address = viewModel.Address,
                    Name = viewModel.Name,
                    Phone = viewModel.Phone,
                    Email = viewModel.Email,
                    BirthDay = new DateTime(Convert.ToInt32(date[date.Length - 1]), Convert.ToInt32(date[date.Length - 2]), Convert.ToInt32(date[date.Length - 3])),
                    ClassName = viewModel.ClassName,
                    MajorName = viewModel.MajorName,
                    CreatedAt = viewModel.CreatedAt
                };
                _dbContext.Students.Add(student);
                _dbContext.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public Student GetStudentById(string id)
        {
            try
            {
                var result = _dbContext.Students.FirstOrDefault(x => x.Id == id);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        public int UpdateStudent(EditStudentViewModel viewModel)
        {
            try
            {
                
                var result = _dbContext.Students.FirstOrDefault(x => x.Id == viewModel.Id);
                result.Name = viewModel.Name;
                result.Address = viewModel.Address;
                result.BirthDay = viewModel.BirthDay;
                result.MajorName= viewModel.MajorName;
                result.ClassName = viewModel.ClassName;
                result.UpdatedAt = DateTime.Now;
                _dbContext.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public int DeleteStudent(string id)
        {
            try
            {
                var data = _dbContext.Students.Where(x => x.Id == id).FirstOrDefault();
                _dbContext.Students.Remove(data);
                _dbContext.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}