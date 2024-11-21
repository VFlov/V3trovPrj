using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V3trovPrj.Models
{
    [Table("software")]
    public class Software
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("icon_path")]
        public string? IconPath { get; set; }
        [Column("download_path")]
        public string? DownloadPath { get; set; }
        [Column("size")]
        public int Size { get; set; }
        [Column("uploaded_at")]
        public DateTime UploadedAt{get;set;}
        [Column("description")]
        public string? Description { get; set; }
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("version")]
        public string? Version { get; set; }
        [Column("download_count")]
        public int DownloadCount { get; set; }
        [Column("link")]
        public string? Link { get; set; }
    }
}
