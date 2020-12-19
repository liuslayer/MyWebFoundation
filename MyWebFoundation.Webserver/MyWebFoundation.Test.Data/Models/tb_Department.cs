namespace MyWebFoundation.Test.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tb_department")]
    public partial class tb_Department
    {
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [StringLength(50)]
        [Column("description")]
        public string Description { get; set; }

        [NotMapped]
        public ICollection<tb_User> tb_Users { get; set; }
    }
}
