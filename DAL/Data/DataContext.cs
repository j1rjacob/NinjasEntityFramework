using Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Linq;

namespace DAL.Data
{
    public class DataContext : DbContext
    {
        public virtual DbSet<ReadingsBackup> _ReadingsBackup
        {
            get;
            set;
        }

        public virtual DbSet<Readings> _Readings
        {
            get;
            set;
        }

        public virtual DbSet<MeterInfos> _MeterInfos
        {
            get;
            set;
        }

        public DataContext() : base("DefaultConnection")
        {
            Database.SetInitializer<DbContext>(new CreateDatabaseIfNotExists<DbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public void InitializeDatabase(DbContext context)
        {
            bool flag = !context.Database.Exists() || !context.Database.CompatibleWithModel(false);
            if (flag)
            {
                DbMigrationsConfiguration configuration = new DbMigrationsConfiguration();
                DbMigrator dbMigrator = new DbMigrator(configuration);
                dbMigrator.Configuration.TargetDatabase = new DbConnectionInfo(context.Database.Connection.ConnectionString, "System.Data.SqlClient");
                IEnumerable<string> pendingMigrations = dbMigrator.GetPendingMigrations();
                bool flag2 = pendingMigrations.Any<string>();
                if (flag2)
                {
                    MigratorScriptingDecorator migratorScriptingDecorator = new MigratorScriptingDecorator(dbMigrator);
                    string text = migratorScriptingDecorator.ScriptUpdate(null, pendingMigrations.Last<string>());
                    bool flag3 = !string.IsNullOrEmpty(text);
                    if (flag3)
                    {
                        context.Database.ExecuteSqlCommand(text, new object[0]);
                    }
                }
            }
        }
    }
}
