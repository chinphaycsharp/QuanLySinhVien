using QuanLySinhVien.Models;
using QuanLySinhVien.ViewModel.Student;
using QuanLySinhVien.ViewModel.Violate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Service
{
    public class ViolateEmployeeService
    {
        private readonly QuanLySinhVienDbContext _dbContext;

        public ViolateEmployeeService()
        {
            _dbContext = new QuanLySinhVienDbContext();
        }

        public List<ViolateViewModel> GetStudentViolates()
        {
            var violateViewModels = from v in _dbContext.Violates
                                    join e in _dbContext.Students
                                    on v.idStudent equals e.Id
                                    select new ViolateViewModel()
                                    {
                                        Id = v.id,
                                        idStudent = v.idStudent,
                                        ContentViolate = v.ContentViolate,
                                        CreatedAt = v.CreatedAt,
                                        StudentName = e.Name
                                    };
            return violateViewModels.ToList();
        }

        public int AddViolate(AddViolateViewModel viewModel)
        {
            try
            {
                Violate violate = new Violate()
                {
                    idStudent = viewModel.idStudent,
                    ContentViolate = viewModel.ContentViolate,
                    CreatedAt = viewModel.CreatedAt
                };
                _dbContext.Violates.Add(violate);
                _dbContext.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public Violate GetViolateById(int id)
        {
            try
            {
                var result = _dbContext.Violates.FirstOrDefault(x => x.id == id);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        public int UpdateViolate(EditViolateViewModel viewModel)
        {
            try
            {
                var result = _dbContext.Violates.FirstOrDefault(x => x.id == viewModel.Id);
                result.idStudent = viewModel.idStudent;
                result.ContentViolate = viewModel.ContentViolate;
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

        public int DeleteViolate(int id)
        {
            try
            {
                var data = _dbContext.Violates.Where(x => x.id == id).FirstOrDefault();
                _dbContext.Violates.Remove(data);
                _dbContext.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;           }
        }
    }
}