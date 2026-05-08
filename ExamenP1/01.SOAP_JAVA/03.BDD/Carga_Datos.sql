-- =============================================
-- Datos de prueba para ticketpremium_db
-- =============================================
USE ticketpremium_db;

-- =============================================
-- 5 registros en PARTIDO_FUTBOL (fechas futuras)
-- =============================================
INSERT INTO PARTIDO_FUTBOL (EQUIPO_LOCAL, EQUIPO_VISITA, FECHA, LUGAR) VALUES
('Barcelona SC',            'Liga de Quito',        '2026-06-15 16:00:00', 'Estadio Monumental Banco Pichincha - Guayaquil'),
('Emelec',                  'Deportivo Cuenca',     '2026-06-22 19:00:00', 'Estadio George Capwell - Guayaquil'),
('Independiente del Valle', 'El Nacional',          '2026-07-01 15:00:00', 'Estadio Rumiñahui - Sangolquí'),
('Aucas',                   'Mushuc Runa',          '2026-07-10 12:00:00', 'Estadio Gonzalo Pozo Ripalda - Quito'),
('Universidad Católica',    'Macará',               '2026-08-05 18:00:00', 'Estadio Olímpico Atahualpa - Quito');

-- =============================================
-- 20 registros en LOCALIDAD_PARTIDO
-- (4 localidades x 5 partidos = 20)
-- =============================================

-- Partido 1: Barcelona SC vs Liga de Quito
INSERT INTO LOCALIDAD_PARTIDO (CODIGO_PARTIDO, CODIGO_LOCALIDAD, DISPONIBILIDAD, PRECIO) VALUES
(1, 'PALCO',            50,  45.00),
(1, 'TRIBUNA',         300,  25.00),
(1, 'GENERAL',        2000,   8.00),
(1, 'GENERAL_VISITA', 1500,   8.00);

-- Partido 2: Emelec vs Deportivo Cuenca
INSERT INTO LOCALIDAD_PARTIDO (CODIGO_PARTIDO, CODIGO_LOCALIDAD, DISPONIBILIDAD, PRECIO) VALUES
(2, 'PALCO',            40,  50.00),
(2, 'TRIBUNA',         250,  28.00),
(2, 'GENERAL',        1800,  10.00),
(2, 'GENERAL_VISITA', 1200,  10.00);

-- Partido 3: Independiente del Valle vs El Nacional
INSERT INTO LOCALIDAD_PARTIDO (CODIGO_PARTIDO, CODIGO_LOCALIDAD, DISPONIBILIDAD, PRECIO) VALUES
(3, 'PALCO',            30,  40.00),
(3, 'TRIBUNA',         200,  22.00),
(3, 'GENERAL',        1500,   7.00),
(3, 'GENERAL_VISITA', 1000,   7.00);

-- Partido 4: Aucas vs Mushuc Runa
INSERT INTO LOCALIDAD_PARTIDO (CODIGO_PARTIDO, CODIGO_LOCALIDAD, DISPONIBILIDAD, PRECIO) VALUES
(4, 'PALCO',            25,  35.00),
(4, 'TRIBUNA',         180,  20.00),
(4, 'GENERAL',        1200,   6.00),
(4, 'GENERAL_VISITA',  800,   6.00);

-- Partido 5: Universidad Católica vs Macará
INSERT INTO LOCALIDAD_PARTIDO (CODIGO_PARTIDO, CODIGO_LOCALIDAD, DISPONIBILIDAD, PRECIO) VALUES
(5, 'PALCO',            20,  30.00),
(5, 'TRIBUNA',         150,  18.00),
(5, 'GENERAL',        1000,   5.00),
(5, 'GENERAL_VISITA',  700,   5.00);
