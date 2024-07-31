using Management.Models.Entities;
using Microsoft.EntityFrameworkCore;
using QLNV.Models.Entities;

namespace QLNV.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<PhanQuyen> PhanQuyen { get; set; }
        public DbSet<ChucNang> ChucNang { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<NguoiLam> NguoiLam { get; set; }
        public DbSet<Tep> Teps { get; set; }
    }
}