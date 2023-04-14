

namespace ChildCareCore.Interfaces
{
   public  interface IUnitOfWork
    {
        public IUserRepository User { get; }
        public IUserRoleRepository UserRole { get; }
        void Save();
    }
}



