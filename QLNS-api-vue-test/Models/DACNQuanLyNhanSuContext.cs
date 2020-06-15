using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QLNS_api_vue_test.Models
{
    public partial class DACNQuanLyNhanSuContext : DbContext
    {
        public DACNQuanLyNhanSuContext()
        {
        }

        public DACNQuanLyNhanSuContext(DbContextOptions<DACNQuanLyNhanSuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chamcong> Chamcong { get; set; }
        public virtual DbSet<Chitietchamcong> Chitietchamcong { get; set; }
        public virtual DbSet<Chucvu> Chucvu { get; set; }
        public virtual DbSet<Hopdong> Hopdong { get; set; }
        public virtual DbSet<Nhanvien> Nhanvien { get; set; }
        public virtual DbSet<Phancong> Phancong { get; set; }
        public virtual DbSet<Phongban> Phongban { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-08CCL2K;Database=DACNQuanLyNhanSu;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Chamcong>(entity =>
            {
                entity.HasKey(e => e.Day);

                entity.ToTable("CHAMCONG");

                entity.Property(e => e.Day).HasColumnType("datetime");
            });

            modelBuilder.Entity<Chitietchamcong>(entity =>
            {
                entity.HasKey(e => new { e.MaNhanVien, e.Day });

                entity.ToTable("CHITIETCHAMCONG");

                entity.Property(e => e.Day).HasColumnType("datetime");

                entity.Property(e => e.GioBatDau).HasColumnType("datetime");

                entity.Property(e => e.GioKetThuc).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DayNavigation)
                    .WithMany(p => p.Chitietchamcong)
                    .HasForeignKey(d => d.Day)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHITIETCHAMCONG_CHAMCONG");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.Chitietchamcong)
                    .HasForeignKey(d => d.MaNhanVien)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CHITIETCHAMCONG_NHANVIEN");
            });

            modelBuilder.Entity<Chucvu>(entity =>
            {
                entity.HasKey(e => e.MaChucVu)
                    .HasName("PK__CHUCVU__D463953318C1797C");

                entity.ToTable("CHUCVU");

                entity.Property(e => e.MaChucVu)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.LuongCb)
                    .HasColumnName("LuongCB")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TenChucVu).HasMaxLength(50);
            });

            modelBuilder.Entity<Hopdong>(entity =>
            {
                entity.HasKey(e => e.MaLoaiHopDong)
                    .HasName("PK__HOPDONG__BCF5FB7CA2AF81CB");

                entity.ToTable("HOPDONG");

                entity.Property(e => e.NgayHetHan).HasColumnType("date");

                entity.Property(e => e.NgayVaoLam).HasColumnType("date");

                entity.Property(e => e.TenHopDong).HasMaxLength(50);

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.Hopdong)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK_HopDong");
            });

            modelBuilder.Entity<Nhanvien>(entity =>
            {
                entity.HasKey(e => e.MaNhanVien)
                    .HasName("PK__NHANVIEN__77B2CA473FDD33B1");

                entity.ToTable("NHANVIEN");

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.GioiTinh)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoKhau).HasMaxLength(50);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.MaChucVu)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.NoiSinh).HasMaxLength(50);

                entity.Property(e => e.SoCmnd)
                    .HasColumnName("SoCMND")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.TenDangNhap)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ThucLanh).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.MaChucVuNavigation)
                    .WithMany(p => p.Nhanvien)
                    .HasForeignKey(d => d.MaChucVu)
                    .HasConstraintName("FK_ChucVu");

                entity.HasOne(d => d.MaPhongBanNavigation)
                    .WithMany(p => p.Nhanvien)
                    .HasForeignKey(d => d.MaPhongBan)
                    .HasConstraintName("FK_PhongBan");
            });

            modelBuilder.Entity<Phancong>(entity =>
            {
                entity.HasKey(e => e.MaPhanCong);

                entity.ToTable("PHANCONG");

                entity.Property(e => e.NoiDungCongViec).HasMaxLength(50);

                entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");

                entity.Property(e => e.ThoiGianKetThuc).HasColumnType("datetime");

                entity.Property(e => e.TienDo)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Phongban>(entity =>
            {
                entity.HasKey(e => e.MaPhongBan)
                    .HasName("PK__PHONGBAN__D0910CC8275D6EC4");

                entity.ToTable("PHONGBAN");

                entity.Property(e => e.TenPhongBan)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
