drop database workspace
create database workspace
go
use workspace
go
create table task
(
	id int IDENTITY(1,1) primary key,
	Tpriority tinyint default 2,
	content ntext not null,
	createtime datetime default getdate(),
	deadline datetime,
	checked tinyint default 0, -- 0 is unchecked
)

create table Tpriority
(
	id tinyint primary key,
	content nvarchar(10),
)

insert into Tpriority(id,content) values (0,N'High')
insert into Tpriority(id,content) values (1,N'Medium')
insert into Tpriority(id,content) values (2,N'Low')

alter table dbo.task add foreign key (Tpriority) references dbo.Tpriority(id)

insert into dbo.task(Tpriority,content,checked) values (0,N'yêu Kiều Anh',1)


create table diary
(
id int IDENTITY(1,1) primary key,
createtime datetime default getdate(),
storydate date default getdate(),
title nvarchar(4000) default 'Untitle',
story ntext,
fontfamily varchar(50) default 'Arial',
fontsize smallint default 13,
fontstyle varchar(20),
fontcolor varchar(20) default '#000000',
)


---------------

create table accountcategory
(
id int IDENTITY(1,1) primary key,
content nvarchar(100) not null
)
create table account
(
id int IDENTITY(1,1) primary key,
createdate date default getdate(),
lastupdate date default getdate(),
title nvarchar(200) not null default 'Untitle',
username nvarchar(100) not null,
Apassword nvarchar(100) not null,
website varchar(200),
idcategory int default 1,

foreign key (idcategory) references dbo.accountcategory(id),
)

create table customfield
(
id int IDENTITY(1,1) primary key,
title nvarchar(100) not null,
content nvarchar(200),
idaccount int,

foreign key (idaccount) references dbo.account(id)
)

insert into dbo.accountcategory(content) values (N'none') 
insert into dbo.accountcategory(content) values (N'giải trí') 
insert into dbo.accountcategory(content) values (N'mạng xã hội') 
insert into dbo.accountcategory(content) values (N'thư điện tử')
insert into dbo.accountcategory(content) values (N'wifi') 
insert into dbo.accountcategory(content) values (N'công việc')

--------
create table scheduleitem
(
id int primary key identity(1,1),
title nvarchar(200) not null,
place nvarchar(200),
dayinweek tinyint not null,
starttime time not null,
endtime time not null,
hexcolor varchar(10) default '#FFFFFFFF',
weektype tinyint not null default 0, 

constraint check_day check (dayinweek >= 1 and dayinweek <=7)
)


create table event
(
 id int primary key identity(1,1),
 name nvarchar(100) not null,
 date date not null,
 time time,
)

create table rate
(
id int primary key identity(1,1),
ratepoint tinyint not null default 0,
ratetime datetime not null default getdate(),
)

insert into dbo.rate(ratepoint) values (0)
