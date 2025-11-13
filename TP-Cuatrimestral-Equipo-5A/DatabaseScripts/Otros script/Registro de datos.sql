USE CLINICA_DB;
GO

INSERT INTO Permisos (Descripcion)
VALUES 
('Paciente'), ('Medico'), ('Recepcionista'), ('Administrador');
GO

INSERT INTO DiasSemana (Descripcion)
VALUES 
('Lunes'), ('Martes'), ('Miércoles'), ('Jueves'), ('Viernes'), ('Sábado'), ('Domingo');
GO

INSERT INTO Estados (Descripcion)
VALUES 
('Nuevo'), ('Reprogramado'), ('Cancelado'), ('No Asistió'), ('Cerrado');
GO

INSERT INTO Especialidades (Descripcion)
VALUES
('Clínica Médica'), -- Id 1
('Cardiología'),    -- Id 2
('Pediatría'),      -- Id 3
('Dermatología'),   -- Id 4
('Traumatología'),  -- Id 5
('Ginecología');    -- Id 6
GO

INSERT INTO Usuarios (Usuario, Clave, IdPermiso, Activo)
VALUES
('jperez', '1234', 1, 1),       -- IdUsuario 1 (Paciente)
('mgomez', '1234', 1, 1),       -- IdUsuario 2 (Paciente)
('mlopez', '1234', 1, 1),       -- IdUsuario 3 (Paciente)
('asanchez', '1234', 1, 1),     -- IdUsuario 4 (Paciente)
('cruiz', 'abcd', 2, 1),        -- IdUsuario 5 (Medico)
('lfernandez', 'abcd', 2, 1),   -- IdUsuario 6 (Medico)
('dmoreno', 'abcd', 2, 1),      -- IdUsuario 7 (Medico)
('pmartinez', 'qwerty', 3, 1),  -- IdUsuario 8 (Recepcionista)
('admin', 'admin123', 4, 1);    -- IdUsuario 9 (Admin)
GO

INSERT INTO Pacientes (Nombre, Apellido, FechaNacimiento, Email, Telefono, Dni, IdUsuario)
VALUES
('Juan', 'Pérez', '1990-05-10', 'juan.perez@mail.com', '1122334455', '40123456', 1),
('María', 'Gómez', '1985-07-22', 'maria.gomez@mail.com', '1199887766', '37222444', 2),
('Martín', 'López', '2005-01-15', 'martin.lopez@mail.com', '1155667788', '45111222', 3),
('Ana', 'Sánchez', '1998-11-30', 'ana.sanchez@mail.com', '1177889900', '41555666', 4);
GO
INSERT INTO Medicos (Nombre, Apellido, FechaNacimiento, Telefono, Dni, Email, Matricula, IdUsuario)
VALUES
('Carlos', 'Ruiz', '1978-09-14', '1144556677', '30111222', 'carlos.ruiz@clinicadb.com', 'M-12345', 5),
('Lucía', 'Fernández', '1982-03-30', '1133445566', '28333444', 'lucia.fernandez@clinicadb.com', 'M-67890', 6),
('Diego', 'Moreno', '1980-06-20', '1122334411', '31555888', 'diego.moreno@clinicadb.com', 'M-54321', 7);
GO
INSERT INTO Recepcionistas (Nombre, Apellido, FechaNacimiento, Email, Telefono, Dni, IdUsuario)
VALUES
('Paula', 'Martínez', '1995-10-02', 'paula.martinez@clinicadb.com', '1145678910', '35999888', 8);
GO

INSERT INTO EspecialidadesPorMedico (IdMedico, IdEspecialidad)
VALUES
(1, 1), -- Carlos Ruiz -> Clínica Médica
(1, 2), -- Carlos Ruiz -> Cardiología
(2, 3), -- Lucía Fernández -> Pediatría
(2, 4), -- Lucía Fernández -> Dermatología
(3, 1), -- Diego Moreno -> Clínica Médica
(3, 5); -- Diego Moreno -> Traumatología
GO

INSERT INTO HorariosPorMedicos (IdDiaSemana, HoraEntrada, HoraSalida, IdMedico, IdEspecialidad) 
VALUES (1, '09:00:00', '12:00:00', 1, 1); -- Lunes 9-12 (Clínica)
INSERT INTO HorariosPorMedicos (IdDiaSemana, HoraEntrada, HoraSalida, IdMedico, IdEspecialidad) 
VALUES (3, '14:00:00', '18:00:00', 1, 2); -- Miércoles 14-18 (Cardio)

INSERT INTO HorariosPorMedicos (IdDiaSemana, HoraEntrada, HoraSalida, IdMedico, IdEspecialidad) 
VALUES (2, '08:00:00', '13:00:00', 2, 3); -- Martes 8-13 (Pediatría)
INSERT INTO HorariosPorMedicos (IdDiaSemana, HoraEntrada, HoraSalida, IdMedico, IdEspecialidad) 
VALUES (4, '10:00:00', '16:00:00', 2, 4); -- Jueves 10-16 (Derma)

INSERT INTO HorariosPorMedicos (IdDiaSemana, HoraEntrada, HoraSalida, IdMedico, IdEspecialidad) 
VALUES (5, '09:00:00', '17:00:00', 3, 5); -- Viernes 9-17 (Trauma)
GO


INSERT INTO Turnos (Fecha, Observaciones, IdPaciente, IdEstado, IdMedico, IdEspecialidad) VALUES 
('2025-11-17 09:00:00', 'Chequeo', 1, 1, 1, 1), -- Lunes (Carlos, Clínica)
('2025-11-17 09:30:00', NULL, 2, 1, 1, 1),
('2025-11-19 14:00:00', 'Control anual', 1, 1, 1, 2), -- Miércoles (Carlos, Cardio)
('2025-11-19 14:30:00', 'Revisión marcapasos', 2, 1, 1, 2),
('2025-11-26 15:00:00', NULL, 3, 1, 1, 2),
('2025-11-18 09:00:00', 'Vacunación', 3, 1, 2, 3), -- Martes (Lucía, Pedia)
('2025-11-18 09:30:00', 'Control niño sano', 4, 1, 2, 3),
('2025-11-20 10:00:00', 'Consulta por mancha', 1, 1, 2, 4), -- Jueves (Lucía, Derma)
('2025-11-21 10:00:00', 'Dolor de rodilla', 2, 1, 3, 5), -- Viernes (Diego, Trauma)
('2025-11-21 10:30:00', 'Revisión post-yeso', 4, 1, 3, 5),
('2025-11-28 11:00:00', NULL, 1, 1, 3, 5);

INSERT INTO Turnos (Fecha, Observaciones, IdPaciente, IdEstado, IdMedico, IdEspecialidad) VALUES 
('2024-10-14 10:00:00', 'Chequeo general', 1, 5, 1, 1), -- ID Turno 12
('2024-10-14 10:30:00', 'Consulta gripe', 2, 5, 1, 1), -- ID Turno 13
('2024-10-17 11:00:00', 'Revisión de alergias', 4, 5, 2, 4), -- ID Turno 14
('2024-10-17 11:30:00', 'Control lunar', 1, 5, 2, 4), -- ID Turno 15
('2024-10-18 14:00:00', 'Esguince de tobillo', 3, 5, 3, 5), -- ID Turno 16
('2024-10-18 14:30:00', 'Dolor lumbar', 2, 5, 3, 5), -- ID Turno 17
('2024-10-21 09:00:00', 'Paciente canceló', 4, 3, 1, 1), -- ID Turno 18
('2024-10-22 08:00:00', NULL, 1, 4, 2, 3), -- ID Turno 19
('2024-10-25 10:00:00', 'Llamó para reprogramar', 2, 2, 3, 5); -- ID Turno 20
GO

INSERT INTO HistoriasClinicas (Fecha, IdPaciente, Asunto, Descripcion, IdTurno, IdMedico, IdEspecialidad) VALUES 
('2024-10-14 10:00:00', 1, 'Chequeo general', 'Paciente acude para chequeo anual. Presión arterial estable.', 12, 1, 1),
('2024-10-14 10:30:00', 2, 'Consulta gripe', 'Presenta fiebre y tos. Se receta paracetamol.', 13, 1, 1),
('2024-10-17 11:00:00', 4, 'Revisión de alergias', 'Erupción cutánea por alergia. Se receta antihistamínico.', 14, 2, 4),
('2024-10-17 11:30:00', 1, 'Control lunar', 'Se revisa lunar en espalda. Sin cambios. Control en 6 meses.', 15, 2, 4),
('2024-10-18 14:00:00', 3, 'Esguince de tobillo', 'Esguince Grado 1. Vendaje y reposo.', 16, 3, 5),
('2024-10-18 14:30:00', 2, 'Dolor lumbar', 'Contractura por mala postura. Se indica fisioterapia.', 17, 3, 5);

INSERT INTO HistoriasClinicas (Fecha, IdPaciente, Asunto, Descripcion, IdTurno, IdMedico, IdEspecialidad) VALUES 
('2024-10-21 09:00:00', 4, 'Cancelación', 'Paciente canceló telefónicamente.', 18, 1, 1);

INSERT INTO HistoriasClinicas (Fecha, IdPaciente, Asunto, Descripcion, IdTurno, IdMedico, IdEspecialidad) VALUES 
('2024-09-15 11:00:00', 1, 'Atención Guardia (Trauma)', 'Paciente ingresa por corte en la mano.', NULL, 3, 5),
('2024-09-20 17:30:00', 2, 'Atención Guardia (Clínica)', 'Reacción alérgica leve.', NULL, 1, 1);
GO


