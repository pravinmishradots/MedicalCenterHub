

namespace ChildCareCore.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? SaltKey { get; set; }

    public int? UserRoleId { get; set; }

    public int? TypeId { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsVerified { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string? DisplayName { get; set; }
    public string? Token { get; set; }

    public virtual UserRole? UserRole { get; set; }
    public virtual ICollection<UserRole> userrole { get; set; }
}
