using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteQuiz.DataAccess.Entities
{
    public class Answers_Binary : BaseObject<int>
    {
        public int QuoteID { get; set; }
        public bool CorrectAnswer { get; set; }

        public Quotes Quote { get; set; }
    }
}
