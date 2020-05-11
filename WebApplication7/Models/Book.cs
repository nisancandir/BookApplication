using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class Book
    {
        private BookStoreContext context;

        public int Id { get; set; }

        public string BookName { get; set; }

        public string AuthorName { get; set; }

        public string Alphabet { get; set; }
    }
}
