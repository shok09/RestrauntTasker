using API.Models;
using DAL.Entities.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using API.Services.JwtAuth;
using Microsoft.AspNetCore.Identity.UI.Services;
using BLL.Services.Interfaces;
using API.Services.JwtAuth.Interfaces;
using BLL.DTO;
using API.Models.Settings;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly AuthSettings _authSettings;
        readonly IJwtFactory _jwtFactory;
        readonly ITokenValidator _tokenValidator;
        readonly IEmailSender _emailSender;
        readonly IUserService _userService;

        public AuthController(UserManager<ApplicationUser> userManager,
            IOptions<AuthSettings> authSettings,
            IJwtFactory jwtFactory,
            IEmailSender emailSender,
            IUserService userService,
            ITokenValidator tokenValidator)
        {
            _userManager = userManager;
            _authSettings = authSettings.Value;
            _jwtFactory = jwtFactory;
            _tokenValidator = tokenValidator;
            _emailSender = emailSender;
            _userService = userService;
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

            var userCreateResult = await _userManager.CreateAsync(appUser, model.Password);
            
            if (userCreateResult.Succeeded)
            {
                /*await _userService.CreateUserAsync(new UserDTO 
                { 
                    Name = model.UserName,
                    Email = model.Email,
                    ApplicationUserId = appUser.Id
                });
                */
                //await _userManager.AddToRoleAsync(appUser, model.Role);

                //var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);

                //var callbackUrl = Url.Action(
                //    "ConfirmEmail",
                //    "Auth",
                //    new { UserId = appUser.Id, EmailToken = emailToken },
                //    HttpContext.Request.Scheme);

                //await _emailSender.SendEmailAsync(
                //    appUser.Email,
                //    "RestrauntTasker - Confirm Your Email",
                //    "Please confirm your e-mail by clicking this link: <a href=\"" + callbackUrl + "\">click here</a>");

                return Ok(userCreateResult);
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
                //var roles = await _userManager.GetRolesAsync(user);
               
                var accessToken = await _jwtFactory.GenerateEncodedToken(user.Id, user.UserName, "Cook");
                var refreshToken = _jwtFactory.GenerateRefreshToken();

                //await _userService.AddRefreshTokenAsync(refreshToken, user.Id);
                
                return Ok( new { accessToken, refreshToken });
            }
            else return BadRequest(new { message = "Incorrect user info" });
        }

        [HttpPost]
        [Route("auth/refreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenViewModel model)
        {
            var principal = _tokenValidator.GetPrincipalFromToken(model.AccessToken, _authSettings.SecretKey);

            if (principal == null) return BadRequest(new { message = "Invalid token" });

            var id = principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(id.Value);
            
            var jwtToken = await _jwtFactory.GenerateEncodedToken(user.Id, user.UserName, "Cook");
            var refreshToken = _jwtFactory.GenerateRefreshToken();

            await _userService.ExchangeRefreshTokenAsync(model.RefreshToken, refreshToken, user.Id);

            return Ok(new { jwtToken, refreshToken });           
        }   


        //public async Task<IActionResult> ConfirmEmail(string userId, string emailToken)
        //{
        //    if(string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(emailToken))
        //    {
        //        ModelState.AddModelError("", "User Id and Code are required");
        //        return BadRequest(ModelState);
        //    }

        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null)
        //        return BadRequest(new { message = "error"});

        //    if (user.EmailConfirmed)
        //        return Redirect("/login");

        //    var result = await _userManager.ConfirmEmailAsync(user, emailToken);

        //    if (!result.Succeeded)
        //        return BadRequest(new { message = "error" });

        //    return RedirectToAction();
        //}
    }
}
