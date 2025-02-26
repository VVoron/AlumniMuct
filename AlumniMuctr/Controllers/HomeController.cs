﻿using AlumniMuctr.Data;
using AlumniMuctr.Models;
using AlumniMuctr.Services.SaveFileService;
using AlumniMuctr.Services.Support;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AlumniMuctr.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ISaveFileService _saveFile;

        public HomeController(ApplicationDbContext db, IWebHostEnvironment appEnvironment, ISaveFileService saveFile)
        {
            _db = db;
            _appEnvironment = appEnvironment;
            _saveFile = saveFile;
        }

        public IActionResult Index()
        {
            AllModels model = new AllModels();
            model.AllNews = _db.News.ToList();
            model.AllProgramms = _db.Programms.ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationFormRequest obj)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Вы неправильно ввели данные!";
                return RedirectToAction("Index");
            }

            if (_db.RegistrationForm.Where(x => x.FCs == obj.FCs) != null || _db.RegistrationForm.Where(x => x.FCsгUniversity == obj.FCsгUniversity) != null) {
                obj.FCs += " (дубликат)";
            }

            _db.RegistrationForm.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> HelperRequest(Helper obj)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            Support support = new Support();
            support.AddedNewQuestion(obj, _db);
            return RedirectToAction("Index");
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