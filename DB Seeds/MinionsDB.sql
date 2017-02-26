-- use master
-- drop database MinionsDB
-- go

create database MinionsDB
go

use MinionsDB
go

create table Countries(
	[Code] varchar(3) primary key,
	[Name] nvarchar(100) not null
)

create table Towns(
	        [Id] int primary key identity(1,1),
	      [Name] nvarchar(100) not null,
	[ContryCode] varchar(3) not null foreign key references Countries([Code])
)

create table Minions(
	    [Id] int primary key identity(1,1),
	  [Name] nvarchar(100) not null,
	   [Age] int not null,
	[TownId] int not null foreign key references Towns([Id])
)

create table EvilnessFactors(
	    [Id] int primary key identity(1,1),
	[Factor] nvarchar(20) not null
)

create table Villains(
	              [Id] int primary key identity(1,1),
	            [Name] nvarchar(100) not null,
	[EvilnessFactorId] int not null foreign key references EvilnessFactors([Id]) 
)

create table VillainsMinions(
	  [MinionId] int not null foreign key references Minions([Id]),
	[VillainsId] int not null foreign key references Villains([Id])

	constraint [PK_MinionsVillain] primary key ([MinionId], [VillainsId])
)
go

insert into Countries values
('BGR', 'Bulgaria'),
('DEU', 'Germany'),
('GRC', 'Greece'),
('FRA', 'Francce'),
('GRB', 'England')

insert into Towns values
('Burgas', 'BGR'), 
('Stara Zagora', 'BGR'), 
('London', 'GRB'), 
('Paris', 'FRA'),
('Sofia', 'BGR')

insert into Minions values
('Bugu', 500, 2),
('Darko', 123, 4),
('Bugs', 1, 1),
('Baivan', 999, 5),
('Flambe', 10, 4)

insert into EvilnessFactors values
('good'),
('bad'),
('evil'),
('super evil')

insert into Villains values
('Bai Shile', 4),
('Drakula', 1),
('Grozdor', 2),
('Sestrata', 4),
('Badman', 3)

insert into VillainsMinions values
(1, 1),
(1, 2),
(1, 3),
(3, 4),
(2, 5),
(4, 1),
(4, 4),
(5, 2),
(5, 5)
go



-- 01. Get all villains names and their minions count

SELECT [Name], Count(vm.[MinionId]) as [Minions Count] FROM Villains as v
right join VillainsMinions as vm on v.[Id] = vm.[VillainsId]
group by v.[Name]












