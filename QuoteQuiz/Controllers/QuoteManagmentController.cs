using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuoteQuiz.DataAccess.Models;
using QuoteQuiz.DataAccess.Services;
using QuoteQuiz.Web.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuoteQuiz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteManagmentController : BaseController
    {

        private readonly IQuoteManagmentService _quoteManagmentService;

        public QuoteManagmentController(ILogger<QuoteManagmentController> logger, IQuoteManagmentService quoteManagmentService)
            : base(logger)
        {
            _quoteManagmentService = quoteManagmentService;
        }

        [HttpGet]
        [Route("GetQuotes")]
        public async Task<IEnumerable> GetQuotes()
        {
            return await _quoteManagmentService.GetQuotes();
        }

        [HttpGet]
        [Route("GetQuote")]
        public async Task<object> GetQuote([FromQuery] int QuoteID)
        {
            return await _quoteManagmentService.GetQuote(QuoteID);
        }

        [HttpPost]
        [Route("CreateQuoteBinary")]
        public async Task<bool> CreateQuoteBinary([FromBody] CreateQuoteBinaryModel Quote)
        {
            return await _quoteManagmentService.CreateQuoteBinary(Quote);
        }

        [HttpPost]
        [Route("UpdateQuoteBinary")]
        public async Task<bool> UpdateQuoteBinary([FromBody] UpdateQuoteBinaryModel Quote)
        {
            return await _quoteManagmentService.UpdateQuoteBinary(Quote);
        }

        [HttpPost]
        [Route("CreateQuoteMultiple")]
        public async Task<bool> CreateQuoteMultiple([FromBody] CreateQuoteMultipleModel Quote)
        {
            return await _quoteManagmentService.CreateQuoteMultiple(Quote);
        }

        [HttpPost]
        [Route("UpdateQuoteMultiple")]
        public async Task<bool> UpdateQuoteMultiple([FromBody] UpdateQuoteMultipleModel Quote)
        {
            return await _quoteManagmentService.UpdateQuoteMultiple(Quote);
        }
    }
}
