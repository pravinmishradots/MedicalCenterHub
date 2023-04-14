using ChildCareCore.Interfaces;
using ChildCareCore.Entities;
using ChildCareInfra.Entities;
using ChildCareCore.Helper;
using ChildCareCore.Specifications;
using ChildCareInfra.Helper;
using ChildCareCore.Enums;

namespace ChildCareInfra.Repos
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MedicareHubContext medicarehubcontext) : base(medicarehubcontext)
        {
            
        }
        public List<User> GetAllUsers()
        {

            return FindAll().ToList();


        }
        public bool GetLoginUser(string email, string password)
        {
            return FindByCondition(x => x.Email == email && x.Password == password).Any();
        }

        public User CreateUser(User user)
        {
            user.UserId= Guid.NewGuid();
            user.IsVerified = true;
            user.IsActive = true;
            user.CreatedOn = DateTime.Now;
            user.UpdatedOn = DateTime.Now;
            Create(user);
            return user;
        }
        public User UpdateUser(User user)
        {
           user.UpdatedOn = DateTime.Now;
            Update(user);
            return user;
        }



        public User GetUserByEmailAndPassword(string email, string password)
        {
            return FindByCondition(x => x.Email!.ToLower() == email.ToLower() && x.Password == password).SingleOrDefault()!;
        }



        public User GetUserById(Guid id)
        {
            return FindByCondition(x => x.UserId == id).SingleOrDefault()!;
        }






        public PageList<User> GetUserList(UserSpecification model)
        {


            var user = FindAll();
            if (model.Status > 0)
            {
                user = FindByCondition(x => x.TypeId == model.Status);
            }
            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                user = user.Where(x => x.DisplayName!.ToLower().Contains(model.Title.ToLower()));
            }
            user = SortHelper<ChildCareCore.Entities.User>.ApplySort(user, model.OrderBy);
            return PageList<User>.ToPageList(user, model.PageNumber, model.PageSize);

        }


        public PageList<User> GetUserByUser(Guid userId, UserSpecification model)
        {

            var user = FindByCondition(x => x.UserId == userId);
            if (model.Status > 0)
            {
                user = FindByCondition(x => x.TypeId == model.Status);
            }
            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                user = user.Where(x => x.DisplayName!.ToLower().Contains(model.Title.ToLower()));
            }
            user = SortHelper<ChildCareCore.Entities.User>.ApplySort(user, model.OrderBy);
            return PageList<User>.ToPageList(user, model.PageNumber, model.PageSize);


        }

        public User GetUserByEmail(string email)
        {

            return FindByCondition(x => x.Email == email).SingleOrDefault()!;
        }

        //public User GetUserByEmail(string email,int typeid ,int userRoleId)
        //{

        //    return FindByCondition(x => x.Email == email  &&x.TypeId== typeid && x.UserRoleId== userRoleId).SingleOrDefault()!;
        //}


        public void DeleteUser(User user)
        {
            Delete(user);

        }






    }
}
