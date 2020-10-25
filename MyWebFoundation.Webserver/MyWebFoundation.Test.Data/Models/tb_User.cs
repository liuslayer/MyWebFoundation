namespace MyWebFoundation.Test.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_User
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        public int Age { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public long DepartmentId { get; set; }
    }
}
