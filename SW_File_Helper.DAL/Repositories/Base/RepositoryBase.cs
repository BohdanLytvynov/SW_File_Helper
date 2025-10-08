using SW_File_Helper.DAL.Models;

namespace SW_File_Helper.DAL.Repositories.Base
{
    public abstract class RepositoryBase<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : ModelBase
    {
        public abstract void Add(TEntity entity);

        public abstract void Delete(TEntity entity);

        public abstract IEnumerable<TEntity> GetAll();

        public abstract IEnumerable<TEntity> GetAll(List<TId> ids);

        public abstract TEntity? GetById(TId id);

        public abstract void Clear();

        public abstract void LoadData();
    }
}
