using App.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Library.Repositories
{
    public interface IWorkingHourRepository
    {
        Task<Tuple<bool, List<WorkingHour>>> GetAllWorkingHours();
        Task<Tuple<bool>> AssignWorkingHour(WorkingHour workingHour);
        Task<Tuple<bool, bool>> DeleteWorkingHour(int Id);

    }
}
