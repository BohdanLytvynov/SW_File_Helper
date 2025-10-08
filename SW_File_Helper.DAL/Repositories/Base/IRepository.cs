namespace SW_File_Helper.DAL.Repositories.Base
{
    public interface IRepository<TEntity, TId>
    {
        IEnumerable<TEntity> GetAll();

        TEntity? GetById(TId id);

        IEnumerable<TEntity> GetAll(List<TId> ids);

        void Add(TEntity entity);

        void Delete(TEntity entity);

        void Clear();

        void LoadData();
    }
}
