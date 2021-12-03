using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;

namespace QuoteQuiz.Web.Controllers
{
    public class BaseController: ControllerBase
    {
        protected readonly ILogger _logger;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        protected int CurrentUserID
        {
            get
            {
                var identity = (ClaimsIdentity)User.Identity;

                var idClaim = identity.Claims.First(f => f.Type == ClaimTypes.NameIdentifier);

                return int.Parse(idClaim.Value);
            }
        }

    }
}
