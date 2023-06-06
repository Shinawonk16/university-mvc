using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Dto.ResponseModel;
using UniversityManagementMvc.Interface.Services;

namespace UniversityManagementMvc.Controllers
{
    public class ManagementController : Controller
    {
        private readonly IManagementService _managementService;

        private readonly IUserServices _userService;
        public ManagementController(IManagementService managementService, IUserServices userServices)
        {
            _managementService = managementService;
            _userService = userServices;
        }
        [Authorize(Roles = "Management")]
        public IActionResult ManageChancellors()
        {
            return View();
        }
        public IActionResult ManageUsers()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public IActionResult CreateManagement(CreateManagementRequestModel model)
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
                    var stud = _managementService.AddManagement(model);
                    if (stud.Success == true)
                    {
                        TempData["success"] = Content(stud.Message);
                        return RedirectToAction("Login", "Login");

                    }
                    if (model.ProfilePicture == null)
                    {
                        TempData["profile"] = "Profile picture is required";
                    }
                }
            }
            return NoContent();
        }


        [Authorize(Roles = "Management")]
        public IActionResult ManageUser()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> UpdateManagement(UpdateManagementReequestModel requestModel, int id)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var let = await _managementService.EditManagement(requestModel, id);
                if (let.Success == true)
                {
                    TempData["success"] = Content(let.Message);
                    return RedirectToAction("ManageUser");
                }
                return Content(let.Message);
            }
            return View();
        }
        [Authorize(Roles = "Management")]
        [HttpGet]
        public IActionResult Profile(int id)
        {
            id = int.Parse(User.FindFirst(ClaimTypes.PrimaryGroupSid).Value);
            var profile = _managementService.ViewManagement(id);
            return View(profile);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string passWord)
        {
            // if (HttpContext.Request.Method == "POST")
            // {
            var log = await _userService.Login(email, passWord);
            if (log.Success == true)
            {
                var roles = new List<string>();
                var claims = new List<Claim>
                {
                    new Claim (ClaimTypes.Name, log.UserDto.FirstName + " "+log.UserDto.LastName),
                    new Claim (ClaimTypes.NameIdentifier, log.UserDto.Email),
                     new Claim(ClaimTypes.Role , "Manangement"),
                    new Claim (ClaimTypes.PrimarySid, log.UserDto.Managements.Id.ToString()),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                return RedirectToAction(nameof(ManageUsers));

            }
            return Content("Email or Password does not exist ");

            // }
            // return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");

        }


    }
}