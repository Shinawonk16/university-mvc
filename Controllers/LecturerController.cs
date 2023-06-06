using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Interface.Services;

namespace UniversityManagementMvc.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ILecturerService _lecturerService;
        private readonly IUserServices _userService;
        public LecturerController(ILecturerService lecturerService, IUserServices userServices)
        {
            _lecturerService = lecturerService;
            _userService = userServices;
        }
        [Authorize(Roles = "Lecturer")]
        public IActionResult ManageUser()
        {
            return View();
        }
        [ActionName("Create")]
        public IActionResult CreateLecturer(CreateLecturerRequestModel model)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var let = _lecturerService.AddLecturer(model);
                if (let.Success == true)
                {
                    TempData["success"] = Content(let.Message);
                    return RedirectToAction("Login", "Login");
                }
                  else
            {
                // ViewBag.Error = "Wrong Input";
                 TempData["error"] = "wrong input"; 
               return View();
            }
            }

            return View();
        }
        [Authorize(Roles = "Management,Lecturer")]
        public IActionResult DeleteLecturer()
        {
            return View();
        }
        public async Task<IActionResult> DeleteLecturer(string lectureId)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var let = await _lecturerService.DeleteLecturer(lectureId);
                if (let.Success == true)
                {
                    return Content(let.Message);
                }
                return Content(let.Message);
            }
            return View();
        }
         [Authorize]
        public async Task<IActionResult> UpdateLecturer(UpdateLecturerRequestModel requestModel,string lecturerId)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var let = await _lecturerService.EditLecturer(requestModel,lecturerId);
                if (let.Success == true)
                {
                    TempData["success"] = Content(let.Message);
                    return RedirectToAction("ManageUser");
                }
                return Content(let.Message);
            }
              return View();
        }
             [Authorize(Roles="Lecturer")]
        [HttpGet]
        public IActionResult Profile(string lectureId)
        {
            lectureId = User.FindFirst(ClaimTypes.PrimaryGroupSid).Value;
            var studentprofile = _lecturerService.ViewLecturer(lectureId);
            return View(studentprofile);

        }
        [Authorize(Roles ="Management")]
        public async Task<IActionResult> ApproveLecture(string lecturerId)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var let = await _lecturerService.ApproveLecturer(lecturerId);
                if (let.Success == true)
                {
                    TempData["success"] = Content(let.Message);
                    // return RedirectToAction("ManageUser");
                }
                return Content(let.Message);
            }
            return View();
        }
        [Authorize(Roles = "Management")]
        public IActionResult GetAllLecturer()
        {
            var lets = _lecturerService.ViewAllLecturers();
            return View(lets);
        }
        [Authorize(Roles = "Management,Lecturer")]
        public async Task<IActionResult> GetLecturer(string lecturerId)
        {
            if (HttpContext.Request.Method == "GET")
            {
                var let = await _lecturerService.ViewLecturer(lecturerId);
                if (let.Success == false)
                {
                    return Content(let.Message);
                }
                return Content(let.Message);
            }
            return View();
        }
        
        public async Task<IActionResult> Login(string email, string passWord)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var log = await _userService.Login(email, passWord);
                if (log.Success == false)
                {
                    return Content("Email or Password does not exist ");
                }
                var claims = new List<Claim>
                {
                   new Claim (ClaimTypes.Name, log.UserDto.FirstName + " "+log.UserDto.LastName),
                    new Claim (ClaimTypes.NameIdentifier, log.UserDto.Email),
                     new Claim(ClaimTypes.Role , "Lecturer"),
                    new Claim (ClaimTypes.NameIdentifier, log.UserDto.Lecturers.LecturerId),

                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                return RedirectToAction(nameof(ManageUser));
            }
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");

        }
    }
}