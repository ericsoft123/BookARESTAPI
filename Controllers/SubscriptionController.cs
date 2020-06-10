using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MywebApi.Data;
using MywebApi.Models;
using MywebApi.Services;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MywebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signManager;
        private IUserService _userService;
        private readonly BookDbContext _db;
        public SubscriptionController(IUserService userservices, SignInManager<ApplicationUser> signInManager, BookDbContext db)
        {
            _userService = userservices;
            _signManager = signInManager;
            _db = db;
        }
        [Authorize]
        [HttpPost("mysubscription")]
        public IActionResult Getall()
        {
            var subscriptions = _db.Subscriptions.ToList();
            //return (new { data = subscriptions });
            return Ok(new { data = subscriptions });
        }
        //api/Account/
        [Authorize]
        [HttpPost("Subscribe")]
        public async Task<IActionResult> SubscriptionAsync([FromBody]Subscription model)
        {
            if (ModelState.IsValid)
            {
                model.Created_at = DateTime.Today;
                model.End_at = DateTime.Today.AddMonths(1);
                model.email = User.Identity.Name;
                var result = await _userService.SubscribeUserAsync(model);
                if (result.IsSuccess)
                    return Ok(result);//from userInterface Repository
                return BadRequest(result);
            }
            return BadRequest("Errors occured your forms is not valid");
        }

        //api/Account/
        [Authorize]
        [HttpDelete("UnSubscribe")]
        public async Task<IActionResult> UnSubscriptionAsync([FromBody]Subscription model)
        {
            if (ModelState.IsValid)
            {
                model.email = User.Identity.Name;
                var result = await _userService.UnSubscribeUserAsync(model);
                if (result.IsSuccess)
                    return Ok(result);//from userInterface Repository
                return BadRequest(result);
            }
            return BadRequest("Errors occured your forms is not valid");
        }



    }
}