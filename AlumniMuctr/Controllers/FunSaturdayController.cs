﻿using AlumniMuctr.Data;
using AlumniMuctr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlumniMuctr.Controllers
{
    public class FunSaturdayController : Controller
    {
        private readonly ApplicationDbContext _db;
        public FunSaturdayController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<News> news = (from News in _db.News
                                       where News.CategoryId == 3
                                      select News).ToList();
            NewsAndHelper thisModel = new NewsAndHelper();
            thisModel.News = news;
            thisModel.Helper = new Helper();

            return View(thisModel);
        }

        [HttpPost]
        public async Task<IActionResult> HelperRequest(Helper obj)
        {
            _db.Helper.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
