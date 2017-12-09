using DAL.Data;
using Model;

namespace DAL.Repositories
{
    public class MeterInfosRepository : RepositoryBase<MeterInfos>
    {
        public MeterInfosRepository(DataContext context) : base(context)
        {
        }
    }
}
