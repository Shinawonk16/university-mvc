using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityManagementMvc.Dto.RequestModel;
using UniversityManagementMvc.Interface.Services;

namespace UniversityManagementMvc.Controllers
{
    public class ChancellorController : Controller
    {
        private readonly IChancellorService _chancllorService;
        private readonly IUserServices _userService;
        public ChancellorController(IChancellorService chancellorService, IUserServices userServices)
        {
            _chancllorService = chancellorService;
            _userService = userServices;
        }
        
        // [Authorize(Roles = "Chancellor,Management")] 
         public IActionResult Manage()
        {
            return View();
        }
        
        // [Authorize(Roles = "Management")]
        [HttpGet]
         public IActionResult Create()
        {
            return View();
        }
       [HttpPost]
        public IActionResult CreateChancellor(CreateChancellorRequestModel model)
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
                    var stud = _chancllorService.AddChancellor(model);
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
        //  [Authorize(Roles = "Chancellor,Management")] 
        [HttpGet]
         public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteChancellor(string chancellorId)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var chan =await _chancllorService.DeleteChancellor(chancellorId);
                if (chan.Success == true)
                {
                    return Content(chan.Message);
                }
                return Content(chan.Message);
            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> UpdateChancellor(UpdateChancellorRequestModel requestModel,string chancellorId)
        {
            if (HttpContext.Request.Method == "POST")
            {
                var chan = await _chancllorService.EditChancellor(requestModel,chancellorId);
                if (chan.Success == true)
                {
                    TempData["success"] = Content(chan.Message);
                    return RedirectToAction("Manage");
                }
                return Content(chan.Message);
            }
              return View();
        }
        //  [Authorize(Roles = "Management")] 
        public IActionResult GetAllChancellor()
        {
            var chans = _chancllorService.ViewAllChancellors();
            return View(chans);
        }
         [Authorize(Roles = "Chancellor,Management")] 
        public async Task<IActionResult> GetChancellor(string chancellorId)
        {
            if (HttpContext.Request.Method == "GET")
            {
                var chan =await _chancllorService.ViewChancellor(chancellorId);
                if (chan.Success == false)
                {
                    return Content(chan.Message);
                }
                return View(chan.ChancellorDto);
            }
            return View();
        }       

        //  [ActionName("Login")]
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
                if (log.Success == false)
                {
                    return Content("Email or Password does not exist ");
                }
                var claims = new List<Claim>
                {
                    new Claim (ClaimTypes.NameIdentifier, (log.UserDto.Id).ToString()),
                    new Claim (ClaimTypes.NameIdentifier, log.UserDto.Email),
                    new Claim (ClaimTypes.NameIdentifier, log.UserDto.Password),

                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authenticationProperties = new AuthenticationProperties();
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
                return RedirectToAction(nameof(Manage));
            // }
            
        }
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");

        }

    }

}