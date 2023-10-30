using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

using Microsoft.AspNetCore.Authentication.Cookies;
using Project_Quan_ly_buong_phong.Models;
using Microsoft.EntityFrameworkCore;
using Project_Quan_ly_buong_phong.Models.ViewModel;
using System.Net;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;

namespace Project_Quan_ly_buong_phong.Controllers
{
    public class AccessController : Controller
    {
        private readonly Quan_ly_buong_phongContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccessController(Quan_ly_buong_phongContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var items = _context.Logins.ToList();
            return View(items);
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated ) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(Login ModelLogin)
        {
            if (_context.Logins.Where(b => b.Email == ModelLogin.Email && b.Password == ModelLogin.Password).FirstOrDefault() != null ) 
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, ModelLogin.Email),
                    new Claim("OtherProperties","Example Role")
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties authenticationProperties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authenticationProperties);
                return RedirectToAction("Index","Home");
            }

            ViewData["ValidateMessage"] = "User not found";
            return View();
        }
/*        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(LoginViewModel _temp)
        {
            string stringFileName = UploadFile(_temp);

            var _user = new Login()
            {
                Email = _temp.Email,
                Password = _temp.Password,
                ImagePath = stringFileName,
            };
            if (ModelState.IsValid)
            {
                var check = _context.Logins.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    *//*_context.Configuration.ValidateOnSaveEnabled = false;*//*
                    _context.Logins.Add(_user);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }*/

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LoginViewModel _temp)
        {

            string stringFileName = null;
            stringFileName = UploadFile(_temp);


            var _user = new Login()
            {
                Email = _temp.Email,
                Password = _temp.Password,
                ImagePath = stringFileName,
            };
            if (ModelState.IsValid)
            {
                var check = _context.Logins.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                    {
                        /*_context.Configuration.ValidateOnSaveEnabled = false;*/
                        _context.Logins.Add(_user);
                        _context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Email already exists";
                        return View();
                    }
                }
                return View();
            }
        private string UploadFile(LoginViewModel _temp)
        {
            string FileName = null;
            if (_temp.ImagePath != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "Images");
                FileName = Guid.NewGuid().ToString() + "-" + _temp.ImagePath.FileName;
                string FilePath = Path.Combine(uploadDir, FileName);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    _temp.ImagePath.CopyTo(fileStream);
                }
            }
            return FileName;
        }
    }
}

