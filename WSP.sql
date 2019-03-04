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

	constraint task_date check (deadline >= createtime)
)
alter table dbo.task drop task_date
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

--insert into dbo.task(Tpriority,content,checked,deadline) values (1,N'làm hết task',0,GETDATE())

--select * from dbo.task

--select Tpriority as [Priority],content as [Content],deadline as [Deadline],checked as [Checked] from dbo.task
--                            where DATEPART(week,deadline) = 5 and year(deadline) = 2019

--ALTER DATABASE workspace SET COMPATIBILITY_LEVEL = 100

--select Tpriority as [Priority],content as [Content],deadline as [Deadline],checked as [Checked] from dbo.task where MONTH(deadline) = 5 and YEAR(deadline) = 2019


--select Tpriority as [Priority],content as [Content],deadline as [Deadline],checked as [Checked] from dbo.task" + "where year(deadline) = " + DateTime.Now.Year
--select Tpriority as [Priority],content as [Content],deadline as [Deadline],checked as [Checked] from dbo.task where year(deadline) = 

--delete from dbo.task where id > 2

--select Tpriority as [IDPriority], p.content as [Priority], t.content as [Content],deadline as [Deadline],checked as [Checked] from dbo.task t 
--inner join dbo.Tpriority p on t.Tpriority = p.id

drop table diary

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

--create trigger diary_title_unique_by_day
--on dbo.diary for insert 
--as
--begin
--	declare @title nvarchar(4000)
--	select @title = title from inserted
--	declare @storydate date
--	select @storydate = storydate from inserted
--    declare @checktitle nvarchar(4000)
--	select @checktitle = (select top 1 title from dbo.diary where storydate = @storydate)
--	if (@title = @checktitle)
--	begin
--		select @title = @title + '+'
--		update dbo.diary set title = @title 
--	end
--end

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
insert into dbo.accountcategory(content) values (N'xam xam')  
select * from dbo.accountcategory

insert into dbo.account(title,username,Apassword,website,idcategory) values ('facebook','quanghuy1998kh@gmail.com','astroboy145','www.facebook.com',3)
insert into dbo.account(title,username,Apassword,website,idcategory) values ('gmail','quanghuy1998kh@gmail.com','0971904687','www.google.com',4)
insert into dbo.account(title,username,Apassword,website,idcategory) values ('UIT','17520583','astroboy19','www.uit.edu.vn',6)
insert into dbo.account(title,username,Apassword,website,idcategory) values ('UIT','17520583','astroboy19','www.uit.edu.vn',7)

insert into customfield values (N'testtitle3','content3',14)

select * from account
select * from customfield

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


insert into dbo.scheduleitem(title,place,dayinweek,starttime,endtime) values ('anh văn 2','A305',2,'7:30','9:30')
insert into dbo.scheduleitem(title,place,dayinweek,starttime,endtime) values ('khoa hoc','A205',2,'9:30','10:30')
insert into dbo.scheduleitem(title,place,dayinweek,starttime,endtime) values ('khoa hoc ly luan thuc tien va co so hinh thanh chu nghia xa hoi','A205',2,'13:30','15:30')

insert into dbo.scheduleitem(title,place,dayinweek,starttime,endtime,weektype) values (N'mạch số','A305',2,'7:30','9:30',1)
insert into dbo.scheduleitem(title,place,dayinweek,starttime,endtime,weektype,hexcolor) values (N'mạch số','A305',2,'7:30','9:30',1,'#AADFD991')

select * from scheduleitem order by starttime 

create table event
(
 id int primary key identity(1,1),
 name nvarchar(100) not null,
 date date not null,
 time time,
)

select * from dbo.event where date >= getdate()
insert into dbo.event(name,date,time) values (N'sinh nhật quang huy', '6/3/2019', '0:0')

insert into dbo.event(name,date,time) values (N'dsc', '28/2/2019', '12:00')