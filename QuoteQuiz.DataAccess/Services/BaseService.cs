using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using QuoteQuiz.DataAccess.EntityFramework;

namespace QuoteQuiz.DataAccess.Services
{
    public abstract class BaseService
    {
        protected readonly IMemoryCache _memoryCache;
        protected readonly IMapper _mapper;
        protected readonly QuoteQuizDbContext _quoteQuizDbContext;

        public BaseService(
            QuoteQuizDbContext quoteQuizDbContext,
            IMemoryCache memoryCache,
            IMapper mapper)
        {
            _quoteQuizDbContext = quoteQuizDbContext;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }

    }
}