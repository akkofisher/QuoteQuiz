using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuoteQuiz.DataAccess.Entities;

namespace QuoteQuiz.DataAccess.EntityFramework.Configurations
{
    public class Answers_BinaryMap : BaseObjectMap<Answers_Binary, int>
    {
        public Answers_BinaryMap() : base("dbo")
        {
        }

        public override void Configure(EntityTypeBuilder<Answers_Binary> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.CorrectAnswer).IsRequired();

            builder.HasOne(o => o.Quote)
                   .WithOne(m => m.Answers_Binary)
                   .HasForeignKey<Quotes>(a => a.ID);

        }
    }
}
