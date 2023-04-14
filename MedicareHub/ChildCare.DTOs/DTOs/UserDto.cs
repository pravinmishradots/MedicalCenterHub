﻿namespace ChildCare.DTOs
{
    public class UserDto
    {

        public Guid UserId { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Password { get; set; }
        public int? UserRoleId { get; set; }

    }

}
