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
    public class WorkingHourRepository : IWorkingHourRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration Config;
        public WorkingHourRepository(ApplicationDbContext dbContext, IConfiguration Config)
        {
            this.dbContext = dbContext;
            this.Config = Config;
        }
        public async Task<Tuple<bool, List<WorkingHour>>> GetAllWorkingHours()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {

                    {
                        using (SqlCommand cmd = new SqlCommand("GetAllWorkingHours", con))
                        {
                            con.Open();
                            List<WorkingHour> WorkingHoursList = new List<WorkingHour>();
                            cmd.CommandType = CommandType.StoredProcedure;
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                WorkingHour workingHour = new WorkingHour();
                                workingHour.Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                workingHour.FromHour = reader.IsDBNull(1) ? DateTime.Now : reader.GetDateTime(1);
                                workingHour.ToHour = reader.IsDBNull(2) ? DateTime.Now : reader.GetDateTime(2);
                                workingHour.Date = reader.IsDBNull(3) ? DateTime.Now : reader.GetDateTime(3);
                                workingHour.IsDeleted = reader.IsDBNull(4) ? false : reader.GetBoolean(4);



                                WorkingHoursList.Add(workingHour);
                            }
                            con.Close();
                            return new Tuple<bool, List<WorkingHour>>(true, WorkingHoursList);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return new Tuple<bool, List<WorkingHour>>(false, null);
            }
        }

        public async Task<Tuple<bool>> AssignWorkingHour(WorkingHour workingHour)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("AssignWorkingHour", con))
                    {
                        con.Open();

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = workingHour.Id;
                        cmd.Parameters.Add("@FromHour", SqlDbType.DateTime).Value = workingHour.FromHour;
                        cmd.Parameters.Add("@ToHour", SqlDbType.DateTime).Value = workingHour.ToHour;
                        cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = workingHour.Date;
                        cmd.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = workingHour.IsDeleted;
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



        public async Task<Tuple<bool, bool>> DeleteWorkingHour(int Id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Config.GetValue<string>("ConnectionStrings:DefaultConnection")))

                {
                    using (SqlCommand cmd = new SqlCommand("DeleteWorkingHour", con))
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
