using App.Library.Models;
using App.Library.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace API.Controllers
{
    [Route("api/status")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class LookupController : ControllerBase
    {

        private readonly IStatusRepository IStatusRepository;
        public LookupController(IStatusRepository IStatusRepository)
        {
            this.IStatusRepository = IStatusRepository;

        }

        [HttpGet]
        [Route("GetAllStatuses")]

        public async Task<IActionResult> GetAllStatuses()
        {
            var Result = await IStatusRepository.GetAllStatuses();
            if (!Result.Item1)
                return BadRequest();
            return Ok(Result.Item2);
        }

        [HttpPost]
        [Route("SaveStatus")]

        public async Task<IActionResult> SaveStatus(Lookup Lookup)
        {
            var AllStatuses = await this.IStatusRepository.GetAllStatuses();
            var Result = await IStatusRepository.SaveStatus(Lookup);
            if (!Result.Item1)
                return BadRequest();

            return Ok(Result.Item1);
        }

        [HttpDelete]
        [Route("DeleteStatus")]
        public async Task<IActionResult> DeleteStatus(int Id)
        {
            var Result = await IStatusRepository.DeleteStatus(Id);

            if (!Result.Item1)
            {
                return BadRequest();
            }

            return Ok(Result.Item1);
        }

        
    }

}
