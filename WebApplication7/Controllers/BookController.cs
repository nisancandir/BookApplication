using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index(int id)
        {
            BookStoreContext context = HttpContext.RequestServices.GetService(typeof(BookStoreContext)) as BookStoreContext;
            List<Book> list = context.GetAllBooks();
            return View(list);
        }
        [HttpPost]
        public IActionResult findBook(int id)
        {
            Bought umodel = new Bought();
            BookStoreContext context = HttpContext.RequestServices.GetService(typeof(BookStoreContext)) as BookStoreContext;
            umodel = context.findBook(id,umodel);
            return View();
        }
        
       

    }
}