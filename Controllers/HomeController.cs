using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BeltExam.Controllers
{
    public class HomeController : Controller
    {
        private int? uid
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }
        private BeltExamContext db;
        public HomeController(BeltExamContext context)
        {
            db = context;
        }

        [HttpPost("/Register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (db.Users.Any(user => user.UserName == newUser.UserName))
                {
                    ModelState.AddModelError("UserName", "this user name is taken");
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }

            PasswordHasher<User> hasher = new PasswordHasher<User>();
            newUser.Password = hasher.HashPassword(newUser, newUser.Password);

            db.Users.Add(newUser);
            db.SaveChanges();

            HttpContext.Session.SetInt32("UserId", newUser.UserId);
            HttpContext.Session.SetString("FirstName", newUser.FirstName);
            return RedirectToAction("Hobby", "BeltExam");
        }

        [HttpPost("/login")]
        public IActionResult Login(LoginUser loginUser)
        {
            if (ModelState.IsValid == false)
            {
                return View("Index");
            }
            User dbUser = db.Users.FirstOrDefault(user => user.UserName == loginUser.LoginName);

            if (dbUser == null)
            {
                ModelState.AddModelError("LoginName", "Login credentials were not accepted. Please try again.");
            }
            else
            {
                PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                PasswordVerificationResult pwCompareResult = hasher.VerifyHashedPassword(loginUser, dbUser.Password, loginUser.LoginPassword);

                if (pwCompareResult == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Login credentials were not accepted. Please try again.");
                }
            }

            if (ModelState.IsValid == false)
            {
                return View("Index");
            }

            HttpContext.Session.SetInt32("UserId", dbUser.UserId);
            HttpContext.Session.SetString("FirstName", dbUser.FirstName);
            return RedirectToAction("Hobby", "BeltExam");

        }

        [HttpPost("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
