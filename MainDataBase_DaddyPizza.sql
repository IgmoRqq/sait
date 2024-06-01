create database PizzaForGoodDaddyDB
go
use PizzaForGoodDaddyDB

go 
create table Roles
(
	id int Primary key identity,
	name nvarchar(max) not null,
	createDate datetime not null,
)


go
create table Users
(
	id int Primary key identity,
	email nvarchar(max) not null,
	password nvarchar(12) not null,
	idRole int foreign key references Roles(id),
	adress nvarchar(max) not null,
	createDate datetime not null
)


go
create table Toppings
(
	id int Primary key identity,
	name nvarchar(max) not null,
	weight nvarchar(max) not null,
	createDate datetime not null,
)

go
create table Categories
(
	id int Primary key identity,
	name nvarchar(max) not null,
	createDate datetime not null,
)

go
create table Pizzas
(
	id int Primary key identity,
	name nvarchar(max) not null,
	size nvarchar(max) not null,
	price float not null,
	description nvarchar(max) not null,
	idCategory int foreign key references Categories(id),
	createDate datetime not null
)

go
create table PizzasToppings
(
	id int Primary key identity,
	idPizza int foreign key references Pizzas(id),
	idToppings int foreign key references Toppings(id),
)

go
create table Orders
(
	id int Primary key identity,
	dateOrder datetime not null,
	status nvarchar(max) not null,
	idUser int foreign key references Users(id),

)

go
create table OrderPizzas
(
	id int Primary key identity,
	idPizza int foreign key references Pizzas(id),
	idOrder int foreign key references Orders(id),
	count int not null
)


go
create table Histories
(
	id int Primary key identity,
	idOrder int foreign key references Orders(id),
	idUser int foreign key references Users(id),
)


go
create table Combos
(
	id int Primary key identity,
	name nvarchar(max) not null,
	desription nvarchar(max) not null,
	createDate datetime not null,
	price float not null,
	idCategory int foreign key references Categories(id)
)

go
create table ComboItems
(
	id int Primary key identity,
	idCombo int foreign key references Combos(id),
	idPizza int foreign key references Pizzas(id)
)
-- Rename the custom Roles table
EXEC sp_rename 'Roles', 'ApplicationRoles';

go
create table OrderCombos
(
	id int Primary key identity,
	idCombo int foreign key references Combos(id),
	idOrder int foreign key references Orders(id),
	count int not null
)
GO
USE PizzaForGoodDaddyDB
GO
insert into Roles values ('пользователь',GETDATE())

select * from Users