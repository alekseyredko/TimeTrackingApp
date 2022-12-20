using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace TimeTrackingApp.Services
{
    public class IdentityValidationProvider<TUser> : RevalidatingServerAuthenticationStateProvider where TUser : class
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IOptions<IdentityOptions> _identityOptions;

        public IdentityValidationProvider(
            IServiceScopeFactory serviceScopeFactory,
            ILoggerFactory loggerFactory,
            IOptions<IdentityOptions> identityOptions) : base(loggerFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _identityOptions = identityOptions;
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromSeconds(10);

        protected override async Task<bool> ValidateAuthenticationStateAsync(AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            using IServiceScope serviceScope = _serviceScopeFactory.CreateScope();
            UserManager<IdentityUser> userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityUser identityUser = await userManager.GetUserAsync(authenticationState.User);
            if (identityUser == null) 
            { 
                return false; 
            }
            else if(!userManager.SupportsUserSecurityStamp)
            {
                return true;
            }
            else
            {
                string principalStamp = authenticationState.User.FindFirstValue(_identityOptions.Value.ClaimsIdentity.SecurityStampClaimType);
                string userStamp = await userManager.GetSecurityStampAsync(identityUser);
                return principalStamp == userStamp;
            }                         
        }
    }
}
