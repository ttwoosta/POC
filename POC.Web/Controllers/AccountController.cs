using Microsoft.Owin.Security;
using POC.DataModel.Services;
using POC_Web.Services;
using POC_Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace POC_Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserServices _userServices;

        public AccountController()
        {
            _userServices = new UserServices();
        }

        // GET: Account
        public ActionResult Login()
        {
            if (HttpContext.Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var selectedUser = await _userServices.GetUserByEmailAsync(user.Email);
                //check username and password from database, naive checking: 
                //password should be in SHA
                if (user != null && (selectedUser.Password == user.Password))
                {
                    var claims = new[] {
                     new Claim(ClaimTypes.Email, selectedUser.Email),
                      new Claim(ClaimTypes.Name, selectedUser.Name)
                   // can add more claims
                         };

                    var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                    // Add roles into claims
                    //var roles = _roleService.GetByUserId(user.Id);
                    //if (roles.Any())
                    //{
                    //    var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r.Name));
                    //    identity.AddClaims(roleClaims);
                    //}

                    var context = Request.GetOwinContext();
                    var authManager = context.Authentication;

                    authManager.SignIn(new AuthenticationProperties
                    { IsPersistent = user.RememberMe }, identity);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //Login Failed
                    ModelState.AddModelError("LogOnError", "The user name or password provided is incorrect.");
                    return View();
                }
            }
            {
                return View(user);
            }
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login");
        }

    }
}