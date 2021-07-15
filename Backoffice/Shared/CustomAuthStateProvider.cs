using System.Security.Claims;
using System.Threading.Tasks;
using Backoffice.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace Backoffice.Shared
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // var userId = await _httpContextAccessor.HttpContext.GetBoUserId();
            var userId = "krasdmi";
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, userId),
            }, "Fake authentication type");

            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }
    }
}
