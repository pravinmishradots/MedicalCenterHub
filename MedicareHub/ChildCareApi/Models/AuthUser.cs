using System.Security.Claims;

namespace ChildCareApi.Models
{
    public class AuthUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AuthUser(ClaimsPrincipal user)
        {
            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                throw new InvalidOperationException("User is not authorized");
            }
            var claim = user.Claims;
            Id = new Guid(claim.First(x => x.Type == ClaimTypes.Sid).Value);



            Name = claim.First(x => x.Type == ClaimTypes.Name).Value;
        }
    }
}

