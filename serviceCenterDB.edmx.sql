
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/08/2021 15:53:52
-- Generated from EDMX file: C:\diplom\serviceCenter\serviceCenterDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [serviceCenter];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[characteristics1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[characteristics1];
GO
IF OBJECT_ID(N'[dbo].[clientDeviceSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[clientDeviceSets];
GO
IF OBJECT_ID(N'[dbo].[clientSet1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[clientSet1];
GO
IF OBJECT_ID(N'[dbo].[contractSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[contractSets];
GO
IF OBJECT_ID(N'[dbo].[moduleSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[moduleSets];
GO
IF OBJECT_ID(N'[dbo].[requestedServicesSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[requestedServicesSets];
GO
IF OBJECT_ID(N'[dbo].[serviceSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[serviceSets];
GO
IF OBJECT_ID(N'[dbo].[typeOfDeviceSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[typeOfDeviceSets];
GO
IF OBJECT_ID(N'[dbo].[typeOfModuleSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[typeOfModuleSets];
GO
IF OBJECT_ID(N'[dbo].[units]', 'U') IS NOT NULL
    DROP TABLE [dbo].[units];
GO
IF OBJECT_ID(N'[dbo].[upgradeReplacementSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[upgradeReplacementSets];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'characteristics'
CREATE TABLE [dbo].[characteristics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(50)  NOT NULL,
    [unitId] int  NOT NULL
);
GO

-- Creating table 'clientsDevices'
CREATE TABLE [dbo].[clientsDevices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [modelName] nvarchar(150)  NOT NULL,
    [serialNumber] nvarchar(50)  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [typeOfDeviceId] int  NOT NULL,
    [contractId] int  NOT NULL
);
GO

-- Creating table 'clients'
CREATE TABLE [dbo].[clients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FIO] nvarchar(160)  NOT NULL,
    [phoneNumber] nvarchar(16)  NOT NULL
);
GO

-- Creating table 'contracts'
CREATE TABLE [dbo].[contracts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [dateOfReceipt] datetime  NOT NULL,
    [approximateEndDate] datetime  NOT NULL,
    [endDate] datetime  NULL,
    [approximateCost] int  NOT NULL,
    [endCost] int  NULL,
    [clientId] int  NOT NULL
);
GO

-- Creating table 'modules'
CREATE TABLE [dbo].[modules] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(100)  NOT NULL,
    [numberInStock] smallint  NOT NULL,
    [needToOrder] smallint  NOT NULL,
    [safetyStock] smallint  NOT NULL,
    [typeOfModuleId] int  NOT NULL
);
GO

-- Creating table 'requestedServices'
CREATE TABLE [dbo].[requestedServices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [cost] int  NOT NULL,
    [clientDeviceId] int  NOT NULL,
    [serviceId] int  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [stageOfImplementationId] int  NOT NULL
);
GO

-- Creating table 'services'
CREATE TABLE [dbo].[services] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(160)  NOT NULL,
    [baseCost] int  NOT NULL
);
GO

-- Creating table 'typesOfDevices'
CREATE TABLE [dbo].[typesOfDevices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(60)  NOT NULL
);
GO

-- Creating table 'typesOfModule'
CREATE TABLE [dbo].[typesOfModule] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'units'
CREATE TABLE [dbo].[units] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(10)  NOT NULL
);
GO

-- Creating table 'upgradesReplacements'
CREATE TABLE [dbo].[upgradesReplacements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [modulePrice] int  NOT NULL,
    [moduleId] int  NOT NULL,
    [description] nvarchar(max)  NOT NULL,
    [clientDeviceId] int  NOT NULL,
    [employeeId] int  NOT NULL
);
GO

-- Creating table 'moduleCharacteristics'
CREATE TABLE [dbo].[moduleCharacteristics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [characteristicId] int  NOT NULL,
    [moduleId] int  NOT NULL,
    [Value] nvarchar(70)  NOT NULL
);
GO

-- Creating table 'employees'
CREATE TABLE [dbo].[employees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FIO] nvarchar(160)  NOT NULL,
    [phoneNumber] nvarchar(16)  NOT NULL,
    [positionId] int  NOT NULL,
    [login] nvarchar(60)  NOT NULL,
    [password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'positions'
CREATE TABLE [dbo].[positions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'stagesOfImplementations'
CREATE TABLE [dbo].[stagesOfImplementations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(60)  NOT NULL
);
GO

-- Creating table 'servicesExecution'
CREATE TABLE [dbo].[servicesExecution] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [employeeId] int  NOT NULL,
    [requestedServiceId] int  NOT NULL,
    [dateOfBegin] datetime  NOT NULL,
    [dateOfEnd] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'characteristics'
ALTER TABLE [dbo].[characteristics]
ADD CONSTRAINT [PK_characteristics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'clientsDevices'
ALTER TABLE [dbo].[clientsDevices]
ADD CONSTRAINT [PK_clientsDevices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'clients'
ALTER TABLE [dbo].[clients]
ADD CONSTRAINT [PK_clients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'contracts'
ALTER TABLE [dbo].[contracts]
ADD CONSTRAINT [PK_contracts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'modules'
ALTER TABLE [dbo].[modules]
ADD CONSTRAINT [PK_modules]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'requestedServices'
ALTER TABLE [dbo].[requestedServices]
ADD CONSTRAINT [PK_requestedServices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'services'
ALTER TABLE [dbo].[services]
ADD CONSTRAINT [PK_services]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'typesOfDevices'
ALTER TABLE [dbo].[typesOfDevices]
ADD CONSTRAINT [PK_typesOfDevices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'typesOfModule'
ALTER TABLE [dbo].[typesOfModule]
ADD CONSTRAINT [PK_typesOfModule]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'units'
ALTER TABLE [dbo].[units]
ADD CONSTRAINT [PK_units]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'upgradesReplacements'
ALTER TABLE [dbo].[upgradesReplacements]
ADD CONSTRAINT [PK_upgradesReplacements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'moduleCharacteristics'
ALTER TABLE [dbo].[moduleCharacteristics]
ADD CONSTRAINT [PK_moduleCharacteristics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'employees'
ALTER TABLE [dbo].[employees]
ADD CONSTRAINT [PK_employees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'positions'
ALTER TABLE [dbo].[positions]
ADD CONSTRAINT [PK_positions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'stagesOfImplementations'
ALTER TABLE [dbo].[stagesOfImplementations]
ADD CONSTRAINT [PK_stagesOfImplementations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'servicesExecution'
ALTER TABLE [dbo].[servicesExecution]
ADD CONSTRAINT [PK_servicesExecution]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [unitId] in table 'characteristics'
ALTER TABLE [dbo].[characteristics]
ADD CONSTRAINT [FK_unitcharacteristic]
    FOREIGN KEY ([unitId])
    REFERENCES [dbo].[units]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_unitcharacteristic'
CREATE INDEX [IX_FK_unitcharacteristic]
ON [dbo].[characteristics]
    ([unitId]);
GO

-- Creating foreign key on [characteristicId] in table 'moduleCharacteristics'
ALTER TABLE [dbo].[moduleCharacteristics]
ADD CONSTRAINT [FK_characteristicmoduleCharacteristic]
    FOREIGN KEY ([characteristicId])
    REFERENCES [dbo].[characteristics]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_characteristicmoduleCharacteristic'
CREATE INDEX [IX_FK_characteristicmoduleCharacteristic]
ON [dbo].[moduleCharacteristics]
    ([characteristicId]);
GO

-- Creating foreign key on [moduleId] in table 'moduleCharacteristics'
ALTER TABLE [dbo].[moduleCharacteristics]
ADD CONSTRAINT [FK_modulemoduleCharacteristic]
    FOREIGN KEY ([moduleId])
    REFERENCES [dbo].[modules]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_modulemoduleCharacteristic'
CREATE INDEX [IX_FK_modulemoduleCharacteristic]
ON [dbo].[moduleCharacteristics]
    ([moduleId]);
GO

-- Creating foreign key on [typeOfModuleId] in table 'modules'
ALTER TABLE [dbo].[modules]
ADD CONSTRAINT [FK_typeOfModulemodule]
    FOREIGN KEY ([typeOfModuleId])
    REFERENCES [dbo].[typesOfModule]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_typeOfModulemodule'
CREATE INDEX [IX_FK_typeOfModulemodule]
ON [dbo].[modules]
    ([typeOfModuleId]);
GO

-- Creating foreign key on [moduleId] in table 'upgradesReplacements'
ALTER TABLE [dbo].[upgradesReplacements]
ADD CONSTRAINT [FK_moduleupgradeReplacement]
    FOREIGN KEY ([moduleId])
    REFERENCES [dbo].[modules]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_moduleupgradeReplacement'
CREATE INDEX [IX_FK_moduleupgradeReplacement]
ON [dbo].[upgradesReplacements]
    ([moduleId]);
GO

-- Creating foreign key on [clientDeviceId] in table 'upgradesReplacements'
ALTER TABLE [dbo].[upgradesReplacements]
ADD CONSTRAINT [FK_clientDeviceupgradeReplacement]
    FOREIGN KEY ([clientDeviceId])
    REFERENCES [dbo].[clientsDevices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_clientDeviceupgradeReplacement'
CREATE INDEX [IX_FK_clientDeviceupgradeReplacement]
ON [dbo].[upgradesReplacements]
    ([clientDeviceId]);
GO

-- Creating foreign key on [typeOfDeviceId] in table 'clientsDevices'
ALTER TABLE [dbo].[clientsDevices]
ADD CONSTRAINT [FK_typeOfDeviceclientDevice]
    FOREIGN KEY ([typeOfDeviceId])
    REFERENCES [dbo].[typesOfDevices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_typeOfDeviceclientDevice'
CREATE INDEX [IX_FK_typeOfDeviceclientDevice]
ON [dbo].[clientsDevices]
    ([typeOfDeviceId]);
GO

-- Creating foreign key on [clientDeviceId] in table 'requestedServices'
ALTER TABLE [dbo].[requestedServices]
ADD CONSTRAINT [FK_clientDevicerequestedService]
    FOREIGN KEY ([clientDeviceId])
    REFERENCES [dbo].[clientsDevices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_clientDevicerequestedService'
CREATE INDEX [IX_FK_clientDevicerequestedService]
ON [dbo].[requestedServices]
    ([clientDeviceId]);
GO

-- Creating foreign key on [serviceId] in table 'requestedServices'
ALTER TABLE [dbo].[requestedServices]
ADD CONSTRAINT [FK_servicerequestedService]
    FOREIGN KEY ([serviceId])
    REFERENCES [dbo].[services]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_servicerequestedService'
CREATE INDEX [IX_FK_servicerequestedService]
ON [dbo].[requestedServices]
    ([serviceId]);
GO

-- Creating foreign key on [contractId] in table 'clientsDevices'
ALTER TABLE [dbo].[clientsDevices]
ADD CONSTRAINT [FK_contractclientDevice]
    FOREIGN KEY ([contractId])
    REFERENCES [dbo].[contracts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_contractclientDevice'
CREATE INDEX [IX_FK_contractclientDevice]
ON [dbo].[clientsDevices]
    ([contractId]);
GO

-- Creating foreign key on [clientId] in table 'contracts'
ALTER TABLE [dbo].[contracts]
ADD CONSTRAINT [FK_clientcontract]
    FOREIGN KEY ([clientId])
    REFERENCES [dbo].[clients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_clientcontract'
CREATE INDEX [IX_FK_clientcontract]
ON [dbo].[contracts]
    ([clientId]);
GO

-- Creating foreign key on [employeeId] in table 'upgradesReplacements'
ALTER TABLE [dbo].[upgradesReplacements]
ADD CONSTRAINT [FK_employeeupgradeReplacement]
    FOREIGN KEY ([employeeId])
    REFERENCES [dbo].[employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_employeeupgradeReplacement'
CREATE INDEX [IX_FK_employeeupgradeReplacement]
ON [dbo].[upgradesReplacements]
    ([employeeId]);
GO

-- Creating foreign key on [positionId] in table 'employees'
ALTER TABLE [dbo].[employees]
ADD CONSTRAINT [FK_positionemployee]
    FOREIGN KEY ([positionId])
    REFERENCES [dbo].[positions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_positionemployee'
CREATE INDEX [IX_FK_positionemployee]
ON [dbo].[employees]
    ([positionId]);
GO

-- Creating foreign key on [stageOfImplementationId] in table 'requestedServices'
ALTER TABLE [dbo].[requestedServices]
ADD CONSTRAINT [FK_stageOfImplementationrequestedService]
    FOREIGN KEY ([stageOfImplementationId])
    REFERENCES [dbo].[stagesOfImplementations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_stageOfImplementationrequestedService'
CREATE INDEX [IX_FK_stageOfImplementationrequestedService]
ON [dbo].[requestedServices]
    ([stageOfImplementationId]);
GO

-- Creating foreign key on [employeeId] in table 'servicesExecution'
ALTER TABLE [dbo].[servicesExecution]
ADD CONSTRAINT [FK_employeeserviceExecution]
    FOREIGN KEY ([employeeId])
    REFERENCES [dbo].[employees]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_employeeserviceExecution'
CREATE INDEX [IX_FK_employeeserviceExecution]
ON [dbo].[servicesExecution]
    ([employeeId]);
GO

-- Creating foreign key on [requestedServiceId] in table 'servicesExecution'
ALTER TABLE [dbo].[servicesExecution]
ADD CONSTRAINT [FK_requestedServiceserviceExecution]
    FOREIGN KEY ([requestedServiceId])
    REFERENCES [dbo].[requestedServices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_requestedServiceserviceExecution'
CREATE INDEX [IX_FK_requestedServiceserviceExecution]
ON [dbo].[servicesExecution]
    ([requestedServiceId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------