

CREATE TYPE ListaCobros AS TABLE (
   cobro_id INT PRIMARY KEY
);
GO


CREATE PROCEDURE SP_ProcesarCobroLote
    @LoteCobros ListaCobros READONLY,
    @UsuarioId INT,
    @Payload NVARCHAR(MAX),
    @Resultado INT OUTPUT
AS
BEGIN

    DECLARE @FechaHoraActual DATETIME = GETDATE();

    -- Se crea tabla temporal para almacenar los cobros con su nuevo estado
    CREATE TABLE #LoteCobros (
        cobro_id INT PRIMARY KEY,
        cambio_estado BIT,
        nuevo_estado NVARCHAR(50),
    );

    -- Se insertan los cobros con sus nuevos estados en la temporal
    INSERT INTO #LoteCobros (cobro_id, cambio_estado, nuevo_estado)
    SELECT 
        C.cobro_id,
        CASE 
            WHEN C.estado = 'PENDIENTE' THEN 1 
            ELSE 0 
        END,
        CASE
            WHEN C.estado != 'PENDIENTE' THEN C.estado
            WHEN C.monto > 1000 THEN 'FALLIDO'
            ELSE 'PROCESADO'
        END
    FROM Cobro C
    INNER JOIN @LoteCobros L ON C.cobro_id = L.cobro_id;
 

    -- Se valida si la tabla contiene almentos un registro de lo contrario ya no inicia la transacción
    IF NOT EXISTS (SELECT 1 FROM #LoteCobros)
    BEGIN
        SET @Resultado = 0;
        RETURN;
    END
    
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Se actualizan los cobros siempre y cuando su estado nuevo sea procesado
        UPDATE C
        SET 
            C.estado = temp.nuevo_estado,
            C.fecha_proceso = @FechaHoraActual,
            C.hora_proceso = @FechaHoraActual
        FROM Cobro C
        INNER JOIN #LoteCobros temp ON C.cobro_id = temp.cobro_id
        WHERE temp.nuevo_estado = 'PROCESADO'
        AND   temp.cambio_estado = 1;

        SET @Resultado = @@ROWCOUNT;
        
        -- Se inserta en auditoria el evento
        INSERT INTO Auditoria 
            (usuario_id, evento, resumen_payload, fecha_creacion, hora_creacion, status)
        VALUES
            ( @UsuarioId, 'COBRO LOTE', @Payload, @FechaHoraActual, @FechaHoraActual, 'A')

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END