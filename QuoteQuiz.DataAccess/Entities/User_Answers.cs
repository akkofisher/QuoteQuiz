using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteQuiz.DataAccess.Entities
{
    public class User_Answers : BaseObject<int>
    {
        public int UserID { get; set; }
        public int QuoteID { get; set; }
        public int? UserMultipleAnswerID { get; set; }
        public bool? UserBinaryAnswer { get; set; }
        public DateTime CreateDate { get; set; }

        public Users User { get; set; }
        public Quotes Quote { get; set; }
    }
}
