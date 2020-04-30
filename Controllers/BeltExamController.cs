using System;
using System.Collections.Generic;
using System.Linq;
using BeltExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers
{
    public class BeltExamController : Controller
    {
        private BeltExamContext db;
        private int? uid
        {
            get
            {
                return HttpContext.Session.GetInt32("UserId");
            }
        }
        public BeltExamController(BeltExamContext context)
        {
            db = context;
        }

        [HttpGet("/hobby")]
        public IActionResult Hobby()
        {
            if (uid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Hobby> allHobbies = db.Hobbies
                .Include(hobby => hobby.Enthusiasts)
                .ToList();

            return View("Hobby", allHobbies);
        }

        [HttpGet("/New")]
        public IActionResult New()
        {
            if (uid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost("/Create")]
        public IActionResult Create(Hobby newHobby)
        {

            if (uid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                if (db.Hobbies.Any(hobby => hobby.Name == newHobby.Name))
                {
                    ModelState.AddModelError("Name", "is taken");
                }
                else
                {
                    db.Add(newHobby);
                    db.SaveChanges();
                    return RedirectToAction("Hobby");
                }

            }
            // above return did not run, so not valid, re-render page to display error messages
            return View("New");
        }

        [HttpGet("/hobbies/{hobbyId}")]
        public IActionResult Details(int hobbyId)
        {
            if (uid == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Hobby selectedHobby = db.Hobbies
                .Include(hobby => hobby.Enthusiasts)
                .ThenInclude(peeps => peeps.User)
                .FirstOrDefault(hobby => hobby.HobbyId == hobbyId);

            ViewBag.HobbyId = hobbyId;
            return View("Details", selectedHobby);
        }

        [HttpPost("/hobby/{HobbyId}/AddHobby")]
        public IActionResult AddHobby(Enthusiast newEnthusiast, int HobbyId)
        {
            Hobby selectedHobby = db.Hobbies
                .Include(hobby => hobby.Enthusiasts)
                .ThenInclude(peeps => peeps.User)
                .FirstOrDefault(hobby => hobby.HobbyId == HobbyId);

            if (ModelState.IsValid == false)
            {
                return View("Details", selectedHobby);
            }

            bool alreadyAdded = db.Enthusiasts.Any(Enthusiast => Enthusiast.HobbyId == selectedHobby.HobbyId && Enthusiast.UserId == uid);

            if (alreadyAdded)
            {
                ModelState.AddModelError("Body", "Already Enthused");
                return View("Details", selectedHobby);
            }

            newEnthusiast.UserId = (int)uid;
            db.Enthusiasts.Add(newEnthusiast);
            db.SaveChanges();
            return RedirectToAction("Details", new { HobbyId = newEnthusiast.HobbyId });
        }
    }
}