namespace MyWebFoundation.Test.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestDBContext : DbContext
    {
        public TestDBContext(string connName)
            : base($"name={connName}")
        {
        }

        public virtual DbSet<tb_Department> tb_Department { get; set; }
        public virtual DbSet<tb_User> tb_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
