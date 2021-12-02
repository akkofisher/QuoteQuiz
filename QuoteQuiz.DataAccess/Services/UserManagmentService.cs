using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using QuoteQuiz.DataAccess.Entities;
using QuoteQuiz.DataAccess.EntityFramework;
using QuoteQuiz.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteQuiz.DataAccess.Services
{
    public interface IUserManagmentService
    {
        Task<List<UserViewModel>> GetUsers();
        Task<bool> CreateUser(CreateUserModel user);
        Task<bool> UpdateUser(UpdateUserModel user);
        Task<ReviewUserViewModel> ReviewUser(int userID);
    }

    public class UserManagmentService : BaseService, IUserManagmentService
    {
        public UserManagmentService(
            QuoteQuizDbContext quoteQuizDbContext,
            IMemoryCache memoryCache,
            IMapper mapper)
        : base(quoteQuizDbContext, memoryCache, mapper)
        {
        }

        public async Task<List<UserViewModel>> GetUsers()
        {
            var result = await _quoteQuizDbContext.Set<Users>().ToListAsync();

            return _mapper.Map<List<UserViewModel>>(result);
        }

        public async Task<bool> CreateUser(CreateUserModel user)
        {
            try
            {
                await _quoteQuizDbContext.Set<Users>().AddAsync(new Users
                {
                    CreateDate = DateTime.Now,
                    CurrentMode = (int)user.CurrentMode,
                    Email = user.Email,
                    IsDeleted = user.IsDeleted,
                    IsDisabled = user.IsDisabled,
                    Name = user.Name,
                });

                _quoteQuizDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogCritical(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateUser(UpdateUserModel user)
        {
            try
            {
                var result = await _quoteQuizDbContext.Set<Users>().FirstAsync(x => x.ID == user.UserID);

                result.LastModifiedDate = DateTime.Now;
                result.CurrentMode = (int)user.CurrentMode;
                result.IsDeleted = user.IsDeleted;
                result.IsDisabled = user.IsDisabled;
                result.Name = user.Name;
                result.Email = user.Email;

                _quoteQuizDbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //_logger.LogCritical(ex.Message);
                return false;
            }
        }

        public async Task<ReviewUserViewModel> ReviewUser(int userID)
        {
            var result = await _quoteQuizDbContext.Set<User_Answers>()
                .Include(x => x.Quote)
                .Include(x => x.Quote.Answers_Binary)
                .Include(x => x.Quote.Answers_Multiple)
                .Where(x => x.UserID == userID).ToListAsync();

            var resultMapped = _mapper.Map<ReviewUserViewModel>(result);
            resultMapped.UserID = userID;

            var user = await _quoteQuizDbContext.Set<Users>().FirstOrDefaultAsync();
            resultMapped.UserName = user.Name ?? null;

            return resultMapped;
        }
    }
}
