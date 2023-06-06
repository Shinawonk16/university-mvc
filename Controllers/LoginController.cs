using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementMvc.Data.ContextClass;
using UniversityManagementMvc.Interface.Services;

namespace UniversityManagementMvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationContext dbContext;
        private readonly IUserServices _userService;

        public LoginController(IUserServices userService)
        {
            _userService = userService;

        }
        [ActionName("LogIn")]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (HttpContext.Request.Method == "POST")
            {

                if (email == null || password == null)
                {
                    return NotFound();
                }
                var login = await _userService.Login(email, password);
                if (login.Message == null)
                {
                    TempData["logmessage"] = login.Message;
                    return View();
                }
                // HttpContext.Session.SetInt32("Id",login.UserDto.Id);
                var roles = new List<string>();
                var claims = new List<Claim>
                {
                     new Claim(ClaimTypes.Email,login.UserDto.Email),
                     new Claim(ClaimTypes.Name , login.UserDto.Password),
                     new Claim(ClaimTypes.NameIdentifier, (login.UserDto.Role == "Student")? login.UserDto.Students.MatricNumber:""),
                     new Claim(ClaimTypes.PrimarySid, (login.UserDto.Role == "Lecturer")? login.UserDto.Lecturers.LecturerId:""),
                     new Claim(ClaimTypes.Hash, (login.UserDto.Role == "Chancellor")? login.UserDto.Chancellors.ChancellorId:""),
                    new Claim(ClaimTypes.Anonymous, login.UserDto.Email.ToString()),



                    new Claim(ClaimTypes.Role, (login.UserDto.Role == "Student")? "Student":""),
                    new Claim(ClaimTypes.Role, (login.UserDto.Role == "Lecturer")? "Lecturer":""),
                    new Claim(ClaimTypes.Role, (login.UserDto.Role == "Chancellor")? "Chancellor":""),
                    new Claim(ClaimTypes.Role, (login.UserDto.Role == "Management")? "Management":""),
                    new Claim(ClaimTypes.NameIdentifier, login.UserDto.Id.ToString()),



                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                if (login.UserDto.Role == "Student")
                {
                    TempData["success"] = "Login Successfully";
                    return RedirectToAction("ManageUser", "Student");
                }
                if (login.UserDto.Role == "Chancellor")
                {
                    TempData["success"] = "Login Successfully";
                    return RedirectToAction("Manage", "Chancellor");

                }
                if (login.UserDto.Role == "Lecturer")
                {
                    TempData["success"] = "Login Successfully";
                    return RedirectToAction("ManageUser", "Lecturer");

                }
                if (login.UserDto.Role == "Management")
                {
                    TempData["success"] = "Login Successfully";
                    return RedirectToAction("ManageChancellors", "Management");

                }

                return RedirectToAction(nameof(Index));
            }
            return View();

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

    }
}