using DAL.Data;

namespace DAL.Repositories
{
    public class ReadingsBackupRepository : RepositoryBase<ReadingsBackup>
    {
        public ReadingsBackupRepository(DataContext context) : base(context)
        {
        }
    }
}
