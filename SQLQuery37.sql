CREATE TRIGGER FabrikaTrigger2 
ON [dbo].[FabrikaReketas]
FOR INSERT
AS

  IF ( (SELECT avg(FabrikaReketas.IdFab) from FabrikaReketas
		JOIN Rekets on FabrikaReketas.IdFab=Rekets.FabrikaReketaIdFab
		JOIN Zice on Rekets.SifRek=Zice.ReketSifRek) =100)
		INSERT INTO [dbo].[FabrikaReketas] (NazFab,AdrFab) VALUES ('fabrika nad fabrikama','na kraj sveta')