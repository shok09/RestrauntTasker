using API.Models;
using DAL.Entities.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using Microsoft.Extensions.Options;
using API.Services.JwtAuth;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly IJwtFactory _jwtFactory;
        readonly IEmailSender _emailSender;
        
        public AuthController(UserManager<ApplicationUser> userManager, IJwtFactory jwtFactory, IEmailSender emailSender)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Route("auth/register")]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var appUser = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, model.Role);

                var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Auth",
                    new { UserId = appUser.Id, EmailToken = emailToken },
                    HttpContext.Request.Scheme);

                await _emailSender.SendEmailAsync(
                    appUser.Email,
                    "TaskTracker - Confirm Your Email",
                    "Please confirm your e-mail by clicking this link: <a href=\"" + callbackUrl + "\">click here</a>");

                return Ok(result);
            }
            else return BadRequest(new { message = "error"});
        }

        [HttpPost]
        [Route("auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roles = await _userManager.GetRolesAsync(user);

                var token = await _jwtFactory.GenerateEncodedToken(user.Id, user.UserName, roles.FirstOrDefault());

                return Ok(token);
            }
            else return BadRequest(new { message = "Incorrect user info" });
        }

        /*public async Task<IActionResult> ConfirmEmail(string userId, string emailToken)
        {
            if(string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(emailToken))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return BadRequest(new { message = "error"});

            if (user.EmailConfirmed)
                return Redirect("/login");

            var result = await _userManager.ConfirmEmailAsync(user, emailToken);

            if (!result.Succeeded)
                return BadRequest(new { message = "error" });

            return RedirectToAction();
        }*/
    }
}
