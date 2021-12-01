using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuoteQuiz.DataAccess.Entities;

namespace QuoteQuiz.DataAccess.EntityFramework.Configurations
{
    public class BaseObjectMap<TEntity, T> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseObject<T>
    {
        protected readonly string _schema;
        protected readonly string _tableName;

        public BaseObjectMap(string schema, string tableName = "")
        {
            _schema = schema;

            if (string.IsNullOrEmpty(tableName))
            {
                _tableName = $"{typeof(TEntity).Name}";
            }
            else
            {
                _tableName = tableName;
            }
        }


        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(t => t.ID);
            builder.Property(t => t.ID).ValueGeneratedOnAdd();

            builder.ToTable(_tableName, _schema);
        }
    }

    public class BaseCommonObjectMap<TEntity, T> : BaseObjectMap<TEntity, T>
        where TEntity : BaseCommonObject<T>
    {
        public BaseCommonObjectMap(string schema, string tableName = "")
            : base(schema, tableName)
        {
        }


        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.IsDeleted).HasDefaultValue(false);
            builder.Property(t => t.CreateDate).HasColumnType("datetime2(0)").HasDefaultValueSql("sysdatetime()");
            builder.Property(t => t.LastModifiedDate).HasColumnType("datetime2(0)");
        }
    }

}
