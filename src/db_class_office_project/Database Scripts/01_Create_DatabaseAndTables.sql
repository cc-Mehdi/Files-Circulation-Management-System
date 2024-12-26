Create Database FileCirculationManagementSystem_DB

create table FileStatus (
Id int primary key Identity(1,1) not null,
Title nvarchar(256)
);

Create Table Files (
Id int primary key Identity(1, 1) not null,
FileStatus_Id int Unique,
CaseId int,
FullName nvarchar(256),
SubscriptionCode nvarchar(256),

foreign key (FileStatus_Id) References FileStatus(Id)
);