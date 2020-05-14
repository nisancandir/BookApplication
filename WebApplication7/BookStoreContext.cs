using System;
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

        public MySqlConnection GetConnection()
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
                            Available = Convert.ToInt32(reader["booked"]),
                            Price = reader["price"].ToString(),
                        });
                    }
                }
            }
            return list;
        }
        public int SaveDetails(RegisterViewModel umodel)
        {

            int Id;
            using (MySqlConnection conn = GetConnection())
            {
                String username = umodel.Username;
                String email = umodel.Email;
                String password = umodel.Password;

                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "INSERT INTO users(username,email,password,money,admin) VALUES(@Username,@Email,@Password,50,0);";
                comm.Parameters.AddWithValue("?Username", umodel.Username);
                comm.Parameters.AddWithValue("?Email", umodel.Email);
                comm.Parameters.AddWithValue("?Password", umodel.Password);
                comm.ExecuteNonQuery();

                Id = (int)comm.LastInsertedId;
                
                conn.Close();
            }

            return Id;
        }
        public int CheckEmail(RegisterViewModel umodel)
        {
              int a =0;
            int b =0;
            int c =0;
            
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from users", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) {
                        a = 0;
                        b = 0;
                        c = 0;
                        String user_name = reader["username"].ToString();
                        String e_mail = reader["email"].ToString();
                        String pass_word = reader["password"].ToString();
                        if (user_name.Equals(umodel.Username)) {
                            a = 1;
                        }
                        if (e_mail.Equals(umodel.Email))
                        {
                            b = 1;
                        }
                        if (pass_word.Equals(umodel.Password))
                        {
                            c = 1;
                        }


                    }
                }

             }
            if (a == 1 && b==1 && c==1)
                return 1;
            else
                return 0;

        }
        public Bought findBook(int id,Bought bought)
        {
            int Id;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from books", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Id = Convert.ToInt32(reader["id"]);
                        if (id == Id) {
                            bought.BookName = reader["book_name"].ToString();
                            bought.AuthorName = reader["author_name"].ToString();
                            bought.Booked = Convert.ToInt32(reader["booked"]);
                            bought.Money = Convert.ToDouble(reader["price"]);


                        }

                    }
                }
            }
            return bought;
        }
    
        




    }
}
