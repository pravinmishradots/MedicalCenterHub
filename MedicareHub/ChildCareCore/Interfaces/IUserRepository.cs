using ChildCareCore.Entities;
using ChildCareCore.Helper;
using ChildCareCore.Specifications;

namespace ChildCareCore.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
   
        public List<User> GetAllUsers();
        public User GetUserById(Guid id);
        public User CreateUser(User user);
        public User UpdateUser(User user);
        public User GetUserByEmailAndPassword(string email, string password);
        public void DeleteUser(User user);
        public PageList<User> GetUserList(UserSpecification model);
        public PageList<User> GetUserByUser(Guid userId, UserSpecification model);
        public User GetUserByEmail(string email);
        //public User GetUserByEmail(string email, int typeid, int userRoleId);
     
        bool GetLoginUser(string userName, string password);
    }
}
