CREATE PROCEDURE SP_ProcesarCobroIndividual
    @CobroId INT,
    @UsuarioId INT,
    @Payload NVARCHAR(MAX),
    @Resultado BIT OUTPUT
AS
BEGIN

    DECLARE @Monto DECIMAL(18,2);
    DECLARE @Estado NVARCHAR(50);
    DECLARE @NuevoEstado NVARCHAR(50);
    DECLARE @FechaHoraActual DATETIME = GETDATE();

    BEGIN TRY
        BEGIN TRANSACTION
            -- Se consulta el id ingreasado
            SELECT
                @Estado = estado,
                @Monto =  monto
            FROM Cobro
            WHERE cobro_id = @CobroId;

            -- Se valida si hay resultado de la consulta o sino se descartan cambios
            IF @@ROWCOUNT = 0
            BEGIN
                IF @@TRANCOUNT > 0
                    ROLLBACK TRANSACTION;
                
                SET @Resultado = 0;
                RETURN;
            END

        -- Se validan si el cobro tiene un estado que ya no necesita procesarse (FALLIDO o PROCESADO) y se registra la acción en Auditoria 
        IF @Estado != 'PENDIENTE'
        BEGIN
            INSERT INTO Auditoria
                (usuario_id, evento, resultado_evento, resumen_payload, fecha_creacion, hora_creacion, status)
            VALUES  
                (@UsuarioId, 'COBRO INDIVIDUAL', CONCAT('El cobro ya se encuentra ', @Estado), @Payload, @FechaHoraActual, @FechaHoraActual, 'A');

            SET @Resultado = 0;
            COMMIT TRANSACTION;
            RETURN;
        END


        -- Se determina el estado del Cobro al validar condicion de monto
        IF @Monto > 1000
        BEGIN
            SET @NuevoEstado = 'FALLIDO';
            SET @Resultado = 0;
        END
        ELSE
            SET @NuevoEstado = 'PROCESADO';

        -- Se actualiza el Cobro con su nuevo estado, fecha de proceso y hora de proceso
        UPDATE Cobro SET
            estado = @NuevoEstado,
            fecha_proceso = @FechaHoraActual,
            hora_proceso = @FechaHoraActual
        WHERE cobro_id = @CobroId;

        -- Se reigstra la acción en auditoria
        INSERT INTO Auditoria
            (usuario_id, evento, resultado_evento, resumen_payload, fecha_creacion, hora_creacion, status)
        VALUES  
            (@UsuarioId, 'COBRO INDIVIDUAL', CONCAT('Cobro ', @NuevoEstado), @Payload, @FechaHoraActual, @FechaHoraActual, 'A');

        SET @Resultado = 1;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END