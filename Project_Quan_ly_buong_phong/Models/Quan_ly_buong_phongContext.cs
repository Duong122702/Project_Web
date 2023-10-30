using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project_Quan_ly_buong_phong.Models
{
    public partial class Quan_ly_buong_phongContext : DbContext
    {
        public Quan_ly_buong_phongContext()
        {
        }

        public Quan_ly_buong_phongContext(DbContextOptions<Quan_ly_buong_phongContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Chitietdv> Chitietdvs { get; set; } = null!;
        public virtual DbSet<Dichvu> Dichvus { get; set; } = null!;
        public virtual DbSet<Dskdp> Dskdps { get; set; } = null!;
        public virtual DbSet<Hoadon> Hoadons { get; set; } = null!;
        public virtual DbSet<Khachhang> Khachhangs { get; set; } = null!;
        public virtual DbSet<Phong> Phongs { get; set; } = null!;
        public virtual DbSet<Thuoc> Thuocs { get; set; } = null!;

        public virtual DbSet<Login> Logins { get; set; } = null!;
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(local); Initial Catalog=Quan_ly_buong_phong; Persist Security Info=True; User ID=sa;Password=Duong15012003;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__LOGIN__67884E4F811A3412");

                entity.ToTable("Userlogin");

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PasswordID");
                entity.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImagePath");

            });
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.MaDatPhong)
                    .HasName("PK__BOOKING__67884E4FB9F763C4");

                entity.ToTable("BOOKING");

                entity.Property(e => e.MaDatPhong)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDatPhong");

                entity.Property(e => e.NgayDat)
                    .HasColumnType("date")
                    .HasColumnName("ngayDat");

                entity.Property(e => e.SoNguoi).HasColumnName("soNguoi");

                entity.Property(e => e.YeuCau)
                    .HasMaxLength(50)
                    .HasColumnName("yeuCau");
            });

            modelBuilder.Entity<Chitietdv>(entity =>
            {
                entity.ToTable("CHITIETDV");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaDatPhong)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDatPhong");

                entity.Property(e => e.MaDichVu)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDichVu");

                entity.Property(e => e.SoLuong).HasColumnName("soLuong");

                entity.HasOne(d => d.MaDatPhongNavigation)
                    .WithMany(p => p.Chitietdvs)
                    .HasForeignKey(d => d.MaDatPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CHITIETDV__ID__49C3F6B7");

                entity.HasOne(d => d.MaDichVuNavigation)
                    .WithMany(p => p.Chitietdvs)
                    .HasForeignKey(d => d.MaDichVu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CHITIETDV__maDic__4AB81AF0");
            });

            modelBuilder.Entity<Dichvu>(entity =>
            {
                entity.HasKey(e => e.MaDichVu)
                    .HasName("PK__DICHVU__80F48B0914DC10D3");

                entity.ToTable("DICHVU");

                entity.Property(e => e.MaDichVu)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDichVu");

                entity.Property(e => e.DonViTinh)
                    .HasMaxLength(10)
                    .HasColumnName("donViTinh");

                entity.Property(e => e.GiaDichVu).HasColumnName("giaDichVu");

                entity.Property(e => e.TenDichVu)
                    .HasMaxLength(25)
                    .HasColumnName("tenDichVu");
            });

            modelBuilder.Entity<Dskdp>(entity =>
            {
                entity.ToTable("DSKDP");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaDatPhong)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDatPhong");

                entity.Property(e => e.MaKhachHang)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maKhachHang");

                entity.HasOne(d => d.MaDatPhongNavigation)
                    .WithMany(p => p.Dskdps)
                    .HasForeignKey(d => d.MaDatPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DSKDP__maDatPhon__4316F928");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.Dskdps)
                    .HasForeignKey(d => d.MaKhachHang)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DSKDP__ID__4222D4EF");
            });

            modelBuilder.Entity<Hoadon>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon)
                    .HasName("PK__HOADON__026B4D9A44AB035A");

                entity.ToTable("HOADON");

                entity.Property(e => e.MaHoaDon)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maHoaDon");

                entity.Property(e => e.HìnhThucThanhToan)
                    .HasMaxLength(10)
                    .HasColumnName("hìnhThucThanhToan");

                entity.Property(e => e.MaDatPhong)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDatPhong");

                entity.Property(e => e.NgayThanhToan)
                    .HasColumnType("date")
                    .HasColumnName("ngayThanhToan");

                entity.Property(e => e.TongTien).HasColumnName("tongTien");

                entity.HasOne(d => d.MaDatPhongNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.MaDatPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HOADON__maDatPho__3F466844");
            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang)
                    .HasName("PK__KHACHHAN__0CCB3D49250F6709");

                entity.ToTable("KHACHHANG");

                entity.Property(e => e.MaKhachHang)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maKhachHang");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("cccd");

                entity.Property(e => e.Dem)
                    .HasMaxLength(10)
                    .HasColumnName("dem");

                entity.Property(e => e.Email)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.GioiTinh).HasColumnName("gioiTinh");

                entity.Property(e => e.Ho)
                    .HasMaxLength(10)
                    .HasColumnName("ho");

                entity.Property(e => e.NgaySinh)
                    .HasColumnType("date")
                    .HasColumnName("ngaySinh");

                entity.Property(e => e.PhanLoai).HasColumnName("phanLoai");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("sdt");

                entity.Property(e => e.Ten)
                    .HasMaxLength(10)
                    .HasColumnName("ten");
            });

            modelBuilder.Entity<Phong>(entity =>
            {
                entity.HasKey(e => e.TenPhong)
                    .HasName("PK__PHONG__97B99B8FB4D5A677");

                entity.ToTable("PHONG");

                entity.Property(e => e.TenPhong)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("tenPhong");

                entity.Property(e => e.BanCong).HasColumnName("banCong");

                entity.Property(e => e.GiaCa).HasColumnName("giaCa");

                entity.Property(e => e.KichThuoc).HasColumnName("kichThuoc");

                entity.Property(e => e.LoaiGiuong)
                    .HasMaxLength(10)
                    .HasColumnName("loaiGiuong");

                entity.Property(e => e.LoaiPhong).HasColumnName("loaiPhong");

                entity.Property(e => e.LoaiPhongTam)
                    .HasMaxLength(10)
                    .HasColumnName("loaiPhongTam");

                entity.Property(e => e.SoGiuong).HasColumnName("soGiuong");

                entity.Property(e => e.ViewPhong)
                    .HasMaxLength(10)
                    .HasColumnName("viewPhong");
            });

            modelBuilder.Entity<Thuoc>(entity =>
            {
                entity.HasKey(e => new { e.MaDatPhong, e.TenPhong })
                    .HasName("PK__THUOC__8EF3D7F7DE01229E");

                entity.ToTable("THUOC");

                entity.Property(e => e.MaDatPhong)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("maDatPhong");

                entity.Property(e => e.TenPhong)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("tenPhong");

                entity.Property(e => e.NgayDen)
                    .HasColumnType("date")
                    .HasColumnName("ngayDen");

                entity.Property(e => e.NgayDi)
                    .HasColumnType("date")
                    .HasColumnName("ngayDi");

                entity.HasOne(d => d.MaDatPhongNavigation)
                    .WithMany(p => p.Thuocs)
                    .HasForeignKey(d => d.MaDatPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__THUOC__maDatPhon__45F365D3");

                entity.HasOne(d => d.TenPhongNavigation)
                    .WithMany(p => p.Thuocs)
                    .HasForeignKey(d => d.TenPhong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__THUOC__tenPhong__46E78A0C");
            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
