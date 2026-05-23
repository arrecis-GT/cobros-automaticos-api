CREATE DATABASE CobrosAutomaticos;

GO
USE CobrosAutomaticos;

GO
CREATE TABLE Usuario (
    usuario_id INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(50) NOT NULL,
    password NVARCHAR(255) NOT NULL,
    status CHAR(1) NOT NULL
);
GO

CREATE TABLE Cliente (
    cliente_id INT IDENTITY(1,1) PRIMARY KEY,
    dpi NVARCHAR(13) NOT NULL,
    nombre NVARCHAR(100) NOT NULL,
    email NVARCHAR(100) NULL,
    telefono NVARCHAR(20) NULL,
    status CHAR(1) NOT NULL
);
GO

CREATE TABLE Auditoria (
    auditoria_id INT IDENTITY(1,1) PRIMARY KEY,
    usuario_id INT NOT NULL,
    evento NVARCHAR(255) NOT NULL,
    estado_evento NVARCHAR (255) NOT NULL,
    resumen_payload NVARCHAR(MAX) NOT NULL,
    fecha_creacion DATE NOT NULL,
    hora_creacion TIME(0) NOT NULL,
    status CHAR(1) NOT NULL,
    CONSTRAINT FK_Auditoria_Usuario FOREIGN KEY (usuario_id)
    REFERENCES Usuario(usuario_id)
);
GO

CREATE TABLE Sesion (
    sesion_id INT IDENTITY(1,1) PRIMARY KEY,
    usuario_id INT,
    token NVARCHAR(500) NOT NULL,
    fecha_creacion DATE NOT NULL,
    hora_creacion TIME(0) NOT NULL,
    ultima_conexion TIME(0) NOT NULL,
    status CHAR(1) NOT NULL,
    CONSTRAINT FK_Sesion_Usuario FOREIGN KEY (usuario_id) 
    REFERENCES Usuario(usuario_id)
);
GO

CREATE TABLE Cobro (
    cobro_id INT IDENTITY(1,1) PRIMARY KEY,
    cliente_id INT NOT NULL,
    monto DECIMAL(18,2) NOT NULL,
    moneda CHAR(3) NOT NULL,
    estado NVARCHAR(50) NOT NULL,
    fecha_creacion DATE NOT NULL,
    fecha_proceso DATE,
    hora_proceso TIME(0),
    referencia_externa NVARCHAR(100) NOT NULL,
    status CHAR(1) NOT NULL,
    CONSTRAINT FK_Cobro_Cliente FOREIGN KEY (cliente_id) 
    REFERENCES Cliente(cliente_id)
);
GO
