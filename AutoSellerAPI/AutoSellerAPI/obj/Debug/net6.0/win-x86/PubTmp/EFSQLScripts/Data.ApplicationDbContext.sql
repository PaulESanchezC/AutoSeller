IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [Discriminator] nvarchar(max) NOT NULL,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [Address] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [StateOrProvince] nvarchar(max) NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE TABLE [Images] (
        [Id] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Images] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE TABLE [Intents] (
        [Id] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Intents] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE TABLE [Vehicles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Vehicles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217184946_InitialModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220217184946_InitialModel', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Vehicles]') AND [c].[name] = N'Name');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Vehicles] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Vehicles] DROP COLUMN [Name];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Discriminator');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Discriminator];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    ALTER TABLE [Vehicles] ADD [MakerId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    ALTER TABLE [Vehicles] ADD [VehicleName] nvarchar(100) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    ALTER TABLE [Intents] ADD [DateOfIntent] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    ALTER TABLE [Intents] ADD [IntentReciverId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    ALTER TABLE [Intents] ADD [IntentSenderId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    ALTER TABLE [Intents] ADD [VehicleOfIntentId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    ALTER TABLE [Images] ADD [ImageBytes] varbinary(max) NOT NULL DEFAULT 0x;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    ALTER TABLE [Images] ADD [VehicleId] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'StateOrProvince');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [StateOrProvince] nvarchar(100) NOT NULL;
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'' FOR [StateOrProvince];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'LastName');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [LastName] nvarchar(100) NOT NULL;
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'' FOR [LastName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'FirstName');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [FirstName] nvarchar(100) NOT NULL;
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'' FOR [FirstName];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'City');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [City] nvarchar(100) NOT NULL;
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'' FOR [City];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Address');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Address] nvarchar(300) NOT NULL;
    ALTER TABLE [AspNetUsers] ADD DEFAULT N'' FOR [Address];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    CREATE TABLE [Makers] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Makers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    CREATE INDEX [IX_Vehicles_MakerId] ON [Vehicles] ([MakerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    CREATE INDEX [IX_Intents_IntentReciverId] ON [Intents] ([IntentReciverId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    CREATE INDEX [IX_Intents_IntentSenderId] ON [Intents] ([IntentSenderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    CREATE INDEX [IX_Intents_VehicleOfIntentId] ON [Intents] ([VehicleOfIntentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    CREATE INDEX [IX_Images_VehicleId] ON [Images] ([VehicleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    ALTER TABLE [Images] ADD CONSTRAINT [FK_Images_Vehicles_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicles] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    ALTER TABLE [Intents] ADD CONSTRAINT [FK_Intents_AspNetUsers_IntentReciverId] FOREIGN KEY ([IntentReciverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    ALTER TABLE [Intents] ADD CONSTRAINT [FK_Intents_AspNetUsers_IntentSenderId] FOREIGN KEY ([IntentSenderId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    ALTER TABLE [Intents] ADD CONSTRAINT [FK_Intents_Vehicles_VehicleOfIntentId] FOREIGN KEY ([VehicleOfIntentId]) REFERENCES [Vehicles] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    ALTER TABLE [Vehicles] ADD CONSTRAINT [FK_Vehicles_Makers_MakerId] FOREIGN KEY ([MakerId]) REFERENCES [Makers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220217224113_CreatedInitialModels')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220217224113_CreatedInitialModels', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220219164604_Create_FluentApiForModels')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Intents]') AND [c].[name] = N'Id');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Intents] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Intents] ADD DEFAULT N'NEWID()' FOR [Id];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220219164604_Create_FluentApiForModels')
BEGIN
    CREATE UNIQUE INDEX [IX_Vehicles_VehicleName] ON [Vehicles] ([VehicleName]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220219164604_Create_FluentApiForModels')
BEGIN
    CREATE UNIQUE INDEX [IX_Makers_Name] ON [Makers] ([Name]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220219164604_Create_FluentApiForModels')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220219164604_Create_FluentApiForModels', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220219164923_Add_VehiclesListedToAppUser')
BEGIN
    ALTER TABLE [Vehicles] ADD [ApplicationUserId] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220219164923_Add_VehiclesListedToAppUser')
BEGIN
    CREATE INDEX [IX_Vehicles_ApplicationUserId] ON [Vehicles] ([ApplicationUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220219164923_Add_VehiclesListedToAppUser')
BEGIN
    ALTER TABLE [Vehicles] ADD CONSTRAINT [FK_Vehicles_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220219164923_Add_VehiclesListedToAppUser')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220219164923_Add_VehiclesListedToAppUser', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221162628_Create_ListedVehiclesModels')
BEGIN
    ALTER TABLE [Images] ADD [ListedVehicleId] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221162628_Create_ListedVehiclesModels')
BEGIN
    CREATE TABLE [ListedVehicles] (
        [Id] nvarchar(450) NOT NULL,
        [Transmission] int NOT NULL,
        [Color] nvarchar(100) NOT NULL,
        [Description] nvarchar(500) NOT NULL,
        [VehicleId] nvarchar(450) NOT NULL,
        [ApplicationUserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_ListedVehicles] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ListedVehicles_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ListedVehicles_Vehicles_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221162628_Create_ListedVehiclesModels')
BEGIN
    CREATE INDEX [IX_Images_ListedVehicleId] ON [Images] ([ListedVehicleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221162628_Create_ListedVehiclesModels')
BEGIN
    CREATE INDEX [IX_ListedVehicles_ApplicationUserId] ON [ListedVehicles] ([ApplicationUserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221162628_Create_ListedVehiclesModels')
BEGIN
    CREATE INDEX [IX_ListedVehicles_VehicleId] ON [ListedVehicles] ([VehicleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221162628_Create_ListedVehiclesModels')
BEGIN
    ALTER TABLE [Images] ADD CONSTRAINT [FK_Images_ListedVehicles_ListedVehicleId] FOREIGN KEY ([ListedVehicleId]) REFERENCES [ListedVehicles] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221162628_Create_ListedVehiclesModels')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220221162628_Create_ListedVehiclesModels', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221164638_Update_IntentModel')
BEGIN
    ALTER TABLE [Intents] DROP CONSTRAINT [FK_Intents_Vehicles_VehicleOfIntentId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221164638_Update_IntentModel')
BEGIN
    DROP INDEX [IX_Intents_VehicleOfIntentId] ON [Intents];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221164638_Update_IntentModel')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Intents]') AND [c].[name] = N'VehicleOfIntentId');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Intents] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Intents] DROP COLUMN [VehicleOfIntentId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221164638_Update_IntentModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220221164638_Update_IntentModel', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221164912_Update_IntentModel_Add_ListedVehicle')
BEGIN
    ALTER TABLE [Intents] ADD [ListedVehicleId] nvarchar(450) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221164912_Update_IntentModel_Add_ListedVehicle')
BEGIN
    CREATE INDEX [IX_Intents_ListedVehicleId] ON [Intents] ([ListedVehicleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221164912_Update_IntentModel_Add_ListedVehicle')
BEGIN
    ALTER TABLE [Intents] ADD CONSTRAINT [FK_Intents_ListedVehicles_ListedVehicleId] FOREIGN KEY ([ListedVehicleId]) REFERENCES [ListedVehicles] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221164912_Update_IntentModel_Add_ListedVehicle')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220221164912_Update_IntentModel_Add_ListedVehicle', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221170544_Add_fluentApis')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Vehicles]') AND [c].[name] = N'Id');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Vehicles] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Vehicles] ADD DEFAULT N'NEWID()' FOR [Id];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221170544_Add_fluentApis')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Makers]') AND [c].[name] = N'Id');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Makers] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Makers] ADD DEFAULT N'NEWID()' FOR [Id];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221170544_Add_fluentApis')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ListedVehicles]') AND [c].[name] = N'Id');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [ListedVehicles] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [ListedVehicles] ADD DEFAULT N'NEWID()' FOR [Id];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221170544_Add_fluentApis')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Images]') AND [c].[name] = N'Id');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Images] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [Images] ADD DEFAULT N'NEWID()' FOR [Id];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220221170544_Add_fluentApis')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220221170544_Add_fluentApis', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220222121726_Added_DeleteStatusToListedVehicleModel_andTo_IntentModel')
BEGIN
    ALTER TABLE [ListedVehicles] ADD [DateListed] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220222121726_Added_DeleteStatusToListedVehicleModel_andTo_IntentModel')
BEGIN
    ALTER TABLE [ListedVehicles] ADD [DateSold] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220222121726_Added_DeleteStatusToListedVehicleModel_andTo_IntentModel')
BEGIN
    ALTER TABLE [ListedVehicles] ADD [IsSold] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220222121726_Added_DeleteStatusToListedVehicleModel_andTo_IntentModel')
BEGIN
    ALTER TABLE [Intents] ADD [DateSold] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220222121726_Added_DeleteStatusToListedVehicleModel_andTo_IntentModel')
BEGIN
    ALTER TABLE [Intents] ADD [IsSold] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220222121726_Added_DeleteStatusToListedVehicleModel_andTo_IntentModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220222121726_Added_DeleteStatusToListedVehicleModel_andTo_IntentModel', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223173345_Update_IdTags_to_SpecificModelIdTag')
BEGIN
    ALTER TABLE [Images] DROP CONSTRAINT [FK_Images_Vehicles_VehicleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223173345_Update_IdTags_to_SpecificModelIdTag')
BEGIN
    DROP INDEX [IX_Images_VehicleId] ON [Images];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223173345_Update_IdTags_to_SpecificModelIdTag')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Images]') AND [c].[name] = N'VehicleId');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Images] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Images] DROP COLUMN [VehicleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223173345_Update_IdTags_to_SpecificModelIdTag')
BEGIN
    EXEC sp_rename N'[Vehicles].[Id]', N'VehicleId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223173345_Update_IdTags_to_SpecificModelIdTag')
BEGIN
    EXEC sp_rename N'[Makers].[Id]', N'MakerId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223173345_Update_IdTags_to_SpecificModelIdTag')
BEGIN
    EXEC sp_rename N'[ListedVehicles].[Id]', N'ListedVehicleId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223173345_Update_IdTags_to_SpecificModelIdTag')
BEGIN
    EXEC sp_rename N'[Intents].[Id]', N'IntentId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223173345_Update_IdTags_to_SpecificModelIdTag')
BEGIN
    EXEC sp_rename N'[Images].[Id]', N'ImageId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223173345_Update_IdTags_to_SpecificModelIdTag')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220223173345_Update_IdTags_to_SpecificModelIdTag', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223200305_Update_MakerNameCol')
BEGIN
    EXEC sp_rename N'[Makers].[Name]', N'MakerName', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223200305_Update_MakerNameCol')
BEGIN
    EXEC sp_rename N'[Makers].[IX_Makers_Name]', N'IX_Makers_MakerName', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220223200305_Update_MakerNameCol')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220223200305_Update_MakerNameCol', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220224000507_Update_TransmissionCol_fromEnumToString_ListedVehicleTbl')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ListedVehicles]') AND [c].[name] = N'Transmission');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [ListedVehicles] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [ListedVehicles] ALTER COLUMN [Transmission] nvarchar(20) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220224000507_Update_TransmissionCol_fromEnumToString_ListedVehicleTbl')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220224000507_Update_TransmissionCol_fromEnumToString_ListedVehicleTbl', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220224015820_Update_ListedVehicleTbl_CreatedNewCols')
BEGIN
    ALTER TABLE [ListedVehicles] ADD [DriveTrain] nvarchar(10) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220224015820_Update_ListedVehicleTbl_CreatedNewCols')
BEGIN
    ALTER TABLE [ListedVehicles] ADD [Mileage] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220224015820_Update_ListedVehicleTbl_CreatedNewCols')
BEGIN
    ALTER TABLE [ListedVehicles] ADD [Price] float NOT NULL DEFAULT 0.0E0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220224015820_Update_ListedVehicleTbl_CreatedNewCols')
BEGIN
    ALTER TABLE [ListedVehicles] ADD [Year] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220224015820_Update_ListedVehicleTbl_CreatedNewCols')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220224015820_Update_ListedVehicleTbl_CreatedNewCols', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220224175238_Update_IntentTbl_intentReceiverIdTypo')
BEGIN
    ALTER TABLE [Intents] DROP CONSTRAINT [FK_Intents_AspNetUsers_IntentReciverId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220224175238_Update_IntentTbl_intentReceiverIdTypo')
BEGIN
    EXEC sp_rename N'[Intents].[IntentReciverId]', N'IntentReceiverId', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220224175238_Update_IntentTbl_intentReceiverIdTypo')
BEGIN
    EXEC sp_rename N'[Intents].[IX_Intents_IntentReciverId]', N'IX_Intents_IntentReceiverId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220224175238_Update_IntentTbl_intentReceiverIdTypo')
BEGIN
    ALTER TABLE [Intents] ADD CONSTRAINT [FK_Intents_AspNetUsers_IntentReceiverId] FOREIGN KEY ([IntentReceiverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220224175238_Update_IntentTbl_intentReceiverIdTypo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220224175238_Update_IntentTbl_intentReceiverIdTypo', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [Images] DROP CONSTRAINT [FK_Images_ListedVehicles_ListedVehicleId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [Intents] DROP CONSTRAINT [FK_Intents_AspNetUsers_IntentReceiverId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [Intents] DROP CONSTRAINT [FK_Intents_AspNetUsers_IntentSenderId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [ListedVehicles] DROP CONSTRAINT [FK_ListedVehicles_AspNetUsers_ApplicationUserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [Vehicles] DROP CONSTRAINT [FK_Vehicles_AspNetUsers_ApplicationUserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    DROP TABLE [AspNetRoleClaims];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    DROP TABLE [AspNetUserClaims];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    DROP TABLE [AspNetUserLogins];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    DROP TABLE [AspNetUserRoles];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    DROP TABLE [AspNetUserTokens];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    DROP TABLE [AspNetRoles];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [AspNetUsers] DROP CONSTRAINT [PK_AspNetUsers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    DROP INDEX [EmailIndex] ON [AspNetUsers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    DROP INDEX [UserNameIndex] ON [AspNetUsers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    EXEC sp_rename N'[AspNetUsers]', N'AppUsers';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    DROP INDEX [IX_Images_ListedVehicleId] ON [Images];
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Images]') AND [c].[name] = N'ListedVehicleId');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Images] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [Images] ALTER COLUMN [ListedVehicleId] nvarchar(450) NOT NULL;
    ALTER TABLE [Images] ADD DEFAULT N'' FOR [ListedVehicleId];
    CREATE INDEX [IX_Images_ListedVehicleId] ON [Images] ([ListedVehicleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [Images] ADD [ImageDescription] nvarchar(120) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [Images] ADD [ImageIndex] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppUsers]') AND [c].[name] = N'UserName');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [AppUsers] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [AppUsers] ALTER COLUMN [UserName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppUsers]') AND [c].[name] = N'NormalizedUserName');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [AppUsers] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [AppUsers] ALTER COLUMN [NormalizedUserName] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppUsers]') AND [c].[name] = N'NormalizedEmail');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [AppUsers] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [AppUsers] ALTER COLUMN [NormalizedEmail] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AppUsers]') AND [c].[name] = N'Email');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [AppUsers] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [AppUsers] ALTER COLUMN [Email] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [AppUsers] ADD [ClientConfirmationUrl] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [AppUsers] ADD CONSTRAINT [PK_AppUsers] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [Images] ADD CONSTRAINT [FK_Images_ListedVehicles_ListedVehicleId] FOREIGN KEY ([ListedVehicleId]) REFERENCES [ListedVehicles] ([ListedVehicleId]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [Intents] ADD CONSTRAINT [FK_Intents_AppUsers_IntentReceiverId] FOREIGN KEY ([IntentReceiverId]) REFERENCES [AppUsers] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [Intents] ADD CONSTRAINT [FK_Intents_AppUsers_IntentSenderId] FOREIGN KEY ([IntentSenderId]) REFERENCES [AppUsers] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [ListedVehicles] ADD CONSTRAINT [FK_ListedVehicles_AppUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AppUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    ALTER TABLE [Vehicles] ADD CONSTRAINT [FK_Vehicles_AppUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AppUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045029_attemptToRegenerateIdentityDatabase')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220226045029_attemptToRegenerateIdentityDatabase', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    ALTER TABLE [Intents] DROP CONSTRAINT [FK_Intents_AppUsers_IntentReceiverId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    ALTER TABLE [Intents] DROP CONSTRAINT [FK_Intents_AppUsers_IntentSenderId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    ALTER TABLE [ListedVehicles] DROP CONSTRAINT [FK_ListedVehicles_AppUsers_ApplicationUserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    ALTER TABLE [Vehicles] DROP CONSTRAINT [FK_Vehicles_AppUsers_ApplicationUserId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    ALTER TABLE [AppUsers] DROP CONSTRAINT [PK_AppUsers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    EXEC sp_rename N'[AppUsers]', N'AspNetUsers';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'UserName');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [UserName] nvarchar(256) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'NormalizedUserName');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [NormalizedUserName] nvarchar(256) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'NormalizedEmail');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [NormalizedEmail] nvarchar(256) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    DECLARE @var23 sysname;
    SELECT @var23 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Email');
    IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var23 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Email] nvarchar(256) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    ALTER TABLE [Intents] ADD CONSTRAINT [FK_Intents_AspNetUsers_IntentReceiverId] FOREIGN KEY ([IntentReceiverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    ALTER TABLE [Intents] ADD CONSTRAINT [FK_Intents_AspNetUsers_IntentSenderId] FOREIGN KEY ([IntentSenderId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    ALTER TABLE [ListedVehicles] ADD CONSTRAINT [FK_ListedVehicles_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    ALTER TABLE [Vehicles] ADD CONSTRAINT [FK_Vehicles_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226045311_ReBuildNetCoreIdentityDatabase')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220226045311_ReBuildNetCoreIdentityDatabase', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226184423_Delete_ClientConfirmationUrlCol_from_ApplicationUserTbl')
BEGIN
    DECLARE @var24 sysname;
    SELECT @var24 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'ClientConfirmationUrl');
    IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var24 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [ClientConfirmationUrl];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220226184423_Delete_ClientConfirmationUrlCol_from_ApplicationUserTbl')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220226184423_Delete_ClientConfirmationUrlCol_from_ApplicationUserTbl', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311042418_Removed_ImageDescriptionCol')
BEGIN
    DECLARE @var25 sysname;
    SELECT @var25 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Images]') AND [c].[name] = N'ImageDescription');
    IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [Images] DROP CONSTRAINT [' + @var25 + '];');
    ALTER TABLE [Images] DROP COLUMN [ImageDescription];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311042418_Removed_ImageDescriptionCol')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220311042418_Removed_ImageDescriptionCol', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318004548_Add_isDeletedCol_to_ListeVehicleTbl')
BEGIN
    ALTER TABLE [ListedVehicles] ADD [DateDeleted] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318004548_Add_isDeletedCol_to_ListeVehicleTbl')
BEGIN
    ALTER TABLE [ListedVehicles] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318004548_Add_isDeletedCol_to_ListeVehicleTbl')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220318004548_Add_isDeletedCol_to_ListeVehicleTbl', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318034801_Add_IsReadCol_and_IsDescartedCol_to_IntentTbl')
BEGIN
    ALTER TABLE [Intents] ADD [IsDescarted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318034801_Add_IsReadCol_and_IsDescartedCol_to_IntentTbl')
BEGIN
    ALTER TABLE [Intents] ADD [IsRead] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318034801_Add_IsReadCol_and_IsDescartedCol_to_IntentTbl')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220318034801_Add_IsReadCol_and_IsDescartedCol_to_IntentTbl', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318095529_Update__IsDiscardedCol_typo')
BEGIN
    EXEC sp_rename N'[Intents].[IsDescarted]', N'IsDiscarded', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220318095529_Update__IsDiscardedCol_typo')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220318095529_Update__IsDiscardedCol_typo', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321220539_Add-Guest-Roles-to-Database')
BEGIN

                    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
                    VALUES (N'9551516f-99d9-434d-9242-0ea524d76d2d', N'Guest', N'GUEST', N'd2275a1e-ad7d-47d6-9d7a-76e1f5a8ffba')
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321220539_Add-Guest-Roles-to-Database')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220321220539_Add-Guest-Roles-to-Database', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322001128_Add-SuperRole_deCartierSanchez')
BEGIN

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
                VALUES (N'eaea6f97-c61b-45c4-b76f-e8af8d1a7cfc', N'deCartierSanchez', N'DECARTIERSANCHEZ', N'85e84651-ac93-4207-b910-d4142c16a164')
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322001128_Add-SuperRole_deCartierSanchez')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220322001128_Add-SuperRole_deCartierSanchez', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322022913_add-Admin-to-Database')
BEGIN

    INSERT INTO [dbo].[AspNetUsers] 
    ([Id], 
    [FirstName], 
    [LastName], 
    [Address], 
    [City], 
    [StateOrProvince], 
    [UserName], 
    [NormalizedUserName], 
    [Email], [NormalizedEmail], 
    [EmailConfirmed], 
    [PasswordHash], 
    [SecurityStamp], 
    [ConcurrencyStamp], 
    [PhoneNumber], 
    [PhoneNumberConfirmed], 
    [TwoFactorEnabled], 
    [LockoutEnd], 
    [LockoutEnabled], 
    [AccessFailedCount]) 

    VALUES (
    N'b583c380-7610-4160-9487-0055c0e1d786', 
    N'Paulyglot', 
    N'Sanchez', 
    N'does not matter', 
    N'does not matter', 
    N'does not matter', 
    N'paulesanchezc@outlook.com', 
    N'PAULESANCHEZC@OUTLOOK.COM', 
    N'paulesanchezc@outlook.com', 
    N'PAULESANCHEZC@OUTLOOK.COM', 
    1, 
    N'AQAAAAEAACcQAAAAECOQB+sMz39poNXpVeOlB8mq0rq7hJq6Byj0/0y8+8MDHXYyzNujVpJqgcIznaTvfg==', 
    N'CYYCEZCEO35CUWQSKM6VEBRTAXABXSRN', 
    N'01c08957-79b1-4089-a896-0189a62a9b87', 
    N'does not matter', 0, 0, NULL, 1, 0)

END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322022913_add-Admin-to-Database')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220322022913_add-Admin-to-Database', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322175726_Seed-UsersData-to-database')
BEGIN

                INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Address], [City], [StateOrProvince], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'Tutorial User # 2', N'Tutorial User # 2', N'Tutorial User # 2', N'Tutorial User # 2', N'Tutorial User # 2', N'User2@tutorial.com', N'USER2@TUTORIAL.COM', N'User2@tutorial.com', N'USER2@TUTORIAL.COM', 1, N'AQAAAAEAACcQAAAAEK1X5LuURVIAQMaaOEmEqFM2mLPgESIlox1fpKdukkQGeZ3s7X+oW3BwimfthRfm+Q==', N'GGPGF4FAXMNTGDXKSWI4JQQFWLKYZUHJ', N'99bcc594-2d76-47b9-84c1-93c985568824', N'Tutorial User # 2', 0, 0, NULL, 1, 0)
                INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Address], [City], [StateOrProvince], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'dce92991-de0e-4390-b83b-87f8d7047f50', N'Tutorial User # 3', N'Tutorial User # 3', N'Tutorial User # 3', N'Tutorial User # 3', N'Tutorial User # 3', N'User3@tutorial.com', N'USER3@TUTORIAL.COM', N'User3@tutorial.com', N'USER3@TUTORIAL.COM', 1, N'AQAAAAEAACcQAAAAEMppqMXZiChQaJjEB3VALD4O9YD8Nk6Hr8BELegeUq5GM5lh+1hOABH/udTiXLFEvg==', N'GIGD42FEBITJARCM2XNZZNF7NM2FVSWP', N'a8bc3406-0592-4966-9157-4e40988203da', N'Tutorial User # 3', 0, 0, NULL, 1, 0)
                INSERT INTO [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Address], [City], [StateOrProvince], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'Tutorial User # 1', N'Tutorial User # 1', N'Tutorial User # 1', N'Tutorial User # 1', N'Tutorial User # 1', N'user1@tutorial.com', N'USER1@TUTORIAL.COM', N'user1@tutorial.com', N'USER1@TUTORIAL.COM', 1, N'AQAAAAEAACcQAAAAEODyGWyEhhnwgd4GxUNm+CmxjkOqEdDNHGae1xyhmjW9rV5jFmtcc6N1MFYjs44MfA==', N'ZJXUCOBVQGFQSR7WHJH6AXWAQR26BQ2R', N'da6fb2b0-68bd-4ea4-9847-b0cb7d4234a1', N'Tutorial User # 1', 0, 0, N'3/18/2022 3:33:01 AM +00:00', 1, 0)
                
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322175726_Seed-UsersData-to-database')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220322175726_Seed-UsersData-to-database', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322180151_Seed-MakersData-to-database')
BEGIN

                    INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'BUICK')
                    INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'CHEVROLET')
                    INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'9f53bca0-7626-4268-8dc6-aef96508667c', N'VOLKSWAGEN')
                    INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'FORD')
                    INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'2e4bf369-7a7d-477b-b4ec-32fe7dd1d78b', N'HONDA')
                    INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'fa4c00ca-1de5-4118-b306-35f88d970dca', N'MAZDA')
                    INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'1ee37317-c4b9-4728-b009-5be115c648fa', N'MERCEDES')
                    INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'44abd3f2-eb74-4c55-8d64-53896be97133', N'MITSUBISHI')
                    INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'42534b91-b666-4ac7-9985-11c94f49a10c', N'TESLA')
                    INSERT INTO [dbo].[Makers] ([MakerId], [MakerName]) VALUES (N'a0cdc54c-2132-4c2d-b2df-396111ab86c1', N'TOYOTA')

END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322180151_Seed-MakersData-to-database')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220322180151_Seed-MakersData-to-database', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322180315_Seed-VehiclesData-to-database')
BEGIN

                    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'005e7fc1-ef41-4227-bca1-0b721b20b012', N'42534b91-b666-4ac7-9985-11c94f49a10c', N'ROADSTER', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'01c021d5-9476-47bc-bcf2-a5a25eb65b06', N'2e4bf369-7a7d-477b-b4ec-32fe7dd1d78b', N'CIVIC', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'056e7474-20dc-454b-b8f3-83a1ae2d7bf9', N'44abd3f2-eb74-4c55-8d64-53896be97133', N'ENDEAVOR', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'061c36f2-1e37-42ea-a028-1946d0c285a3', N'44abd3f2-eb74-4c55-8d64-53896be97133', N'GALANT', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'06981c93-1f74-479a-8049-23eb3ff377a2', N'9f53bca0-7626-4268-8dc6-aef96508667c', N'RABBIT', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'0abbcd08-6baa-4ac5-8bac-fa77a169299f', N'a0cdc54c-2132-4c2d-b2df-396111ab86c1', N'LAND CRUISER', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'0cffb44e-ef37-4200-9669-a1de7a3dc0ca', N'44abd3f2-eb74-4c55-8d64-53896be97133', N'MONTERO', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'0ecea250-6089-4a0d-8e7d-ba01923fce99', N'42534b91-b666-4ac7-9985-11c94f49a10c', N'MODEL S', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'11aef7d1-1e52-4708-aa26-8de60faa405f', N'9f53bca0-7626-4268-8dc6-aef96508667c', N'GOLF', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'1353f18f-a381-4769-9d73-4321141a63ac', N'a0cdc54c-2132-4c2d-b2df-396111ab86c1', N'TACOMA', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'1cb289fc-00ac-4c76-ab4e-06806df8569d', N'fa4c00ca-1de5-4118-b306-35f88d970dca', N'CX-9', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'1d22a4d3-a2a9-41ce-b745-92cfe73e4c77', N'1ee37317-c4b9-4728-b009-5be115c648fa', N'SLS-CLASS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'24b4f69e-7c40-4dad-9e8d-2e2cf153454a', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'ENCORE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'2944dfe7-82ab-4872-b24e-133e156bc2b4', N'a0cdc54c-2132-4c2d-b2df-396111ab86c1', N'SCION TC', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'2a3ee3d0-4069-445f-baad-bfad8804f7a2', N'a0cdc54c-2132-4c2d-b2df-396111ab86c1', N'SCION XB', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'2af6a4e3-5226-4be9-917c-8ef7f7a47817', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'CAPTIVA SPORT', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'309ade55-9154-4d6f-806f-d3aa5eb909d2', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'LUCERNE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'3198a718-8d27-4d39-ab04-209684359ec3', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'LESABRE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'3a5076c9-5789-4a53-962d-5b491910f8d8', N'9f53bca0-7626-4268-8dc6-aef96508667c', N'PASSAT', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'3b1da50d-d8d4-44e3-a59f-9e6ffc43ddf9', N'1ee37317-c4b9-4728-b009-5be115c648fa', N'S-CLASS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'3bd28bf5-a7ac-41ef-925d-7d44b12c2620', N'44abd3f2-eb74-4c55-8d64-53896be97133', N'RVR', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'44b72f0f-a631-46a2-86d5-60645461bd99', N'fa4c00ca-1de5-4118-b306-35f88d970dca', N'MAZDA5', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'45fb323e-b537-479e-96b5-9093b4e6969f', N'9f53bca0-7626-4268-8dc6-aef96508667c', N'JETTA WAGON', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'499772bc-75ee-4612-a8ce-e6a825c404ee', N'a0cdc54c-2132-4c2d-b2df-396111ab86c1', N'SCION XA', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'4b78d3c7-20a4-476c-845d-77e032fae9e0', N'44abd3f2-eb74-4c55-8d64-53896be97133', N'LANCER', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'4b828be4-400e-4956-882b-4fb85b7f6fcd', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'F-150', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'4c204593-471f-48a4-82de-fd5ae6387cc8', N'a0cdc54c-2132-4c2d-b2df-396111ab86c1', N'PRIUS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'508ccdd3-a0d3-45a8-85d3-9cd5115a693e', N'9f53bca0-7626-4268-8dc6-aef96508667c', N'TOUAREG', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'510f4b0a-948f-4ae4-bb83-33caa874616b', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'AVEO', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'538b37c2-da37-451b-acef-39af8ac9ff3f', N'1ee37317-c4b9-4728-b009-5be115c648fa', N'C-CLASS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'553be964-7ce9-4fab-a888-30e9e885d9b5', N'fa4c00ca-1de5-4118-b306-35f88d970dca', N'MAZDA6', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'5f30a104-0870-406b-acbd-0d22966c3c34', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'CRUZE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'60317ea3-a11a-4b38-8896-b77e2d185312', N'a0cdc54c-2132-4c2d-b2df-396111ab86c1', N'YARIS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'6163caca-a1f0-49e2-8a75-f56c13a0a237', N'1ee37317-c4b9-4728-b009-5be115c648fa', N'E-CLASS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'62d08a45-45ce-4033-8d84-1989693b751f', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'TAURUS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'637ff5ed-d634-4fa9-a9b9-e42a6ffd7371', N'9f53bca0-7626-4268-8dc6-aef96508667c', N'GTI', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'653f6ed8-aac7-4ae5-8c2b-de76af5f54ca', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'CROWN VICTORIA', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'69f09a91-94ea-4c06-8650-7f2881b2105b', N'9f53bca0-7626-4268-8dc6-aef96508667c', N'NEW GTI', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'6cfde550-886b-4cb7-928b-269c02bba42d', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'EDGE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'6d7efb41-b525-42bd-90e4-6c308c38b71e', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'INCOMPLETE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'6f49b9f7-a72f-4535-9c85-6c2cfd8d22ff', N'fa4c00ca-1de5-4118-b306-35f88d970dca', N'CX-5', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'6f791e36-3714-427c-93e5-e4a072dab37f', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'VERANO', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'73937a42-af8a-41b5-992c-8dc5b76d36b7', N'fa4c00ca-1de5-4118-b306-35f88d970dca', N'MAZDA3', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'7861dcb5-6aa6-4ba4-af5a-fab8bec31fd1', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'RAINIER', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'788f2175-4e43-48fe-8110-28d198999109', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'CAMARO', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'7a5b1399-2532-4fc0-b20e-9f517b04582a', N'44abd3f2-eb74-4c55-8d64-53896be97133', N'ECLIPSE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'80554a28-d232-49dc-b59d-14c3084c72d4', N'fa4c00ca-1de5-4118-b306-35f88d970dca', N'MPV', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'80a69786-137c-46ed-9345-ee56b72677d2', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'VOLT', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'821775c7-9487-4227-94fb-790e43a94eba', N'1ee37317-c4b9-4728-b009-5be115c648fa', N'SPRINTER', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'87898a14-f734-42ec-af58-426eed66e8e0', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'FUSION', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'89c2d16d-15ed-4f41-aa2e-c121b34d16cd', N'42534b91-b666-4ac7-9985-11c94f49a10c', N'MODEL X', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'89d81884-99c5-423c-8c3e-6b8999a0fbda', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'EXPEDITION', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'8b9d7820-d849-42ab-bc4e-6471bb14d470', N'9f53bca0-7626-4268-8dc6-aef96508667c', N'ROUTAN', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'8c7dfdb9-2dbb-4840-93cd-6911a4f1dcc6', N'fa4c00ca-1de5-4118-b306-35f88d970dca', N'MAZDA2', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'8f1a4e8f-1aa3-440f-b631-ba9fd4ed0f12', N'2e4bf369-7a7d-477b-b4ec-32fe7dd1d78b', N'PILOT', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'905a7ba5-4b43-41a0-926a-654159f75ffe', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'CORVETTE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'92058494-5b43-40a0-87bb-bb7e862e9424', N'2e4bf369-7a7d-477b-b4ec-32fe7dd1d78b', N'ELEMENT', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'92532019-ef5e-49ad-a088-fdd806b70cd2', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'RANGER', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'92ad240a-5305-468b-99b7-80d8cc14092f', N'1ee37317-c4b9-4728-b009-5be115c648fa', N'CLS-CLASS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'940d43bd-655f-4c78-9f5a-b556e42922ce', N'a0cdc54c-2132-4c2d-b2df-396111ab86c1', N'COROLLA', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'94dd2103-20c7-4381-b3c3-480b3471b7b7', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'TERRAZA', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'96e02ad1-ae16-4ca3-9811-bcbf564fdefc', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'RIVIERA', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'9931afe0-5598-418a-9019-1af3d280a67a', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'SS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'9a00c239-4de2-403a-ad6a-7b435ec034c8', N'fa4c00ca-1de5-4118-b306-35f88d970dca', N'TRIBUTE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'9d60a2dc-24af-48b4-a99e-8ea8a8c8aa86', N'2e4bf369-7a7d-477b-b4ec-32fe7dd1d78b', N'ACCORD', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'9ee95d2e-ea65-4308-8140-0c46ea9264f0', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'LACROSSE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'a66e2235-fac5-4b1d-9e37-03d31ec09483', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'ALLURE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'a838a7be-c7eb-4a88-9503-1ca0c9eb3fc1', N'1ee37317-c4b9-4728-b009-5be115c648fa', N'CLA-CLASS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'a94d5d01-989b-4242-bf34-6e90abadd67c', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'SONIC', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'ab5bb0b0-3963-49a1-acf2-e9da487c7956', N'42534b91-b666-4ac7-9985-11c94f49a10c', N'MODEL Y', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'ac4dc8e9-3556-4933-86c6-6f04b1fe3572', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'ENCLAVE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'ae6d306a-e8c7-44fe-bb19-409587f811c0', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'RENDEZVOUS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'aeff9bd7-2ae8-4e51-b4f3-78541e5967af', N'44abd3f2-eb74-4c55-8d64-53896be97133', N'MIRAGE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'b47189cc-73ab-4c7c-bd0d-68092fe80ef6', N'fa4c00ca-1de5-4118-b306-35f88d970dca', N'RX-8', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'b875cc7d-4f3b-4a98-b6ce-7c1bd31e1ae0', N'1ee37317-c4b9-4728-b009-5be115c648fa', N'SL-CLASS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'b97d25d7-cca8-44fc-bb5c-f9ff8413e568', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'FLEX', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'bbbc22cf-bab4-4cf6-81cd-9648a1cc040c', N'44abd3f2-eb74-4c55-8d64-53896be97133', N'RAIDER', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'bea62787-7a84-44c5-bd38-bd70c3d763e2', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'CENTURY', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'bfdd9dcb-d316-4058-b14d-5447eb6b0437', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'EXPRESS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'c1727df6-39d2-4a09-80a4-88d39a39a43d', N'44abd3f2-eb74-4c55-8d64-53896be97133', N'OUTLANDER', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'c3a58fc5-0616-4ddf-9827-aa68d7550fbd', N'9f53bca0-7626-4268-8dc6-aef96508667c', N'EOS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'c8348264-1660-4c68-b305-73d93eb2685e', N'2e4bf369-7a7d-477b-b4ec-32fe7dd1d78b', N'RIDGELINE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'cb97e25a-cbbc-472a-adbe-130b89f0b8d1', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'F-450', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'd014d3f1-9075-4c69-8866-aba11d83d042', N'2e4bf369-7a7d-477b-b4ec-32fe7dd1d78b', N'ODYSSEY', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'd0963dd8-c43f-4bda-9304-e20ccf3a8b85', N'a0cdc54c-2132-4c2d-b2df-396111ab86c1', N'RAV4', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'd45520d4-663d-4986-ae15-5253b1b56fba', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'E-150', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'd603ea22-d235-433c-aa16-e79c370091d0', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'F-250', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'd8bda6cf-d129-4eb7-bb67-fd42f8eda2da', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'REGAL', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'd92ed4a5-ace4-4e6f-b847-74ad56e9211a', N'9f53bca0-7626-4268-8dc6-aef96508667c', N'R32', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'db7e2996-979a-4751-ba70-cf804591e40e', N'fa4c00ca-1de5-4118-b306-35f88d970dca', N'CX-7', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'dc00fa4c-1e09-4c27-8794-db70360339ae', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'MALIBU', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'dcf99184-910a-4ba7-96d0-65dd738de9eb', N'1ee37317-c4b9-4728-b009-5be115c648fa', N'SLK-CLASS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'e0c74393-9014-4b48-96ac-cdd388c13b40', N'42534b91-b666-4ac7-9985-11c94f49a10c', N'MODEL 3', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'e1052833-153e-4b86-82ed-5f31f5da5a6a', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'FOCUS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'e1f19199-f8be-448d-b7a2-2ea97fede007', N'9f53bca0-7626-4268-8dc6-aef96508667c', N'PHAETON', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'e1f657e2-ca30-464b-abe0-e0d94b059d42', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'IMPALA', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'ebbc9a20-d419-4c9c-810e-0ef31c07a077', N'1ee37317-c4b9-4728-b009-5be115c648fa', N'GLA-CLASS', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'ebe8aad9-cff3-4e5a-b4c8-82850b0c537e', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'EXPLORER', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'ee43d817-d1fa-4389-8d67-cbb780dcc9bc', N'2e4bf369-7a7d-477b-b4ec-32fe7dd1d78b', N'CR-V', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'f0666df8-b2cc-4331-8130-43b015bdd389', N'fa4c00ca-1de5-4118-b306-35f88d970dca', N'MX-5', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'f06f4691-11da-452b-ad5c-e4942ef14f76', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'SPARK', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'f233409e-cec7-4c59-94fd-da067214ca6e', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'CAPRICE POLICE VEHICLE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'f3bf528a-20dd-4c8e-8bf8-ab0c1090d3f0', N'fbfafed9-8a3b-4eb8-bc1f-7846bf9f33e9', N'PARK AVENUE', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'f3ef0926-c971-40b1-908f-6152f73ad3ff', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'F-350', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'f3fa5ce0-a745-47a6-b26f-e4d254bd51eb', N'a0cdc54c-2132-4c2d-b2df-396111ab86c1', N'HIGHLANDER', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'f427897b-0e52-4171-b25c-434838a584b1', N'a0cdc54c-2132-4c2d-b2df-396111ab86c1', N'4-RUNNER', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'f444a73b-0f87-4f40-92e2-cf1f1ee080fc', N'5a1c3850-f9fc-49f0-9fd9-30d8e9c53b2f', N'MUSTANG', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'f888a471-8cac-4219-b276-d52875da6f1c', N'88feeca2-1ddf-4155-aad9-2587fbb25950', N'EQUINOX', NULL)
    INSERT INTO [dbo].[Vehicles] ([VehicleId], [MakerId], [VehicleName], [ApplicationUserId]) VALUES (N'fb9eb5b0-ce9c-4f5f-b1fa-5faed783338c', N'9f53bca0-7626-4268-8dc6-aef96508667c', N'JETTA', NULL)


END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322180315_Seed-VehiclesData-to-database')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220322180315_Seed-VehiclesData-to-database', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322180426_Seed-ListedVehiclesData-to-database')
BEGIN

                INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'02aac73b-c806-4dbc-a944-fe2b1ff697bd', N'Automatic', N'Gray', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'01c021d5-9476-47bc-bcf2-a5a25eb65b06', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-21 10:27:48', N'0001-01-01 00:00:00', 0, N'FWD', 103000, 22000, 2018, N'0001-01-01 00:00:00', 0)
    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'0af81327-6efa-4296-a62a-ad51e8b52361', N'Automatic', N'White', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'89c2d16d-15ed-4f41-aa2e-c121b34d16cd', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-18 21:29:48', N'0001-01-01 00:00:00', 0, N'FWD', 411000, 105000, 2018, N'0001-01-01 00:00:00', 0)
    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'1e01bbd9-9813-4451-a76f-060b043eb926', N'Manual', N'Yellow', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'e1052833-153e-4b86-82ed-5f31f5da5a6a', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-21 10:11:04', N'0001-01-01 00:00:00', 0, N'FWD', 71000, 25000, 2020, N'0001-01-01 00:00:00', 0)
    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'1f9a69b7-e585-4103-a62e-854c70e4fe6d', N'Manual', N'Red', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'11aef7d1-1e52-4708-aa26-8de60faa405f', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-18 21:10:53', N'0001-01-01 00:00:00', 0, N'FWD', 185000, 15000, 2015, N'0001-01-01 00:00:00', 0)
    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'26b31ed8-09a1-4fe7-a2c3-dd18c96bce78', N'Automatic', N'Red', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'92058494-5b43-40a0-87bb-bb7e862e9424', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-18 21:22:50', N'0001-01-01 00:00:00', 0, N'FWD', 312000, 9300, 2009, N'0001-01-01 00:00:00', 0)
    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'30c93d96-aae5-4c19-971a-9c510f1dcece', N'Automatic', N'White', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'b97d25d7-cca8-44fc-bb5c-f9ff8413e568', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-18 21:17:12', N'0001-01-01 00:00:00', 0, N'AWD', 158000, 40000, 2019, N'0001-01-01 00:00:00', 0)
    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'3efedc5a-98b9-4370-95ff-3f40230192ec', N'Manual', N'Red', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'73937a42-af8a-41b5-992c-8dc5b76d36b7', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-21 10:16:09', N'0001-01-01 00:00:00', 0, N'RWD', 320000, 14500, 2014, N'0001-01-01 00:00:00', 0)
    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'64bf6daa-2787-494c-837c-bb256813d8f2', N'Manual', N'Red', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'a94d5d01-989b-4242-bf34-6e90abadd67c', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-18 20:47:29', N'2022-03-20 01:55:07', 0, N'FWD', 189000, 14000, 2017, N'0001-01-01 00:00:00', 0)
    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'6827bb48-b3c3-4dfb-805d-46fe460fde87', N'Automatic', N'White', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'd603ea22-d235-433c-aa16-e79c370091d0', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-18 21:26:14', N'0001-01-01 00:00:00', 0, N'4x4', 295000, 19000, 2018, N'0001-01-01 00:00:00', 0)
    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'6f3042f2-0ce8-4034-9a93-136943f58e03', N'Manual', N'Green', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'0abbcd08-6baa-4ac5-8bac-fa77a169299f', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-18 20:45:34', N'2022-03-20 02:00:55', 0, N'4x4', 549600, 11900, 1989, N'0001-01-01 00:00:00', 0)
    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'd8172244-6cb9-4e8d-be6e-f7c88d986bcc', N'Automatic', N'Gray', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'f427897b-0e52-4171-b25c-434838a584b1', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-21 10:22:59', N'0001-01-01 00:00:00', 0, N'AWD', 54000, 18000, 2010, N'0001-01-01 00:00:00', 0)
    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'e4d6feae-953d-4faf-9e46-b85940df990f', N'Manual', N'Black', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'3bd28bf5-a7ac-41ef-925d-7d44b12c2620', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-18 21:05:57', N'2022-03-20 02:12:24', 0, N'FWD', 306000, 11400, 2013, N'0001-01-01 00:00:00', 0)
    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'e539a4b7-97d2-4e64-b5e5-a1b15cae1572', N'Automatic', N'Gray', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'bea62787-7a84-44c5-bd38-bd70c3d763e2', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-18 21:33:17', N'0001-01-01 00:00:00', 0, N'FWD', 580000, 16500, 1992, N'0001-01-01 00:00:00', 0)
    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'f8cf56be-e51e-4c63-a4d8-6373e8b819f8', N'Automatic', N'Black', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'a66e2235-fac5-4b1d-9e37-03d31ec09483', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-18 20:46:48', N'2022-03-20 02:10:36', 0, N'FWD', 369000, 8500, 2008, N'0001-01-01 00:00:00', 0)


END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322180426_Seed-ListedVehiclesData-to-database')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220322180426_Seed-ListedVehiclesData-to-database', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322180747_Seed-IntentsData-to-database')
BEGIN

                    INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'181546d9-dee8-4f35-81cb-7cd79ee20a2c', N'2022-03-20 02:08:38', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'1f9a69b7-e585-4103-a62e-854c70e4fe6d', N'0001-01-01 00:00:00', 0, 0, 0)
    INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'2869c41d-856a-4f05-b4a0-35fcdea361a6', N'2022-03-20 02:08:32', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'e4d6feae-953d-4faf-9e46-b85940df990f', N'2022-03-20 02:12:24', 0, 0, 0)
    INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'7fdaac96-659c-4a9b-9ffc-c4372a9c4622', N'2022-03-20 00:37:02', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'6f3042f2-0ce8-4034-9a93-136943f58e03', N'2022-03-20 02:00:55', 0, 0, 0)
    INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'827c7f78-0f3f-49fc-8f96-bdcf1e4db500', N'2022-03-20 02:08:25', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'f8cf56be-e51e-4c63-a4d8-6373e8b819f8', N'2022-03-20 02:10:36', 0, 0, 0)
    INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'b40a411c-92f8-4cab-aa22-116cab49c0be', N'2022-03-20 00:31:26', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'64bf6daa-2787-494c-837c-bb256813d8f2', N'2022-03-20 01:53:00', 0, 0, 1)

END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220322180747_Seed-IntentsData-to-database')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220322180747_Seed-IntentsData-to-database', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220323195752_Seed-ListedVehicleData02-to-database')
BEGIN

                    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'02aac73b-c806-4dbc-a944-fe2b1ff697bd', N'Automatic', N'Gray', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'01c021d5-9476-47bc-bcf2-a5a25eb65b06', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-23 13:00:32', N'0001-01-01 00:00:00', 0, N'FWD', 103000, 22000, 2018, N'0001-01-01 00:00:00', 0)
                    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'191b1dcb-09c9-499d-b0c9-37069ae1636f', N'Automatic', N'Red', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'7a5b1399-2532-4fc0-b20e-9f517b04582a', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'2022-03-23 13:55:27', N'0001-01-01 00:00:00', 0, N'FWD', 193000, 20000, 2020, N'0001-01-01 00:00:00', 0)
                    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'64bf6daa-2787-494c-837c-bb256813d8f2', N'Manual', N'Red', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'a94d5d01-989b-4242-bf34-6e90abadd67c', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'2022-03-23 12:57:21', N'0001-01-01 00:00:00', 0, N'FWD', 189000, 14000, 2017, N'0001-01-01 00:00:00', 0)
                    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'808dda8d-5cbd-4264-9965-4a33cc1b9bd7', N'Automatic', N'Gray', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'788f2175-4e43-48fe-8110-28d198999109', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'2022-03-23 14:07:37', N'0001-01-01 00:00:00', 0, N'FWD', 250000, 30000, 2000, N'0001-01-01 00:00:00', 0)
                    INSERT INTO [dbo].[ListedVehicles] ([ListedVehicleId], [Transmission], [Color], [Description], [VehicleId], [ApplicationUserId], [DateListed], [DateSold], [IsSold], [DriveTrain], [Mileage], [Price], [Year], [DateDeleted], [IsDeleted]) VALUES (N'f4926e5f-d3f6-47f4-83a7-c2e3fe9d390f', N'Manual', N'White', N'This is a tutorial vehicle, created to demonstrate the functionalities of this web application.', N'940d43bd-655f-4c78-9f5a-b556e42922ce', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'2022-03-23 14:12:03', N'0001-01-01 00:00:00', 0, N'FWD', 150000, 25000, 2018, N'0001-01-01 00:00:00', 0)
                
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220323195752_Seed-ListedVehicleData02-to-database')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220323195752_Seed-ListedVehicleData02-to-database', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220323203643_Seed-IntentsData02-to-database')
BEGIN

                    INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'456a1985-1dea-4a8e-ac8c-54d8fdf4596d', N'2022-03-23 14:27:47', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'dce92991-de0e-4390-b83b-87f8d7047f50', N'808dda8d-5cbd-4264-9965-4a33cc1b9bd7', N'0001-01-01 00:00:00', 0, 0, 0)
                    INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'64c2dbae-0a6d-41d8-889c-de0121938dda', N'2022-03-23 14:27:04', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'dce92991-de0e-4390-b83b-87f8d7047f50', N'f4926e5f-d3f6-47f4-83a7-c2e3fe9d390f', N'0001-01-01 00:00:00', 0, 0, 0)
                    INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'ab98112a-f74a-4b13-9e91-49fa4e4283fb', N'2022-03-23 14:28:01', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'dce92991-de0e-4390-b83b-87f8d7047f50', N'191b1dcb-09c9-499d-b0c9-37069ae1636f', N'0001-01-01 00:00:00', 0, 0, 0)
                    INSERT INTO [dbo].[Intents] ([IntentId], [DateOfIntent], [IntentReceiverId], [IntentSenderId], [ListedVehicleId], [DateSold], [IsSold], [IsDiscarded], [IsRead]) VALUES (N'b40a411c-92f8-4cab-aa22-116cab49c0be', N'2022-03-20 00:31:26', N'eff2f35a-488b-49da-84d4-b396b60d2ade', N'0ec1aac5-4c9e-4213-b573-9984c16d8ec1', N'64bf6daa-2787-494c-837c-bb256813d8f2', N'2022-03-20 01:53:00', 0, 0, 0)
                
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220323203643_Seed-IntentsData02-to-database')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220323203643_Seed-IntentsData02-to-database', N'6.0.2');
END;
GO

COMMIT;
GO

