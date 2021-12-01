using QuoteQuiz.DataAccess.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteQuiz.DataAccess.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }

        public string CreateDate { get; set; }
        public string LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public ModeEnum CurrentMode { get; set; }
        public bool IsDisabled { get; set; }
    }

    public class CreateUserModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public ModeEnum CurrentMode { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class UpdateUserModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public ModeEnum CurrentMode { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ReviewUserViewModel
    {
        public ReviewUserViewModel()
        {
            UserBinaryAnswers = new List<UserBinaryAnswerViewModel>();
            UserMultipleAnswers = new List<UserMultipleAnswerViewModel>();
        }

        public int UserID { get; set; }
        public string UserName { get; set; }
        public int TotalUserAnsweredQuestions { get; set; }
        public decimal UserCorrectAnswerPercentage { get; set; }

        public List<UserBinaryAnswerViewModel> UserBinaryAnswers { get; set; }
        public List<UserMultipleAnswerViewModel> UserMultipleAnswers { get; set; }
    }

    public class UserBinaryAnswerViewModel
    {
        public int QuoteID { get; set; }
        public string QuoteText { get; set; }
        public bool CorrectAnswer { get; set; }
        public bool UserBinaryAnswer { get; set; }
        public bool IsCorrectAnswered { get; set; }

    }

    public class UserMultipleAnswerViewModel
    {
        public UserMultipleAnswerViewModel()
        {
            UserMultiplePossibleAnswers = new List<UserMultiplePossibleAnswerViewModel>();
        }

        public int QuoteID { get; set; }
        public string QuoteText { get; set; }
        public int UserMultipleAnswerID { get; set; }
        public List<UserMultiplePossibleAnswerViewModel> UserMultiplePossibleAnswers { get; set; }
        public bool IsCorrectAnswered { get; set; }
    }

    public class UserMultiplePossibleAnswerViewModel
    {
        public int PossibleAnswerID { get; set; }
        public string PossibleAnwerText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
