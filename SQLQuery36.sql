CREATE TRIGGER FabrikaTrigger 
ON [dbo].[FabrikaReketas]
FOR INSERT
AS

  IF ( (SELECT avg(Igraci_Profesionalac.IdIgr) from Igraci_Profesionalac
		JOIN Klubovi on Igraci_Profesionalac.KlubIdKl=Klubovi.IdKl
		JOIN Treners on Klubovi.IdKl=Treners.KlubIdKl) =100)
		INSERT INTO [dbo].[FabrikaReketas] (NazFab,AdrFab) VALUES ('fabrika nad fabrikama','na kraj sveta')

