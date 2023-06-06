using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Interface.Services;

namespace UniversityManagementMvc.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        private readonly IUserServices _userService;
        public StudentController(IStudentService studentService, IUserServices userServices)
        {
            _studentService = studentService;
            _userService = userServices;
        }
        [Authorize(Roles = "Student")]
        public IActionResult ManageStudent()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateOld()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateOldStudent(CreateOldStudentRequest model)
        {


            if (HttpContext.Request.Method == "POST")
            {
                if (model != null)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var datastream = new MemoryStream())
                    {
                        file.CopyToAsync(datastream);
                        model.ProfilePicture = datastream.ToArray();
                    }
                    if (model.ProfilePicture == null)
                    {
                        TempData["profile"] = "Profile picture is required";
                    }
                    var stud = _studentService.AddOldStudent(model);
                    if (stud.Success == true)
                    {
                        TempData["success"] = Content(stud.Message);
                        return RedirectToAction("Login", "Login");

                    }
                }

            }
            return View();
        }
        [HttpGet]
        public IActionResult CreateNew()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewStudent(CreateStudentRequestModel model)
        {
            if (HttpContext.Request.Method == "POST")
            {
                if (model != null)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var datastream = new MemoryStream())
                    {
                        file.CopyToAsync(datastream);
                        model.ProfilePicture = datastream.ToArray();
                    }
                    if (model.ProfilePicture == null)
                    {
                        TempData["profile"] = "Profile picture is required";
                    }
                    var stud = _studentService.AddStudent(model);
                    if (stud.Success == true)
                    {
                        TempData["success"] = Content(stud.Message);
                        return RedirectToAction("Login", "Login");

                    }
                }
            }
            return View();
        }
        [Authorize(Roles = "Management,Student")]
        public async Task<IActionResult> DeleteStudent(string matricNumber)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var stud = await _studentService.DeleteStudent(matricNumber);
                if (stud.Success == true)
                {
                    return Content(stud.Message);
                }
                return Content(stud.Message);
            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> UpdateStudent(UpdateStudentRequestModel requestModel, string matricNumber)
        {
            if (HttpContext.Request.Method == "POST")
            {
                if (requestModel != null)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var datastream = new MemoryStream())
                    {
                        await file.CopyToAsync(datastream);
                        requestModel.ProfilePicture = datastream.ToArray();
                    }
                    if (requestModel.ProfilePicture == null)
                    {
                        TempData["profile"] = "Profile picture is required";
                    }
                    var stud = await _studentService.EditStudent(requestModel, matricNumber);
                    if (stud.Success == true)
                    {
                        TempData["success"] = Content(stud.Message);
                        return RedirectToAction("ManageStudent");
                    }
                    return Content(stud.Message);
                }

            }
            return View();
        }
        [Authorize(Roles="Student")]
        [HttpGet]
        public IActionResult Profile(string matricNumber)
        {
            matricNumber = User.FindFirst(ClaimTypes.PrimaryGroupSid).Value;
            var studentprofile = _studentService.ViewStudent(matricNumber);
            return View(studentprofile);

        }
         [Authorize(Roles ="Management")]
        public async Task<IActionResult> ApproveStudent(string matricNumber)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var let = await _studentService.ApproveStudent(matricNumber);
                if (let.Success == true)
                {
                    TempData["success"] = Content(let.Message);
                    // return RedirectToAction("ManageUser");
                }
                return Content(let.Message);
            }
            return View();
        }

        [Authorize(Roles = "Management,Lecturer,Chancellor")]
        public IActionResult GetAllStudent()
        {
            var stud = _studentService.ViewAllStudents();
            return View(stud);
        }
        [Authorize(Roles = "Management,Student,Lecturer,Chancellor")]
        public async Task<IActionResult> GetStudent(string matricNumber)
        {
            if (HttpContext.Request.Method == "GET")
            {
                var stud = await _studentService.ViewStudent(matricNumber);
                if (stud.Success == false)
                {
                    return Content(stud.Message);
                }
                return Content(stud.Message);
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
                var roles = new List<string>();
                var claims = new List<Claim>
                {
                    new Claim (ClaimTypes.Name, log.UserDto.FirstName + " "+log.UserDto.LastName),
                    new Claim (ClaimTypes.NameIdentifier, log.UserDto.Email),
                     new Claim(ClaimTypes.Role , "Student"),
                    new Claim (ClaimTypes.NameIdentifier, log.UserDto.Students.MatricNumber),

                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                return RedirectToAction(nameof(ManageStudent));
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