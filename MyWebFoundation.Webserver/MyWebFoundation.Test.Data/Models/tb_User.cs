namespace MyWebFoundation.Test.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tb_user")]
    public partial class tb_User
    {
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column("username")]
        public string UserName { get; set; }

        [Column("age")]
        public int Age { get; set; }

        [StringLength(50)]
        [Column("address")]
        public string Address { get; set; }

        [Column("departmentid")]
        public long DepartmentId { get; set; }
    }
}
