using QuoteQuiz.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteQuiz.DataAccess.Models
{
    public class QuoteViewModel
    {
        public int ID { get; set; }

        public string CreateDate { get; set; }
        public string LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public string QuoteText { get; set; }
        public ModeEnum Mode { get; set; }
    }

    public class QuoteBinaryViewModel
    {
        public int QuoteID { get; set; }
        public bool CorrectAnswer { get; set; }
    }


    public class MultiplePossibleAnswerViewModel
    {
        public int QuoteID { get; set; }
        public int PossibleAnswerID { get; set; }
        public string PossibleAnwerText { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class CreateQuoteBinaryModel
    {
        public string QuoteText { get; set; }
        public bool CorrectAnswer { get; set; }
    }

    public class UpdateQuoteBinaryModel
    {
        public int QuoteID { get; set; }
        public string QuoteText { get; set; }
        public bool CorrectAnswer { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class CreateQuoteMultipleModel
    {
        public string QuoteText { get; set; }
        public List<MultiplePossibleAnswerModel> MultiplePossibleAnswers { get; set; }
    }

    public class UpdateQuoteMultipleModel
    {
        public int QuoteID { get; set; }
        public string QuoteText { get; set; }
        public bool IsDeleted { get; set; }
        public List<MultiplePossibleAnswerModel> MultiplePossibleAnswers { get; set; }
    }

    public class MultiplePossibleAnswerModel
    {
        public int PossibleAnswerID { get; set; }
        public string PossibleAnwerText { get; set; }
        public bool IsCorrect { get; set; }
    }

}
