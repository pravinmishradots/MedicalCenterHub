

using ChildCareCore.Entities;
using ChildCareCore.Interfaces;
using ChildCareInfra.Entities;

namespace ChildCareInfra.Repos
{
    public class UserRoleRepository:RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(MedicareHubContext medicareHubContext):base(medicareHubContext) { 
        
        }
        public List<UserRole> GetAllUsers()
        {

            return FindAll().ToList();


        }

        public UserRole CreateUser(UserRole user)
        {

            user.CreatedOn = DateTime.Now;
    
            Create(user);
            return user;
        }
        public UserRole UpdateUser(UserRole user)
        {


            user.CreatedOn = DateTime.Now;
            Update(user);
            return user;
        }

        public UserRole GetUserById(int id)
        {
            return FindByCondition(x => x.RoleId == id).SingleOrDefault()!;
        }

    }
}





