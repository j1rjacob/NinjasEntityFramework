using NinjaDomain.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Ninja
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
            //InsertNinjas();
            InsertMultipleNinjas();
            SimpleNinjaQueries();
            Console.ReadKey();
        }

        private static void SimpleNinjaQueries()
        {
            throw new NotImplementedException();
        }

        private static void InsertNinjas()
        {
            var ninja = new NinjaDomain.Classes.Ninja()
            {
                Name = "Junar",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1986,06,20),
                ClanId = 1
            };
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine; 
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }
        }
        private static void InsertMultipleNinjas()
        {
            var ninja1 = new NinjaDomain.Classes.Ninja()
            {
                Name = "Raphael",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1986,06,20),
                ClanId = 1
            };
            var ninja2 = new NinjaDomain.Classes.Ninja()
            {
                Name = "Donatelo",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1986,06,20),
                ClanId = 1
            };
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine; 
                context.Ninjas.AddRange(new List<NinjaDomain.Classes.Ninja>() { ninja1, ninja2 });
                context.SaveChanges();
            }
        }
    }
}
