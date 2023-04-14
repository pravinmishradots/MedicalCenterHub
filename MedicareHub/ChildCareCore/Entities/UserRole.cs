
namespace ChildCareCore.Entities;

public partial class UserRole
{
    public int RoleId { get; set; }

    public string? Name { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
