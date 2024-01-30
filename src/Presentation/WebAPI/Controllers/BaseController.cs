using System.Security.Claims;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/action")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        // Метод для получения утверждений пользователя из токена доступа
        // protected IEnumerable<Claim> UserClaims => !User.Identity!.IsAuthenticated ? new List<Claim>() : User.Claims;
        internal Guid UserId => !User.Identity!.IsAuthenticated
        ? Guid.Empty
        : Guid.Parse(GetUserClaimValue(ClaimTypes.NameIdentifier));

        internal int Age => !User.Identity!.IsAuthenticated
        ? 0
        : GetUserAge();

        internal Gender? Gender => !User.Identity!.IsAuthenticated
        ? null
        : (Gender)Enum.Parse(typeof(Gender), GetUserClaimValue(ClaimTypes.Gender));

        private int GetUserAge()
        {
            var birthDateClaim = GetUserClaimValue(ClaimTypes.DateOfBirth);
            if (DateTime.TryParse(birthDateClaim, out var birthDate))
            {
                var today = DateTime.Today;
                var age = today.Year - birthDate.Year;
                if (birthDate.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
            return 0;
        }

        private string GetUserClaimValue(string claimType)
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == claimType);
            return claim!.Value;
        }
    }
}