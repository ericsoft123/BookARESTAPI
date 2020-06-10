using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MywebApi.Models;
using MywebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MywebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly SignInManager<ApplicationUser> _signManager;
        private IUserService _userService;
        public AccountController(IUserService userservices, SignInManager<ApplicationUser> signInManager)
        {
            _userService = userservices;
            _signManager = signInManager;
        }

        //api/Account/
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]Registerview model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);
                if (result.IsSuccess)
                    return Ok(result);//from userInterface Repository
                return BadRequest(result);
            }
            return BadRequest("Errors occured your forms is not valid");
        }
      
        

        //api/Account/
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]Loginview model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                if (result.IsSuccess)
                    return Ok(result);
                return BadRequest(result);

            }
            return BadRequest("Errors occured your forms is not valid");
        }





    }

}