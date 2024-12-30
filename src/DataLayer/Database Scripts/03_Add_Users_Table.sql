create table Users(
Id int primary key identity(1,1) not null,
Fullname nvarchar(256),
Username nvarchar(500),
HashPassword nvarchar(500),
Phone nvarchar(20),
Email nvarchar(300),
PictureAddress nvarchar(300)
);