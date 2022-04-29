namespace Model1.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SearchPost")]
    public partial class SearchPost
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        [StringLength(50)]
        public string Keyword { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedDay { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }
    }
}
