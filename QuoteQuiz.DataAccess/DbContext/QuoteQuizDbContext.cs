

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using QuoteQuiz.DataAccess.Entities;
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

        public DbSet<Users> Users { get; set; }
        public DbSet<Quotes> Quotes { get; set; }
        public DbSet<Answers_Binary> Answers_Binary { get; set; }
        public DbSet<Answers_Multiple> Answers_Multiple { get; set; }
        public DbSet<User_Answers> User_Answers { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.ConfigureBinaryChoiceQuestionRelations();
        //    //modelBuilder.ConfigureAuthorBinaryChoiceQuestionRelations();
        //    //modelBuilder.ConfigureMultipleChoiceQuestionRelations();
        //    //modelBuilder.ConfigureCorrectInMultipleChoiceQuestionsRelations();
        //    //modelBuilder.ConfigureMultipleChoiceAnswerRelations();

        //    base.OnModelCreating(modelBuilder);
        //}

    }
}