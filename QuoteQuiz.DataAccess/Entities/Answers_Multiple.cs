using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteQuiz.DataAccess.Entities
{
    public class Answers_Multiple : BaseObject<int>
    {
        public int QuoteID { get; set; }
        public string PossibleAnwerText { get; set; }
        public bool IsCorrect { get; set; }

        public Quotes Quote { get; set; }
    }
}
