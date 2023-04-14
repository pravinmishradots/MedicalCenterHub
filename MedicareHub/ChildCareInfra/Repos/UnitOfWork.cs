

using ChildCareCore.Interfaces;
using ChildCareInfra.Entities;

namespace ChildCareInfra.Repos
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly MedicareHubContext _medicareHubContext;
        private IUserRepository? _userRepo;
        private IUserRoleRepository? _userRepository;

        public UnitOfWork(MedicareHubContext  medicareHubContext)
        {
            _medicareHubContext = medicareHubContext;
        }
        public IUserRepository User
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new UserRepository(_medicareHubContext);
                }
                return _userRepo;
            }
        }
        public IUserRoleRepository UserRole
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRoleRepository(_medicareHubContext);
                }
                return _userRepository;
            }
        }



        public void Save()
        {
            _medicareHubContext.SaveChanges();
        }
    }
}
