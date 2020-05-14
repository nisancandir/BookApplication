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

        [HttpPost]
        public IActionResult GetDetails()
        {
            RegisterViewModel umodel = new RegisterViewModel();
            umodel.Username = HttpContext.Request.Form["txtName"].ToString();
            umodel.Email = HttpContext.Request.Form["txtName"].ToString();
            umodel.Password = HttpContext.Request.Form["txtCity"].ToString();
            BookStoreContext context = HttpContext.RequestServices.GetService(typeof(BookStoreContext)) as BookStoreContext;
            int result = context.SaveDetails(umodel);
            if (result > 0)
            {
                ViewBag.Result = "Data Saved Successfully" + result;
            }
            else
            {
                ViewBag.Result = "Something Went Wrong";
            }
            return View("Regis");

           
        }
        public IActionResult Login()
        {

            return View();
        }
    }
}