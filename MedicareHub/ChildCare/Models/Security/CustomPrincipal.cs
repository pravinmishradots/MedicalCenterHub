using ChildCare.Code.Attributes;

using ChildCareCore.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Principal;

namespace ChildCare.Models.Security
{
    public class CustomPrincipal

    {

        public CustomPrincipal()
        {

        }
        private readonly ClaimsPrincipal claimsPrincipal;

        public CustomPrincipal(ClaimsPrincipal principal)
        {
            claimsPrincipal = principal;
            this.IsAuthenticated = claimsPrincipal == null ? false : claimsPrincipal.Identity!.IsAuthenticated;

            if (this.IsAuthenticated)
            {
                this.Id = Guid.Parse(claimsPrincipal!.Claims.FirstOrDefault(u => u.Type == ClaimTypes.PrimarySid)?.Value!);

                this.Token = claimsPrincipal!.Claims.FirstOrDefault(u => u.Type == nameof(this.Token))!.Value;
                this.DisplayName = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value!;
                this.Email = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.Email))?.Value!;
                this.UserRoleId = int.Parse(claimsPrincipal.Claims.FirstOrDefault(u => u.Type == nameof(this.UserRoleId))?.Value!);

                this.Role = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role)?.Value!;

            }
        }


        public bool IsAuthenticated { get; private set; }
        public Guid Id { get; private set; }
        public string Token { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }
        public int UserRoleId { get; set; }
        public string Role { get; private set; }
        //public int[] Roles { get; set; }
        public byte[] Roles { get; set; }
        public int[] Permissions { get; set; }
        private string _imageName;

        public string ImageName
        {
            get { return _imageName; }
            set
            {
                UpdateClaim(nameof(ImageName), value.ToString());
                _imageName = value;
            }
        }

        public IIdentity Identity { get; private set; }


        public CustomPrincipal(string userName, params byte[] userRoles)
        {
            this.Identity = new GenericIdentity(userName);
            this.Email = userName;
            this.Roles = userRoles;
            this.UserRoleId = UserRoleId;
            this.DisplayName = DisplayName;
            this.Token = Token;


        }

        //public bool IsInRole(UserRolesEnum userRole)
        //{
        //    return Roles.Contains((int)userRole);
        //}
        public bool IsInRole(UserRolesEnum userRole)
        {
            return Roles.Contains((byte)userRole);
        }

        public bool IsInRole(params UserRolesEnum[] userRoles)
        {
            return userRoles.Any(r => Roles.Contains((byte)r));
        }


        public bool IsInRole(string role)
        {
            //Check with enum
            UserRolesEnum userRole;
            if (Enum.TryParse(role, out userRole)) { return IsInRole(userRole); }
            return false;

        }

        public bool HasPermission(int permissionId)
        {
            var hasPermission = false;

            hasPermission = Permissions.Contains(permissionId);

            return hasPermission;
        }

        private void UpdateClaim(string key, string value)
        {
            var claims = claimsPrincipal.Claims.ToList();
            if (claims.Any())
            {
                var pmClaim = claimsPrincipal.Claims.FirstOrDefault(u => u.Type == key);
                if (pmClaim != null)
                {
                    claims.Remove(pmClaim);
                    claims.Add(new Claim(key, value));
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };
            ContextProvider.HttpContext.SignInAsync(
                  CookieAuthenticationDefaults.AuthenticationScheme,
                   new ClaimsPrincipal(claimsIdentity),
                   authProperties
                 ).Wait();
        }
    }

}


