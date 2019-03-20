using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using SQLite;

namespace BoozeHound
{
    class DataAccess
    {
        private const string dbName = "boozehound_db.db3";

        private static SQLiteConnection db = null;

        private static SQLiteConnection Database
        {
            get
            {
                if (db == null)
                    db = new SQLiteConnection(Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName));

                return db;
            }
        }

        public static void CreateTables()
        {
            Database.CreateTable<Beer>();
            Database.CreateTable<Wine>();
            Database.CreateTable<Spirit>();
        }

        #region Save Methods

        public static void SaveBeer(Beer beer)
        {
            try
            {
                Database.Insert(beer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SaveBeer(): {0}", ex.Message);
            }
        }

        public static void SaveWine(Wine wine)
        {
            try
            {
                Database.Insert(wine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SaveWine(): {0}", ex.Message);
            }
        }

        public static void SaveSpirit(Spirit spirit)
        {
            try
            {
                Database.Insert(spirit);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SaveSpirit(): {0}", ex.Message);
            }
        }

        #endregion Save Methods

        public static List<Beer> GetBeers()
        {
            return Database.Table<Beer>().ToList();
        }

        public static List<Beer> GetBeers(string where, string[] args)
        {
            return Database.Query<Beer>(where, args);
        }

        public static List<Beer> SearchBeers(string name)
        {
            return Database.Query<Beer>("SELECT * FROM Beers WHERE Name LIKE '%" + name + "%';");
        }

        public static void AddTestBeers()
        {
            Database.DeleteAll<Beer>();
            Database.Insert(new Beer() { Name = "High Life", Rating = 1.5, Timestamp = DateTime.Now });
            Database.Insert(new Beer() { Name = "Lil Juicy", Rating = 4.5, Brewery = "Two Roads", Timestamp = DateTime.Now });
            Database.Insert(new Beer() { Name = "Horizontal Lines", Rating = 4.0, Brewery = "Finback", Timestamp = DateTime.Now });
            Database.Insert(new Beer() { Name = "John Henry Milk Stout", Rating = 4.0, Timestamp = DateTime.Now });
        }
    }
}