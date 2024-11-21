using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V3trovPrj.Models
{
    [Table("categories")]
    public class Categories
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("sort_order")]
        public int SortOrder { get; set; }
    }
}
