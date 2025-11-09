USE master
GO

CREATE DATABASE CLINICA_DB
GO

USE CLINICA_DB
GO

CREATE TABLE Permisos(
	Id INT PRIMARY KEY IDENTITY (1, 1),
	Descripcion VARCHAR(20) NOT NULL
)
GO

CREATE TABLE Especialidades(
	Id INT PRIMARY KEY IDENTITY (1, 1),
	Descripcion VARCHAR(50) NOT NULL
)
GO

CREATE TABLE Estados(
	Id INT PRIMARY KEY IDENTITY (1, 1),
	Descripcion VARCHAR(50) NOT NULL
)
GO

CREATE TABLE DiasSemana(
	Id INT PRIMARY KEY IDENTITY (1, 1),
	Descripcion VARCHAR(20) NOT NULL
)
GO

CREATE TABLE Usuarios(
	Id INT PRIMARY KEY IDENTITY (1, 1),
	Usuario VARCHAR(20) NOT NULL UNIQUE,
	Clave VARCHAR(100) NOT NULL,
	Activo BIT NOT NULL DEFAULT 1,
	IdPermiso INT NULL FOREIGN KEY REFERENCES Permisos(Id)
)
GO

CREATE TABLE Pacientes(
	Id INT PRIMARY KEY IDENTITY (1, 1),
	Nombre VARCHAR(30) NOT NULL,
	Apellido VARCHAR(30) NOT NULL,
	FechaNacimiento DATE NOT NULL,
	Email VARCHAR(50) NOT NULL UNIQUE,
	Telefono VARCHAR(20) NULL,
	Dni VARCHAR(8) NOT NULL UNIQUE,
	UrlImagen VARCHAR(200) NULL,
	IdUsuario INT NULL FOREIGN KEY REFERENCES Usuarios(Id),
)
GO

CREATE TABLE Medicos(
	Id INT PRIMARY KEY IDENTITY (1, 1),
	Nombre VARCHAR(30) NOT NULL,
	Apellido VARCHAR(30) NOT NULL,
	FechaNacimiento DATE NOT NULL,
	Telefono VARCHAR(20) NULL,
	Dni VARCHAR(8) NOT NULL UNIQUE,
	Email VARCHAR(50) NOT NULL UNIQUE,
	UrlImagen VARCHAR(200) NULL,
	Matricula VARCHAR(20) NOT NULL UNIQUE,
	IdUsuario INT NULL FOREIGN KEY REFERENCES Usuarios(Id),
)
GO

CREATE TABLE Recepcionistas(
	Id INT PRIMARY KEY IDENTITY (1, 1),
	Nombre VARCHAR(30) NOT NULL,
	Apellido VARCHAR(30) NOT NULL,
	FechaNacimiento DATE NOT NULL,
	Email VARCHAR(50) NOT NULL UNIQUE,
	Telefono VARCHAR(20) NULL,
	Dni VARCHAR(8) NOT NULL UNIQUE,
	UrlImagen VARCHAR(200) NULL,
	IdUsuario INT NULL FOREIGN KEY REFERENCES Usuarios(Id),
)
GO


CREATE TABLE EspecialidadesPorMedico(
	IdMedico INT NOT NULL FOREIGN KEY REFERENCES Medicos(Id),
	IdEspecialidad INT NOT NULL FOREIGN KEY REFERENCES Especialidades(Id)
	PRIMARY KEY (IdMedico, IdEspecialidad)
)
GO

CREATE TABLE HorariosPorMedicos(
	Id INT PRIMARY KEY IDENTITY (1, 1),
	IdDiaSemana INT NOT NULL FOREIGN KEY REFERENCES DiasSemana(Id),
	HoraEntrada TIME NOT NULL,
	HoraSalida TIME NOT NULL,
	IdMedico INT NOT NULL,
	IdEspecialidad INT NOT NULL,
	FOREIGN KEY (IdMedico, IdEspecialidad) REFERENCES EspecialidadesPorMedico(IdMedico, IdEspecialidad)
)
GO


CREATE TABLE Turnos(
	Id INT PRIMARY KEY IDENTITY (1, 1),
	Fecha DATETIME NOT NULL,
	Observaciones VARCHAR(250) NULL,
	IdPaciente INT NOT NULL FOREIGN KEY REFERENCES Pacientes(Id),
	IdEstado INT NOT NULL FOREIGN KEY REFERENCES Estados(Id),
	IdMedico INT NOT NULL,
	IdEspecialidad INT NOT NULL,
	FOREIGN KEY (IdMedico, IdEspecialidad) REFERENCES EspecialidadesPorMedico(IdMedico, IdEspecialidad)
)
GO

CREATE TABLE HistoriasClinicas(
	Id INT PRIMARY KEY IDENTITY (1, 1),
	Fecha DATETIME NOT NULL,
	IdPaciente INT NOT NULL FOREIGN KEY REFERENCES Pacientes(Id),
	Asunto VARCHAR (100) NOT NULL,
	Descripcion VARCHAR(250) NOT NULL,
	IdTurno INT NULL FOREIGN KEY REFERENCES Turnos(Id),
	IdMedico INT NOT NULL,
	IdEspecialidad INT NOT NULL,
	FOREIGN KEY (IdMedico, IdEspecialidad) REFERENCES EspecialidadesPorMedico(IdMedico, IdEspecialidad)
)
GO
INSERT INTO Permisos (Descripcion)
VALUES 
('Paciente'),
('Medico'),
('Recepcionista'),
('Administrador');
GO

INSERT INTO DiasSemana (Descripcion)
VALUES 
('Lunes'),
('Martes'),
('Miércoles'),
('Jueves'),
('Viernes'),
('Sábado'),
('Domingo');
GO

INSERT INTO Estados (Descripcion)
VALUES 
('Nuevo'),
('Reprogramado'),
('Cancelado'),
('No Asistió'),
('Cerrado');
GO

INSERT INTO Especialidades (Descripcion)
VALUES
('Clínica Médica'),
('Cardiología'),
('Pediatría'),
('Dermatología'),
('Traumatología'),
('Ginecología');
GO

INSERT INTO Usuarios (Usuario, Clave, IdPermiso)
VALUES
('paciente1', '1234', 1),
('paciente2', '1234', 1),
('medico1', 'abcd', 2),
('medico2', 'abcd', 2),
('recep1', 'qwerty', 3),
('admin', 'admin123', 4);
GO

INSERT INTO Pacientes (Nombre, Apellido, FechaNacimiento, Email, Telefono, Dni, UrlImagen, IdUsuario)
VALUES
('Juan', 'Pérez', '1990-05-10', 'juan.perez@mail.com', '1122334455', '40123456', NULL, 1),
('María', 'Gómez', '1985-07-22', 'maria.gomez@mail.com', '1199887766', '37222444', NULL, 2);
GO

INSERT INTO Medicos (Nombre, Apellido, FechaNacimiento, Telefono, Dni, Email, UrlImagen, Matricula, IdUsuario)
VALUES
('Carlos', 'Ruiz', '1978-09-14', '1144556677', '30111222', 'carlos.ruiz@clinicadb.com', NULL, 'M-12345', 3),
('Lucía', 'Fernández', '1982-03-30', '1133445566', '28333444', 'lucia.fernandez@clinicadb.com', NULL, 'M-67890', 4);
GO

INSERT INTO Recepcionistas (Nombre, Apellido, FechaNacimiento, Email, Telefono, Dni, UrlImagen, IdUsuario)
VALUES
('Paula', 'Martínez', '1995-10-02', 'paula.martinez@clinicadb.com', '1145678910', '35999888', NULL, 5);
GO

INSERT INTO EspecialidadesPorMedico (IdMedico, IdEspecialidad)
VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4);
GO


select * from Medicos
select * from EspecialidadesPorMedico
select * from usuarios
select * from pacientes
SELECT * FROM Turnos
select * from Estados

INSERT INTO Turnos (Fecha, Observaciones, IdPaciente, IdEstado, IdMedico, IdEspecialidad)
VALUES
('2025-08-11T14:10:30', 'Turno de prueba', 2, 1, 1, 2);
GO
