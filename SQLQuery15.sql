GO
ALTER PROCEDURE INS_UGOVOR
(
	
	@NazivKluba VARCHAR(30),
	@AdresaKluba VARCHAR(35),
	@NazivFabrike VARCHAR(30),
	@AdresaFabrike VARCHAR(30)
)

AS 

	DECLARE @nazKluba VARCHAR(30)='',
			@nazFabrike VARCHAR(30)='',
			@idKluba int=-1,
			@idFabrike int=-1;


	DECLARE CURSOR_KLUB2 CURSOR FOR
	SELECT IdKl 
	FROM Klubovi
	WHERE  NazKl=@NazivKluba
	DECLARE
		@Idkl int

	DECLARE CURSOR_FABRIKA2 CURSOR FOR
	SELECT IdFab 
	FROM FabrikaReketas
	WHERE  NazFab=@NazivFabrike
	DECLARE
		@IdFab int
	

	BEGIN 
		INSERT INTO [dbo].[Klubovi] (NazKl,AdrKl) VALUES (@NazivKluba,@AdresaKluba);
		INSERT INTO [dbo].[FabrikaReketas] (NazFab,AdrFab) VALUES (@NazivFabrike,@AdresaFabrike);

		OPEN CURSOR_KLUB2
		FETCH NEXT FROM CURSOR_KLUB2 INTO @IdKl

		WHILE @@FETCH_STATUS =0
			BEGIN
				
				SET @idKluba = @IdKl;

				FETCH NEXT FROM CURSOR_KLUB2 INTO @IdKl

			END
		CLOSE CURSOR_KLUB2;
		DEALLOCATE CURSOR_KLUB2;


		OPEN CURSOR_FABRIKA2
		FETCH NEXT FROM CURSOR_FABRIKA2 INTO @IdKl

		WHILE @@FETCH_STATUS =0
			BEGIN
				
				SET @idFabrike = @IdFab;

				FETCH NEXT FROM CURSOR_FABRIKA2 INTO @IdFab

			END
		CLOSE CURSOR_FABRIKA2;
		DEALLOCATE CURSOR_FABRIKA2;

		PRINT @idFabrike
		PRINT @idKluba
		INSERT INTO [dbo].[ima_ugovors] (KlubIdKl,FabrikaReketaIdFab) VALUES (@idKluba,@idFabrike);

	END


GO