CREATE DATABASE iGameOthelloDB

USE iGameOthelloDB

CREATE TABLE Usuarios
	(
	idUsuario int IDENTITY(1,1) PRIMARY KEY,
	NombreUsuario nvarchar(50),
	Nombre nvarchar(50),
	Apellido nvarchar(50),
	Contrasena nvarchar(50),
	Pais nvarchar(10),
	Email nvarchar(50),
	Nacimiento date
	)

CREATE TABLE Torneo
	(
	idTorneo int IDENTITY(1,1) PRIMARY KEY,
	Tipo nvarchar(50),
	Estado nvarchar(50),
	Ganador nvarchar(50),
	)

	CREATE TABLE UsuarioTorneo
	(
	idUsuarioPartida int IDENTITY(1,1) PRIMARY KEY,
	idUsuario int FOREIGN KEY REFERENCES Usuarios(idUsuario) ON UPDATE CASCADE ON DELETE CASCADE,
	idTorneo int FOREIGN KEY REFERENCES Torneo(idTorneo) ON UPDATE CASCADE ON DELETE CASCADE
	)

CREATE TABLE Ronda
	(
	idRonda int IDENTITY(1,1) PRIMARY KEY,
	NumeroRonda nvarchar(50),
	idTorneo int FOREIGN KEY REFERENCES Torneo(idTorneo) ON UPDATE CASCADE ON DELETE CASCADE
	)

CREATE TABLE Partida
	(
	idPartida int IDENTITY(1,1) PRIMARY KEY,
	Modo nvarchar(50),
	Estado nvarchar(50),
	Movimientos xml,
	idRonda int FOREIGN KEY REFERENCES Ronda(idRonda) ON UPDATE CASCADE ON DELETE CASCADE
	)

CREATE TABLE ResultadoPartida
	(
	idResultado int IDENTITY(1,1) PRIMARY KEY,
	Ganador nvarchar(50),
	Perdedor nvarchar(50),
	Apellido nvarchar(50),
	Empate nvarchar(50),
	idPartida int FOREIGN KEY REFERENCES Partida(idPartida) ON UPDATE CASCADE ON DELETE CASCADE
	)

CREATE TABLE UsuarioPartida
	(
	idUsuarioPartida int IDENTITY(1,1) PRIMARY KEY,
	idUsuario int FOREIGN KEY REFERENCES Usuarios(idUsuario) ON UPDATE CASCADE ON DELETE CASCADE,
	idPartida int FOREIGN KEY REFERENCES Partida(idPartida) ON UPDATE CASCADE ON DELETE CASCADE,
	ColorFicha nvarchar(50)
	)