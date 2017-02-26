-- 02. Get all villains names and their minions count

SELECT [Name], Count(vm.[MinionId]) as [Minions Count] FROM Villains as v
right join VillainsMinions as vm on v.[Id] = vm.[VillainsId]
group by v.[Name]


-- 03. Get Minions Names
declare @id int = 2
SELECT m.[Name], m.[Age], v.[Name] from Minions as m
join VillainsMinions as vm on m.[Id] = vm.[MinionId]
join Villains as v on vm.[VillainsId] = v.[Id]
where v.[Id] = @id

-- 04. Add minion

begin transaction
declare @newMinionName nvarchar(100) = 'Kiro';
declare @newMinionAge int = 100;
declare @town nvarchar(100) = 'Pleven';
declare @villain nvarchar(100) = 'Dzvera'

declare @minionTownId int;
declare @villainId int;

select @minionTownId = [Id] from Towns
where [Name] = @town

if (@minionTownId is null)
	begin
	insert into Towns values (@town, 'BGR')
	select @minionTownId = [Id] from Towns
	where [Name] = @town
	end

insert into Minions values
(@newMinionName, @newMinionAge, @minionTownId)

select @villainId = [Id] from Villains
where [Name] = @villain

if(@villainId is null)
	begin
	insert into Villains values (@villain, (select [Id] from EvilnessFactors where [Factor] = 'evil'))
	select @villainId = [Id] from Villains
	where [Name] = @villain
	end

insert into VillainsMinions values
((select [Id] from Minions where [Name] = @newMinionName), @villainId)

rollback


-- 05. Change Town Names Casing

begin transaction

declare @cname nvarchar(100) = 'Bulgaria';
update Towns
   set [Name]=UPPER([Name])
OUTPUT INSERTED.[Name]
 where [ContryCode] = (select [Code] 
					  from Countries 
					  where [Name] = @cname)
  and [Name] <> UPPER([Name])
rollback
go


-- 06. Remove Villain

begin transaction

declare @vid int = 188; 
declare @rm table([Id] int)

Delete from VillainsMinions
output DELETED.[MinionId] INTO @rm
where [VillainsId] = @vid

SELECT [Name] FROM Minions as m
join @rm on m.[Id] = @rm.[Id]

rollback

