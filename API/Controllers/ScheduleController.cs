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
    [Route("api/schedule")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleRepository IScheduleRepository;
        public ScheduleController(IScheduleRepository IScheduleRepository)
        {
            this.IScheduleRepository = IScheduleRepository;
        }

        [HttpGet]
        [Route("GetSchedule")]
        public async Task<IActionResult> GetSchedule()
        {
            var Result = await IScheduleRepository.GetSchedule();
            if (!Result.Item1)
                return BadRequest();
            return Ok(Result.Item2);


        }

        [HttpDelete]
        [Route("DeleteSchedule")]
        public async Task<IActionResult> DeleteAppointment(int Id)
        {
            var Result = await IScheduleRepository.DeleteSchedule(Id);

            if (!Result.Item1)
            {
                return BadRequest();
            }

            return Ok(Result.Item1);
        }

        [HttpPost]
        [Route("SaveSchedule")]
        public async Task<IActionResult> SaveSchedule(Schedule Schedule)
        {
            var AllSchedule = await this.IScheduleRepository.GetSchedule();
            var Result = await IScheduleRepository.SaveSchedule(Schedule);
            if (!Result.Item1)
                return BadRequest();
            return Ok(Result.Item1);
        }

        [HttpPost]
        [Route("SaveSlot")]
        public async Task<IActionResult> SaveSlot([FromBody]List<Schedule> Schedule)
        {
            
            foreach (Schedule schedule in Schedule)
            {
                var Result = await IScheduleRepository.SaveSchedule(schedule);

                if (!Result.Item1)
                {
                    return BadRequest();
                }
            }
            return Ok(true);
            ;
        }
    }
}
