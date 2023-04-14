using ChildCareCore.Interfaces;
using ChildCareInfra.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChildCareInfra.Repos
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly MedicareHubContext _medicarehubcontext;

        public RepositoryBase(MedicareHubContext medicarehubcontext)
        {
            _medicarehubcontext = medicarehubcontext;
        }
        public void Create(T entity)
        {
            _medicarehubcontext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _medicarehubcontext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return _medicarehubcontext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition)
        {
            return _medicarehubcontext.Set<T>().Where(condition).AsNoTracking();
        }

        public void Update(T entity)
        {
            _medicarehubcontext.Set<T>().Update(entity);
        }
    }
}
