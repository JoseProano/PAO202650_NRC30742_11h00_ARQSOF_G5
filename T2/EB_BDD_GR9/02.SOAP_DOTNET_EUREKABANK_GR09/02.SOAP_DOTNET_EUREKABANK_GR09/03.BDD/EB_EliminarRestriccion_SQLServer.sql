--Primero usar base de datos
USE eurekabank;
GO

--Segundo verificar en  base de datos
EXEC sp_help 'movimiento';
GO

--Tercer verificar la tabla de definiciones 
--Se debe tener 2 al inicio (importe) (importe4)
SELECT name, definition 
FROM sys.check_constraints 
WHERE parent_object_id = OBJECT_ID('movimiento');
GO

--Eliminar la restricción de importe
ALTER TABLE movimiento DROP CONSTRAINT chk_Movimiento_importe;
GO

--Cambiar la restriccion de importe4 (1era forma) USADA
ALTER TABLE movimiento ADD CONSTRAINT chk_Movimiento_importe4 CHECK (dec_moviimporte <= 0 OR dec_moviimporte > 0);;
GO

--Cambiar restriccion de importe4 (2da forma) NO USADA
ALTER TABLE movimiento ADD CONSTRAINT chk_Movimiento_importe4 CHECK (dec_moviimporte <> 0);
GO


