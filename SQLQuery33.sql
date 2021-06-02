CREATE FUNCTION f_profesionalaca_koji_imaju_trenera
() RETURNS DECIMAL AS
	BEGIN
		DECLARE @ret_val AS DECIMAL
		SELECT @ret_val=COUNT(Igraci_Profesionalac.IdIgr) from Igraci_Profesionalac
		JOIN Klubovi on Igraci_Profesionalac.KlubIdKl=Klubovi.IdKl
		JOIN Treners on Klubovi.IdKl=Treners.KlubIdKl;

		RETURN @ret_val
	END;
			