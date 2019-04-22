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
            try
            {
                //Database.DropTable<Beer>();
                //Database.DropTable<Wine>();
                //Database.DropTable<Spirit>();
                Database.CreateTable<Beer>();
                Database.CreateTable<Wine>();
                Database.CreateTable<Spirit>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in CreateTables(): {0}", ex.Message);
            }
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

        public static void UpdateBeer(Beer beer)
        {
            try
            {
                beer.Name = beer.Name.ToUpper();
                beer.Brewery = beer.Brewery?.ToUpper();
                beer.Style = beer.Style?.ToUpper();
                Database.Update(beer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateBeer(): {0}", ex.Message);
            }
        }

        public static void DeleteBeer(Beer beer)
        {
            try
            {
                if (!string.IsNullOrEmpty(beer.ImagePath))
                    File.Delete(beer.ImagePath);

                Database.Delete<Beer>(beer.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DeleteBeer(): {0}", ex.Message);
            }
        }

        public static List<Beer> GetBeers()
        {
            try
            {
                return Database.Table<Beer>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetBeers(): {0}", ex.Message);
            }

            return new List<Beer>();
        }

        public static List<Beer> GetBeers(string query)
        {
            try
            {
                return Database.Query<Beer>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetBeers(query): {0}", ex.Message);
            }

            return new List<Beer>();
        }

        public static List<Beer> SearchBeers(string name)
        {
            try
            {
                return Database.Query<Beer>($"SELECT * FROM Beers WHERE Name LIKE '%{name}%';");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SearchBeers(): {0}", ex.Message);
            }

            return new List<Beer>();
        }

        public static string[] GetBreweries()
        {
            try
            {
                return Database.Query<Beer>("SELECT DISTINCT Brewery FROM Beers WHERE Brewery NOT NULL ORDER BY Brewery;").Select(x => x.Brewery).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception is GetBreweries(): {0}", ex.Message);
            }

            return new string[] { };
        }

        public static string[] GetBeerStyles()
        {
            try
            {
                return Database.Query<Beer>("SELECT DISTINCT Style FROM Beers Where Style NOT NULL ORDER BY Style;").Select(x => x.Style).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetBeerStyles(): {0}", ex.Message);
            }

            return new string[] { };
        }

        public static void AddTestBeers()
        {
            //Database.DeleteAll<Beer>();
            Database.CreateTable<Beer>();
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
                wine.Date = DateTime.Now;
                wine.Name = wine.Name.ToUpper();
                wine.Winery = wine.Winery?.ToUpper();
                wine.Type = wine.Type?.ToUpper();
                Database.Insert(wine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SaveWine(): {0}", ex.Message);
            }
        }

        public static void UpdateWine(Wine wine)
        {
            try
            {
                wine.Name = wine.Name.ToUpper();
                wine.Winery = wine.Winery?.ToUpper();
                wine.Type = wine.Type?.ToUpper();
                Database.Update(wine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateWine(): {0}", ex.Message);
            }
        }

        public static void DeleteWine(Wine wine)
        {
            try
            {
                if (!string.IsNullOrEmpty(wine.ImagePath))
                    File.Delete(wine.ImagePath);

                Database.Delete<Wine>(wine.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DeleteWine(): {0}", ex.Message);
            }
        }

        public static List<Wine> GetWines()
        {
            try
            {
                return Database.Table<Wine>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetWines(): {0}", ex.Message);
            }

            return new List<Wine>();
        }

        public static List<Wine> GetWines(string query)
        {
            try
            {
                return Database.Query<Wine>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetWines(query): {0}", ex.Message);
            }

            return new List<Wine>();
        }

        public static List<Wine> SearchWines(string name)
        {
            try
            {
                return Database.Query<Wine>($"SELECT * FROM Wines WHERE Name LIKE '%{name}%';");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SearchWines(): {0}", ex.Message);
            }

            return new List<Wine>();
        }

        public static string[] GetWineries()
        {
            try
            {
                return Database.Query<Wine>("SELECT DISTINCT Winery FROM Wines WHERE Winery NOT NULL ORDER BY Winery;").Select(x => x.Winery).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetWineries(): {0}", ex.Message);
            }

            return new string[] { };
        }

        public static string[] GetWineTypes()
        {
            try
            {
                return Database.Query<Wine>("SELECT DISTINCT Type FROM Wines WHERE Type NOT NULL ORDER BY Type;").Select(x => x.Type).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetWineTypes(): {0}", ex.Message);
            }

            return new string[] { };
        }

        #endregion Wine Methods

        #region Spirit 

        public static void SaveSpirit(Spirit spirit)
        {
            try
            {
                spirit.Date = DateTime.Now;
                spirit.Name = spirit.Name.ToUpper();
                spirit.Distiller = spirit.Distiller?.ToUpper();
                spirit.Type = spirit.Type?.ToUpper();
                Database.Insert(spirit);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SaveSpirit(): {0}", ex.Message);
            }
        }

        public static void UpdateSpirit(Spirit spirit)
        {
            try
            {
                spirit.Name = spirit.Name.ToUpper();
                spirit.Distiller = spirit.Distiller.ToUpper();
                spirit.Type = spirit.Type.ToUpper();
                Database.Update(spirit);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateSpirit(): {0}", ex.Message);
            }
        }

        public static void DeleteSpirit(Spirit spirit)
        {
            try
            {
                if (!string.IsNullOrEmpty(spirit.ImagePath))
                    File.Delete(spirit.ImagePath);

                Database.Delete<Spirit>(spirit.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DeleteSpirit(): {0}", ex.Message);
            }
        }

        public static List<Spirit> GetSpirits()
        {
            try
            {
                return Database.Table<Spirit>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetSpirits(): {0}", ex.Message);
            }

            return new List<Spirit>();
        }

        public static List<Spirit> GetSpirits(string query)
        {
            try
            {
                return Database.Query<Spirit>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception is GetSpirits(query): {0}", ex.Message);
            }

            return new List<Spirit>();
        }

        public static List<Spirit> SearchSpirits(string name)
        {
            try
            {
                return Database.Query<Spirit>($"SELECT * FROM Spirits WHERE Name LIKE '%{name}%';");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SearchSpirits(): {0}", ex.Message);
            }

            return new List<Spirit>();
        }

        public static string[] GetDistillers()
        {
            try
            {
                return Database.Query<Spirit>("SELECT DISTINCT Distiller FROM Spirits WHERE Distillery NOT NULL ORDER BY Distiller;").Select(x => x.Distiller).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetDistillers(): {0}", ex.Message);
            }

            return new string[] { };
        }

        public static string[] GetSpiritTypes()
        {
            try
            {
                return Database.Query<Spirit>("SELECT DISTINCT Type FROM Spirits WHERE Type NOT NULL ORDER BY Type;").Select(x => x.Type).ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetSpiritTypes(): {0}", ex.Message);
            }

            return new string[] { };
        }

        #endregion Spirit Methods
    }
}