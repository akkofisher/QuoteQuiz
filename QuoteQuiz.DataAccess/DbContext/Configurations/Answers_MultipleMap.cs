using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuoteQuiz.DataAccess.Entities;
using QuoteQuiz.DataAccess.EntityFramework;

namespace Insurance.CoreAPI.Persistence.Configurations
{
    public class Answers_MultipleMap : BaseObjectMap<Answers_Multiple, int>
    {
        public Answers_MultipleMap() : base("dbo")
        {
        }

        public override void Configure(EntityTypeBuilder<Answers_Multiple> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.IsCorrect).IsRequired();
            builder.Property(p => p.PossibleAnwerText).HasMaxLength(50).IsRequired();

            builder.HasOne(o => o.Quote)
                   .WithMany()
                   .HasForeignKey(t => t.QuoteID);

        }
    }
}
