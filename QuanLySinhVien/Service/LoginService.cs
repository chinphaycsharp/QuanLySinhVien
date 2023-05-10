using QuanLySinhVien.Helper;
using QuanLySinhVien.Models;
using QuanLySinhVien.ViewModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySinhVien.Service
{
    public class LoginService
    {
        private readonly QuanLySinhVienDbContext _dbContext;

        public LoginService()
        {
            _dbContext = new QuanLySinhVienDbContext();
        }

        public Login CheckLogin(string username, string password)
        {
            var account = _dbContext.Logins.Where(x=>x.UserName == username && x.PassWord == EncryptionHelper.ToMD5(password)).FirstOrDefault();
            if(account == null) 
            {
                return null;
            }
            return account;
        }

        public List<Login> GetLogins()
        {
            var logins= _dbContext.Logins.ToList();
            return logins;
        }

        public Login GetLoginById(int id)
        {
            var login = _dbContext.Logins.Where(x=>x.Id == id).FirstOrDefault();
            return login;
        }

        public int AddAccount(AddLoginViewModel viewModel) 
        {
            var login = new Login()
            {
                UserName = viewModel.UserName,
                PassWord = EncryptionHelper.ToMD5(viewModel.PassWord),
                Email = viewModel.Email,
                CreatedAt = viewModel.CreatedAt
            };

            try
            {
                _dbContext.Logins.Add(login);
                _dbContext.SaveChanges();
                return login.Id;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public int UpdateAccount(EditLoginViewModel viewModel)
        {
            var login = _dbContext.Logins.Where(x => x.Id == viewModel.Id).FirstOrDefault();
            if(login == null)
            {
                return -1;
            }

            try
            {
                login.UserName = viewModel.UserName;
                login.PassWord = EncryptionHelper.ToMD5(viewModel.PassWord);
                login.Email = viewModel.Email;
                _dbContext.SaveChanges();
                return login.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public int DeleteAccount(int id)
        {
            try
            {
                var data = _dbContext.Logins.Where(x => x.Id == id).FirstOrDefault();
                _dbContext.Logins.Remove(data);
                _dbContext.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
    }
}