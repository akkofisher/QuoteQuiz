using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuoteQuiz.DataAccess.Models;
using QuoteQuiz.DataAccess.Models.Enums;
using QuoteQuiz.DataAccess.Services;
using QuoteQuiz.Web.Controllers;
using System.Threading.Tasks;

namespace QuoteQuiz.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserQuoteController : BaseController
    {

        private readonly IUserQuoteService _userQuoteService;

        public UserQuoteController(ILogger<UserQuoteController> logger, IUserQuoteService userQuoteService)
            : base(logger)
        {
            _userQuoteService = userQuoteService;
        }

        [HttpGet]
        [Route("GetUserQuote")]
        public async Task<IActionResult> GetUserQuote()
        {
            var result = await _userQuoteService.GetUserQuote(CurrentUserID);

            if (result == null)
            {
                return Ok(false);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("UpdateUserMode")]
        public async Task<bool> UpdateUserMode([FromQuery] ModeEnum userMode)
        {
            return await _userQuoteService.UpdateUserMode(CurrentUserID, userMode);
        }

        [HttpPost]
        [Route("AnswerUserQuote")]
        public async Task<AnswerResultUserQuoteViewModel> AnswerUserQuote([FromBody] AnswerUserQuoteModel answerUserQuote)
        {
            return await _userQuoteService.AnswerUserQuote(CurrentUserID, answerUserQuote);
        }

    }
}
