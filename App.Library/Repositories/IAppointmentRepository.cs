using App.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Library.Repositories
{
    public interface IAppointmentRepository
    {
        Task<Tuple<bool, List<Appointment>>> GetAll();
        Task<Tuple<bool>> SaveAppointment(Appointment appointment);
        Task<Tuple<bool, bool>> DeleteAppointment(int Id);


    }
}
