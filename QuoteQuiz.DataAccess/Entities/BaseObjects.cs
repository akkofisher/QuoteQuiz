using System;
using System.Collections.Generic;
using System.Text;

namespace QuoteQuiz.DataAccess.Entities
{
    public class BaseObject<T>
    {
        public T ID { get; set; }
    }

    public class BaseCommonObject<T> : BaseObject<T>
    {
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
