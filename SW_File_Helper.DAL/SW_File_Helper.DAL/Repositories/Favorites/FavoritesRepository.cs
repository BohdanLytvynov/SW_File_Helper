using SW_File_Helper.DAL.DataProviders.Favorites;
using SW_File_Helper.DAL.Models;

namespace SW_File_Helper.DAL.Repositories.Favorites
{
    public class FavoritesRepository : IFavoritesRepository
    {
        IFavoritesDataProvider m_dataProvider;

        public FavoritesRepository(IFavoritesDataProvider dataProvider)
        {
            if(dataProvider == null) throw new ArgumentNullException(nameof(dataProvider));

            m_dataProvider = dataProvider;
        }

        public void Add(ModelBase entity)
        {
            if(entity == null) throw new ArgumentNullException(nameof(entity));

            entity.Id = Guid.NewGuid();
            m_dataProvider.GetData().Add(entity);

            m_dataProvider.SaveData();
        }

        public void Delete(ModelBase entity)
        {
            if (m_dataProvider.GetData().Contains(entity))
                m_dataProvider.GetData().Remove(entity);
        }

        public IEnumerable<ModelBase> GetAll()
        {
            return m_dataProvider.GetData();
        }

        public ModelBase? GetById(Guid id)
        {
            return m_dataProvider.GetData().Where(x => x.Id == id).FirstOrDefault();
        }

        public void Clear()
        { 
            m_dataProvider.GetData().Clear();
        }

        public void LoadData()
        {
            m_dataProvider.LoadData();
        }

        public IEnumerable<ModelBase> GetAll(List<Guid> ids)
        {
            List<ModelBase> result = new List<ModelBase>();

            foreach (Guid id in ids)
            {
                result.Add(m_dataProvider.GetData().Where(x => x.Id == id).FirstOrDefault());
            }

            return result;
        }
    }
}
