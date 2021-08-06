using FreeSql.DataAnnotations;
using System;

namespace FreeSqlDemo.Model
{
    public class Blog
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }

    }
}
