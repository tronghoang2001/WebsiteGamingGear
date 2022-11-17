using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebsiteGamingGear.Models
{
    public partial class dbGamingGear : DbContext
    {
        public dbGamingGear()
            : base("name=dbGamingGear3")
        {
        }

        public virtual DbSet<API> APIs { get; set; }
        public virtual DbSet<BinhLuanTinTuc> BinhLuanTinTucs { get; set; }
        public virtual DbSet<ChiTietDDH> ChiTietDDHs { get; set; }
        public virtual DbSet<ChuongTrinhGiamGia> ChuongTrinhGiamGias { get; set; }
        public virtual DbSet<DanhGia> DanhGias { get; set; }
        public virtual DbSet<DanhMucSP> DanhMucSPs { get; set; }
        public virtual DbSet<DanhMucTinTuc> DanhMucTinTucs { get; set; }
        public virtual DbSet<DiaChiDDH> DiaChiDDHs { get; set; }
        public virtual DbSet<DiaChiKhachHang> DiaChiKhachHangs { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; }
        public virtual DbSet<LienHe> LienHes { get; set; }
        public virtual DbSet<Phuong_Xa> Phuong_Xa { get; set; }
        public virtual DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; }
        public virtual DbSet<PhuongThucVanChuyen> PhuongThucVanChuyens { get; set; }
        public virtual DbSet<Quan_Huyen> Quan_Huyen { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<ThaoTac> ThaoTacs { get; set; }
        public virtual DbSet<TheLoaiSP> TheLoaiSPs { get; set; }
        public virtual DbSet<TheLoaiTinTuc> TheLoaiTinTucs { get; set; }
        public virtual DbSet<ThuongHieu> ThuongHieux { get; set; }
        public virtual DbSet<Tinh_ThanhPho> Tinh_ThanhPho { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<API>()
                .Property(e => e.clientID)
                .IsUnicode(false);

            modelBuilder.Entity<API>()
                .Property(e => e.secretKey)
                .IsUnicode(false);

            modelBuilder.Entity<API>()
                .Property(e => e.returnURL)
                .IsUnicode(false);

            modelBuilder.Entity<BinhLuanTinTuc>()
                .Property(e => e.trangThai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietDDH>()
                .Property(e => e.trangThai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietDDH>()
                .Property(e => e.maGiamGia)
                .IsUnicode(false);

            modelBuilder.Entity<ChuongTrinhGiamGia>()
                .Property(e => e.maGiamGia)
                .IsFixedLength();

            modelBuilder.Entity<ChuongTrinhGiamGia>()
                .Property(e => e.trangThai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DanhGia>()
                .Property(e => e.trangThai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DanhMucSP>()
                .Property(e => e.icon)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMucSP>()
                .Property(e => e.link)
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
                .Property(e => e.icon)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMucTinTuc>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMucTinTuc>()
                .HasMany(e => e.TheLoaiTinTucs)
                .WithRequired(e => e.DanhMucTinTuc)
                .HasForeignKey(e => e.idDanhMucTT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DanhMucTinTuc>()
                .HasMany(e => e.TheLoaiTinTucs1)
                .WithRequired(e => e.DanhMucTinTuc1)
                .HasForeignKey(e => e.idDanhMucTT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DiaChiDDH>()
                .Property(e => e.soDienThoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DiaChiDDH>()
                .Property(e => e.email)
                .IsUnicode(false);

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
                .Property(e => e.soDienThoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LienHe>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<LienHe>()
                .Property(e => e.hinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<LienHe>()
                .Property(e => e.trangThai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Phuong_Xa>()
                .HasMany(e => e.DiaChiDDHs)
                .WithOptional(e => e.Phuong_Xa)
                .HasForeignKey(e => e.phuongXa);

            modelBuilder.Entity<Phuong_Xa>()
                .HasMany(e => e.DiaChiKhachHangs)
                .WithRequired(e => e.Phuong_Xa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhuongThucVanChuyen>()
                .Property(e => e.giaVanChuyen)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Quan_Huyen>()
                .HasMany(e => e.DiaChiDDHs)
                .WithOptional(e => e.Quan_Huyen)
                .HasForeignKey(e => e.quanHuyen);

            modelBuilder.Entity<Quan_Huyen>()
                .HasMany(e => e.DiaChiKhachHangs)
                .WithRequired(e => e.Quan_Huyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Quan_Huyen>()
                .HasMany(e => e.Phuong_Xa)
                .WithRequired(e => e.Quan_Huyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Quyen>()
                .HasMany(e => e.TaiKhoans)
                .WithOptional(e => e.Quyen1)
                .HasForeignKey(e => e.quyen);

            modelBuilder.Entity<Quyen>()
                .HasMany(e => e.ThaoTacs)
                .WithMany(e => e.Quyens)
                .Map(m => m.ToTable("QuyenThaoTac").MapLeftKey("idQuyen").MapRightKey("idThaoTac"));

            modelBuilder.Entity<SanPham>()
                .Property(e => e.trangThai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.hinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.link)
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
                .HasMany(e => e.DiaChiKhachHangs)
                .WithRequired(e => e.TaiKhoan)
                .HasForeignKey(e => e.idTaiKhoan)
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
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<TheLoaiSP>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.TheLoaiSP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TheLoaiTinTuc>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<TheLoaiTinTuc>()
                .HasMany(e => e.TinTucs)
                .WithOptional(e => e.TheLoaiTinTuc)
                .HasForeignKey(e => e.theLoaiTin);

            modelBuilder.Entity<ThuongHieu>()
                .Property(e => e.logoThuongHieu)
                .IsUnicode(false);

            modelBuilder.Entity<ThuongHieu>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<ThuongHieu>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.ThuongHieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tinh_ThanhPho>()
                .HasMany(e => e.DiaChiDDHs)
                .WithOptional(e => e.Tinh_ThanhPho)
                .HasForeignKey(e => e.thanhPho);

            modelBuilder.Entity<Tinh_ThanhPho>()
                .HasMany(e => e.DiaChiKhachHangs)
                .WithRequired(e => e.Tinh_ThanhPho)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tinh_ThanhPho>()
                .HasMany(e => e.Quan_Huyen)
                .WithRequired(e => e.Tinh_ThanhPho)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TinTuc>()
                .Property(e => e.hinhAnh1)
                .IsUnicode(false);

            modelBuilder.Entity<TinTuc>()
                .Property(e => e.hinhAnh2)
                .IsUnicode(false);

            modelBuilder.Entity<TinTuc>()
                .Property(e => e.trangThai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TinTuc>()
                .HasMany(e => e.BinhLuanTinTucs)
                .WithRequired(e => e.TinTuc)
                .WillCascadeOnDelete(false);
        }
    }
}
