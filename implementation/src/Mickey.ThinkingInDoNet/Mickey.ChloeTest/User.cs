using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mickey.ChloeTest
{
    public enum Gender
    {
        Man = 1,
        Woman
    }

    [Table("Users")]
    public class User
    {
        [Column(IsPrimaryKey = true)]
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender? Gender { get; set; }
        public int? Age { get; set; }
        public int? CityId { get; set; }
        public DateTime? OpTime { get; set; }
    }

    public class City
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
    }

    public class Province
    {
        [Column(IsPrimaryKey = true)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
