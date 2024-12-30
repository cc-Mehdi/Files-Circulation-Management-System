Create Database FileCirculationManagementSystem_DB

create table FileStatus (
Id int primary key Identity(1,1) not null,
Title nvarchar(256)
);

create table Users(
Id int primary key identity(1,1) not null,
Fullname nvarchar(256),
Username nvarchar(500),
HashPassword nvarchar(500),
Phone nvarchar(20),
Email nvarchar(300),
PictureAddress nvarchar(300)
);

Create Table Files (
Id int primary key Identity(1, 1) not null,
FileStatus_Id int,
User_Id int,
CaseId int,
FullName nvarchar(256),
SubscriptionCode nvarchar(256),

foreign key (FileStatus_Id) References FileStatus(Id),
foreign key (User_Id) References Users(Id)
);