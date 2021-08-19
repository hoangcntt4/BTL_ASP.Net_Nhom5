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

insert into KhachHang values(N'Trần Thủ Độ',N'Bắc Kạn','0392049023','user1','user1','user1.png','2021/2/2','hang','','')
insert into KhachHang values(N'Nguyễn Văn Anh',N'Bắc Ninh','0398799023','user2','user2','user2.png','2021/2/2','hang','','')
insert into KhachHang values(N'Lê Thị Bé',N'Bắc Giang','0365449023','user2','user2','user2.png','2021/2/2','hang','','')
insert into KhachHang values(N'Tấn Văn Tạ',N'Hà Nội','0392025423','user3','user3','user3.png','2021/2/2','hang','','')
insert into KhachHang values(N'Tạ Thị Lạng',N'Hà Nam','0354279023','user4','user4','user4.png','2021/2/2','hang','','')
insert into KhachHang values(N'Hoài Thị Đào',N'Hà Đông','0388849023','user5','user5','user5.png','2021/2/2','hang','','')
insert into KhachHang values(N'Lãnh Thu Hằng',N'Quảng Trị','0392777023','user6','user6','user6.png','2021/2/2','hang','','')
insert into KhachHang values(N'Nguyễn Thu Hoàng',N'Quảng Nam','0366649023','user7','user7','user7.png','2021/2/2','hang','','')
insert into KhachHang values(N'Trần Đình Trọng',N'Quảng Ninh','0392555023','user8','user8','user8.png','2021/2/2','hang','','')
insert into KhachHang values(N'Vũ Minh Anh',N'Quảng Bình','0392049444','user9','user9','user9.png','2021/2/2','hang','','')

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

insert into KhuyenMai values(N'Tết nguyên đán','2021/2/2','2021/2/10',0.2,'2021/2/1','hoai','','')
insert into KhuyenMai values(N'Trung thu','2021/9/2','2021/9/10',0.1,'2021/9/1','hoai','','')

insert into SanPham values(1,12,N'TẨY DA CHẾT DẠNG BÔNG NEULII PEELING PAD','tay-da-chet-dang-bong-neulii-peeling-pad',N'',500,606000,7,N'Neulii BHA PHA 5.5 peel pad là sản phẩm tẩy da chết kết hợp với toner dạng miếng. Đây là thiết kế tiện dụng giúp quá trình skincare đơn giản và dễ dàng hơn. Những miếng đệm này được cho là giúp kiểm soát bã nhờn và tẩy tế bào chết nhẹ nhàng cho da. Chỉ cần lau sạch bằng miếng lột BHA PHA 5.5 để tẩy tế bào chết nhẹ nhàng và loại bỏ các tạp chất trên da, đồng thời dưỡng ẩm cho làn da mịn màng và mềm mại. Đặc biệt miếng pad này hỗ trợ rất tốt trong quá trình điều trị mụn (mụn đầu đen, mụn cám, mụn ẩn), làm mịn, dưỡng sáng da phù hợp với mọi loại da đặc biệt là da dầu','sp1.png',N'','2021/2/5','hoang','','',1)
insert into SanPham values(1,12,N'SỮA RỬA MẶT TẠO BỌT AC CLEAN SAVER','sua-rua-mat-tao-bot-ac-clean-saver',N'SỮA RỬA MẶT TẠO BỌT',550,336000,12,N'Sữa rửa mặt tạo bọt lành tính làm dịu làn da bị mụn, làm sạch và mềm mại làn da nhạy cảm. Đây không phải là sản phẩm y dược để phòng ngừa và điều trị bệnh.','sp2.png',N'','2021/2/3','hoang','','',1)
insert into SanPham values(1,12,N'NƯỚC HOA HỒNG AC CLEAN SAVER TONER','nuoc-hoa-hong-ac-clean-saver-toner',N'Nước hoa hồng Neulii AC Clean Saver Toner',550,580000,12,N'một loại huyết thanh hoạt động nhanh có chức năng giống như một loại tinh chất để kiểm soát nhờn và giúp làm sáng da hiệu quả. Mặc dù có độ sệt giống như gel, nhưng thực sự thì toner có cảm giác rất nhẹ khi thoa. Nó nhanh chóng hấp thụ vào da và để lại một kết thúc tươi mát, không bết dính.','sp3.png',N'Cosmetic','2021/2/3','hoang','','',1)
insert into SanPham values(1,12,N'TINH CHẤT NEULLI AC CLEAN SAVER SERUM','tinh-chat-neulii-ac-clean-saver-serum',N'Serum',550,624000,12,N'Dành cho làn da vô cùng quý giá của bạn. nhưng bị xỉn màu và thiếu sức sống Giải pháp của chúng tôi, Bước thứ 2 là #serum. Bắt đầu từ bước Skin care Cơ bản cho làn da mệt mỏi','sp4.png',N'Cosmetic','2021/2/3','hoang','','',1)
insert into SanPham values(1,12,N'KEM DƯỠNG DA TRỊ MỤN AC CLEAN SAVER CREAM','kem-duong-da-tri-mun-ac-clean-saver-cream',N'KEM DƯỠNG DA TRỊ MỤN',220,586000,12,N'Dành cho làn da vô cùng quý giá của bạn. nhưng bị xỉn màu và thiếu sức sống Giải pháp của chúng tôi, Bước thứ 3 là #Scream','sp5.png',N'Cosmetic','2021/2/3','hoang','','',1)
insert into SanPham values(2,9,N'MÁY ĐO NỒNG ĐỘ OXY TRONG MÁU SPO2 CONTEC CMS50D2','may-do-nong-do-oxy-trong-mau-spo2-contec-cms50d2',N'MÁY ĐO NỒNG ĐỘ OXY',210,1060000,12,N'Máy Đo Nồng Độ Oxy Trong Máu SPO2 Contec CMS50D2 là một thiết bị không xâm lấn dùng để đo nồng độ oxy trong máu và nhịp tim của bệnh nhân người lớn và trẻ em trong môi trường gia đình và bệnh viện trong những trường hợp cấp cứu, cần sử dụng máy giúp thở hoặc trợ thở bằng nguồn oxy bên ngoài.','sp6.png',N'','2021/2/3','hoang','','',1)
insert into SanPham values(3,10,N'THẠCH NGHỆ COLLAGEN NANO CURCUMIN JELLY 365 VỊ XOÀI HÀN QUỐC','thach-nghe-collagen-nano-curcumin-jelly-365-vi-xoai-han-quoc',N'THẠCH NGHỆ',210,1350000,12,N'Thạch Nghệ Collagen Nano Curcumin Jelly 365 Vị Xoài là tinh chất curcumin dạng lỏng giúp phân giải curcumin, hóa lỏng bằng nước khoáng giúp tăng cường tỷ lệ hấp thụ.','sp7.png',N'','2021/2/3','hoang','','',1)
insert into SanPham values(3,1,N'TINH NGHỆ NANO CURCUMIN GOLD GOLDEN GIFT HỘP 100 ỐNG X2ML','tinh-nghe-nano-curcumin-gold-golden-gift-hop-100-ong-x2ml',N'TINH NGHỆ',210,1950000,12,N'Nghệ nano Curcumin Gold nhanh chóng trở thành trào lưu làm đẹp của phụ nữ Hàn Quốc. Tinh chất nghệ tươi Curcumin được điều chế dạng nano hấp thu cực tốt vào cơ thể, hiệu quả nhanh, trong khi đó lại rất an toàn','sp8.png',N'Golden Gift','2021/2/3','hoang','','',1)
insert into SanPham values(4,12,N'KEM ĐẶC TRỊ NÁM SUPER MELA CREAM - FROM YOUR SKIN','kem-dac-tri-nam-super-mela-cream',N'KEM ĐẶC TRỊ NÁM',210,880000,12,N'Super Mela Cream được công nhận là kem trị tàn nhang mang lại kết quả mong đợi nhất quả không sai bởi những thành phần chính như: Retinol- Hydrolyzed Collagen- Arbutin. Những thành phần này không chỉ giúp dưỡng da từ bên trong, làm mờ thâm nám nhanh chóng mà còn vô cùng an toàn, không gây kích ứng da. Giúp các chị em hoàn toàn yên tâm sử dụng mà không lo ảnh hưởng tới sức khỏe và sinh hoạt hàng ngày.','sp9.png',N'','2021/2/3','hoang','','',1)
insert into SanPham values(5,15,N'ĐAI SIẾT EO ĐỊNH HÌNH CAO CẤP S SHAPE- SLINE','dai-sieu-eo-dinh-hinh-cao-cap-s-shape-sline',N'ĐAI SIẾT EO',200,450000,12,N'Đai Siết Eo Định Hình Cao Cấp S Shape - Sline với chất liệu là vải cotton 4D co dãn, thoải mái nhẹ nhàng 24/24, thấm hút mồ hôi ko nóng ko ngứa, giảm mỡ nhanh chóng, định hình vòng eo, tạo đường cong quyến rũ!','sp10.png',N'','2021/2/3','hoang','','',1)

insert into HoaDon values('2021/3/3','hang','','',1)
insert into HoaDon values('2021/3/4','hang','','',1)
insert into HoaDon values('2021/3/4','hang','','',2)
insert into HoaDon values('2021/3/5','hoai','','',1)
insert into HoaDon values('2021/3/6','hoai','','',2)
insert into HoaDon values('2021/3/6','hoai','','',3)
insert into HoaDon values('2021/3/7','hoai','','',3)
insert into HoaDon values('2021/3/7','hoai','','',4)


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

