﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WebApplication7.Models;
namespace WebApplication7
{
    public class BookStoreContext
    {
        public string ConnectionString { get; set; }

        public BookStoreContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        public List<Book> GetAllBooks()
        {
            List<Book> list = new List<Book>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from books", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Book()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            BookName = reader["book_name"].ToString(),
                            AuthorName = reader["author_name"].ToString(),
                            Available= Convert.ToInt32(reader["booked"]),
                            Price = reader["price"].ToString(),
                        });
                    }
                }
            }
            return list;
        }

    }
}
