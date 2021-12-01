using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuoteQuiz.DataAccess.Entities;
using QuoteQuiz.DataAccess.EntityFramework;

namespace QuoteQuiz.DataAccess.EntityFramework.Configurations
{
    public class User_AnswersMap :  BaseObjectMap<User_Answers, int>
    {
        public User_AnswersMap() : base("dbo")
        {
        }

        public override void Configure(EntityTypeBuilder<User_Answers> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.UserBinaryAnswer).IsRequired();
            builder.Property(t => t.CreateDate).HasColumnType("datetime2(0)").HasDefaultValueSql("sysdatetime()");

            builder.HasOne(o => o.User)
                   .WithMany(m => m.User_Answers)
                   .HasForeignKey(t => t.UserID);

            builder.HasOne(o => o.Quote)
                   .WithMany(m => m.User_Answers)
                   .HasForeignKey(t => t.QuoteID);
        }
    }
}
