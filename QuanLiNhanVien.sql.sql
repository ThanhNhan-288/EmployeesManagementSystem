USE [QuanLyNhanSu]
GO
/****** Object:  Table [dbo].[BoPhan]    Script Date: 5/14/2025 1:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BoPhan](
	[MaBoPhan] [int] IDENTITY(1,1) NOT NULL,
	[TenBoPhan] [nvarchar](100) NOT NULL,
	[MaPhongBan] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaBoPhan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChucVu]    Script Date: 5/14/2025 1:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChucVu](
	[MaChucVu] [int] IDENTITY(1,1) NOT NULL,
	[TenChucVu] [nvarchar](100) NOT NULL,
	[MaBoPhan] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaChucVu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LuongNhanVien]    Script Date: 5/14/2025 1:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LuongNhanVien](
	[MaLuong] [int] IDENTITY(1,1) NOT NULL,
	[MaNhanVien] [varchar](10) NULL,
	[ThangNam] [date] NULL,
	[LuongCoBan] [decimal](18, 2) NULL,
	[PhuCap] [decimal](18, 2) NULL,
	[Thuong] [decimal](18, 2) NULL,
	[KhauTru] [decimal](18, 2) NULL,
	[LuongThucNhan]  AS ((([LuongCoBan]+[PhuCap])+[Thuong])-[KhauTru]),
PRIMARY KEY CLUSTERED 
(
	[MaLuong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 5/14/2025 1:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNhanVien] [varchar](10) NOT NULL,
	[HoTen] [nvarchar](150) NOT NULL,
	[GioiTinh] [nvarchar](10) NOT NULL,
	[NgaySinh] [date] NOT NULL,
	[DiaChi] [nvarchar](255) NULL,
	[CCCD] [varchar](20) NOT NULL,
	[SoDienThoai] [varchar](15) NULL,
	[MaPhongBan] [int] NULL,
	[MaBoPhan] [int] NULL,
	[MaChucVu] [int] NULL,
	[HinhAnh] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhongBan]    Script Date: 5/14/2025 1:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhongBan](
	[MaPhongBan] [int] IDENTITY(1,1) NOT NULL,
	[TenPhongBan] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPhongBan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 5/14/2025 1:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[MaTaiKhoan] [int] IDENTITY(1,1) NOT NULL,
	[TenDangNhap] [nvarchar](100) NOT NULL,
	[TenHienThi] [nvarchar](100) NOT NULL,
	[MatKhau] [nvarchar](1000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaTaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TenDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BoPhan]  WITH CHECK ADD FOREIGN KEY([MaPhongBan])
REFERENCES [dbo].[PhongBan] ([MaPhongBan])
GO
ALTER TABLE [dbo].[ChucVu]  WITH CHECK ADD FOREIGN KEY([MaBoPhan])
REFERENCES [dbo].[BoPhan] ([MaBoPhan])
GO
ALTER TABLE [dbo].[LuongNhanVien]  WITH CHECK ADD FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_BoPhan] FOREIGN KEY([MaBoPhan])
REFERENCES [dbo].[BoPhan] ([MaBoPhan])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_BoPhan]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_ChucVu] FOREIGN KEY([MaChucVu])
REFERENCES [dbo].[ChucVu] ([MaChucVu])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_ChucVu]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_PhongBan] FOREIGN KEY([MaPhongBan])
REFERENCES [dbo].[PhongBan] ([MaPhongBan])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_PhongBan]
GO
/****** Object:  StoredProcedure [dbo].[sp_DangKy]    Script Date: 5/14/2025 1:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_DangKy]
    @TenDangNhap NVARCHAR(100),
    @MatKhau NVARCHAR(1000),
    @TenHienThi NVARCHAR(100)
AS
BEGIN
    IF EXISTS (SELECT * FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap)
    BEGIN
        RAISERROR(N'Tên đăng nhập đã tồn tại.', 16, 1);
        RETURN;
    END

    INSERT INTO TaiKhoan (TenDangNhap, MatKhau, TenHienThi)
    VALUES (@TenDangNhap, @MatKhau, @TenHienThi);
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DangNhap]    Script Date: 5/14/2025 1:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ĐĂNG NHẬP TÀI KHOẢN
CREATE PROCEDURE [dbo].[sp_DangNhap]
    @TenDangNhap NVARCHAR(100),
    @MatKhau NVARCHAR(1000)
AS
BEGIN
    SELECT * FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau
END

GO
/****** Object:  StoredProcedure [dbo].[sp_LayDanhSachNhanVienVaLuong]    Script Date: 5/14/2025 1:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_LayDanhSachNhanVienVaLuong]
    @ThangNam DATE
AS
BEGIN
    SELECT 
        nv.MaNhanVien,
        nv.HoTen,
        nv.GioiTinh,
        cv.TenChucVu,
        ISNULL(SUM(lv.LuongCoBan + lv.PhuCap + lv.Thuong - lv.KhauTru), 0) AS LuongThucNhan
    FROM NhanVien nv
    LEFT JOIN ChucVu cv ON nv.MaChucVu = cv.MaChucVu
    LEFT JOIN LuongNhanVien lv ON nv.MaNhanVien = lv.MaNhanVien AND lv.ThangNam = @ThangNam
    GROUP BY 
        nv.MaNhanVien, 
        nv.HoTen, 
        nv.GioiTinh, 
        cv.TenChucVu
    ORDER BY nv.MaNhanVien;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_LayTatCaNhanVien]    Script Date: 5/14/2025 1:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_LayTatCaNhanVien]
AS
BEGIN
    SELECT nv.MaNhanVien, nv.HoTen, nv.GioiTinh, nv.NgaySinh, nv.DiaChi, nv.CCCD, nv.SoDienThoai, 
           pb.TenPhongBan, bp.TenBoPhan, cv.TenChucVu,
           nv.MaPhongBan, nv.MaBoPhan, nv.MaChucVu,nv.HinhAnh
    FROM NhanVien nv
    LEFT JOIN PhongBan pb ON nv.MaPhongBan = pb.MaPhongBan
    LEFT JOIN BoPhan bp ON nv.MaBoPhan = bp.MaBoPhan
    LEFT JOIN ChucVu cv ON nv.MaChucVu = cv.MaChucVu
    ORDER BY nv.MaNhanVien;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_LayTatCaPhongBan]    Script Date: 5/14/2025 1:43:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_LayTatCaPhongBan]
AS
BEGIN
    SELECT MaPhongBan, TenPhongBan FROM PhongBan;
END
GO
