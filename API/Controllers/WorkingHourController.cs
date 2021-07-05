using App.Library.Models;
using App.Library.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/workinghours")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class WorkingHourController : ControllerBase
    {
        private readonly IWorkingHourRepository IWorkingHourRepository;


        public WorkingHourController(IWorkingHourRepository IWorkingHourRepository)
        {
            this.IWorkingHourRepository = IWorkingHourRepository;
        }

        [HttpGet]
        [Route("GetAllWorkingHours")]

        public async Task<IActionResult> GetAllWorkingHours()
        {
            var Result = await IWorkingHourRepository.GetAllWorkingHours();
            if (!Result.Item1)
                return BadRequest();
            return Ok(Result.Item2);


        }


        [HttpDelete]
        [Route("DeleteWorkingHour")]
        public async Task<IActionResult> DeleteWorkingHour(int Id)
        {
            var Result = await IWorkingHourRepository.DeleteWorkingHour(Id);

            if (!Result.Item1)
            {
                return BadRequest();
            }

            return Ok(Result.Item1);
        }


        [HttpPost]
        [Route("AssignWorkingHour")]
        public async Task<IActionResult> AssignWorkingHour(WorkingHour WorkingHour)
        {
            var AllWorkingHours = await this.IWorkingHourRepository.GetAllWorkingHours();

            foreach (WorkingHour workingHour in AllWorkingHours.Item2)
            {
                if (WorkingHour.Date == workingHour.Date)
                {
                    if (WorkingHour.FromHour >= workingHour.FromHour && workingHour.FromHour < WorkingHour.ToHour)
                    {
                        return Ok(new ResponseMessage { Success = false, Message = "Working Hour Already Assigned" });
                    }
                }
            }

            var Result = await IWorkingHourRepository.AssignWorkingHour(WorkingHour);
            if (!Result.Item1)
                return BadRequest();
            return Ok(Result.Item1);
        }
    }
}
