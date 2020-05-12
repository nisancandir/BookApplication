using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
namespace WebApplication7.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Regis()
        {

            return View();
        }
        public IActionResult Login()
        {

            return View();
        }
    }
}