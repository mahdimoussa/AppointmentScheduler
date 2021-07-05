using App.Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace App.Library.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration Config;
        public ScheduleRepository(ApplicationDbContext dbContext, IConfiguration Config)
        {
            this.dbContext = dbContext;
            this.Config = Config;
        }

        public async Task<Tuple<bool, List<Schedule>>> GetSchedule()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("GetSchedule", con))
                    {
                        con.Open();
                        List<Schedule> ScheduleList = new List<Schedule>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Schedule app = new Schedule();
                            app.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            app.Weekdays = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            app.NumberOfAppointments = reader.IsDBNull(2) ? "" : reader.GetString(2);
                            app.StartingTime = reader.IsDBNull(3) ? DateTime.Now : reader.GetDateTime(3);
                            app.BreakBetweenApps = reader.IsDBNull(4) ? "" : reader.GetString(4);

                            ScheduleList.Add(app);
                        }
                        con.Close();
                        return new Tuple<bool, List<Schedule>>(true, ScheduleList);
                    }
                }
            }
            catch (Exception e)
            {
                return new Tuple<bool, List<Schedule>>(false, null);
            }
        }

        public async Task<Tuple<bool>> SaveSchedule(Schedule schedule)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("CreateSchedule", con))
                    {
                        con.Open();


                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = schedule.Id;
                        cmd.Parameters.Add("@NumberOfAppointments", SqlDbType.Int).Value = schedule.NumberOfAppointments;
                        cmd.Parameters.Add("@StartingTime", SqlDbType.DateTime).Value = schedule.StartingTime;
                        cmd.Parameters.Add("@BreakBetweenApps", SqlDbType.VarChar).Value = schedule.BreakBetweenApps;
                        cmd.Parameters.Add("@AppointmentDuration", SqlDbType.VarChar).Value = schedule.AppointmentDuration;

                        cmd.ExecuteNonQuery();
                        con.Close();
                        return new Tuple<bool>(true);


                    }

                }
            }
            catch (Exception e)
            {
                return new Tuple<bool>(false);
            }




        }

        public async Task<Tuple<bool, bool>> DeleteSchedule(int Id)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))

                {
                    using (SqlCommand cmd = new SqlCommand("DeleteSchedule", con))
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
