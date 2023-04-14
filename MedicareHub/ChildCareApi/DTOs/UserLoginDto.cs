

namespace ChildCareApi.DTOs
{
    public class UserLoginDto
    {
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? Token { get; set; }
            public string? DisplayName { get; set; }
            //public Guid UserId { get; set; }
 

        public int? UserRoleId { get; set; }

        public int? TypeId { get; set; }

    }
}
