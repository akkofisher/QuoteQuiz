
using AutoMapper;
using QuoteQuiz.DataAccess.Entities;
using QuoteQuiz.DataAccess.Models;
using QuoteQuiz.DataAccess.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace QuoteQuiz.DataAccess.Profiles
{
    public class UserMappingActions :
        IMappingAction<List<Users>, List<UserViewModel>>,
        IMappingAction<Users, UserViewModel>,
        IMappingAction<List<User_Answers>, ReviewUserViewModel>

    {
        public void Process(List<Users> source, List<UserViewModel> destination, ResolutionContext context)
        {
            foreach (var item in source)
            {
                destination.Add(new UserViewModel
                {
                    ID = item.ID,

                    Email = item.Email,
                    Name = item.Name,
                    IsDisabled = item.IsDisabled,

                    CreateDate = item.CreateDate.ToShortDateString(),
                    CurrentMode = (ModeEnum)item.CurrentMode,
                    IsDeleted = item.IsDeleted,
                    LastModifiedDate = item.LastModifiedDate?.ToShortDateString(),
                });
            }
        }

        public void Process(Users source, UserViewModel destination, ResolutionContext context)
        {
            destination.ID = source.ID;
            destination.Name = source.Name;
            destination.IsDisabled = source.IsDisabled;
            destination.CreateDate = source.CreateDate.ToShortDateString();
            destination.CurrentMode = (ModeEnum)source.CurrentMode;
            destination.Email = source.Email;
            destination.IsDeleted = source.IsDeleted;
            destination.LastModifiedDate = source.LastModifiedDate?.ToShortDateString();
        }

        public void Process(List<User_Answers> source, ReviewUserViewModel destination, ResolutionContext context)
        {
            foreach (var userAnswer in source)
            {
                if (userAnswer.Quote.Mode == (int)ModeEnum.Binary)
                {
                    destination.UserBinaryAnswers.Add(
                        new UserBinaryAnswerViewModel
                        {
                            QuoteID = userAnswer.QuoteID,
                            QuoteText = userAnswer.Quote.QuoteText,
                            CorrectAnswer = userAnswer.Quote.Answers_Binary.CorrectAnswer,
                            UserBinaryAnswer = userAnswer.UserBinaryAnswer.Value,
                            IsCorrectAnswered = userAnswer.Quote.Answers_Binary.CorrectAnswer == userAnswer.UserBinaryAnswer.Value
                        });
                }
                else if (userAnswer.Quote.Mode == (int)ModeEnum.Multiple)
                {
                    destination.UserMultipleAnswers.Add(
                      new UserMultipleAnswerViewModel
                      {
                          QuoteID = userAnswer.QuoteID,
                          QuoteText = userAnswer.Quote.QuoteText,
                          UserMultipleAnswerID = userAnswer.UserMultipleAnswerID.Value,
                          UserMultiplePossibleAnswers = userAnswer.Quote.Answers_Multiple.Select(a => new UserMultiplePossibleAnswerViewModel
                          {
                              IsCorrect = a.IsCorrect,
                              PossibleAnwerText = a.PossibleAnwerText,
                              PossibleAnswerID = a.ID,
                          }).ToList(),
                          IsCorrectAnswered = userAnswer.Quote.Answers_Multiple.Any(x => x.IsCorrect && x.ID == userAnswer.UserMultipleAnswerID)
                      });

                }
            }

            var totalCorrectAnswers = destination.UserBinaryAnswers.Count(x => x.IsCorrectAnswered) + destination.UserMultipleAnswers.Count(x => x.IsCorrectAnswered);
            destination.TotalUserAnsweredQuestions = source.Count;
         
            if (totalCorrectAnswers != 0 && destination.TotalUserAnsweredQuestions != 0)
                destination.UserCorrectAnswerPercentage = totalCorrectAnswers / (decimal)destination.TotalUserAnsweredQuestions * 100;
        }

        public class UserMappingProfile : Profile
        {
            public UserMappingProfile()
            {
                CreateMap<List<Users>, List<UserViewModel>>()
                    .AfterMap<UserMappingActions>();
                CreateMap<Users, UserViewModel>()
                    .AfterMap<UserMappingActions>();
                CreateMap<List<User_Answers>, ReviewUserViewModel>()
                    .AfterMap<UserMappingActions>();
            }
        }
    }
}