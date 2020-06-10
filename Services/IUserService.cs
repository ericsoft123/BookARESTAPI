using MywebApi.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MywebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MywebApi.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(Registerview model);

        Task<UserManagerResponse> LoginUserAsync(Loginview model);

        Task<UserManagerResponse> SubscribeUserAsync(Subscription model);
        Task<UserManagerResponse> UnSubscribeUserAsync(Subscription model);




        //T Get(int id);
        //IEnumerable<T> Getall();
    }
    public class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> _signManager;
        private readonly BookDbContext _db;
        private UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;

      
        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignManager, IConfiguration configuration,BookDbContext db)
        {
            _userManager = userManager;
            _signManager = SignManager;
            _configuration = configuration;
            _db = db;
            
        }

       

        public async Task<UserManagerResponse> LoginUserAsync(Loginview model)
        {
            if (model == null)
                throw new NullReferenceException("invalid forms");
            var result = await _signManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);//registration /insert data
            if (result.Succeeded)//if create user will be successful then sign in that account 
            {
                //
                var claims = new[]
                {
                     new Claim("Email",model.Email),
                     new Claim(ClaimTypes.NameIdentifier,model.Password),
                     new Claim(ClaimTypes.Name, model.Email),
                     
                      
            };
                //ValidAudience = Configuration["AuthSettings:Audience"],
                //ValidIssuer = Configuration["AuthSettings:Issuer"],
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:keys"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["AuthSettings:Audience"],
                    audience: _configuration["AuthSettings:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

                string tokenAsstring = new JwtSecurityTokenHandler().WriteToken(token);
                return new UserManagerResponse
                {
                    Message = tokenAsstring,
                    IsSuccess = true,
                    Expired = token.ValidTo
                };

                //
            }
            return new UserManagerResponse
            {
                Message = "User not exist please create account",
                IsSuccess = false,

            };

        }

       

        public async Task<UserManagerResponse> RegisterUserAsync(Registerview model)
        {
            if (model == null)
                throw new NullReferenceException("Register Model is null");
            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm Password does not match with Password",
                    IsSuccess = false,
                };
            var identityUser = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName


            };
            var result = await _userManager.CreateAsync(identityUser, model.Password);
            if (result.Succeeded)
            {
                //
                return new UserManagerResponse
                {
                    Message = "User Created Successfuly",
                    IsSuccess = true,

                };

                //
            }
            return new UserManagerResponse
            {
                Message = "User did not Create",
                IsSuccess = false,
                Erros = result.Errors.Select(e => e.Description),
            };



        }

        public async Task<UserManagerResponse> SubscribeUserAsync(Subscription model)
        {
            if (model == null)
                return new UserManagerResponse
                {
                    Message = "something went wrong with forms",
                    IsSuccess = false,




                };
            //search first in plan if data is available for security reason
            var searchplan = _db.plan.FirstOrDefault(u => u.planId == model.planId);
            if(searchplan==null)
                return new UserManagerResponse
                {
                    Message = "unable to subscribe to our plan,This plan Id is not available to our plan please check plan and choose plan id",
                    IsSuccess = false,




                };


            var searchData = _db.Subscriptions.FirstOrDefault(u => u.planId == model.planId && u.email == model.email);

            if (searchData!= null)
                return new UserManagerResponse
                {
                    Message = "You have already subscribed to this item",
                    IsSuccess = false,
                    



                };
          
            _db.Subscriptions.Add(model);
            var result = await _db.SaveChangesAsync();

            return new UserManagerResponse
            {
                Message = "you have successfully subscribed",
                IsSuccess = true,



            };

        }

        public async Task<UserManagerResponse> UnSubscribeUserAsync(Subscription model)
        {
            //
            if (model == null)
                return new UserManagerResponse
                {
                    Message = "something went wrong with forms",
                    IsSuccess = false,




                };

            var searchData = _db.Subscriptions.FirstOrDefault(u => u.planId == model.planId && u.email == model.email);

            if (searchData == null)//there is no data
                return new UserManagerResponse
                {
                    Message = "There is no data correspond to your input",
                    IsSuccess = false,




                };

            _db.Subscriptions.Remove(searchData);
            var result = await _db.SaveChangesAsync();

            return new UserManagerResponse
            {
                Message = "you have successfully Unsubscribed",
                IsSuccess = true,



            };
            //
        }
    }
}
