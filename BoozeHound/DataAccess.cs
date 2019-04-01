using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using SQLite;
using System.Linq;

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
            //Database.DropTable<Beer>();
            //Database.DropTable<Wine>();
            //Database.DropTable<Spirit>();
            Database.CreateTable<Beer>();
            Database.CreateTable<Wine>();
            Database.CreateTable<Spirit>();
        }

        #region Beer Methods

        public static void SaveBeer(Beer beer)
        {
            try
            {
                beer.Date = DateTime.Now;
                beer.Name = beer.Name.ToUpper();
                beer.Brewery = beer.Brewery?.ToUpper();
                beer.Style = beer.Style?.ToUpper();
                Database.Insert(beer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SaveBeer(): {0}", ex.Message);
            }
        }

        public static void DeleteBeer(int id)
        {
            Database.Delete<Beer>(id);
        }

        public static List<Beer> GetBeers()
        {
            return Database.Table<Beer>().ToList();
        }

        public static List<Beer> GetBeers(string query)
        {
            try
            {
                return Database.Query<Beer>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new List<Beer>();
        }

        public static List<Beer> SearchBeers(string name)
        {
            return Database.Query<Beer>("SELECT * FROM Beers WHERE Name LIKE '%" + name + "%';");
        }

        public static string[] GetBreweries()
        {
            return Database.Query<Beer>("SELECT DISTINCT Brewery FROM Beers WHERE Brewery NOT NULL ORDER BY Brewery").Select(x => x.Brewery).ToArray();
        }

        public static string[] GetBeerStyles()
        {
            return Database.Query<Beer>("SELECT DISTINCT Style FROM Beers Where Style NOT NULL ORDER BY Style").Select(x => x.Style).ToArray();
        }

        public static void AddTestBeers()
        {
            if (Database.Table<Beer>().Count() == 0)
            {
                Database.Insert(new Beer()
                {
                    Name = "HIGH LIFE",
                    Rating = 1.5,
                    Date = DateTime.Now.AddDays(-20),
                    Style = "LAGER",
                    ABV = 5.0,
                    Brewery = "MILLER",
                    Notes = "Unremarkable and bland, but drinkable."
                });
                Database.Insert(new Beer()
                {
                    Name = "LIL JUICY",
                    Rating = 4.5,
                    Brewery = "TWO ROADS",
                    Date = DateTime.Now,
                    ABV = 5.2,
                    Style = "IPA",
                    Notes = "Juicy and delicious. Incredibly drinkable."
                });
                Database.Insert(new Beer()
                {
                    Name = "HORIZONTAL LINES",
                    Rating = 4.0,
                    Brewery = "FINBACK",
                    Date = DateTime.Now.AddDays(-14),
                    Style = "PILSNER",
                    Notes = "Non-traditional, but can still taste the pilsner characteristics. Somewhat herbally."
                });
                Database.Insert(new Beer()
                {
                    Name = "JOHN HENRY MILK STOUT",
                    Rating = 4.0,
                    Date = DateTime.Now.AddDays(-32),
                    Style = "STOUT",
                    Notes = "Creamy and delicious."
                });
            }
        }

#endregion Beer Methods

            #region Wine Methods

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

            #endregion Wine Methods

            #region Spirit 

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

            #endregion Spirit Methods
    }
}