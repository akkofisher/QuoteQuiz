using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuoteQuiz.DataAccess.Models;
using QuoteQuiz.DataAccess.Services;
using QuoteQuiz.Web.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuoteQuiz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagmentController : BaseController
    {

        private readonly IUserManagmentService _userManagmentService;

        public UserManagmentController(ILogger<UserManagmentController> logger, IUserManagmentService userManagmentService)
            : base(logger)
        {
            _userManagmentService = userManagmentService;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IEnumerable> GetUsers()
        {
            return await _userManagmentService.GetUsers();
        }

        [HttpGet]
        [Route("UserLogOut")]
        public async Task<IActionResult> UserLogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(true);
        }

        [HttpPost]
        [Route("UserSimpleAuth")]
        public async Task<IActionResult> UserSimpleAuth([FromBody] int userID)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var result = await _userManagmentService.GetUser(userID);

            if (result == null && !result.IsDisabled)
            {
                return NotFound();
            }

            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, result.ID.ToString()),
              new Claim(ClaimTypes.Name, result.Name),
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Ok(true);
        }

        [HttpGet]
        [Route("GetUserAuthInfo")]
        public async Task<IActionResult> GetUserAuthInfo()
        {
            if (User.Identity.IsAuthenticated)
            {
                var result = await _userManagmentService.GetUser(CurrentUserID);
                return Ok(result);
            }

            return Ok(false);
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<bool> CreateUser([FromBody] CreateUserModel user)
        {
            return await _userManagmentService.CreateUser(user);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<bool> UpdateUser([FromBody] UpdateUserModel user)
        {
            return await _userManagmentService.UpdateUser(user);
        }

        [HttpGet]
        [Route("ReviewUser")]
        public async Task<ReviewUserViewModel> ReviewUser([FromQuery] int userID)
        {
            return await _userManagmentService.ReviewUser(userID);
        }
    }
}
