using QuanLySinhVien.Common;
using QuanLySinhVien.Service;
using QuanLySinhVien.ViewModel;
using QuanLySinhVien.ViewModel.Login;
using System.Web.Mvc;

namespace QuanLySinhVien.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;

        public LoginController()
        {
            _loginService = new LoginService();
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _loginService.CheckLogin(model.UserName, model.PassWord);
                if (result != null)
                {
                    var userSession = new userLoginViewModel();
                    userSession.userName = result.UserName;
                    userSession.userID = result.Id;

                    Session.Add(commonConst.user_session, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng !");
                }
            }
            return View("Index");
        }
    }
}