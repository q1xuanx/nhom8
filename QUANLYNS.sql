create database QLNhanS


use QLNhanS



create table PhongBan(
	MaPB char(2),
	TenPB nvarchar(30),
	primary key (MaPB)
)

create table NhanVien(
	MaNV char(6),
	TenNV nvarchar(20),
	NgaySinh datetime,
	MaPB char(2),
	primary key (MaNV),
	foreign key (MaPB) references PhongBan(MaPB)
)

set DATEFORMAT dmy


insert into PhongBan values ('KD', N'Kinh Doanh'),('KT', N'Kế Toán')

insert into NhanVien values ('NV001', N'Trần Văn Nam', '25/5/1981', 'KD'), ('NV002',N'Nguyễn Thị Yến', '25/5/1981', 'KT'), ('NV003',N'Lý Kim Tuyền', '10/8/1979', 'KD')

select * from NhanVien
