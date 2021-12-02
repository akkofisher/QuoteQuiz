﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuoteQuiz.DataAccess.Models;
using QuoteQuiz.DataAccess.Models.Enums;
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
    public class UserQuoteController : ControllerBase
    {

        private readonly IUserQuoteService _userQuoteService;
        private readonly ILogger<UserQuoteController> _logger;

        public UserQuoteController(ILogger<UserQuoteController> logger, IUserQuoteService userQuoteService)
        {
            _userQuoteService = userQuoteService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetUserQuote")]
        public async Task<UserQuoteViewModel> GetUserQuote(int userID)
        {
            return await _userQuoteService.GetUserQuote(userID);
        }

        [HttpPost]
        [Route("UpdateUserMode")]
        public async Task<bool> UpdateUserMode(int userID, ModeEnum userMode)
        {
            return await _userQuoteService.UpdateUserMode(userID, userMode);
        }

        [HttpPost]
        [Route("AnswerUserQuote")]
        public async Task<AnswerResultUserQuoteViewModel> AnswerUserQuote(AnswerUserQuoteModel answerUserQuote)
        {
            return await _userQuoteService.AnswerUserQuote(answerUserQuote);
        }


    }
}
