﻿using Microsoft.AspNetCore.Mvc;

namespace Moon.AspNetCore.Validation.Sample.Views.Home
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
            => View();

        [HttpGet("server")]
        public IActionResult Server()
            => View(new FormModel());

        [HttpPost("server")]
        public IActionResult Server(FormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}