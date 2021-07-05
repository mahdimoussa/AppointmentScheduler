using App.Library.Models;
using App.Library.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository IAppointmentRepository;
        public AppointmentController(IAppointmentRepository IAppointmentRepository)
        {
            this.IAppointmentRepository = IAppointmentRepository;
        }
        
        [HttpGet]
        [Route("GetAllAppointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var Result = await IAppointmentRepository.GetAllAppointments();
            if (!Result.Item1)
                return BadRequest();
            return Ok(Result.Item2);


        }

        [HttpDelete]
        [Route("DeleteAppointment")]
        public async Task<IActionResult> DeleteAppointment(int Id)
        {
            var Result = await IAppointmentRepository.DeleteAppointment(Id);

            if (!Result.Item1)
            {
                return BadRequest();
            }

            return Ok(Result.Item1);
        }

        [HttpPost]
        [Route("SaveAppointment")]
        public async Task<IActionResult> SaveAppointment(Appointment Appointment)
        {
            var AllAppointments = await this.IAppointmentRepository.GetAllAppointments();

            foreach (Appointment appointment in AllAppointments.Item2)
            {
                if (Appointment.Date == appointment.Date)
                {
                    if (Appointment.StartTime >= appointment.StartTime && appointment.StartTime < Appointment.EndTime)
                    {
                        return Ok(new ResponseMessage { Success = false, Message = "Appointment time Already reserved" });
                    }
                }
            }

            //If there exist several appointments at the same time 
            // foreach(Appointment  appointment in AllAppointments.Item2)
            //  {
            //     if (Appointment.Date == appointment.Date && Appointment.StartTime == appointment.StartTime)
            //     {
            //         return Ok(new ResponseMessage { Success = false, Message = "Appointment Already exists" });
            //     }
            //  }

            var Result = await IAppointmentRepository.SaveAppointment(Appointment);
            if (!Result.Item1)
                return BadRequest();
            return Ok(Result.Item1);
        }
    }
}
