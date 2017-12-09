using DAL.Data;

namespace DAL.Repositories
{
    public class ReadingsRepository : RepositoryBase<Readings>
    {
        public ReadingsRepository(DataContext context) : base(context)
        {
        }
    }
}
