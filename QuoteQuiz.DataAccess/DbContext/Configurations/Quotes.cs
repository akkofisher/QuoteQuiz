using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuoteQuiz.DataAccess.Entities;

namespace QuoteQuiz.DataAccess.EntityFramework.Configurations
{
    public class QuotesMap : BaseCommonObjectMap<Quotes, int>
    {
        public QuotesMap() : base("dbo")
        {
        }

        public override void Configure(EntityTypeBuilder<Quotes> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.QuoteText).HasMaxLength(500).IsRequired();

            builder.Property(t => t.Mode).IsRequired();

            builder.HasMany(o => o.Answers_Multiple)
                   .WithOne(m => m.Quote)
                   .HasForeignKey(t => t.QuoteID);

            builder.HasMany(o => o.User_Answers)
                   .WithOne(m => m.Quote)
                   .HasForeignKey(t => t.QuoteID);

            builder.HasOne(o => o.Answers_Binary)
                   .WithOne(m => m.Quote)
                   .HasForeignKey<Answers_Binary>(a => a.QuoteID);
        }
    }
}
