using App.Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace App.Library.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration Config;
        public AppointmentRepository(ApplicationDbContext dbContext, IConfiguration Config)
        {
            this.dbContext = dbContext;
            this.Config = Config;
        }

        public async Task<Tuple<bool, List<Appointment>>> GetAllAppointments()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("GetAllAppointments", con))
                    {
                        con.Open();
                        List<Appointment> AppointmentsList = new List<Appointment>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Appointment app = new Appointment();
                            app.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            app.FirstName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            app.MiddleName = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            app.LastName = reader.IsDBNull(3) ? "" : reader.GetString(3);
                            app.Mobile = reader.IsDBNull(4) ? "" : reader.GetString(4);
                            app.Email = reader.IsDBNull(5) ? "" : reader.GetString(5);
                            app.Date = reader.IsDBNull(6) ? DateTime.Now : reader.GetDateTime(6);
                            app.StartTime = reader.IsDBNull(7) ? DateTime.Now : reader.GetDateTime(7);
                            app.EndTime = reader.IsDBNull(8) ? DateTime.Now : reader.GetDateTime(8);
                            app.Duration = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                            app.IsDeleted = reader.IsDBNull(10) ? false : reader.GetBoolean(10);
                            app.Status = reader.IsDBNull(11) ? "" : reader.GetString(11);

                            AppointmentsList.Add(app);
                        }
                        con.Close();
                        return new Tuple<bool, List<Appointment>>(true, AppointmentsList);
                    }
                }
            }
            catch (Exception e)
            {
                return new Tuple<bool, List<Appointment>>(false, null);
            }
        }

        public async Task<Tuple<bool>> SaveAppointment(Appointment App)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("CreateAppointment", con))
                    {
                        con.Open();

              
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = App.Id;
                        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = App.FirstName;
                        cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = App.MiddleName;
                        cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = App.LastName;
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = App.Email;
                        cmd.Parameters.Add("@Mobile", SqlDbType.VarChar).Value = App.Mobile;
                        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = App.Date;
                        cmd.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = App.StartTime;
                        cmd.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = App.EndTime;
                        cmd.Parameters.Add("@Duration", SqlDbType.Int).Value = App.Duration;
                        cmd.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = App.IsDeleted;
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = App.Status;

                        cmd.ExecuteNonQuery();
                        con.Close();
                    return new Tuple<bool>(true);


                    }

                }
            }
            catch(Exception e)
            {
                return new Tuple<bool>(false);
            }




        }

        public async Task<Tuple<bool, bool>> DeleteAppointment(int Id)
        {
            
                try
                {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))

                {
                    using (SqlCommand cmd = new SqlCommand("DeleteAppointment", con))
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

