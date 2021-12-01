

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuoteQuiz.DataAccess.EntityFramework
{
    public class QuoteQuizDbContext : DbContext
    {
        public QuoteQuizDbContext(DbContextOptions<QuoteQuizDbContext> options)
           : base(options)
        {
        }
               

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ConfigureBinaryChoiceQuestionRelations();
            //modelBuilder.ConfigureAuthorBinaryChoiceQuestionRelations();
            //modelBuilder.ConfigureMultipleChoiceQuestionRelations();
            //modelBuilder.ConfigureCorrectInMultipleChoiceQuestionsRelations();
            //modelBuilder.ConfigureMultipleChoiceAnswerRelations();

            base.OnModelCreating(modelBuilder);
        }

    }
}