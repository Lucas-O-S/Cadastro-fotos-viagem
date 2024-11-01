use AULADB
go
create table Cadastro(
	id int  primary key identity (1,1),
	nome varchar(200),
	loginUsuario varchar(200),
	senha varchar(max)
)
go
create table FotosViagem(
	id int  primary key identity (1,1),
	localFoto varchar(200),
	dataFoto datetime,
	foto01 varbinary(max),
	foto02 varbinary(max) null,
	foto03 varbinary(max) null,
	dataRegistro datetime,
	dataAlteracaoRegistro datetime null,
	usuario int,
	foreign key (usuario) references Cadastro(id) 


	
)
go


create or alter procedure sp_delete(
	@id int,
	@tabela varchar(max)
)
as
begin
	declare @sql varchar(max)
	set @sql = 'delete ' + @tabela + ' where id = ' + cast( @id as varchar(max))
	exec(@sql)
end
go

create or alter procedure sp_select(
	@id int,
	@tabela varchar(max)
)
as
begin
	declare @sql varchar(max)
	set @sql = 'select * from  ' + @tabela + ' where id = ' + cast( @id as varchar(max))
	exec(@sql)
end
go

create or alter procedure sp_list(
	@tabela varchar(max)
)
as
begin
	declare @sql varchar(max)
	set @sql = 'select * from  ' + @tabela
	exec(@sql)
end
go

create or alter procedure sp_insert_Cadastro(
	@nome varchar(200),
	@loginUsuario varchar(200),
	@senha varchar(max)
)
as
begin
	insert into Cadastro(nome,loginUsuario,senha) values(@nome,@loginUsuario,@senha)
end
go

create or alter procedure sp_update_Cadastro(
	@id int,
	@nome varchar(200),
	@loginUsuario varchar(200),
	@senha varchar(max)
)
as
begin
	update Cadastro set nome =@nome, loginUsuario=@loginUsuario,senha=@senha where id=@id
end
go

create or alter procedure sp_insert_FotosViagem(
	@localFoto varchar(200),
	@dataFoto datetime,
	@foto01 varbinary(max),
	@foto02 varbinary(max),
	@foto03 varbinary(max),
	@usuario int
)
as
begin
	insert into FotosViagem(
		localFoto,
		dataFoto,
		foto01,
		foto02,
		foto03,
		dataRegistro,
		usuario

		) 
	values(
		@localFoto,
		@dataFoto,
		@foto01,
		@foto02,
		@foto03,
		getdate(),
		@usuario

	)
end
go

create or alter procedure sp_update_localFoto(
	@localFoto varchar(200),
	@dataFoto datetime,
	@foto01 varbinary(max),
	@foto02 varbinary(max),
	@foto03 varbinary(max)
)
as
begin
	update FotosViagem set
	localFoto = @localFoto,
	dataFoto = @dataFoto,
	foto01 = @foto01,
	foto02 = @foto02,
	foto03 = @foto03,
	dataAlteracaoRegistro = GETDATE()


end
go