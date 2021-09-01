use master
go
if(exists(select *from sys.databases where name='TrueMart'))
	drop database TrueMart
go
create database TrueMart
go
use TrueMart
go
Create table [SanPham]
(
	[MaSP] Integer identity(1,1) NOT NULL,
	[MaTH] Integer NOT NULL,
	[MaDM] Bigint NOT NULL,
	[TenSP] Nvarchar(250) NULL,
	[MetaTitle] Varchar(250) NULL,
	[MoTa] Nvarchar(250) NULL,
	[SoLuong] Integer NULL,
	[DonGia] Decimal(18,0) NULL,
	[BaoHanh] Integer NULL,
	[ChiTiet] Ntext NULL,
	[HinhAnh] Varchar(250) NULL,
	[Ncc] Nvarchar(100) NULL,
	[CreatedDate] Datetime NULL,
	[CreatedBy] Varchar(50) NULL,
	[ModifiedDate] Datetime NULL,
	[ModifiedBy] Varchar(50) NULL,
	[MaKM] Integer NOT NULL,
Primary Key ([MaSP])
) 
go

Create table [KhachHang]
(
	[ID] Integer identity(1,1) NOT NULL,
	[HoTen] Nvarchar(250) NULL,
	[Email] Nvarchar(250) NULL,
	[DiaChi] Nvarchar(250) NULL,
	[SoDT] Char(10) NULL,
	[TenDangNhap] Varchar(250) NULL,
	[MatKhau] Varchar(250) NULL,
	[Avatar] Varchar(250) NULL,
	[CreatedDate] Datetime NULL,
	[CreatedBy] Varchar(50) NULL,
	[ModifiedDate] Datetime NULL,
	[ModifiedBy] Varchar(50) NULL,
Primary Key ([ID])
) 
go

Create table [DanhMuc]
(
	[MaDM] Bigint identity(1,1) NOT NULL,
	[TenDM] Nvarchar(250) NULL,
	[MetaTitle] Varchar(250) NULL,
	[ParentID] Bigint NULL,
	[DisplayOrder] Integer NULL,
	[CreatedDate] Datetime NULL,
	[CreatedBy] Varchar(50) NULL,
	[ModifiedDate] Datetime NULL,
	[ModifiedBy] Varchar(50) NULL,
Primary Key ([MaDM])
) 
go
Create table [BaiViet]
(
	[MaBV] Integer identity(1,1) NOT NULL,
	[TieuDe] Nvarchar(250) NULL,
	[NoiDung] Ntext NULL,
	[HinhAnh] Varchar(250) NULL,
	[CreatedDate] Datetime NULL,
	[CreatedBy] Varchar(50) NULL,
	[ModifiedDate] Datetime NULL,
	[ModifiedBy] Varchar(50) NULL,
Primary Key ([MaBV])
) 
go

Create table [ThuongHieu]
(
	[MaTH] Integer identity(1,1) NOT NULL,
	[TenTH] Nvarchar(250) NULL,
	[CreatedDate] Datetime NULL,
	[CreatedBy] Varchar(50) NULL,
	[ModifiedDate] Datetime NULL,
	[ModifiedBy] Varchar(50) NULL,
Primary Key ([MaTH])
) 
go

Create table [HoaDon]
(
	[MaHD] Integer identity(1,1) NOT NULL,
	[TrangThai] nvarchar(250),
	[CreatedDate] Datetime NULL,
	[CreatedBy] Varchar(50) NULL,
	[ModifiedDate] Datetime NULL,
	[ModifiedBy] Varchar(50) NULL,
	[ID] Integer NOT NULL,
Primary Key ([MaHD])
) 
go

Create table [KhuyenMai]
(
	[MaKM] Integer identity(1,1) NOT NULL,
	[TenKM] Nvarchar(100) NULL,
	[NgayBatDau] Datetime NULL,
	[NgayKetThuc] Datetime NULL,
	[Discount] Float NULL,
	[CreatedDate] Datetime NULL,
	[CreatedBy] Varchar(50) NULL,
	[ModifiedDate] Datetime NULL,
	[ModifiedBy] Varchar(50) NULL,
Primary Key ([MaKM])
) 
go

Create table [ChiTietHoaDon]
(
	[MaHD] Integer NOT NULL,
	[MaSP] Integer NOT NULL,
	[SoLuong] Integer NULL,
	[DonGia] Decimal(18,0) NULL,
Primary Key ([MaHD],[MaSP])
) 
go

Create table [QuanTri]
(
	[ID] Integer identity(1,1) NOT NULL,
	[HoTen] Nvarchar(250) NOT NULL,
	[DiaChi] Nvarchar(250) NULL,
	[SoDT] Char(10) NULL,
	[UserName] Varchar(250) NULL,
	[Password] Varchar(250) NULL,
	[Avatar] Varchar(250) NULL,
	[ChucVu] Integer NULL,
Primary Key ([ID])
) 
go


Alter table [ChiTietHoaDon] add  foreign key([MaSP]) references [SanPham] ([MaSP])  on update no action on delete no action 
go
Alter table [HoaDon] add  foreign key([ID]) references [KhachHang] ([ID])  on update no action on delete no action 
go
Alter table [SanPham] add  foreign key([MaDM]) references [DanhMuc] ([MaDM])  on update no action on delete no action 
go
Alter table [SanPham] add  foreign key([MaTH]) references [ThuongHieu] ([MaTH])  on update no action on delete no action 
go
Alter table [ChiTietHoaDon] add  foreign key([MaHD]) references [HoaDon] ([MaHD])  on update no action on delete no action 
go
Alter table [SanPham] add  foreign key([MaKM]) references [KhuyenMai] ([MaKM])  on update no action on delete no action 
go

insert into QuanTri values(N'Lãnh Thị Hằng', N'Bắc Giang','0329382328','hang','admin','hang.png',1 )
insert into QuanTri values(N'Đào Thị Hoài', N'Thái Bình','0363668356','hoai','admin','hoai.png',1 )
insert into QuanTri values(N'Nguyễn Hữu Hoàng', N'Hà Tĩnh','0385219270','hoang','admin','hoang.png',2 )
insert into QuanTri values(N'Vũ Văn Hôm', N'Bắc Giang','0388151485','hom','admin','hom.png',2 )

insert into DanhMuc values(N'LÀM ĐẸP - SỨC KHỎE','lam-dep-suc-khoe',-1,1,'2021/1/1','hang','','')
insert into DanhMuc values(N'THỰC PHẨM CHỨC NĂNG','thuc-pham-chuc-nang',-1,1,'2021/1/1','hoai','','')
insert into DanhMuc values(N'MỸ PHẨM','my-pham',-1,1,'2021/1/2','hang','','')
insert into DanhMuc values(N'NƯỚC HOA','nuoc-hoa',-1,1,'2021/2/1','hoang','','')
insert into DanhMuc values(N'MẸ - BÉ','me-be',-1,1,'2021/1/3','hoai','','')
insert into DanhMuc values(N'THỜI TRANG','thoi-trang',-1,1,'2021/1/5','hang','','')

insert into DanhMuc values(N'Thiết bị làm đẹp','thiet-bi-lam-dep',1,1,'2021/1/1','hang','','')
insert into DanhMuc values(N'Sản phẩm thiên nhiên','lam-dep-suc-khoe',1,1,'2021/1/1','hang','','')
insert into DanhMuc values(N'Thiết bị chăm sóc sức khỏe','thiet-bi-cham-soc-suc-khoe',1,1,'2021/1/1','hang','','')
insert into DanhMuc values(N'Chăm sóc cơ thể','cham-soc-co-the',1,1,'2021/1/1','hang','','')
insert into DanhMuc values(N'Đồ trang điểm','do-trang-diem',1,1,'2021/1/1','hang','','')
insert into DanhMuc values(N'Chăm sóc da','cham-soc-da',1,1,'2021/1/1','hang','','')
insert into DanhMuc values(N'Chăm sóc tóc và da đầu','cham-soc-toc-va-da-dau',1,1,'2021/1/1','hang','','')
insert into DanhMuc values(N'Tinh dầu spa','tinh-dau-spa',1,1,'2021/1/1','hang','','')
insert into DanhMuc values(N'Bộ sản phẩm làm đẹp','bo-san-pham-lam-dep',1,1,'2021/1/1','hang','','')
insert into DanhMuc values(N'Thực phẩm chức năng tăng cân','thuc-pham-chuc-nang-tang-can',2,1,'2021/1/2','hoai','','')
insert into DanhMuc values(N'Chăm sóc sức khỏe','cham-soc-suc-khoe',2,1,'2021/1/2','hoai','','')
insert into DanhMuc values(N'Thực phẩm chức năng làm đẹp','thuc-pham-chuc-nang-lam-dep',2,1,'2021/1/2','hoai','','')
insert into DanhMuc values(N'Thực phẩm từ thiên nhiên','thuc-pham-tu-thien-nhien',2,1,'2021/1/2','hoai','','')
insert into DanhMuc values(N'Thực phẩm chức năng Nhật Bản','thuc-pham-chuc-nang-nhat-ban',2,1,'2021/1/2','hoai','','')
insert into DanhMuc values(N'Thực phẩm chức năng giảm cân','thuc-pham-chuc-nang-giam-can',2,1,'2021/1/2','hoai','','')
insert into DanhMuc values(N'Bộ mỹ phẩm','bo-my-pham',3,1,'2021/1/3','hoang','','')
insert into DanhMuc values(N'Dưỡng trắng da','duong-trang-da',3,1,'2021/1/3','hoang','','')
insert into DanhMuc values(N'Tẩy trang, tẩy tế bào chết','tay-trang-tay-te-bao-chet',3,1,'2021/1/3','hoang','','')
insert into DanhMuc values(N'Chống lão hóa da','chong-lao-hoa-da',3,1,'2021/1/3','hoang','','')
insert into DanhMuc values(N'Mỹ phẩm nam','my-pham-nam',3,1,'2021/1/3','hoang','','')
insert into DanhMuc values(N'Dưỡng ẩm da','duong-am-da',3,1,'2021/1/3','hoang','','')
insert into DanhMuc values(N'Nước hoa nữ','nuoc-hoa-nu',4,1,'2021/1/4','hoai','','')
insert into DanhMuc values(N'Nước hoa nam','nuoc-hoa-nam',4,1,'2021/1/4','hoai','','')
insert into DanhMuc values(N'Nước hoa Unisex','nuoc-hoa-unisex',4,1,'2021/1/4','hoai','','')
insert into DanhMuc values(N'Nước hoa mini','nuoc-hoa-mini',4,1,'2021/1/4','hoai','','')
insert into DanhMuc values(N'Nước hoa cho bé','nuoc-hoa-cho-be',4,1,'2021/1/4','hoai','','')
insert into DanhMuc values(N'Bộ quà tặng nước hoa','bo-qua-tang-nuoc-hoa',4,1,'2021/1/4','hoai','','')
insert into DanhMuc values(N'Dụng cụ học tập','dung-cu-hoc-tap',5,1,'2021/1/4','hang','','')
insert into DanhMuc values(N'Bình sữa - Đồ dùng ăn uống','binh-sua-do-dung-an-uong',5,1,'2021/1/4','hang','','')
insert into DanhMuc values(N'DỤNG CỤ AN TOÀN','dung-cu-an-toan',5,1,'2021/1/4','hang','','')
insert into DanhMuc values(N'VỆ SINH & SỨC KHỎE','ve-sinh-suc-khoe',5,1,'2021/1/4','hang','','')
insert into DanhMuc values(N'ĐỒ CHƠI TRẺ EM','do-choi-tre-em',5,1,'2021/1/4','hang','','')
insert into DanhMuc values(N'Tắm, Dưỡng da, Vệ sinh','tam-duong-da-ve-sinh',5,1,'2021/1/4','hang','','')
insert into DanhMuc values(N'Thời trang nam','thoi-trang-nam',6,1,'2021/1/4','hoang','','')
insert into DanhMuc values(N'Thời trang nữ','thoi-trang-nu',6,1,'2021/1/4','hoang','','')
insert into DanhMuc values(N'Vali & Balo','vali-balo',6,1,'2021/1/4','hoang','','')

insert into KhachHang values(N'Trần Thủ Độ',N'do@gmail.com',N'Bắc Kạn','0392049023','user1','user1','user1.png','2021/2/2','hang','','')
insert into KhachHang values(N'Nguyễn Văn Anh',N'anh@gmail.com',N'Bắc Ninh','0398799023','user2','user2','user2.png','2021/2/2','hang','','')
insert into KhachHang values(N'Lê Thị Bé',N'be@gmail.com',N'Bắc Giang','0365449023','user2','user2','user2.png','2021/2/2','hang','','')
insert into KhachHang values(N'Tấn Văn Tạ',N'ta@gmail.com',N'Hà Nội','0392025423','user3','user3','user3.png','2021/2/2','hang','','')
insert into KhachHang values(N'Tạ Thị Lạng',N'lang@gmail.com',N'Hà Nam','0354279023','user4','user4','user4.png','2021/2/2','hang','','')
insert into KhachHang values(N'Hoài Thị Đào',N'dao@gmail.com',N'Hà Đông','0388849023','user5','user5','user5.png','2021/2/2','hang','','')
insert into KhachHang values(N'Lãnh Thu Hằng',N'hang@gmail.com',N'Quảng Trị','0392777023','user6','user6','user6.png','2021/2/2','hang','','')
insert into KhachHang values(N'Nguyễn Thu Hoàng',N'hoang@gmail.com',N'Quảng Nam','0366649023','user7','user7','user7.png','2021/2/2','hang','','')
insert into KhachHang values(N'Trần Đình Trọng',N'trong@gmail.com',N'Quảng Ninh','0392555023','user8','user8','user8.png','2021/2/2','hang','','')
insert into KhachHang values(N'Vũ Minh Anh',N'anh@gmail.com',N'Quảng Bình','0392049444','user9','user9','user9.png','2021/2/2','hang','','')


insert into ThuongHieu values(N'Neulii Cosmetic','2021/1/1','hoai','','')
insert into ThuongHieu values(N'Oximeter','2021/1/1','hoai','','')
insert into ThuongHieu values(N'NGHỆ 365','2021/1/1','hoai','','')
insert into ThuongHieu values(N'From Your Skin','2021/1/1','hoai','','')
insert into ThuongHieu values(N'SLINE','2021/1/1','hoai','','')
insert into ThuongHieu values(N'DTOXI','2021/1/1','hoai','','')
insert into ThuongHieu values(N'SkinShot','2021/1/1','hoai','','')
insert into ThuongHieu values(N'Mosclean','2021/1/1','hoai','','')
insert into ThuongHieu values(N'CUBEME','2021/1/1','hoai','','')
insert into ThuongHieu values(N'Hera Plus','2021/1/1','hoai','','')
insert into ThuongHieu values(N'HanaYuki','2021/1/1','hoai','','')
insert into ThuongHieu values(N'Crystal Slim','2021/1/1','hoai','','')
insert into ThuongHieu values(N'OEM','2021/1/1','hoai','','')
insert into ThuongHieu values(N'Banobagi','2021/1/1','hoai','','')
insert into ThuongHieu values(N'Beucup','2021/1/1','hoai','','')
insert into ThuongHieu values(N'Jumper','2021/1/1','hoai','','')
insert into ThuongHieu values(N'Daedong Korea Ginseng','2021/1/1','hoai','','')
insert into ThuongHieu values(N'SereneLife','2021/1/1','hoai','','')
insert into ThuongHieu values(N'Homedics','2021/1/1','hoai','','')
insert into ThuongHieu values(N'HUXLEY','2021/1/1','hoai','','')
insert into ThuongHieu values(N'Ziaja','2021/1/1','hoai','','')
insert into ThuongHieu values(N'Tesori','2021/1/1','hoai','','')
insert into ThuongHieu values(N'Ogx','2021/1/1','hoai','','')
insert into ThuongHieu values(N'82x','2021/1/1','hoai','','')
insert into ThuongHieu values(N'Nature Republic','2021/1/1','hoai','','')

insert into BaiViet values(N'BỘ 3 “PHÉP THUẬT” RETINOL - ARBUTIN - HYDROLYZED',N'Khi bắt tay vào công cuộc chống lão hóa cho da, chị em rất dễ rơi vào cảnh “vung tay" quá đà khiến liệu trình chăm da của bản thân trở nên phức tạp rối ren mà nhiều khi lại không có kết quả như mong muốn.','baiviet1.png','2021/2/2','hoang','','')
insert into BaiViet values(N'HYDROLYZED COLLAGEN - NGUỒN DINH DƯỠNG TUYỆT VỜI',N'Collagen là protein dồi dào nhất trong cơ thể mỗi người (trong đó có 25% tổng lượng protein trong cơ thể và','baiviet2.png','2021/2/2','hoang','','')
insert into BaiViet values(N'NGOÀI VITAMIN C, ĐÂY LÀ THÀNH PHẦN ĐƯỢC VÍ NHƯ',N'Vitamin C từ lâu đã trở thành cái tên quá đỗi quen thuộc đối với làn da không đều màu, thâm nám; nhưng','baiviet3.png','2021/2/2','hoang','','')
insert into BaiViet values(N'VỚI RETINOL, DÙ LÀ NÁM, MỤN TRỨNG CÁ HAY NẾP NHĂN,',N'Retinol chính là một dẫn xuất vitamin A (thuộc nhóm retinoid). Retinol hoạt động bằng cách trung hòa những gốc','baiviet4.png','2021/2/2','hoang','','')
insert into BaiViet values(N'FROM YOUR SKIN RA MẮT KEM “TRỊ NÁM – SÁNG DA – CĂNG',N'Sau một thời gian nghiên cứu, ấp ủ, From Your Skin chính thức ra mắt khách hàng sản phẩm Kem trị nám, tàn	','baiviet5.png','2021/2/2','hoang','','')
insert into BaiViet values(N'DÀNH CẢ THANH XUÂN CHO TRỊ NÁM MÀ CHƯA BIẾT SUPER',N'Tại sao tôi trị nám da mặt bao nhiêu lâu vẫn không cải thiện? Tại sao tôi dùng nhiều sản phẩm đắt tiền, đi spa','baiviet6.png','2021/2/2','hoang','','')
insert into BaiViet values(N'SUPER MELA CREAM: XỨNG DANH “CAO THỦ TRỊ NÁM" KHI',N'Super Mela Cream: Xứng danh “cao thủ trị nám" khi sở hữu bảng thành phần lý tưởng đối với làn da lão','baiviet7.png','2021/2/2','hoang','','')
insert into BaiViet values(N'GỢI Ý 6 SET QUÀ TẶNG MỸ PHẨM, XINH XẮN, Ý NGHĨA, TIẾT',N'"Phụ nữ cũng giống như một cuốn sách, nếu bìa không đẹp thì chẳng ai buồn lật xem nội dung bên trong".','baiviet8.png','2021/2/2','hoang','','')
insert into BaiViet values(N'BIO TÂM AN – CẢI THIỆN TÌNH TRẠNG MẤT NGỦ',N'Giấc ngủ đóng vai trò quan trọng trong đời sống của mỗi người, giúp bạn nghỉ ngơi sau những thời gian làm','baiviet9.png','2021/2/2','hoang','','')
insert into BaiViet values(N'5 QUY TẮC ĂN DẶM – BÍ KÍP “VÀNG” TRONG LÀNG CHĂM',N'Bé đến độ tuổi ăn dặm rồi, liệu mẹ có biết 5 quy tắc ăn dặm này? Hãy cùng tìm hiểu ngay những nguyên tắc ăn','baiviet10.png','2021/2/2','hoang','','')

insert into KhuyenMai values(N'KM0','2021/8/2','2021/8/5',0,'2021/8/1','hoai','','')
insert into KhuyenMai values(N'KM1','2021/8/2','2021/8/5',0.3,'2021/8/1','hoai','','')
insert into KhuyenMai values(N'KM2','2021/8/4','2021/8/5',0.2,'2021/8/1','hoai','','')
insert into KhuyenMai values(N'KM3','2021/8/2','2021/8/4',0.1,'2021/8/1','hoai','','')
insert into KhuyenMai values(N'KM8','2021/8/10','2021/8/12',0.05,'2021/8/1','hoai','','')
insert into KhuyenMai values(N'KM19','2021/8/2','2021/8/6',0.15,'2021/8/1','hoai','','')

insert into SanPham values(1,12,N'TẨY DA CHẾT DẠNG BÔNG NEULII PEELING PAD','tay-da-chet-dang-bong-neulii-peeling-pad',N'TẨY DA CHẾT DẠNG BÔNG NEULII PEELING PAD',500,606000,7,N'Neulii BHA PHA 5.5 peel pad là sản phẩm tẩy da chết kết hợp với toner dạng miếng. Đây là thiết kế tiện dụng giúp quá trình skincare đơn giản và dễ dàng hơn. Những miếng đệm này được cho là giúp kiểm soát bã nhờn và tẩy tế bào chết nhẹ nhàng cho da. Chỉ cần lau sạch bằng miếng lột BHA PHA 5.5 để tẩy tế bào chết nhẹ nhàng và loại bỏ các tạp chất trên da, đồng thời dưỡng ẩm cho làn da mịn màng và mềm mại. Đặc biệt miếng pad này hỗ trợ rất tốt trong quá trình điều trị mụn (mụn đầu đen, mụn cám, mụn ẩn), làm mịn, dưỡng sáng da phù hợp với mọi loại da đặc biệt là da dầu','sp1.jpg',N'','2021/2/5','hoang','','',1)
insert into SanPham values(1,12,N'SỮA RỬA MẶT TẠO BỌT AC CLEAN SAVER','sua-rua-mat-tao-bot-ac-clean-saver',N'SỮA RỬA MẶT TẠO BỌT',550,336000,12,N'Sữa rửa mặt tạo bọt lành tính làm dịu làn da bị mụn, làm sạch và mềm mại làn da nhạy cảm. Đây không phải là sản phẩm y dược để phòng ngừa và điều trị bệnh.','sp2.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(1,12,N'NƯỚC HOA HỒNG AC CLEAN SAVER TONER','nuoc-hoa-hong-ac-clean-saver-toner',N'Nước hoa hồng Neulii AC Clean Saver Toner',550,580000,12,N'một loại huyết thanh hoạt động nhanh có chức năng giống như một loại tinh chất để kiểm soát nhờn và giúp làm sáng da hiệu quả. Mặc dù có độ sệt giống như gel, nhưng thực sự thì toner có cảm giác rất nhẹ khi thoa. Nó nhanh chóng hấp thụ vào da và để lại một kết thúc tươi mát, không bết dính.','sp3.jpg',N'Cosmetic','2021/2/3','hoang','','',1)
insert into SanPham values(1,12,N'TINH CHẤT NEULLI AC CLEAN SAVER SERUM','tinh-chat-neulii-ac-clean-saver-serum',N'Serum',550,624000,12,N'Dành cho làn da vô cùng quý giá của bạn. nhưng bị xỉn màu và thiếu sức sống Giải pháp của chúng tôi, Bước thứ 2 là #serum. Bắt đầu từ bước Skin care Cơ bản cho làn da mệt mỏi','sp4.jpg',N'Cosmetic','2021/2/3','hoang','','',1)
insert into SanPham values(1,12,N'KEM DƯỠNG DA TRỊ MỤN AC CLEAN SAVER CREAM','kem-duong-da-tri-mun-ac-clean-saver-cream',N'KEM DƯỠNG DA TRỊ MỤN',220,586000,12,N'Dành cho làn da vô cùng quý giá của bạn. nhưng bị xỉn màu và thiếu sức sống Giải pháp của chúng tôi, Bước thứ 3 là #Scream','sp5.jpg',N'Cosmetic','2021/2/3','hoang','','',1)
insert into SanPham values(2,9,N'MÁY ĐO NỒNG ĐỘ OXY TRONG MÁU SPO2 CONTEC CMS50D2','may-do-nong-do-oxy-trong-mau-spo2-contec-cms50d2',N'MÁY ĐO NỒNG ĐỘ OXY',210,1060000,12,N'Máy Đo Nồng Độ Oxy Trong Máu SPO2 Contec CMS50D2 là một thiết bị không xâm lấn dùng để đo nồng độ oxy trong máu và nhịp tim của bệnh nhân người lớn và trẻ em trong môi trường gia đình và bệnh viện trong những trường hợp cấp cứu, cần sử dụng máy giúp thở hoặc trợ thở bằng nguồn oxy bên ngoài.','sp6.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(3,10,N'THẠCH NGHỆ COLLAGEN NANO CURCUMIN JELLY 365 VỊ XOÀI HÀN QUỐC','thach-nghe-collagen-nano-curcumin-jelly-365-vi-xoai-han-quoc',N'THẠCH NGHỆ',210,1350000,12,N'Thạch Nghệ Collagen Nano Curcumin Jelly 365 Vị Xoài là tinh chất curcumin dạng lỏng giúp phân giải curcumin, hóa lỏng bằng nước khoáng giúp tăng cường tỷ lệ hấp thụ.','sp7.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(3,1,N'TINH NGHỆ NANO CURCUMIN GOLD GOLDEN GIFT HỘP 100 ỐNG X2ML','tinh-nghe-nano-curcumin-gold-golden-gift-hop-100-ong-x2ml',N'TINH NGHỆ',210,1950000,12,N'Nghệ nano Curcumin Gold nhanh chóng trở thành trào lưu làm đẹp của phụ nữ Hàn Quốc. Tinh chất nghệ tươi Curcumin được điều chế dạng nano hấp thu cực tốt vào cơ thể, hiệu quả nhanh, trong khi đó lại rất an toàn','sp8.jpg',N'Golden Gift','2021/2/3','hoang','','',1)
insert into SanPham values(4,12,N'KEM ĐẶC TRỊ NÁM SUPER MELA CREAM - FROM YOUR SKIN','kem-dac-tri-nam-super-mela-cream',N'KEM ĐẶC TRỊ NÁM',210,880000,12,N'Super Mela Cream được công nhận là kem trị tàn nhang mang lại kết quả mong đợi nhất quả không sai bởi những thành phần chính như: Retinol- Hydrolyzed Collagen- Arbutin. Những thành phần này không chỉ giúp dưỡng da từ bên trong, làm mờ thâm nám nhanh chóng mà còn vô cùng an toàn, không gây kích ứng da. Giúp các chị em hoàn toàn yên tâm sử dụng mà không lo ảnh hưởng tới sức khỏe và sinh hoạt hàng ngày.','sp9.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(5,15,N'ĐAI SIẾT EO ĐỊNH HÌNH CAO CẤP S SHAPE- SLINE','dai-sieu-eo-dinh-hinh-cao-cap-s-shape-sline',N'ĐAI SIẾT EO',200,450000,2,N'Đai Siết Eo Định Hình Cao Cấp S Shape - Sline với chất liệu là vải cotton 4D co dãn, thoải mái nhẹ nhàng 24/24, thấm hút mồ hôi ko nóng ko ngứa, giảm mỡ nhanh chóng, định hình vòng eo, tạo đường cong quyến rũ!','sp10.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(19,7,N'MÁY TRẺ HÓA DA, TĂNG CƯỜNG COLLAGEN BẰNG ÁNH SÁNG HOMEDICS 7LS BEAUTY FA7 - 1450J','may-tre-hoa-da-tang-cuong-collagen-bang-anh-sang-homedics-7ls-beauty-fa7-1450j',N'MÁY TRẺ HÓA DA',120,6600000,12,N'Giảm vi khuẩn gây mụn, Tăng cường collagen, Giúp khôi phục độ đàn hồi của da, Giúp cân bằng sản xuất dầu, Giảm thiểu sự xuất hiện của đường nhăn và nếp nhăn, Làm đều màu da và tăng kết cấu da, Phù hợp với mọi loại da và thích hợp cho mọi vùng trên cơ thể, không mất thời gian nghỉ dưỡng','sp11.jpg',N'HoMedics','2021/2/3','hoang','','',3)
insert into SanPham values(19,7,N'MÁY TẨY DA CHẾT GÓT CHÂN RIO PEDI4','may-tay-da-chet-got-chan-rio-pedi4',N'MÁY TẨY DA CHẾT',123,1190000,12,N'Máy có thiết kế nhỏ gọn với hai đầu mài đi kèm dành cho hai vùng da thường và vùng chai sần nhiều. Kích thước nhỏ gọn giúp bạn có thể dễ dàng mang máy theo trong các chuyến du lịch, dã ngoại, công tác,... ','sp12.jpg',N'Rio Beauty','2021/2/3','hoang','','',4)
insert into SanPham values(5,8,N'TINH DẦU TAN MỠ SLIMMING BODY 2','tinh-dau-tan-mo-slimming-body-2',N'TINH DẦU',115,450000,4,N'Mỡ thừa ở bụng, đùi, bắp tay,... là những vấn đề rất khó chịu đối với tất cả mọi người, không chỉ các chị em phụ nữ mặc cảm về tình trạng béo bụng mà các đấng mày râu cũng rất tự ti về vấn đề này. Sản phẩm Bộ ủ nóng tan mỡ Slimming Body 2 giúp các quý bà tự tin, quý ông giảm mỡ bụng bia an toàn hiệu quả.','sp13.png',N'Slimming Care','2021/2/3','hoang','','',1)
insert into SanPham values(6,8,N'BỘT ĐÁNH RĂNG BRING TOOTH POWDER','bot-danh-rang-bring-tooth-powder',N'BỘT ĐÁNH RĂNG',122,418000,3,N'Bột đánh răng Bring Tooth Powder với khả năng hòa tan trong nước giúp chăm sóc từng kẽ răng, loại bỏ vi khuẩn ngăn ngừa mùi hôi triệt để. Với các thành phần tự nhiên đặc biệt, ngay cả khi không chứa Florua vẫn ngăn ngừa sâu răng cách hiệu quả.','sp14.jpg',N'','2021/2/3','hoang','','',2)
insert into SanPham values(6,9,N'CÂN NHÀ BẾP SALTER 1036UJBKDR','can-nha-bep-salter-1036ujbkdr',N'CÂN NHÀ BẾP',111,440000,1,N'Cân nhà bếp Salter 1036UJBKDR được nhập khẩu chính hãng từ Anh rất được tin dùng tại thị trường Việt Nam.','sp15.jpg',N'Homedics','2021/2/3','hoang','','',1)
insert into SanPham values(7,10,N'NƯỚC ÉP CẦN TÂY VỊ LÁ NẾP GREEN BEAUTY','nuoc-ep-can-tay-vi-la-nep-green-beauty',N'NƯỚC ÉP',130,360000,2,N'Nước ép cần tây vị lá nếp giúp giảm mỡ nội tạng, đẹp da, điều hoà nội tiết tố nữ, tăng sức đề kháng, thải độc tố trong cơ thể, hỗ trợ cải thiện bệnh đại tràng, tiểu đường, điều hoà huyết áp.','sp16.jpg',N'GREEN BEAUTY','2021/2/3','hoang','','',1)
insert into SanPham values(7,10,N'VIÊN CẤP NƯỚC WATERBANK CUBE','vien-cap-nuoc-waterbank-cube',N'VIÊN CẤP NƯỚC',110,1220000,12,N'Waterbank Cube chứa hàm lượng lớn là hoạt chất Hyaluronic acid, một cái tên mà đang được các tín đồ làm đẹp tìm kiếm và sử dụng. Thành phần này giúp tăng khả năng giữ nước cho da gấp 1000 lần, giúp da giữ được lượng ẩm tuyệt vời, làm mờ vết nhăn một cách thần kì.','sp17.jpg',N'WATERBANK CUBE','2021/2/3','hoang','','',1)
insert into SanPham values(8,11,N'KEM DƯỠNG MÔI XOUL','kem-duong-moi-xoul',N'KEM DƯỠNG MÔI',112,400000,2,N'Son dưỡng môi xoul là một loại kem dưỡng môi không hề gây bết dính, đem lại cảm giác mềm mịn, tự nhiên sau khi sử dụng, Hương thơm đến từ nguồn nguyên liệu tự nhiên không gây khó chịu cho dù là người nhạy cảm nhất , Chứa dầu chiết xuất lá bạc hà giúp mang đến cảm giác mát lạnh tự nhiên, mang lại một đôi môi tươi mới với hiệu quả lâu dài ','sp18.jpg',N'XOUL','2021/2/3','hoang','','',1)
insert into SanPham values(8,11,N'3 MÀU SON DƯỠNG MAGIC LIPS','3-mau-son-duong-magic-lips',N'Son',68,259000,1,N'Dưỡng mềm môi, giữ ẩm lâu, 3 tone màu tự nhiên phù hợp với mọi làn da, Tùy theo độ PH của mỗi người sẽ tạo ra màu son riêng, Giúp môi ửng màu nhẹ nhàng, ẩm mướt căng mọng, Hương thơm nhẹ nhàng dễ chịu, Xuất xứ sản phẩm: Hàn Quốc (Made in Korea), *Sản phẩm được cấp giấy phép chứng nhận tiêu chuẩn chất lượng ở 2 quốc gia Hàn Quốc – Việt Nam.','sp19.jpg',N'Moi Cosmetic','2021/2/3','hoang','','',4)
insert into SanPham values(9,13,N'DẦU HẤP BÙN WEILAIYA - MINI 50ML','dau-hap-bun-weilaiya-mini-50ml',N'DẦU HẤP BÙN',99,80000,1,N'Dầu hấp bùn Weilaiya là bùn ủ tóc được sử dụng tại các salon tóc chuyên nghiệp, có chứa các yếu tố giữ ẩm tự nhiên, keratin, khoáng chất, vitamin B5 và các yếu tố phục hồi tóc hư tổn. Bùn ủ có khả năng thấm sâu vào trong cấu trúc sợi tóc, giúp giữ tóc luôn khỏe mạnh mỗi khi tẩy tóc, nhuộm cũng như dùng các loại thuốc khóa màu tóc.','sp20.jpg',N'Weilaiya','2021/2/3','hoang','','',1)
insert into SanPham values(9,13,N'CẶP DẦU PHỤC HỒI TÓC WEILAIYA - SET MINI','cap-dau-phuc-hoi-toc-weilaiya-set-mini',N'DẦU PHỤC HỒI TÓC',88,80000,1,N'Cặp Dầu Phục Hồi Tóc Weilaiya là một bộ sản phẩm gồm dầu gội và dầu xả của Weilaiya, giúp phục hồi tóc hư tổn nặng. Sản phẩm có chiết xuất hoàn toàn từ thiên nhiên, an toàn với mọi đối tượng sử dụng, kể cả phụ nữ đang mang thai. Với hương thơm kinh điển độc nhất của nước hoa Armani, cùng hiệu quả chăm sóc tóc vượt trội, sản phẩm là một trong những lựa chọn hàng đầu của các chuyên gia chăm sóc tóc.','sp21.jpg',N'Weilaiya','2021/2/3','hoang','','',1)
insert into SanPham values(10,14,N'SYLIC AMPOULE HCM','sylic-ampoule-hcm',N'Ampoule Sylic',66,855000,2,N'Sợi kim mảnh giúp giảm thiểu tối đa kích ứng lên da., Sử dụng kĩ thuật tiên tiến trong chế tác công cụ laser, Ngoài ra còn giúp tăng độ đàn hồi cho da, Sản phẩm cao cấp được sử dụng trong các bệnh viện thẩm mỹ','sp22.jpg',N'Viet Nam','2021/2/3','hoang','','',1)
insert into SanPham values(10,14,N'NƯỚC DƯỠNG CÂN BẰNG DA MOTHER CARE','nuoc-duong-can-bang-da-mother-care',N'NƯỚC DƯỠNG',113,345000,12,N'Cân bằng lại độ PH của da sau khi rửa mặt, trang điểm, Giảm thiểu các tình trạng gây kích ứng da, Tăng hấp thụ kem dưỡng chất, kem trị mụn và se khít lỗ chân lông','sp23.jpg',N'Mother care','2021/2/3','hoang','','',1)
insert into SanPham values(11,15,N'TỦ LẠNH SÓNG ÂM GENIE 6 LÍT','tu-lanh-song-am-genie-6-lit',N'TỦ LẠNH',22,2250000,12,N'Giúp kháng khuẩn, khử mùi cho mỹ phẩm, triệt tiêu các vi khuẩn làm hỏng mỹ phẩm, Giúp làm lạnh mỹ phẩm, tạo cảm giác mát da khi sử dụng., Xuất xứ: Hàn Quốc, dung tích: 6l','sp24.jpg',N'Genie','2021/2/3','hoang','','',1)
insert into SanPham values(11,16,N'NGŨ CỐC TĂNG CÂN HERA','ngu-coc-tang-can-hera',N'NGŨ CỐC',114,280000,1,N'Cung cấp chất béo, chất xơ cùng các vitamin như B1, B2, B3, B6, Tăng cường trí nhớ, giảm cholesterol trong máu, bổ tâm tỳ, dễ ngủ, ăn ngon miệng, Bổ gan, tim, tiêu nóng, giải độc, giảm thâm nám, cung cấp vitamin C, làm đẹp, sáng, mịn da, trị mụn, chống oxy hóa.','sp25.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(12,16,N'VIÊN THẢO MỘC TĂNG CÂN YUMMY PLUS','vien-thao-moc-tang-can-yummy-plus',N'VIÊN THẢO MỘC',213,495000,1,N'Viên thảo mộc tăng cân Yummy Plus được sản xuất bởi công ty TNHH TM & DV Slimming Care Việt Nam trên dây chuyền hiện đại và được Bộ Y Tế cấp phép lưu hành tại Việt Nam, kích thích ăn ngon và hỗ trợ hấp thu tốt hơn giúp những người khó tăng cân nhất cũng đạt được hiệu quả tốt. Hiện nay, Thảo mộc tăng cân Yummy Plus đã tái xuất với một diện mạo hoàn toàn mới. Thành phần 100% từ thiên nhiên giúp tăng cân hiệu quả thông qua việc ích khí, kiện tỳ, nuôi dưỡng cơ thể.','sp26.png',N'Slimming Care','2021/2/3','hoang','','',5)
insert into SanPham values(12,17,N'CÂN SỨC KHỎE ĐIỆN TỬ LANAFORM PDS-110 AS','can-suc-khoe-dien-tu-lanaform-pds110-as',N'CÂN SỨC KHỎE ĐIỆN TỬ',127,527000,3,N'Cân Sức Khỏe Điện Tử LANAFORM PDS-110 AS trang bị một lớp kính cường lực màu đen, có khả năng chịu lực, chịu va đập hiệu quả. Dòng cân này có tải trọng từ 5kg-180kg, vô cùng hữu ích trong việc cân đo sức khỏe cho cả gia đình, cũng như đáp ứng được việc cân hàng hóa nặng, đóng hàng thùng,...','sp27.jpg',N'Lanaform','2021/2/3','hoang','','',1)
insert into SanPham values(13,17,N'TRÀ LINH CHI DEADONG HÀN QUỐC - 100G','tra-linh-chi-deadong-han-quoc',N'TRÀ LINH CHI',55,395000,1,N'Trà Linh Chi Deadong Hàn Quốc một thức uống phổ biến được kết hợp từ nấm linh chi và các thành phần tạo ngọt, giúp làm giảm vị đắng đặc trưng của linh chi, trở thành món đồ uống thanh nhiệt giải độc không thể thiếu trong mùa hè này.','sp28.jpg',N'Daedong Korea Ginseng','2021/2/3','hoang','','',1)
insert into SanPham values(13,18,N'VB COLLAGEN HÀN QUỐC','vb-collagen-han-quoc',N'COLLAGEN',120,1535000,3,N'ước uống Collagen VB Vital Beautie hộp 30 chai chính hãng Hàn Quốc, giá ưu đãi từ đại lý cấp 1 - Cải thiện nếp nhăn và làm trắng da, trị nám','sp29.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(14,18,N'COLLAGEN INTER TECHNO 25.000MG','collagen-inter-techno-25000mg',N'COLLAGEN',22,1790000,12,N'Collagen Inter Techno 25000mg là sản phẩm bổ sung collagen nước phong phú và cao cấp nhất tại Nhật Bản, cung cấp thành phần Collagen tự nhiên, dễ hấp thụ nhất (collagen tươi lấy trực tiếp từ vảy cá) lên tới 25000mg không chỉ bổ sung collagen thiếu hụt trên da mà còn cung cấp đủ collagen cho xương khớp giúp xương khớp khỏe mạnh.','sp30.jpg',N'','2021/2/3','hoang','','',6)
insert into SanPham values(14,19,N'TINH DẦU VỎ CHANH LEMON ZEST LANAFORM','tinh-dau-vo-chanh-lemon-zest-lanaform',N'TINH DẦU VỎ CHANH',33,260000,2,N'Tinh dầu vỏ chanh Lemon Zest Lanaform là một phương thuốc tự nhiên mang lại rất nhiều lợi ích. Tinh dầu chanh giúp thanh lọc không khí, giúp không khí sạch sẽ, trong lành và chống vi khuẩn. Khi được khuếch tán, mùi hương trái cây dễ chịu lấp đầy không khí sẽ tiếp thêm sinh lực cho bạn. Loại tinh dầu hữu cơ này cũng được sử dụng để điều trị da dầu bằng cách mát xa và là một giải pháp tuyệt vời thanh lọc thải độc.','sp31.png',N'Lanaform','2021/2/3','hoang','','',1)
insert into SanPham values(15,19,N'THANH LỌC MẠCH MÁU STATICONCEPT Q10 EVOLUTION CHUẨN PHÁP','thanh-loc-mach-mau-staticoncept-q10-evolution-chuan-phap',N'THANH LỌC MẠCH MÁU',33,1150000,12,N'Ngoài việc tích cực sử dụng các loại thực phẩm hỗ trợ giúp thanh lọc mạch máu, tăng cường tập thể dục, ăn uống lành mạnh để hạn chế việc "chất bẩn" bám ở các thành mạch máu, chị em nên CHỦ ĐỘNG bổ sung các sản phẩm hỗ trợ giúp detox mạch máu, phòng ngừa đột quỵ hiệu quả và an toàn','sp32.jpg',N'EVOLUTION','2021/2/3','hoang','','',1)
insert into SanPham values(15,20,N'DUNG DỊCH NHỎ MẮT SANCOBA 5ML NHẬT BẢN','dung-dich-nho-mat-sancoba-5ml-nhat-ban',N'DUNG DỊCH NHỎ MẮT',33,1000000,2,N'Thuốc nhỏ mắt Sancoba của Nhật được ưa chuộng tại thị trường Nhật Bản và trên Thế giới. Chai thuốc nhỏ nhắn 5ml chứa hàm lượng Cyanocobalamin 0.02% vừa đủ để hỗ trợ điều tiết cho mắt, giảm mỏi mắt, bồi bổ giác mạc, giúp đôi mắt sáng khỏe.','sp33.jpg',N'Sancoba','2021/2/3','hoang','','',2)
insert into SanPham values(16,20,N'VIÊN UỐNG HỖ TRỢ ĐIỀU TRỊ TIỂU ĐƯỜNG KIKUIMO','vien-thuoc-uong-ho-tro-dieu-tri-tieu-duong-kikuimo',N'HỖ TRỢ ĐIỀU TRỊ',12,1430000,12,N'Kikuimo Seikatsu được chiết xuất 100% từ cây Cúc vu Nhật Bản nên rất an toàn cho sức khỏe, không tác dụng phụ, không biến chứng. ','sp34.jpg',N'KIKUIMO','2021/2/3','hoang','','',1)
insert into SanPham values(16,21,N'CÀ PHÊ GIẢM CÂN FOELLIE CHÍNH HÃNG','ca-phe-giam-can-foellie-chinh-hang',N'CÀ PHÊ GIẢM CÂN',21,280000,1,N'Để có được vóc dáng thon gọn, bên cạnh việc tập luyện và thực hiện dinh dưỡng hợp lý thì sử dụng các thực phẩm chức năng để duy trì cân nặng cũng là lựa chọn được nhiều người tin dùng. Cà phê giảm cân Foellie là một sản phẩm được nhiều người biết đến và sử dụng đông đảo trong liệu trình giảm cân mang lại hiệu quả cao.','sp35.jpg',N'FOELLIE','2021/2/3','hoang','','',1)
insert into SanPham values(17,21,N'THẠCH GIẢM CÂN S-LINE JELLY','thach-giam-can-sline-jelly',N'THẠCH GIẢM CÂN',12,1590000,12,N'Thạch hỗ trợ giảm cân S-Line Jelly- một loại thực phẩm giảm cân như chiếc chìa khoá vàng giúp xoa dịu những âu lo cho phụ nữ, nhất là những người đang cần giảm cân một cách tích cực.','sp36.jpg',N'JELLY','2021/2/3','hoang','','',1)
insert into SanPham values(17,22,N'PHẤN NƯỚC OHUI CHE KHUYẾT ĐIỂM OHUI POWDERY METAL CUSHION SPF 50 PA/+++','phan-nuoc-ohui-che-khuyet-diem-ohui-powdery-metal-cushion-spf-50pa/+++',N'PHẤN NƯỚC',22,1380000,2,N'Phấn Nước Chống Nắng OHUI SPF50 Powdery Metal Cushion chiết xuất chủ yếu từ khoáng chất và dược liệu tự nhiên cùng những công thức làm đẹp được mỹ phẩm OHUI nghiên cứu trong thời gian dài nên sản phẩm rất an toàn, đem đến một làn da tươi sáng, rạng ngời và tràn đầy sức sống. Nhờ thành phần Cây Me Tây ngăn chặn da xỉn màu, và duy trì độ tươi sáng trên da, hạt phấn nhỏ, cực mịn, thẩm thấu nhanh, bám lâu và không lo bóng nhờn.','sp37.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(18,22,N'SON M.O.I X CÔNG TRÍ - NO2 SATIN','son-moi-x-cong-tri-no2-satin',N'Son moi',88,379000,1,N'Tuyệt tác son thỏi M.O.I x Công Trí được tạo ra từ sắc đẹp và thời trang nhằm tôn vinh vẻ đẹp, khí chất của phụ nữ Việt Nam. BST gồm 6 màu son thời thượng, hợp tông da châu Á và dựa trên cảm hứng thời trang nên mỗi thỏi son đặt tên theo từng chất liệu vải mềm mượt, vừa thân thuộc, vừa dễ nhớ nhưng cũng thể hiện nét duyên dáng, sang trọng vô cùng','sp38.jpg',N'Moi Cosmetic','2021/2/3','hoang','','',1)
insert into SanPham values(18,23,N'KEM FACE SYLIC BLINK BLINK - DƯỠNG TRẮNG DA, KEM DƯỠNG BAN NGÀY','kem-face-sylic-blink-blink-duong-trang-da-kem-duong-ban-ngay',N'DƯỠNG TRẮNG DA',55,530000,2,N'Một bước tiến mới trong làm đẹp, giờ đây, các chị em có thể vừa make-up vừa có thể dưỡng trắng da từ bên trong nhờ công nghệ cải tiến tới từ Sylic Face Cream.','sp39.jpg',N'Slimming Care','2021/2/3','hoang','','',1)
insert into SanPham values(19,24,N'NƯỚC TẨY TRANG SINH HỌC GENIE MAKEUP REMOVER WATER','nuoc-tay-trang-sinh-hoc-genie-makeup-remover-water',N'NƯỚC TẨY TRANG SINH HỌC',33,270000,1,N'Nước Tẩy Trang Sinh Học Genie Makeup Remover Water được nghiên cứu và cho ra thị trường là sản phẩm với cơ chế làm sạch sâu, vừa có công dụng bảo vệ và chăm sóc làn da.','sp40.jpg',N'Genie','2021/2/3','hoang','','',1)
insert into SanPham values(20,24,N'SỮA TẮM TRẮNG DA MOTHER & CARE','sua-tam-trang-da-mother-care',N'SỮA TẮM TRẮNG DA',77,450000,3,N'Sữa tắm Mother & Care được sản xuất theo một công thức riêng với dây truyền sản xuất hiện đại, rất hiệu quả đối với việc chăm sóc, bảo vệ và trắng sáng mịn màng, với hương thơm nhẹ nhàng. Tẩy sạch lớp da chết đồng thời cân bằng độ ẩm và nuôi dưỡng da mới.','sp41.jpg',N'Viet Nam','2021/2/3','hoang','','',4)
insert into SanPham values(20,25,N'TINH CHẤT TRỊ THÂM MẮT GENIE ANTI - DARK CIRLES EYE CREAM','tinh-chat-tri-tham-mat-genie-anti-dark-cirles-eye-cream',N'TINH CHẤT TRỊ THÂM MẮT',32,590000,2,N'Tinh Chất Đặc Trị Quầng Thâm Mắt Genie Anti-Dark Circles Eye Cream với chức năng kép giúp làm trắng và cải thiện nếp nhăn, chứa thành phần các loại vitamin và hoạt chất glutathione áp dụng công thức Luminous Network (mạng lưới phát sáng) tăng cường độ đàn hồi, mang đến cho bạn vùng da quanh mắt tươi sáng và mịn màng.','sp42.jpg',N'Genie','2021/2/3','hoang','','',1)
insert into SanPham values(21,25,N'KEM ĐÊM SYLIC CAVIAR TRỨNG CÁ TẦM','kem-dem-sylic-caviar-trung-ca-tam',N'KEM ĐÊM',33,657000,23,N'Xa rồi cái thời trứng cá tầm dùng làm đồ ăn và vàng 24K dùng để đeo lên người! Thời nay 2 siêu phẩm này được quy tụ trong 1 lọ kem, chính là kem dưỡng da ban đêm trứng cá tầm Sylic.','sp43.jpg',N'Slimming Care','2021/2/3','hoang','','',1)
insert into SanPham values(21,26,N'NƯỚC HOA CHARME ADORE 25ML','nuoc-hoa-charme-adore-25ml',N'NƯỚC HOA',66,330000,2,N'Mùi hoa nhài đằm thắm và hoa lan rạo rực giữ tông chủ đạo trong Charme Adore. Rực rỡ, gợi cảm, tinh tế, Adore là loại nước hoa tôn vinh sự trở lại của vẻ đẹp nữ tính và sức mạnh của những xúc cảm bột phát. Nó mang mùi hương của một bó hoa lan thơm ngát, cảm giác êm ái như nhung của mận xứ Đamas và vị ngọt đậm của gỗ Amarante.','sp44.jpg',N'Charme','2021/2/3','hoang','','',1)
insert into SanPham values(22,26,N'BỘ MẪU THỬ NƯỚC HOA 17 MÙI CHARME','bo-mau-thu-nuoc-hoa-17-mui-charme',N'NƯỚC HOA',12,250000,1,N'Bộ test nước hoa Charme gồm 17 loại cho cả nam – nữ theo mùi hương của các thương hiệu nổi tiếng trên thế giới. Giúp bạn cảm nhận từng mùi hương tuyệt vời với mức giá hấp dẫn. Nếu bạn còn đang phân vân chưa biết lựa chọn nước hoa nào phù hợp cho mình thì bộ mẫu thử nước hoa Charme sẽ giúp bạn có những trải nghiệm và lựa chọn chính xác nhất.','sp45.png',N'CHARME','2021/2/3','hoang','','',1)
insert into SanPham values(22,27,N'OHUI NƯỚC HOA HỒNG CHO NAM OHUI FRESH SKIN','ohui-nuoc-hoa-hong-cho-nam-ohui-fresh-skin',N'NƯỚC HOA',13,900000,3,N'Fresh Skin dành cho da thường và da dầu giúp làm sạch và kiểm soát bã nhờn trên da, cân bằng làn da nhạy cảm sau khi cạo râu. Bên cạnh đó, cung cấp độ ẩm tối ưu mang lại làn da khỏe mạnh, sảng khoái và thoáng mịn suốt cả ngày.','sp46.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(23,31,N'BỘ MẪU THỬ NƯỚC HOA CHARME PERFUME','bo-mau-thu-nuoc-hoa-charme-perfume',N'BỘ MẪU THỬ NƯỚC HOA',11,250000,1,N'Nước hoa Charme đang có gây nên 1 cơn sốt trong cộng đồng Online cũng như thị trường nước hoa tại Việt Nam. Người sáng lập nước hoa Charme, chị Nguyễn Thị Thu Hường, từng tốt nghiệp thạc sỹ kinh tế tại Đại học Kinh Tế TPHCM chia sẻ về Charme với tất cả tâm huyết và mong muốn thổi 1 làn gió mới vào thị trường nước hoa Việt Nam, hương thơm đương đại theo các Brand trên thế giới, tỏa hương tốt, lưu cực lâu với mức giá ai cũng có thể dùng được.','sp47.jpg',N'Charme','2021/2/3','hoang','','',1)
insert into SanPham values(23,33,N'MÁY LÀM ẤM BÌNH SỮA CANPOL BABIES 77/001','may-lam-am-binh-sua-canpol-babies-77-101',N'MÁY LÀM ẤM BÌNH SỮA',15,565000,2,N'Hâm nóng sữa và thức ăn nhanh chóng Có thể điều chỉnh nhiệt độ Bộ cảm biến bảo vệ thức ăn không bị quá nóng Xuất xứ: Ba Lan','sp48.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(24,33,N'DR BROWNS BÌNH CHIA SỮA 3 NGĂN LOẠI DÀI PP 950','dr-browns-binh-chia-sua-3-ngan-loai-dai-pp-950',N'BÌNH CHIA SỮA',44,186000,2,N'Thương hiệu: Dr.Browns Sản xuất: Mỹ Chất liệu: Nhựa PP Màu sắc: Xanh','sp49.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(24,34,N'GHẾ RUNG CHO BÉ BABE','ghe-rung-cho-be-babe',N'GHẾ RUNG',9,1050000,12,N'Ghế dành cho trẻ sơ sinh có thể gấp lại thuận tiện khi cho bé ăn. Có thể điều chỉnh ghế ngồi thẳng để các cử động của bé tạo ra sử chuyển động rung đối với đồ chơi tạo ra âm thanh. Đến giờ ngủ, thanh đồ chơi có thể tháo ra và bật chế độ rung nhẹ. Khi bé lớn hơn, có thể dùng làm ghế bập bênh.','sp50.jpg',N'Viet Nam','2021/2/3','hoang','','',1)
insert into SanPham values(25,34,N'GHẾ RUNG CHO BÉ','ghe-rung-cho-be',N'GHẾ RUNG',12,1050000,12,N' Ghế dành cho trẻ sơ sinh có thể gấp lại thuận tiện khi cho bé ăn. Có thể điều chỉnh ghế ngồi thẳng để các cử động của bé tạo ra sử chuyển động rung đối với đồ chơi tạo ra âm thanh. Đến giờ ngủ, thanh đồ chơi có thể tháo ra và bật chế độ rung nhẹ. Khi bé lớn hơn, có thể dùng làm ghế bập bênh.','sp51.jpg',N'Baby','2021/2/3','hoang','','',1)
insert into SanPham values(25,35,N'NHIỆT KẾ HỒNG NGOẠI','nhiet-ke-hong-ngoai',N'NHIỆT KẾ',44,864000,2,N'Thông tin sản phẩm: Nhiệt kế hồng ngoại đa năng Buben - Hãng sản xuất: Bayern Munchen - Xuất xứ hãng: Germany - Tiêu chuẩn: EC MDD, EN, ASTM, IEC/EN - Tự động tắt sau 1 phút không sử dụng. - Kích thước (mm): 109(dài) x 30(rộng) x 22(cao) - Trọng lượng: 45g bao gồm pin - Pin CR 2032 (đo được trên 1000 lần) - Tiêu chuẩn EN12470-5 và ASTM E-1965 Giá bán lẻ: 864,000 đ/SP Bảo hành 1 năm','sp52.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(1,35,N'MÁY HÚT MỤN OSOLE','may-hut-mun-osole',N'MÁY HÚT MỤN',55,690000,3,N'Máy hút mụn OSOLE hoạt động dựa trên cơ chế nén khí, giúp tạo lực hút tập trung vừa đủ mạnh để làm sạch nhân mụn trong các lỗ chân lông mà không gây tổn thương da như các phương pháp nặn mụn thông thường.','sp53.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(2,36,N'BỘ BÀN GHẾ VẼ GROWN UP 5013','bo-ban-ghe-ve-grown-up-5013',N'BỘ BÀN GHẾ VẼ',22,769000,3,N'Bộ Bàn Ghế Vẽ GrowN Up 5013 Sản phẩm được làm bằng chất liệu nhựa cao cấp Bàn và ghế tách rời, có thể cất giữ dễ dàng Ghế được thiết kế cho nhiều mục đích: cho trẻ em ngồi ăn, ngồi học, chơi đồ chơi… Kích thước: Bàn 39.4 x 45 x 54cm. Ghế: 30 x 30 x 25.7cm.','sp54.jpg',N'','2021/2/3','hoang','','',5)
insert into SanPham values(3,37,N'LÁ TẮM MẸ VÀ BÉ SAM NATURAL','la-tam-me-va-be-sam-natural',N'LÁ TẮM MẸ VÀ BÉ',33,185000,1,N'Lá Tắm Mẹ Và Bé của Sam Natural vô cùng lành tính có tác dụng giúp trẻ sơ sinh chống rôm sảy, mụn nhọt, hăm da, chàm sữa, thanh nhiệt, chân tay lở loét mà ngoài ra nó còn tốt cho cả thai phụ mới sinh, phụ nữ sảy thai, nạo hút thai, tắm luôn không phải kiêng nước lâu, lưu thông khí huyết, tránh hậu sản, tránh phong tê thấp, sạch sản dịch sau hậu sinh, nhanh co dạ con. Vì vậy, Lá tắm mẹ và bé Sam Natural luôn được các mẹ lựa chọn yêu thích và tin dùng.','sp55.jpg',N'Sam Natural','2021/2/3','hoang','','',1)
insert into SanPham values(5,37,N'KEM VICTOR CREAM CHỐNG MUỖI VÀ CÔN TRÙNG','kem-victor-cream-chong-muoi-va-con-trung',N'KEM CHỐNG MUỖI VÀ CÔN TRÙNG',44,260000,1,N' Dưỡng ẩm da, giúp da mềm mịn, Giúp làm mát dịu da khi bị mẩn ngứa, muỗi và côn trùng đốt','sp56.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(4,39,N'QUẦN GEN CHÂN REN ĐEN M/L','quan-gen-chan-ren-den-ml',N'QUẦN GEN',45,175000,2,N'Vòng eo thon gọn là mơ ước của bất kỳ người phụ nữ nào. Đặc biệt với các chị em phụ nữ sau sinh, thì khi đi làm ai mong muốn có được một cơ thể gọn gàng hơn. Quần gen đang làm mưa làm gió trên thị trường là sản phẩm cực kỳ hữu ích và tiện dụng, cho bạn một vòng eo thon gọn và vóc dáng lý tưởng.','sp57.jpg',N'','2021/2/3','hoang','','',1)
insert into SanPham values(7,39,N'QUẦN GEN ĐỊNH HÌNH WACOAL','quan-gen-dinh-hinh-wacoal',N'QUẦN GEN',33,150000,1,N'Gen nịt bụng làm từ sợi cotton mềm với khả năng co giãn rất tốt giúp nhẹ nhàng cố định phần bụng gọn gàng hơn cho các mẹ sau sinh. Chất liệu vải mềm nên các mẹ thoải mái khi sử dụng khi không làm mẹ đau hay khó chịu.. Vấn đề cân nặng và đặc biệt là giảm số đo vòng 2 luôn làm các bà mẹ đau đầu sau khi sinh bé. Với gen nịt bụng giúp mẹ nhanh chóng lấy lại vóc dáng khi sử dụng thường xuyên.. Thiết kế lỗ li ti giúp thoát hơi ẩm và tạo cảm giác nhẹ nhàng thoải mái khi mang.. Nhanh chóng lấy lại vóc dáng sau khi sinh.Sử dụng gen nịt bụng mẫu 2 ngay từ những ngày đầu mới sinh giúp cơ thể đốt cháy lượng mỡ thừa vùng bụng và làm gọn lại vùng bụng khi cơ thể được cố định bó chặt vào. Sản phẩm có nhiều nấc cho mẹ điều chỉnh độ rộng phù hợp với các mẹ từ 55 đến 75kg.','sp58.jpg',N'WACOAL','2021/2/3','hoang','','',6)
insert into SanPham values(6,40,N'QUẦN GEN MOVEON MÀU ĐEN SIZE 3XL','quan-gen-moveon-mau-den-size-3xl',N'QUẦN GEN',12,175000,1,N'Quần định hình cạp cao Moveon giúp bạn thon gọn vòng 2, tạo đường cong lý tưởng.','sp59.png',N'MOVEON','2021/2/3','hoang','','',1)
--insert into SanPham values(8,1,N'','',N'',120,340000,12,N'','sp60.jpg',N'','2021/2/3','hoang','','',1)


insert into HoaDon values(N'Đã nhận hàng','2021/3/3','hang','','',1)
insert into HoaDon values(N'Chờ xác nhận','2021/3/4','hang','','',1)
insert into HoaDon values(N'Đã nhận hàng','2021/3/4','hang','','',2)
insert into HoaDon values(N'Đang vận chuyển','2021/3/5','hoai','','',1)
insert into HoaDon values(N'Đang vận chuyển','2021/3/6','hoai','','',2)
insert into HoaDon values(N'Đang vận chuyển','2021/3/6','hoai','','',3)
insert into HoaDon values(N'Đã nhận hàng','2021/3/7','hoai','','',3)
insert into HoaDon values(N'Chờ xác nhận','2021/3/7','hoai','','',4)


insert into ChiTietHoaDon values(1,1,20,600000)
insert into ChiTietHoaDon values(1,2,10,300000)
insert into ChiTietHoaDon values(1,3,5,500000)
insert into ChiTietHoaDon values(2,1,20,900000)
insert into ChiTietHoaDon values(3,3,10,700000)
insert into ChiTietHoaDon values(4,4,12,600000)
insert into ChiTietHoaDon values(5,5,21,200000)
insert into ChiTietHoaDon values(5,6,13,400000)
insert into ChiTietHoaDon values(6,6,15,700000)
insert into ChiTietHoaDon values(7,7,5,900000)
insert into ChiTietHoaDon values(8,8,2,600000)
insert into ChiTietHoaDon values(8,9,7,500000)


select *from KhachHang
select *from QuanTri
select *from HoaDon


ALTER TABLE KhachHang
ADD ResetPasswordCode nvarchar(255);