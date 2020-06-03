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
        public IActionResult Index()
        {
            BookStoreContext context = HttpContext.RequestServices.GetService(typeof(BookStoreContext)) as BookStoreContext;
            List<Book> list = context.GetAllBooks();
            return View(list);
        }
        
        public IActionResult Buy(int id)
        {
            var userDetails = HttpContext.Session.GetObjectFromJson<User>("User");
            int userID = Convert.ToInt32(userDetails.Userıd);
            Book umodel = new Book();
            BookStoreContext context = HttpContext.RequestServices.GetService(typeof(BookStoreContext)) as BookStoreContext;
            umodel = context.findBook(id, umodel);
            if (umodel.Available == 1)
            {
                ViewBag.Ava = "This book is not available";

            }
            String a="Not Available";
            if (umodel.Available == 0) {
                a = "Available";
                ViewBag.Result = "Book Name: " + umodel.BookName + "\n" + "Author:" + umodel.AuthorName + "\n" + "Price:" + umodel.Price + "\n" + a ;
                HttpContext.Session.SetObjectAsJson("CurrentBook", umodel);
            }
            
            

            return View();

            
        }
        [HttpPost]
        public IActionResult BooksBuy()
        {
            var currentbook = HttpContext.Session.GetObjectFromJson<Book>("CurrentBook");
            int bookID = Convert.ToInt32(currentbook.Id);
            var userDetails = HttpContext.Session.GetObjectFromJson<User>("User");
            int userID = Convert.ToInt32(userDetails.Userıd);
            BookStoreContext context = HttpContext.RequestServices.GetService(typeof(BookStoreContext)) as BookStoreContext;
            int readingbook_id = context.StoreReading(bookID, userID);

            if (readingbook_id > 0)
            {
                context.updateDatabase(currentbook, userDetails);
                ViewBag.Ava = "You Bought the book";
            }

            else
                ViewBag.Ava = "Something went Wrong";
            return View("~/Views/Book/Buy.cshtml",bookID);
        }
        
        




        }
}