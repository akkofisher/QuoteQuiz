using AutoMapper;
using QuoteQuiz.DataAccess.Entities;
using QuoteQuiz.DataAccess.Models;
using QuoteQuiz.DataAccess.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace QuoteQuiz.DataAccess.Profiles
{
    public class UserQuoteMappingProfile : Profile
    {
        public UserQuoteMappingProfile()
        {
            CreateMap<Quotes, UserQuoteViewModel>()
                .AfterMap<UserQuoteMappingActions>();
        }
    }

    public class UserQuoteMappingActions :
        IMappingAction<Quotes, UserQuoteViewModel>

    {
        public void Process(Quotes source, UserQuoteViewModel destination, ResolutionContext context)
        {
            destination.QuoteID = source.ID;
            destination.Mode = (ModeEnum)source.Mode;
            destination.CreateDate = source.CreateDate.ToShortDateString();
            destination.QuoteText = source.QuoteText;

            if (source.Mode == (int)ModeEnum.Binary)
            {
                destination.CorrectAnswer = source.Answers_Binary.CorrectAnswer;
            }
            else if (source.Mode == (int)ModeEnum.Multiple)
            {
                foreach (var answer in source.Answers_Multiple)
                {
                    destination.UserMultiplePossibleAnswers.Add(new UserMultiplePossibleAnswerViewModel
                    {
                        IsCorrect = answer.IsCorrect,
                        PossibleAnswerID = answer.ID,
                        PossibleAnwerText = answer.PossibleAnwerText
                    });
                }
            }
        }

    }
}