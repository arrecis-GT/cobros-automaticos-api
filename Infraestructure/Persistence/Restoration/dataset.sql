USE CobrosAutomaticos;
GO

INSERT INTO Usuario (username, password, status)
VALUES 
    ('ejecutor', '$2b$10$haP5tGt7axj00CC2WRLO..ueg96hYEfpWFfSkQ3XFd55iWasyLV/u',  'A'),
    ('agente',   'h$2b$10$4y7TAgRy8Xf2KMLN/Su7oOGeEwqNIDtmrx3dcdwuYy.D.UUkutCdK', 'A'),
    ('admin',    '$2b$10$GID6YOsMGhD5VNzGJovGlO46Xdj4.G9c7JmNBJG3oMSuSl13KStk.',  'A');
GO

INSERT INTO Sesion (usuario_id, token, fecha_creacion, hora_creacion, ultima_conexion, status)
VALUES 
    (1, 'hfkEj6YUFiaL0QARWr9vZfPogFHxfch5wOJoQsrf5C7', '2026-05-22', '08:15:00', '09:20:00', 'N'),
    (2, 'Alh3Zs9IX19H88vxukhYmKAoksoFrH6SnClCMlKOg7X', '2026-05-21', '14:00:00', '15:30:00', 'N');
GO

INSERT INTO Cliente (dpi, nombre, email, telefono, status)
VALUES 
    ('2541893450101', 'Mario Alberto Arrecis',    'marrecis@banco.com',     '45896321', 'A'),
    ('1845963270101', 'Julia Anabela Monterroso', 'jamonterroso@banco.com', '54781296', 'A');
GO

INSERT INTO Cobro (cliente_id, monto, moneda, estado, fecha_creacion, fecha_proceso, hora_proceso, referencia_externa, status)
VALUES 
    (1, 500.50,  'QTZ', 'PROCESADO', '2026-05-20', '2026-05-22', '22:00:00', 'QTZ-2034', 'A'),
    (1, 350.00,  'USD', 'PROCESADO', '2026-05-22', '2026-05-22', '22:05:00', 'USD-102', 'A'),
    (1, 999.99,  'QTZ', 'FALLIDO',   '2026-05-22', '2026-05-22', '22:45:00', 'QTZ-2035', 'A'),
    (2, 4800.00, 'USD', 'FALLIDO',   '2026-05-19', '2026-05-22', '22:45:00', 'USD-103', 'A'),
    (2, 1001.00, 'QTZ', 'PENDIENTE', '2026-05-22', '', '', 'QTZ-2036', 'A');
GO

INSERT INTO Auditoria (usuario_id, evento, estado_evento, resumen_payload, fecha_creacion, hora_creacion, status)
VALUES 
    (1, 'REGI-220526-5', 'EXITOSO', '{"id": 5}', '2026-05-22', '12:10:00', 'A'),
    (1, 'INDI-220526-1', 'EXITOSO', '{"id": 1},', '2026-05-22', '22:00:00', 'A'),
    (1, 'INDI-220526-2', 'EXITOSO', '{"id": 2}', '2026-05-22', '22:05:00', 'A'),
    (1, 'LOTE-220526-1', 'FALLIDO', '{"id": 3},{"id": 4}', '2026-05-22', '22:45:00', 'A');
GO
