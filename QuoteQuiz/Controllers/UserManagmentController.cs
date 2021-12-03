﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuoteQuiz.DataAccess.Models;
using QuoteQuiz.DataAccess.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteQuiz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagmentController : ControllerBase
    {

        private readonly IUserManagmentService _userManagmentService;
        private readonly ILogger<UserManagmentController> _logger;

        public UserManagmentController(ILogger<UserManagmentController> logger, IUserManagmentService userManagmentService)
        {
            _userManagmentService = userManagmentService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IEnumerable> GetUsers()
        {
            return await _userManagmentService.GetUsers();
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
