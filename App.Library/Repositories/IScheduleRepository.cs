using App.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Library.Repositories
{
    public interface IScheduleRepository
    {
        Task<Tuple<bool, List<Schedule>>> GetSchedule();
        Task<Tuple<bool>> SaveSchedule(Schedule schedule);
        Task<Tuple<bool, bool>> DeleteSchedule(int Id);
    }
}

