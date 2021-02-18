using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INF_SUSK_Project_Entities
{
    public class INF_SUSK_Context : DbContext
    {
        public DbSet<TBLSTOKURS> TBLSTOKURS { get; set; }
        public DbSet<TBLISEMRI> TBLISEMRI { get; set; }
        public DbSet<TBLISEMRIEK> TBLISEMRIEK { get; set; }
        public DbSet<TBLSTOKPH> TBLSTOKPH { get; set; }
        public DbSet<TBLISEMRI_MKA> TBLISEMRI_MKA { get; set; }
        //Tabloların sonuna s eklememesi için OnModelCreating metodunu ezdik
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TBLSTOKURS>().ToTable("TBLSTOKURS");
            modelBuilder.Entity<TBLISEMRI>().ToTable("TBLISEMRI");
            modelBuilder.Entity<TBLISEMRIEK>().ToTable("TBLISEMRIEK");
            modelBuilder.Entity<TBLISEMRI_MKA>().ToTable("TBLISEMRI_MKA");
            modelBuilder.Entity<TBLSTOKPH>().ToTable("TBLSTOKPH");
        }
    }
}
