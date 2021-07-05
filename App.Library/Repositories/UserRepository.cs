using App.Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace App.Library.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration Config;
        public UserRepository(ApplicationDbContext dbContext, IConfiguration Config)
        {
            this.dbContext = dbContext;
            this.Config = Config;
        }

        public async Task<Tuple<bool, List<User>>> GetAllUsers()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("GetAllUsers", con))
                    {
                        con.Open();
                        List<User> UsersList = new List<User>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            User user = new User();
                            user.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            user.Email = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            user.Password = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            user.Name = reader.IsDBNull(3) ? "" : reader.GetString(3);
                            user.Mobile = reader.IsDBNull(4) ? "" : reader.GetString(4);
                            user.IsAdmin = reader.IsDBNull(5) ? false : reader.GetBoolean(5);
                            user.IsDeleted = reader.IsDBNull(6) ? false : reader.GetBoolean(6);


                            UsersList.Add(user);
                        }
                        con.Close();
                        return new Tuple<bool, List<User>>(true, UsersList);
                    }
                }
            }
            catch (Exception e)
            {
                return new Tuple<bool, List<User>>(false, null);
            }
        }

        public async Task<Tuple<bool, ResponseMessage>> SaveUser(User User)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("CreateUser", con))
                    {
                        ResponseMessage message = new ResponseMessage();
                        con.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = User.Id;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = User.Email;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = User.Password;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = User.Name;
                        cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = User.Mobile;
                        cmd.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = User.IsAdmin;
                        cmd.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = User.IsDeleted;

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            message.Success = reader.IsDBNull(0) ? false : reader.GetBoolean(0);
                            message.Message = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        }

                        con.Close();
                        return new Tuple<bool, ResponseMessage>(true, message);


                    }

                }
            }
            catch (Exception e)
            {
                return new Tuple<bool, ResponseMessage>(false, null);
            }
        }

        public async Task<Tuple<bool, bool>> DeleteUser(int Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))

                {
                    using (SqlCommand cmd = new SqlCommand("DeleteUser", con))
                    {
                        con.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        return new Tuple<bool, bool>(true, true);
                    }
                }
            }
            catch (Exception e)
            {
                return new Tuple<bool, bool>(false, false);
            }
        }

        
    }
}
