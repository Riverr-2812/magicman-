using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_CuoiKy.Models;

namespace Web_CuoiKy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Nhacungcap> nhacungcaps { get; set; }
        public DbSet<Sanpham> sanphams { get; set; }

        public DbSet<GioHang> gioHangs { get; set; }
        public DbSet<ApplicationUser> applications { get; set; }
        public DbSet<HoaDon> hoaDons { get; set; }
        public DbSet<ChitietHoaDon> chitietHoaDons { get; set; }
        public DbSet<DatBan> datBans { get; set; }
    }
}