using AutoMapper;
using QuoteQuiz.DataAccess.Entities;
using QuoteQuiz.DataAccess.Models;
using QuoteQuiz.DataAccess.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace QuoteQuiz.DataAccess.Profiles
{
    public class QuoteMappingActions :
        IMappingAction<List<Quotes>, List<QuoteViewModel>>,
        IMappingAction<Answers_Binary, QuoteBinaryViewModel>,
        IMappingAction<List<Answers_Multiple>, List<MultiplePossibleAnswerViewModel>>

    {
        public class QuoteMappingProfile : Profile
        {
            public QuoteMappingProfile()
            {
                CreateMap<List<Quotes>, List<QuoteViewModel>>()
                    .AfterMap<QuoteMappingActions>();
                CreateMap<Answers_Binary, QuoteBinaryViewModel>()
                    .AfterMap<QuoteMappingActions>();
                CreateMap<List<Answers_Multiple>, List<MultiplePossibleAnswerViewModel>>()
                    .AfterMap<QuoteMappingActions>();
            }
        }

        public void Process(List<Quotes> source, List<QuoteViewModel> destination, ResolutionContext context)
        {
            foreach (var item in source)
            {
                destination.Add(new QuoteViewModel
                {
                    ID = item.ID,

                    Mode = (ModeEnum)item.Mode,
                    QuoteText = item.QuoteText,

                    CreateDate = item.CreateDate.ToShortDateString(),
                    LastModifiedDate = item.LastModifiedDate?.ToShortDateString(),
                });
            }
        }

        public void Process(Answers_Binary source, QuoteBinaryViewModel destination, ResolutionContext context)
        {
            destination.QuoteID = source.QuoteID;
            destination.CorrectAnswer = source.CorrectAnswer;
        }

        public void Process(List<Answers_Multiple> source, List<MultiplePossibleAnswerViewModel> destination, ResolutionContext context)
        {
            foreach (var item in source)
            {
                destination.Add(new MultiplePossibleAnswerViewModel
                {
                    IsCorrect = item.IsCorrect,
                    QuoteID = item.QuoteID,
                    PossibleAnswerID = item.ID,
                    PossibleAnwerText = item.PossibleAnwerText,
                });
            }
        }
     
    }
}