using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteQuiz.DataAccess.Entities
{
    public class Users : BaseCommonObject<int>
    {
        public Users()
        {
            User_Answers = new HashSet<User_Answers>();
        }

        public string Email { get; set; }
        public string Name { get; set; }
        public int CurrentMode { get; set; }
        public bool IsDisabled { get; set; }

        public ICollection<User_Answers> User_Answers { get; set; }
    }
}
