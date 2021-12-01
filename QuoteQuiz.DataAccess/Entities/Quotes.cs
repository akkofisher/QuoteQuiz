using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteQuiz.DataAccess.Entities
{
    public class Quotes : BaseCommonObject<int>
    {
        public Quotes()
        {
            Answers_Multiple = new HashSet<Answers_Multiple>();
            User_Answers = new HashSet<User_Answers>();
        }

        public string QuoteText { get; set; }
        public int Mode { get; set; }

        public Answers_Binary Answers_Binary { get; set; }
        public ICollection<Answers_Multiple> Answers_Multiple { get; set; }
        public ICollection<User_Answers> User_Answers { get; set; }

    }
}
