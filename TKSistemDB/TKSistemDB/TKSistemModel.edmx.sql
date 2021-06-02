
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/20/2021 20:53:19
-- Generated from EDMX file: D:\cetvrta godina\Baze 2\Projekat\TKSistemDB\TKSistemDB\TKSistemModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TKDataBase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_FabrikaReketaReket]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rekets] DROP CONSTRAINT [FK_FabrikaReketaReket];
GO
IF OBJECT_ID(N'[dbo].[FK_ZicaReket]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Zice] DROP CONSTRAINT [FK_ZicaReket];
GO
IF OBJECT_ID(N'[dbo].[FK_ZicaIgrac]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Zice] DROP CONSTRAINT [FK_ZicaIgrac];
GO
IF OBJECT_ID(N'[dbo].[FK_ProfesionalacKlub]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Igraci_Profesionalac] DROP CONSTRAINT [FK_ProfesionalacKlub];
GO
IF OBJECT_ID(N'[dbo].[FK_TrenerKlub]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Treners] DROP CONSTRAINT [FK_TrenerKlub];
GO
IF OBJECT_ID(N'[dbo].[FK_FabrikaReketaima_ugovor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ima_ugovors] DROP CONSTRAINT [FK_FabrikaReketaima_ugovor];
GO
IF OBJECT_ID(N'[dbo].[FK_ima_ugovorKlub]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ima_ugovors] DROP CONSTRAINT [FK_ima_ugovorKlub];
GO
IF OBJECT_ID(N'[dbo].[FK_ima_ugovorTurnir]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Turnirs] DROP CONSTRAINT [FK_ima_ugovorTurnir];
GO
IF OBJECT_ID(N'[dbo].[FK_Profesionalac_inherits_Igrac]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Igraci_Profesionalac] DROP CONSTRAINT [FK_Profesionalac_inherits_Igrac];
GO
IF OBJECT_ID(N'[dbo].[FK_Rekreativac_inherits_Igrac]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Igraci_Rekreativac] DROP CONSTRAINT [FK_Rekreativac_inherits_Igrac];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FabrikaReketas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FabrikaReketas];
GO
IF OBJECT_ID(N'[dbo].[Rekets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rekets];
GO
IF OBJECT_ID(N'[dbo].[Zice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Zice];
GO
IF OBJECT_ID(N'[dbo].[Igraci]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Igraci];
GO
IF OBJECT_ID(N'[dbo].[Klubovi]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Klubovi];
GO
IF OBJECT_ID(N'[dbo].[Treners]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Treners];
GO
IF OBJECT_ID(N'[dbo].[ima_ugovors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ima_ugovors];
GO
IF OBJECT_ID(N'[dbo].[Turnirs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Turnirs];
GO
IF OBJECT_ID(N'[dbo].[Igraci_Profesionalac]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Igraci_Profesionalac];
GO
IF OBJECT_ID(N'[dbo].[Igraci_Rekreativac]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Igraci_Rekreativac];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'FabrikaReketas'
CREATE TABLE [dbo].[FabrikaReketas] (
    [IdFab] int IDENTITY(1,1) NOT NULL,
    [AdrFab] nvarchar(max)  NOT NULL,
    [NazFab] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Rekets'
CREATE TABLE [dbo].[Rekets] (
    [SifRek] int IDENTITY(1,1) NOT NULL,
    [MarRek] nvarchar(max)  NOT NULL,
    [BojaRek] nvarchar(max)  NOT NULL,
    [ModRek] nvarchar(max)  NOT NULL,
    [FabrikaReketaIdFab] int  NOT NULL
);
GO

-- Creating table 'Zice'
CREATE TABLE [dbo].[Zice] (
    [SifZic] int IDENTITY(1,1) NOT NULL,
    [BojaZic] nvarchar(max)  NOT NULL,
    [MatZic] nvarchar(max)  NOT NULL,
    [ReketSifRek] int  NULL,
    [IgracIdIgr] int  NULL
);
GO

-- Creating table 'Igraci'
CREATE TABLE [dbo].[Igraci] (
    [IdIgr] int IDENTITY(1,1) NOT NULL,
    [ImeIgr] nvarchar(max)  NOT NULL,
    [PrzIgr] nvarchar(max)  NOT NULL,
    [TipIgr] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Klubovi'
CREATE TABLE [dbo].[Klubovi] (
    [IdKl] int IDENTITY(1,1) NOT NULL,
    [NazKl] nvarchar(max)  NOT NULL,
    [AdrKl] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Treners'
CREATE TABLE [dbo].[Treners] (
    [IdTr] int IDENTITY(1,1) NOT NULL,
    [KlubIdKl] int  NULL,
    [ImeTr] nvarchar(max)  NOT NULL,
    [PrzTr] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ima_ugovors'
CREATE TABLE [dbo].[ima_ugovors] (
    [FabrikaReketaIdFab] int  NOT NULL,
    [KlubIdKl] int  NOT NULL
);
GO

-- Creating table 'Turnirs'
CREATE TABLE [dbo].[Turnirs] (
    [IdTur] int IDENTITY(1,1) NOT NULL,
    [ImeTur] nvarchar(max)  NOT NULL,
    [ima_ugovorFabrikaReketaIdFab] int  NOT NULL,
    [ima_ugovorKlubIdKl] int  NOT NULL
);
GO

-- Creating table 'Igraci_Profesionalac'
CREATE TABLE [dbo].[Igraci_Profesionalac] (
    [Rang] int  NOT NULL,
    [KlubIdKl] int  NOT NULL,
    [IdIgr] int  NOT NULL
);
GO

-- Creating table 'Igraci_Rekreativac'
CREATE TABLE [dbo].[Igraci_Rekreativac] (
    [BrojSparingaNedeljno] int  NOT NULL,
    [IdIgr] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdFab] in table 'FabrikaReketas'
ALTER TABLE [dbo].[FabrikaReketas]
ADD CONSTRAINT [PK_FabrikaReketas]
    PRIMARY KEY CLUSTERED ([IdFab] ASC);
GO

-- Creating primary key on [SifRek] in table 'Rekets'
ALTER TABLE [dbo].[Rekets]
ADD CONSTRAINT [PK_Rekets]
    PRIMARY KEY CLUSTERED ([SifRek] ASC);
GO

-- Creating primary key on [SifZic] in table 'Zice'
ALTER TABLE [dbo].[Zice]
ADD CONSTRAINT [PK_Zice]
    PRIMARY KEY CLUSTERED ([SifZic] ASC);
GO

-- Creating primary key on [IdIgr] in table 'Igraci'
ALTER TABLE [dbo].[Igraci]
ADD CONSTRAINT [PK_Igraci]
    PRIMARY KEY CLUSTERED ([IdIgr] ASC);
GO

-- Creating primary key on [IdKl] in table 'Klubovi'
ALTER TABLE [dbo].[Klubovi]
ADD CONSTRAINT [PK_Klubovi]
    PRIMARY KEY CLUSTERED ([IdKl] ASC);
GO

-- Creating primary key on [IdTr] in table 'Treners'
ALTER TABLE [dbo].[Treners]
ADD CONSTRAINT [PK_Treners]
    PRIMARY KEY CLUSTERED ([IdTr] ASC);
GO

-- Creating primary key on [FabrikaReketaIdFab], [KlubIdKl] in table 'ima_ugovors'
ALTER TABLE [dbo].[ima_ugovors]
ADD CONSTRAINT [PK_ima_ugovors]
    PRIMARY KEY CLUSTERED ([FabrikaReketaIdFab], [KlubIdKl] ASC);
GO

-- Creating primary key on [IdTur] in table 'Turnirs'
ALTER TABLE [dbo].[Turnirs]
ADD CONSTRAINT [PK_Turnirs]
    PRIMARY KEY CLUSTERED ([IdTur] ASC);
GO

-- Creating primary key on [IdIgr] in table 'Igraci_Profesionalac'
ALTER TABLE [dbo].[Igraci_Profesionalac]
ADD CONSTRAINT [PK_Igraci_Profesionalac]
    PRIMARY KEY CLUSTERED ([IdIgr] ASC);
GO

-- Creating primary key on [IdIgr] in table 'Igraci_Rekreativac'
ALTER TABLE [dbo].[Igraci_Rekreativac]
ADD CONSTRAINT [PK_Igraci_Rekreativac]
    PRIMARY KEY CLUSTERED ([IdIgr] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FabrikaReketaIdFab] in table 'Rekets'
ALTER TABLE [dbo].[Rekets]
ADD CONSTRAINT [FK_FabrikaReketaReket]
    FOREIGN KEY ([FabrikaReketaIdFab])
    REFERENCES [dbo].[FabrikaReketas]
        ([IdFab])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FabrikaReketaReket'
CREATE INDEX [IX_FK_FabrikaReketaReket]
ON [dbo].[Rekets]
    ([FabrikaReketaIdFab]);
GO

-- Creating foreign key on [ReketSifRek] in table 'Zice'
ALTER TABLE [dbo].[Zice]
ADD CONSTRAINT [FK_ZicaReket]
    FOREIGN KEY ([ReketSifRek])
    REFERENCES [dbo].[Rekets]
        ([SifRek])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ZicaReket'
CREATE INDEX [IX_FK_ZicaReket]
ON [dbo].[Zice]
    ([ReketSifRek]);
GO

-- Creating foreign key on [IgracIdIgr] in table 'Zice'
ALTER TABLE [dbo].[Zice]
ADD CONSTRAINT [FK_ZicaIgrac]
    FOREIGN KEY ([IgracIdIgr])
    REFERENCES [dbo].[Igraci]
        ([IdIgr])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ZicaIgrac'
CREATE INDEX [IX_FK_ZicaIgrac]
ON [dbo].[Zice]
    ([IgracIdIgr]);
GO

-- Creating foreign key on [KlubIdKl] in table 'Igraci_Profesionalac'
ALTER TABLE [dbo].[Igraci_Profesionalac]
ADD CONSTRAINT [FK_ProfesionalacKlub]
    FOREIGN KEY ([KlubIdKl])
    REFERENCES [dbo].[Klubovi]
        ([IdKl])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProfesionalacKlub'
CREATE INDEX [IX_FK_ProfesionalacKlub]
ON [dbo].[Igraci_Profesionalac]
    ([KlubIdKl]);
GO

-- Creating foreign key on [KlubIdKl] in table 'Treners'
ALTER TABLE [dbo].[Treners]
ADD CONSTRAINT [FK_TrenerKlub]
    FOREIGN KEY ([KlubIdKl])
    REFERENCES [dbo].[Klubovi]
        ([IdKl])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TrenerKlub'
CREATE INDEX [IX_FK_TrenerKlub]
ON [dbo].[Treners]
    ([KlubIdKl]);
GO

-- Creating foreign key on [FabrikaReketaIdFab] in table 'ima_ugovors'
ALTER TABLE [dbo].[ima_ugovors]
ADD CONSTRAINT [FK_FabrikaReketaima_ugovor]
    FOREIGN KEY ([FabrikaReketaIdFab])
    REFERENCES [dbo].[FabrikaReketas]
        ([IdFab])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [KlubIdKl] in table 'ima_ugovors'
ALTER TABLE [dbo].[ima_ugovors]
ADD CONSTRAINT [FK_ima_ugovorKlub]
    FOREIGN KEY ([KlubIdKl])
    REFERENCES [dbo].[Klubovi]
        ([IdKl])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ima_ugovorKlub'
CREATE INDEX [IX_FK_ima_ugovorKlub]
ON [dbo].[ima_ugovors]
    ([KlubIdKl]);
GO

-- Creating foreign key on [ima_ugovorFabrikaReketaIdFab], [ima_ugovorKlubIdKl] in table 'Turnirs'
ALTER TABLE [dbo].[Turnirs]
ADD CONSTRAINT [FK_ima_ugovorTurnir]
    FOREIGN KEY ([ima_ugovorFabrikaReketaIdFab], [ima_ugovorKlubIdKl])
    REFERENCES [dbo].[ima_ugovors]
        ([FabrikaReketaIdFab], [KlubIdKl])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ima_ugovorTurnir'
CREATE INDEX [IX_FK_ima_ugovorTurnir]
ON [dbo].[Turnirs]
    ([ima_ugovorFabrikaReketaIdFab], [ima_ugovorKlubIdKl]);
GO

-- Creating foreign key on [IdIgr] in table 'Igraci_Profesionalac'
ALTER TABLE [dbo].[Igraci_Profesionalac]
ADD CONSTRAINT [FK_Profesionalac_inherits_Igrac]
    FOREIGN KEY ([IdIgr])
    REFERENCES [dbo].[Igraci]
        ([IdIgr])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [IdIgr] in table 'Igraci_Rekreativac'
ALTER TABLE [dbo].[Igraci_Rekreativac]
ADD CONSTRAINT [FK_Rekreativac_inherits_Igrac]
    FOREIGN KEY ([IdIgr])
    REFERENCES [dbo].[Igraci]
        ([IdIgr])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------