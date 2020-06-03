using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;


using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;

namespace WebApplication7.Models
{

    public class RegisterViewModel
    {
        

        private BookStoreContext context;
        public string Username { get; set; }

        public string Email { get; set; }

        
        public string Password { get; set; }
    
      



    }







}
