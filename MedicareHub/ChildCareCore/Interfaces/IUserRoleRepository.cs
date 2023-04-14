

using ChildCareCore.Entities;

namespace ChildCareCore.Interfaces
{
    public interface IUserRoleRepository:IRepositoryBase<UserRole>
    {
        public List<UserRole> GetAllUsers();
        public Entities.UserRole GetUserById(int id);
        public Entities.UserRole CreateUser(Entities.UserRole user);
        public UserRole UpdateUser(UserRole user);

    }
}



