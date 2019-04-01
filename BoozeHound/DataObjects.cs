using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace BoozeHound
{
    [Table("Beers")]
    public class Beer
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string Brewery { get; set; }
        public string Style { get; set; }
        public double? ABV { get; set; }
        public string Notes { get; set; }
        [NotNull]
        public double Rating { get; set; }
        [NotNull]
        public DateTime Date { get; set; }
    }

    [Table("Wines")]
    public class Wine
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string Winery { get; set; }
        public string Type { get; set; }
        public float ABV { get; set; }
        public int Vintage { get; set; }
        public string Notes { get; set; }
        [NotNull]
        public double Rating { get; set; }
        [NotNull]
        public DateTime Timestamp { get; set; }
    }

    [Table("Spirits")]
    public class Spirit
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        public string Distiller { get; set; }
        public string Type { get; set; }
        public float ABV { get; set; }
        public int Age { get; set; }
        public string Notes { get; set; }
        [NotNull]
        public float Rating { get; set; }
        [NotNull]
        public DateTime Timestamp { get; set; }
    }
}
