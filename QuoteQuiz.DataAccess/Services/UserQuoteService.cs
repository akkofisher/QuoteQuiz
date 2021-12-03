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
    public interface IUserQuoteService
    {
        Task<UserQuoteViewModel> GetUserQuote(int userID);
        Task<bool> UpdateUserMode(int userID, ModeEnum userMode);
        Task<AnswerResultUserQuoteViewModel> AnswerUserQuote(int userID, AnswerUserQuoteModel answerUserQuote);
    }

    public class UserQuoteService : BaseService, IUserQuoteService
    {
        public UserQuoteService(
            QuoteQuizDbContext quoteQuizDbContext,
            IMemoryCache memoryCache,
            IMapper mapper)
        : base(quoteQuizDbContext, memoryCache, mapper)
        {
        }

        public async Task<UserQuoteViewModel> GetUserQuote(int userID)
        {
            var userResult = await _quoteQuizDbContext.Set<Users>().FirstAsync(x => x.ID == userID);
            if (userResult == null)
                return null;

            var result = await _quoteQuizDbContext.Set<Quotes>()
                .Include(m => m.Answers_Multiple)
                .Where(x =>
                       x.Mode == userResult.CurrentMode &&
                       !x.User_Answers.Any(a => a.UserID == userID && x.Mode == a.User.CurrentMode && x.ID == a.QuoteID) &&
                       !x.IsDeleted)
                .OrderBy(o => o.ID).FirstOrDefaultAsync();

            return _mapper.Map<UserQuoteViewModel>(result);
        }

        public async Task<AnswerResultUserQuoteViewModel> AnswerUserQuote(int userID, AnswerUserQuoteModel answerUserQuote)
        {
            try
            {
                var result = await _quoteQuizDbContext.Set<Quotes>()
                    .Include(x => x.Answers_Multiple)
                    .Include(x => x.Answers_Binary)
                    .FirstOrDefaultAsync(x => x.ID == answerUserQuote.QuoteID);

                if (result == null)
                    return null;

                AnswerResultUserQuoteViewModel data = new AnswerResultUserQuoteViewModel()
                {
                    UserID = userID,
                    QuoteID = answerUserQuote.QuoteID,
                };

                if (result.Mode == (int)ModeEnum.Binary)
                {
                    await _quoteQuizDbContext.Set<User_Answers>().AddAsync(
                        new User_Answers
                        {
                            CreateDate = DateTime.Now,
                            QuoteID = answerUserQuote.QuoteID,
                            UserID = userID,
                            UserBinaryAnswer = answerUserQuote.UserCorrectAnswer
                        });
                    data.IsCorrectAnswered = result.Answers_Binary.CorrectAnswer == answerUserQuote.UserCorrectAnswer;
                }
                else if (result.Mode == (int)ModeEnum.Multiple)
                {
                    await _quoteQuizDbContext.Set<User_Answers>().AddAsync(
                        new User_Answers
                        {
                            CreateDate = DateTime.Now,
                            QuoteID = answerUserQuote.QuoteID,
                            UserID = userID,
                            UserMultipleAnswerID = answerUserQuote.UserMultipleAnswerID
                        });
                    data.IsCorrectAnswered = result.Answers_Multiple.Any(x => x.IsCorrect && x.ID == answerUserQuote.UserMultipleAnswerID);
                    data.CorrectAnswerText = result.Answers_Multiple.FirstOrDefault(x => x.IsCorrect).PossibleAnwerText;
                }

                _quoteQuizDbContext.SaveChanges();

                return data;
            }
            catch (Exception ex)
            {
                //_logger.LogCritical(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateUserMode(int userID, ModeEnum userMode)
        {
            try
            {
                var result = await _quoteQuizDbContext.Set<Users>().FirstAsync(x => x.ID == userID);

                result.LastModifiedDate = DateTime.Now;
                result.CurrentMode = (int)userMode;

                _quoteQuizDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogCritical(ex.Message);
                return false;
            }
        }

    }
}
