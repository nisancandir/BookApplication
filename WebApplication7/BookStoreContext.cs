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
                            Price =Convert.ToDouble( reader["price"]),
                        });
                    }
                }
            }
            return list;
        }
        public User SaveDetails(User umodel)
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
            umodel.Userıd = Id;
            umodel.money = 50.0;
            umodel.Admin = 0;
            return umodel;
        }
        public int CheckEmail(User umodel)
        {
              int a =0;
            int b =0;
            int c =0;
            int id = 0;
            int flag = 0;
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
                        int Id = Convert.ToInt32(reader["user_id"]);
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
                        if (a == 1 && b == 1 && c == 1)
                        {
                            id = Id;
                            flag = 1;
                        }

                    }
                }

             }
            if (flag==1)
                return id;
            else
                return 0;

        }
        public User CheckUserInfo(User umodel)
        {
            int a = 0;
            int b = 0;
            int c = 0;
            int id = 0;
            int flag = 0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from users", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        a = 0;
                        b = 0;
                        c = 0;
                        int Id = Convert.ToInt32(reader["user_id"]);
                        String user_name = reader["username"].ToString();
                        String e_mail = reader["email"].ToString();
                        String pass_word = reader["password"].ToString();
                        double currentmon = Convert.ToDouble(reader["money"]);
                        int curadmin= Convert.ToInt32(reader["admin"]);
                        if (user_name.Equals(umodel.Username))
                        {
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
                        if (a == 1 && b == 1 && c == 1)
                        {
                            umodel.Userıd = Id;
                            umodel.money = currentmon;
                            umodel.Admin = curadmin;
                            flag = 1;

                        }

                    }
                }

            }
            if (flag == 1)
                return umodel;
            else
                umodel.Userıd = 0;
                return umodel;

        }
        public Book findBook(int id,Book bought)
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
                            bought.Id = Id;
                            bought.BookName = reader["book_name"].ToString();
                            bought.AuthorName = reader["author_name"].ToString();
                            bought.Available = Convert.ToInt32(reader["booked"]);
                            bought.Price = Convert.ToDouble(reader["price"]);


                        }

                    }
                }
            }
            return bought;
        }
        public int FindUser(String username)
        {
            int Id=0;
            int uid=0;
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from users", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Id = Convert.ToInt32(reader["user_id"]);
                        String user_name = reader["username"].ToString();

                        if (user_name.Equals(username))
                        {
                            uid = Id;
                            
                        }
                    }
                }
            }
            if (Id == 0)
                return 0;
            else
                return uid;
        }
        public int StoreReading(int bookid, int userid)
        {
            int Id=0;
            
                using (MySqlConnection conn = GetConnection())
                {


                    conn.Open();
                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = "INSERT INTO reading(user_id,book_id,read) VALUES(@Userıd,@Bookıd,@isreaded);";
                    comm.Parameters.AddWithValue("?Userıd", userid);
                    comm.Parameters.AddWithValue("?Bookıd", bookid);
                    comm.Parameters.AddWithValue("?isreaded", 0);
                    comm.ExecuteNonQuery();
                    Id = (int)comm.LastInsertedId;

                    conn.Close();
                }
            
          


            return Id;
}
        public int updateDatabase(Book currentbook,User a)
        {
            int bookID = Convert.ToInt32(currentbook.Id);
            Double bookMoney = Convert.ToDouble(currentbook.Price);
            int ex=0;
            int Id;
            Double usermoney = 0;
            using (MySqlConnection conn = GetConnection())
            {


                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "Update books SET booked=@newvalue WHERE id=@id;";
                comm.Parameters.AddWithValue("?newvalue", 1);
                comm.Parameters.AddWithValue("?id", bookID);
                ex = comm.ExecuteNonQuery();
                conn.Close();

            }
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from users", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Id = Convert.ToInt32(reader["id"]);
                        if (a.Userıd == Id) {
                            usermoney = Convert.ToDouble(reader["price"]);

                        }
                    }
                }
            }
            using (MySqlConnection conn = GetConnection())
            {
                double x=usermoney - bookMoney;
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "Update users SET money=@moneyvalue WHERE id=@id;";
                comm.Parameters.AddWithValue("?moneyvalue", x);
                comm.Parameters.AddWithValue("?id", a.Userıd);
                conn.Close();
            }
                return ex;
        }

        }
}
