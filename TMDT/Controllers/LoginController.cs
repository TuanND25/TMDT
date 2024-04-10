using Microsoft.AspNetCore.Mvc;
using TMDT.Models;
using TMDT.Services;
using TMDT.ViewModels;

namespace TMDT.Controllers
{
	public class LoginController : Controller
	{
		private readonly UserServices userServices;
	
		
		public LoginController()
		{
			userServices = new UserServices();
			
		}
        [HttpGet]
        [Route("user/login")]
		
        public IActionResult Login()
		{
			return View();
		}
		
		[Route("user/login")]
		
		public  IActionResult Login(LoginUser loginUser)
		{
			var userLogin = userServices.GetUsersByUserName(loginUser.Username);
			if (userLogin != null && userLogin.UserPassword == loginUser.Password)
			{

				HttpContext.Session.SetString("Id", Convert.ToString(userLogin.UserId));
				
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.Message = string.Empty;	
				ViewBag.Message = "Tên tài khoản hoặc mật khẩu không chính xác";
				return View();
			}

			
		}
		public IActionResult LogOut()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Login","Login");

		}

		[HttpGet]
	
        [Route("user/sign-up")]
        public IActionResult SignUp()
		{
			return View();
		}


	
		[Route("user/sign-up")]		
		
		public IActionResult SignUp(User user) 
		{
			var CheckExists = userServices.GetUsersByUserName(user.UserName);
			if (CheckExists == null)
			{
				userServices.AddUser(user);
				LoginUser loginuer = new LoginUser();
				loginuer.Username = user.UserName;
				loginuer.Password = user.UserPassword;
				Login(loginuer);
				return RedirectToAction("Index", "Home");
			}
			else
			{
				ViewBag.Message = string.Empty;
				ViewBag.Message = "Tên đăng nhập đã tồn tại vui lòng chọn tên đăng nhập khác";
				return View();
			}
		}
	}
}
