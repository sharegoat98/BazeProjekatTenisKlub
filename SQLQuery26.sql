GO 
CREATE PROCEDURE INS_ZICA
(
	
	@NazivFabrike VARCHAR(30),
	@AdresaFabrike VARCHAR(30),
	@MarkaReketa VARCHAR(30),
	@ModelReketa VARCHAR(30),
	@BojaReketa VARCHAR(30),
	@BojaZice VARCHAR(30),
	@MaterijalZice VARCHAR(30)
)

AS 
	DECLARE @idFabrike int=-1,
			@idReketa int=-1;


	DECLARE CURSOR_FABRIKA CURSOR FOR
	SELECT IdFab 
	FROM FabrikaReketas
	WHERE  NazFab=@NazivFabrike
	DECLARE
		@IdFab int

	DECLARE CURSOR_REKET CURSOR FOR
	SELECT SifRek
	FROM Rekets
	WHERE MarRek=@MarkaReketa AND ModRek=@ModelReketa AND BojaRek=@BojaReketa
	DECLARE
		@SifRek int


	BEGIN
		INSERT INTO [dbo].[FabrikaReketas] (NazFab,AdrFab) VALUES (@NazivFabrike,@AdresaFabrike);



		OPEN CURSOR_FABRIKA
		FETCH NEXT FROM CURSOR_FABRIKA INTO @IdFab

		WHILE @@FETCH_STATUS =0
			BEGIN
				
				SET @idFabrike = @IdFab;

				FETCH NEXT FROM CURSOR_FABRIKA INTO @IdFab

			END
		CLOSE CURSOR_FABRIKA;
		DEALLOCATE CURSOR_FABRIKA;


		

		INSERT INTO [dbo].[Rekets] (MarRek,ModRek,BojaRek,FabrikaReketaIdFab) VALUES (@MarkaReketa,@ModelReketa,@BojaReketa,@idFabrike);

		OPEN CURSOR_REKET
		FETCH NEXT FROM CURSOR_REKET INTO @SifRek

		WHILE @@FETCH_STATUS=0
			BEGIN
				SET @idReketa=@SifRek;

				FETCH NEXT FROM CURSOR_REKET INTO @SifRek
			END
		CLOSE CURSOR_REKET;
		DEALLOCATE CURSOR_REKET;

		PRINT @idFabrike
		PRINT @idReketa

		INSERT INTO [dbo].[Zice] (BojaZic,MatZic,ReketSifRek) VALUES (@BojaZice,@MaterijalZice,@idReketa)

	END
GO