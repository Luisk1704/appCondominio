create database ProyectoCondominio

use ProyectoCondominio

create table TipoUsuario(
ID int identity(1,1) primary key not null,
Descripcion varchar(50) not null
)

insert into TipoUsuario(Descripcion) values ('Administrador')
insert into TipoUsuario(Descripcion) values ('Residente')



--delete from TipoUsuario where ID = 3 or ID = 4 

create table Usuario(
UsuarioID int identity(1,1) primary key not null,
TipoUsuarioID int not null,
Nombre varchar(100) not null,
Apellido1 varchar(100) not null,
Apellido2 varchar(100) not null, 
Correo varchar(100) not null,
Contrasenna varchar(max) not null,
Estado int not null,
constraint FK_Tipo foreign key(TipoUsuarioID) references TipoUsuario(ID)
)


create table AreaComun(
ID int identity(1,1) primary key not null,
Descripcion varchar(100) not null,
HorarioInicio time not null,
HorarioCierre time not null,
Disponibilidad int not null,
Estado int not null
)

insert into AreaComun(Descripcion,HorarioInicio,HorarioCierre,Disponibilidad,Estado)values
('Piscina','09:00:00.0000000','17:00:00.0000000',1,1)
insert into AreaComun(Descripcion,HorarioInicio,HorarioCierre,Disponibilidad,Estado)values
('Salon Comunal','09:00:00.0000000','17:00:00.0000000',1,1)
insert into AreaComun(Descripcion,HorarioInicio,HorarioCierre,Disponibilidad,Estado)values
('Plaza de Futbol 11','09:00:00.0000000','21:00:00.0000000',1,1)
insert into AreaComun(Descripcion,HorarioInicio,HorarioCierre,Disponibilidad,Estado)values
('Parque de niños','09:00:00.0000000','16:00:00.0000000',1,1)
insert into AreaComun(Descripcion,HorarioInicio,HorarioCierre,Disponibilidad,Estado)values
('Cancha de Baloncesto','09:00:00.0000000','21:00:00.0000000',1,1)
insert into AreaComun(Descripcion,HorarioInicio,HorarioCierre,Disponibilidad,Estado)values
('Cancha de Tenis','09:00:00.0000000','21:00:00.0000000',1,1)

create table Reservacion(
AreaComunID int not null,
UsuarioID int not null,
Fecha datetime not null,
Estado varchar(100) not null,
primary key(AreaComunID, UsuarioID),
constraint Fk_Area foreign key(AreaComunID) references AreaComun(ID),
constraint Fk_Usuario foreign key(UsuarioID) references Usuario(UsuarioID)
)

/*
create table Solicitud(
AreaComun int not null,
Residente int not null,
Fecha date not null,
Estado varchar(100) not null,
primary key(AreaComun, Residente),
constraint Fk_AreaSolicitada foreign key(AreaComun) references AreaComun(ID),
constraint Fk_ResidenteSolicitante foreign key(Residente) references Residente(ID)
)*/

create table Residencia(
ID int identity(1,1) primary key not null, 
UsuarioID int not null,
CantPersonas int not null,
FechaInicio date not null,
Estado varchar(100) not null,
CantidadCarros int not null,
constraint Fk_User foreign key(UsuarioID) references Usuario(UsuarioID)
)

create table RubroCobro(
ID int identity(1,1) primary key not null,
Nombre varchar(100) not null,
Descripcion varchar(100) not null,
Costo decimal not null,
Estado int not null
)

insert into RubroCobro(Nombre,Descripcion,Costo,Estado) values ('Cantidad Carros','1 carro',20000,1)
insert into RubroCobro(Nombre,Descripcion,Costo,Estado) values ('Cantidad Carros','2 carros',40000,1)
insert into RubroCobro(Nombre,Descripcion,Costo,Estado) values ('Cantidad Carros','3 carros',80000,1)
insert into RubroCobro(Nombre,Descripcion,Costo,Estado) values ('Plantas','1 planta',50000,1)
insert into RubroCobro(Nombre,Descripcion,Costo,Estado) values ('Plantas','2 plantas',80000,1)
insert into RubroCobro(Nombre,Descripcion,Costo,Estado) values ('Cantidad Personas','1 a 2 personas',50000,1)
insert into RubroCobro(Nombre,Descripcion,Costo,Estado) values ('Cantidad Personas','3 a 5 personas',100000,1)
insert into RubroCobro(Nombre,Descripcion,Costo,Estado) values ('Accesibilidad','1 a 100 mts de la entrada',80000,1)
insert into RubroCobro(Nombre,Descripcion,Costo,Estado) values ('Accesibilidad','200 a 300 mts de la entrada',60000,1)
insert into RubroCobro(Nombre,Descripcion,Costo,Estado) values ('Accesibilidad','400 a 500 mts de la entrada',30000,1)



create table PlanRubro(
PlanCobroID int not null,
RubroCobroID int not null,
primary key(PlanCobroID,RubroCobroID),
constraint Fk_PlanCob foreign key(PlanCobroID) references PlanCobro(ID),
constraint Fk_Rub foreign key(RubroCobroID) references RubroCobro(ID)
)

insert into PlanRubro(PlanCobroID,RubroCobroID) values (1,1)
insert into PlanRubro(PlanCobroID,RubroCobroID) values (1,4)
insert into PlanRubro(PlanCobroID,RubroCobroID) values (1,6)
insert into PlanRubro(PlanCobroID,RubroCobroID) values (2,3)
insert into PlanRubro(PlanCobroID,RubroCobroID) values (2,7)
insert into PlanRubro(PlanCobroID,RubroCobroID) values (2,10)
insert into PlanRubro(PlanCobroID,RubroCobroID) values (3,3)
insert into PlanRubro(PlanCobroID,RubroCobroID) values (3,7)
insert into PlanRubro(PlanCobroID,RubroCobroID) values (3,8)
insert into PlanRubro(PlanCobroID,RubroCobroID) values (4,3)
insert into PlanRubro(PlanCobroID,RubroCobroID) values (4,7)
insert into PlanRubro(PlanCobroID,RubroCobroID) values (4,8)
/*create table PlanResid(
PlanCobro int not null,
Residencia int not null,
primary key(PlanCobro,Residencia),
constraint Fk_PlanCobro foreign key(PlanCobro) references PlanCobro(ID),
constraint Fk_Res foreign key(Residencia) references Residencia(ID)
)*/

create table PlanCobro(
ID int identity(1,1) primary key not null,
Descripcion varchar(100) not null,
TotalMes decimal not null,
Estado int not null,
)

insert into PlanCobro(Descripcion,TotalMes,Estado)values('Plan 1',194000,1)
insert into PlanCobro(Descripcion,TotalMes,Estado)values('Plan 2',230000,1)
insert into PlanCobro(Descripcion,TotalMes,Estado)values('Plan 3',334000,1)
insert into PlanCobro(Descripcion,TotalMes,Estado)values('Plan 4',350000,1)

/*create table TipoPlan(
ID int identity(1,1) primary key not null,
Descripcion varchar(100) not null,
Costo decimal not null,
Estado int not null
)*/


create table EstadoCuenta(
ID int identity(1,1) primary key not null,
ResidenciaID int not null,
PlanCobroID int not null,
Monto decimal not null,
Estado varchar(50) not null,
Mes date not null
constraint Fk_Resid foreign key(ResidenciaID) references Residencia(ID),
constraint Fk_Plan foreign key(PlanCobroID) references PlanCobro(ID)
)

--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(9,1,0.13,0.10,184000.0,'Pendiente',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(10,2,0.15,0.13,409000.0,'Pagada',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(11,3,0.20,0.08,409600.0,'Pagada',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(12,4,0.13,0.10,430500.0,'Pendiente',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(9,1,0.13,0.10,284000.0,'Pendiente',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(12,2,0.15,0.13,309000.0,'Pagada',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(10,3,0.20,0.08,309600.0,'Pagada',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(11,4,0.13,0.10,530500.0,'Pendiente',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(9,1,0.13,0.10,384000.0,'Pagada',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(12,2,0.15,0.13,209000.0,'Pendiente',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(11,3,0.20,0.08,209600.0,'Pendiente',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(10,4,0.13,0.10,330500.0,'Pagada',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(10,1,0.13,0.10,284000.0,'Pagada',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(12,2,0.15,0.13,509000.0,'Pendiente',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(9,3,0.20,0.08,409600.0,'Pendiente',GETDATE()-14)
--insert into EstadoCuenta(ResidenciaID,PlanCobroID,SaldoFinal,Estado,Mes)values(11,4,0.13,0.10,330500.0,'Pagada',GETDATE()-14)


/*
create table DeudaVigente(
ID int identity(1,1) primary key not null,
Fecha date not null,
Pago int not null,
constraint Fk_EstadoCuenta foreign key(EstadoCuenta) references EstadoCuenta(ID)
)*/

/*create table Pago(
ID int identity(1,1) primary key not null,
Residente int not null,
Fecha date not null,
constraint Fk_Re foreign key(Residente) references Residente(ID)
)*/

create table Informacion(
ID int identity(1,1) primary key not null,
UsuarioID int not null,
Titulo varchar(100) not null,
Informacion varchar(max) not null,
Fecha date not null,
Estado int not null,
constraint Fk_Us foreign key(UsuarioID) references Usuario(UsuarioID)
)

create table Incidencia(
ID int identity(1,1) primary key not null,
UsuarioID int not null,
Titulo varchar(100) not null,
Informacion varchar(max) not null,
Fecha date not null,
Estado varchar(50) not null,
constraint Fk_U foreign key(UsuarioID) references Usuario(UsuarioID)
)



