using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Course.Authorization
{
    public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
        {
            var dateClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);

            if (dateClaim == null)
                  return Task.CompletedTask;
            
            var date = Convert.ToDateTime(dateClaim.Value);

            var idade = DateTime.Today.Year - date.Year;

            if(date > DateTime.Today.AddYears(-idade))
            {
                idade--;
            }

            return Task.CompletedTask;
        }
    }
}
