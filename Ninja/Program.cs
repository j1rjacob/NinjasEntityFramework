using NinjaDomain.Classes;
using NinjaDomain.Classes.Enums;
using NinjaDomain.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Ninja
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
            //InsertNinjas();
            //InsertMultipleNinjas();
            //SimpleNinjaQueries();
            //QueryandUpdateNinja();
            //QueryandUpdateNinjaDisconnected();
            //RetrieveDataWithFind();
            //RetrieveDataWithStoredProc();
            //DeleteNinja();
            //DeleteNinjaDisconned();
            //DeleteNinjaWithKeyValue();
            //DeleteNinjaWithStoredProcedure();
            //InsertNinjaWithEquipment();
            //SimpleNinjaGraphQuery();
            //ProjectionQuery();
            Console.ReadKey();
        }

        private static void ProjectionQuery()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninjas = context.Ninjas
                    .Select(n => new {n.Name, n.DateOfBirth, n.EquipmentOwned})
                    .ToList();
            }
        }

        private static void SimpleNinjaGraphQuery()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                //Eager Loading
                //var ninjas = context.Ninjas.Include(n => n.EquipmentOwned)
                //    .FirstOrDefault(n => n.Name.StartsWith("Junar"));

                //Explicit Loading
                var ninja = context.Ninjas
                    .FirstOrDefault(n => n.Name.StartsWith("Kacy"));
                Console.WriteLine($"Ninja Retrieved: {ninja.Name}");
                //context.Entry(ninja).Collection(n => n.EquipmentOwned).Load();

                //Lazy Loading
                //Mark the property to virual = virtual List<NinjaEquipment> on Ninja Class
                Console.WriteLine($"Ninja Equipment Count {ninja.EquipmentOwned.Count()}");
            }
        }

        private static void InsertNinjaWithEquipment()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = new NinjaDomain.Classes.Ninja
                {
                    Name   = "Kacy Cantazaro",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1986,06,20),
                    ClanId = 1
                };
                var muscles = new NinjaEquipment
                {
                    Name="Muscles",
                    Type = EquipmentType.Tool
                };
                var spunk = new NinjaEquipment
                {
                    Name="Spunk",
                    Type = EquipmentType.Weapon
                };
                context.Ninjas.Add(ninja);
                ninja.EquipmentOwned.Add(muscles);
                ninja.EquipmentOwned.Add(spunk);
                context.SaveChanges();
            }
        }

        private static void DeleteNinjaWithStoredProcedure()
        {
            using (var context = new NinjaContext())
            {
                var keyval = 6;
                context.Database.Log = Console.WriteLine;
                context.Database.ExecuteSqlCommand(
                    "exec DeleteNinjaViaId {0}", keyval);
                context.SaveChanges();
            }
        }

        private static void DeleteNinjaWithKeyValue()
        {
            using (var context = new NinjaContext())
            {
                var keyval = 6;
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.Find(keyval);
                context.Ninjas.Remove(ninja);
                context.SaveChanges();
            }
        }

        private static void DeleteNinjaDisconned()
        {
            //For online application only
            NinjaDomain.Classes.Ninja ninja;

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
            }
            
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Attach(ninja);
                context.Entry(ninja).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        private static void DeleteNinja()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                context.Ninjas.Remove(ninja);
                context.SaveChanges();
            }
        }

        private static void RetrieveDataWithStoredProc()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninjas = context.Ninjas.SqlQuery("exec GetOldNinjas");
                foreach (var ninja in ninjas)
                {
                    Console.WriteLine(ninja.Name); 
                }
            }
        }

        private static void RetrieveDataWithFind()
        {
            var keyval = 3;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.Find(keyval);
                Console.WriteLine($"After Find#1 {ninja.Name}");

                var someNinja = context.Ninjas.Find(keyval);
                Console.WriteLine($"After Find#2 {someNinja.Name}");
                ninja = null;
            }
        }

        private static void QueryandUpdateNinjaDisconnected()
        {   //For online application only
            NinjaDomain.Classes.Ninja ninja;

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
            }
            ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Attach(ninja);
                context.Entry(ninja).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private static void QueryandUpdateNinja()
        {//For offline application only.
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);
                context.SaveChanges();
            }
        }

        private static void SimpleNinjaQueries()
        {
            using (var context = new NinjaContext())
            {
                var ninjas = context.Ninjas.ToList();
                //var query = context.Ninjas.Where(x => x.Name == "Junar");
                var query = context.Ninjas
                    .Where(x => x.DateOfBirth == new DateTime(1986,06,20))
                    .FirstOrDefault();
                //var someinninjas = query.ToList();
                Console.WriteLine(query.Name);
                //foreach (var ninja in query)
                //{
                //    Console.WriteLine(ninja.Name);
                //}
            }
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
