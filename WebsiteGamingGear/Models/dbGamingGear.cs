using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebsiteGamingGear.Models
{
    public partial class dbGamingGear : DbContext
    {
        public dbGamingGear()
            : base("name=dbGamingGear1")
        {
        }

        public virtual DbSet<BinhLuanTinTuc> BinhLuanTinTucs { get; set; }
        public virtual DbSet<ChiTietDDH> ChiTietDDHs { get; set; }
        public virtual DbSet<DanhGia> DanhGias { get; set; }
        public virtual DbSet<DanhMucSP> DanhMucSPs { get; set; }
        public virtual DbSet<DanhMucTinTuc> DanhMucTinTucs { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; }
        public virtual DbSet<LienHe> LienHes { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TheLoaiSP> TheLoaiSPs { get; set; }
        public virtual DbSet<TheLoaiTinTuc> TheLoaiTinTucs { get; set; }
        public virtual DbSet<ThuongHieu> ThuongHieux { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BinhLuanTinTuc>()
                .Property(e => e.trangThai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DanhGia>()
                .Property(e => e.trangThai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DanhMucSP>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.DanhMucSP)
                .HasForeignKey(e => e.idDanhMuc);

            modelBuilder.Entity<DanhMucSP>()
                .HasMany(e => e.TheLoaiSPs)
                .WithRequired(e => e.DanhMucSP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DanhMucTinTuc>()
                .HasMany(e => e.TheLoaiTinTucs)
                .WithRequired(e => e.DanhMucTinTuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonDatHang>()
                .Property(e => e.trangThaiThanhToan)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DonDatHang>()
                .Property(e => e.trangThai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DonDatHang>()
                .Property(e => e.sdtNguoiNhan)
                .IsUnicode(false);

            modelBuilder.Entity<DonDatHang>()
                .HasMany(e => e.ChiTietDDHs)
                .WithRequired(e => e.DonDatHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LienHe>()
                .Property(e => e.trangThai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.hinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.hinhAnh1)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.hinhAnh2)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.hinhAnh3)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.hinhAnh4)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.hinhAnh5)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietDDHs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.DanhGias)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.taiKhoan1)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.matKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.requestCode)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.soDienThoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.anhDaiDien)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.gioiTinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.trangThai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.BinhLuanTinTucs)
                .WithRequired(e => e.TaiKhoan)
                .HasForeignKey(e => e.idTaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.DanhGias)
                .WithRequired(e => e.TaiKhoan)
                .HasForeignKey(e => e.idNguoiDanhGia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.DonDatHangs)
                .WithRequired(e => e.TaiKhoan)
                .HasForeignKey(e => e.idTaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.LienHes)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.idTaiKhoan);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.TinTucs)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.idTaiKhoan);

            modelBuilder.Entity<TheLoaiSP>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.TheLoaiSP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TheLoaiTinTuc>()
                .HasMany(e => e.TinTucs)
                .WithOptional(e => e.TheLoaiTinTuc)
                .HasForeignKey(e => e.theLoaiTin);

            modelBuilder.Entity<ThuongHieu>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.ThuongHieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TinTuc>()
                .Property(e => e.hinhAnh1)
                .IsUnicode(false);

            modelBuilder.Entity<TinTuc>()
                .HasMany(e => e.BinhLuanTinTucs)
                .WithRequired(e => e.TinTuc)
                .WillCascadeOnDelete(false);
        }
    }
}
