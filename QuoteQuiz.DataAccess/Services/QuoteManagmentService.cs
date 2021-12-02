using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using QuoteQuiz.DataAccess.Entities;
using QuoteQuiz.DataAccess.EntityFramework;
using QuoteQuiz.DataAccess.Models;
using QuoteQuiz.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.DataAccess.Services
{
    public interface IQuoteManagmentService
    {
        Task<List<QuoteViewModel>> GetQuotes();
        Task<object> GetQuote(int QuoteID);
        Task<bool> CreateQuoteBinary(CreateQuoteBinaryModel Quote);
        Task<bool> UpdateQuoteBinary(UpdateQuoteBinaryModel Quote);
        Task<bool> CreateQuoteMultiple(CreateQuoteMultipleModel Quote);
        Task<bool> UpdateQuoteMultiple(UpdateQuoteMultipleModel Quote);
    }

    public class QuoteManagmentService : BaseService, IQuoteManagmentService
    {
        public QuoteManagmentService(
            QuoteQuizDbContext quoteQuizDbContext,
            IMemoryCache memoryCache,
            IMapper mapper)
        : base(quoteQuizDbContext, memoryCache, mapper)
        {
        }

        public async Task<List<QuoteViewModel>> GetQuotes()
        {
            var result = await _quoteQuizDbContext.Set<Quotes>().ToListAsync();

            return _mapper.Map<List<QuoteViewModel>>(result);
        }

        public async Task<object> GetQuote(int QuoteID)
        {
            var result = await _quoteQuizDbContext.Set<Quotes>()
                .Include(x => x.Answers_Binary)
                .Include(x => x.Answers_Multiple)
                .FirstOrDefaultAsync(x => x.ID == QuoteID);

            if (result.Mode == (int)ModeEnum.Binary)
                return _mapper.Map<QuoteBinaryViewModel>(result.Answers_Binary);
            else if (result.Mode == (int)ModeEnum.Multiple)
                return _mapper.Map<List<MultiplePossibleAnswerViewModel>>(result.Answers_Multiple.ToList());

            return null;
        }

        public async Task<bool> CreateQuoteBinary(CreateQuoteBinaryModel Quote)
        {
            try
            {
                await _quoteQuizDbContext.Set<Quotes>().AddAsync(new Quotes
                {
                    CreateDate = DateTime.Now,
                    IsDeleted = Quote.IsDeleted,
                    LastModifiedDate = DateTime.Now,
                    QuoteText = Quote.QuoteText,
                    Mode = (int)Quote.Mode,
                    Answers_Binary = new Answers_Binary
                    {
                        CorrectAnswer = Quote.CorrectAnswer,
                    }
                });

                _quoteQuizDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                //_logger.LogCritical(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateQuoteBinary(UpdateQuoteBinaryModel Quote)
        {
            try
            {
                var result = await _quoteQuizDbContext.Set<Quotes>().FirstAsync(x => x.ID == Quote.QuoteID);

                result.LastModifiedDate = DateTime.Now;
                result.IsDeleted = Quote.IsDeleted;
                result.QuoteText = Quote.QuoteText;
                result.Answers_Binary.CorrectAnswer = Quote.CorrectAnswer;

                _quoteQuizDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                //_logger.LogCritical(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> CreateQuoteMultiple(CreateQuoteMultipleModel Quote)
        {
            try
            {
                await _quoteQuizDbContext.Set<Quotes>().AddAsync(new Quotes
                {
                    CreateDate = DateTime.Now,
                    IsDeleted = Quote.IsDeleted,
                    LastModifiedDate = DateTime.Now,
                    QuoteText = Quote.QuoteText,
                    Mode = (int)Quote.Mode,
                    Answers_Multiple = Quote.MultiplePossibleAnswers.Select(x =>
                        new Answers_Multiple
                        {
                            IsCorrect = x.IsCorrect,
                            PossibleAnwerText = x.PossibleAnwerText
                        }).ToList()
                });

                _quoteQuizDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                //_logger.LogCritical(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateQuoteMultiple(UpdateQuoteMultipleModel Quote)
        {
            try
            {
                var result = await _quoteQuizDbContext.Set<Quotes>().FirstAsync(x => x.ID == Quote.QuoteID);

                result.LastModifiedDate = DateTime.Now;
                result.IsDeleted = Quote.IsDeleted;

                foreach (var answer in result.Answers_Multiple)
                {
                    answer.IsCorrect = Quote.MultiplePossibleAnswers.FirstOrDefault(x => x.PossibleAnswerID == answer.ID).IsCorrect;
                    answer.PossibleAnwerText = Quote.MultiplePossibleAnswers.FirstOrDefault(x => x.PossibleAnswerID == answer.ID).PossibleAnwerText;
                }

                _quoteQuizDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                //_logger.LogCritical(ex.Message);
                return false;
            }

            return true;
        }

    }
}
