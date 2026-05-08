SELECT * FROM dbo.Cuenta
WHERE chr_cuencodigo = '00100002';
00100001
00100002
SELECT TOP 10 * FROM dbo.Movimiento
WHERE chr_cuencodigo = '00200001'
ORDER BY int_movinumero DESC;

SELECT * FROM dbo.TipoMovimiento WHERE chr_tipocodigo = '003';
SELECT TOP 10 * FROM dbo.Movimiento;
SELECT * FROM dbo.TipoMovimiento;


USE [master];
GO
CREATE LOGIN [IIS APPPOOL\EurekaBank] FROM WINDOWS;
GO
USE [EUREKABANK];
GO
CREATE USER [IIS APPPOOL\EurekaBank] FOR LOGIN [IIS APPPOOL\EurekaBank];
GO
ALTER ROLE [db_datareader] ADD MEMBER [IIS APPPOOL\EurekaBank];
ALTER ROLE [db_datawriter] ADD MEMBER [IIS APPPOOL\EurekaBank];
GO


SELECT 
    m.chr_cuencodigo AS Cuenta,
    m.int_movinumero AS NumeroMovimiento,
    m.dtt_movifecha AS Fecha,
    tm.vch_tipodescripcion AS Tipo,
    tm.vch_tipoaccion AS Acción
FROM dbo.Movimiento m
INNER JOIN dbo.TipoMovimiento tm ON m.chr_tipocodigo = tm.chr_tipocodigo
WHERE m.chr_cuencodigo = '00200001'
ORDER BY m.dtt_movifecha DESC, m.int_movinumero DESC;


DECLARE @ultimoMovNum INT;

-- Obtener el último número de movimiento para la cuenta
SELECT @ultimoMovNum = ISNULL(MAX(int_movinumero), 0)
FROM dbo.Movimiento
WHERE chr_cuencodigo = '00200001';

-- Insertar nuevo movimiento de retiro
