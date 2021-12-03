using QuoteQuiz.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteQuiz.DataAccess.Models
{
    public class UserQuoteViewModel
    {
        public UserQuoteViewModel()
        {
            QuoteMultiplePossibleAnswers = new List<QuoteMultiplePossibleAnswerViewModel>();
        }

        public int QuoteID { get; set; }
        public string CreateDate { get; set; }
        public string QuoteText { get; set; }
        public ModeEnum Mode { get; set; }

        public List<QuoteMultiplePossibleAnswerViewModel> QuoteMultiplePossibleAnswers { get; set; }
    }

    public class QuoteMultiplePossibleAnswerViewModel
    {
        public int PossibleAnswerID { get; set; }
        public string PossibleAnwerText { get; set; }
    }

    public class AnswerUserQuoteModel
    {
        public int QuoteID { get; set; }
        public bool? UserCorrectAnswer { get; set; }
        public int? UserMultipleAnswerID { get; set; }
    }

    public class AnswerResultUserQuoteViewModel
    {
        public int UserID { get; set; }
        public int QuoteID { get; set; }
        public bool IsCorrectAnswered { get; set; }
        public string CorrectAnswerText { get; set; }
    }
}
