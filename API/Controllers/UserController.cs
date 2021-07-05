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
    [Route("api/users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : ControllerBase
    {   
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository IUserRepository;


        public UserController(IUserRepository IUserRepository, UserManager<IdentityUser> _userManager)
        {
            this.IUserRepository = IUserRepository;
            this._userManager = _userManager;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        
        public async Task<IActionResult> GetAllUsers()
        {
            var Result = await IUserRepository.GetAllUsers();
            if (!Result.Item1)
                return BadRequest();
            return Ok(Result.Item2);


        }


        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int Id)
        {
            var Result = await IUserRepository.DeleteUser(Id);
            
            if (!Result.Item1) {
                return BadRequest();
            }
            
            return Ok(Result.Item1);
        }


        [HttpPost]
        [Route("SaveUser")]
        public async Task<IActionResult> SaveUser(User User)
        {
            var AllUsers = await this.IUserRepository.GetAllUsers();
            if (User.Id == 0)
            {
                IdentityUser ExistEmail = await _userManager.FindByEmailAsync(User.Email);
                if (ExistEmail != null)
                    return Ok(new ResponseMessage { Success = false, Message = "Email Already exists" });
            }

            var Result = await IUserRepository.SaveUser(User);
            if (!Result.Item1)
            {
                return BadRequest();
            }
            return Ok(Result.Item2);
        }
    }
}
