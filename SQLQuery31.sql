ALTER FUNCTION f_prosecan_broj_reketa_iz_fabrike_koji_ima_zice
() RETURNS DECIMAL AS
	BEGIN
		DECLARE @prosek AS DECIMAL
		SELECT @prosek=COUNT(FabrikaReketas.NazFab) from FabrikaReketas
		JOIN Rekets on FabrikaReketas.IdFab=Rekets.FabrikaReketaIdFab
		JOIN Zice on Rekets.SifRek=Zice.ReketSifRek;

		RETURN @prosek
	END;
			