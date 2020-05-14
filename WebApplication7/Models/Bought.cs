using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class Bought
    {
        private BookStoreContext context;
        public int Id { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }
        public int Booked { get; set; }
        public string BookName { get; set; }
        public Double Money { get; set; }

        public string AuthorName { get; set; }
        public string UserName { get; set; }


    }
}
