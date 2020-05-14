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
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult GetDetails()
        {
            RegisterViewModel umodel = new RegisterViewModel();
            umodel.Username = HttpContext.Request.Form["txtName"].ToString();
            umodel.Email = HttpContext.Request.Form["txtEmail"].ToString();
            umodel.Password = HttpContext.Request.Form["txtPassword"].ToString();
            BookStoreContext context = HttpContext.RequestServices.GetService(typeof(BookStoreContext)) as BookStoreContext;
            int result1 = context.CheckEmail(umodel);
            if (result1 == 1)
            {
                ViewBag.Result = "This user already exists";

            }
            else
            {

                int result = context.SaveDetails(umodel);
                if (result > 0)
                {
                    ViewBag.Result = "Data Saved Successfully      " + result;
                }
                else
                {
                    ViewBag.Result = "Something Went Wrong";
                }
                
            }
            return View("Regis");

        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult CheckUser()
        {
            RegisterViewModel umodel = new RegisterViewModel();
            umodel.Username = HttpContext.Request.Form["txtName"].ToString();
            umodel.Email = HttpContext.Request.Form["txtEmail"].ToString();
            umodel.Password = HttpContext.Request.Form["txtPassword"].ToString();
            BookStoreContext context = HttpContext.RequestServices.GetService(typeof(BookStoreContext)) as BookStoreContext;
            int result = context.CheckEmail(umodel);
            if (result == 1)
            {
                return View("Index");
               
            }
            else
            {
                
                return View("Regis");
            }
            

        }
    }
}