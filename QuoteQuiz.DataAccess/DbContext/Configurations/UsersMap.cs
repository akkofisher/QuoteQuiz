using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuoteQuiz.DataAccess.Entities;
using QuoteQuiz.DataAccess.EntityFramework;

namespace Insurance.CoreAPI.Persistence.Configurations
{
    public class UsersMap : BaseCommonObjectMap<Users, int>
    {
        public UsersMap() : base("dbo")
        {
        }

        public override void Configure(EntityTypeBuilder<Users> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

            builder.Property(t => t.Email).HasMaxLength(50).IsRequired();
            builder.Property(t => t.CurrentMode).IsRequired();
            builder.Property(t => t.IsDisabled).IsRequired();


            builder.HasMany(o => o.User_Answers)
                   .WithOne(m => m.User)
                   .HasForeignKey(t => t.UserID);
        }
    }
}
