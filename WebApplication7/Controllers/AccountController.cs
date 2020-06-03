using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            int result = 0;
            User umodel = new User();
            umodel.Username = HttpContext.Request.Form["txtName"].ToString();
            umodel.Email = HttpContext.Request.Form["txtEmail"].ToString();
            umodel.Password = HttpContext.Request.Form["txtPassword"].ToString();
            BookStoreContext context = HttpContext.RequestServices.GetService(typeof(BookStoreContext)) as BookStoreContext;
            int result1 = context.CheckEmail(umodel);
            if (result1 > 0)
            {
                ViewBag.Result = "This user already exists";
                return View("~/Views/Account/Regis.cshtml"); ;
            }
            else
            {

                umodel = context.SaveDetails(umodel);
                result = umodel.Userıd;
                if (result > 0)
                {
                    ViewBag.Result = "Data Saved Successfully      " + result;
                    HttpContext.Session.SetObjectAsJson("User", umodel);
                    return View("~/Views/Home/Index.cshtml");
                }
                else
                {
                    ViewBag.Result = "Something Went Wrong";
                    return View("~/Views/Account/Regis.cshtml");
                }
                
            }
           

        }
        public IActionResult Login()
        {

            return View();
        }
        public IActionResult Welcome()
        {

            return View();
        }
      
        [HttpPost]
        public IActionResult CheckUser()
        {
            int result;
            User umodel = new User();
            umodel.Username = HttpContext.Request.Form["txtName"].ToString();
            umodel.Email = HttpContext.Request.Form["txtEmail"].ToString();
            umodel.Password = HttpContext.Request.Form["txtPassword"].ToString();
            BookStoreContext context = HttpContext.RequestServices.GetService(typeof(BookStoreContext)) as BookStoreContext;
            umodel = context.CheckUserInfo(umodel);
            result = umodel.Userıd;
            
            if (result > 0)
            {
                HttpContext.Session.SetObjectAsJson("User", umodel);
                return View("~/Views/Home/Index.cshtml",result);
               
            }
            else
            {
                
                return View("Regis");
            }
            

        }
    }
}