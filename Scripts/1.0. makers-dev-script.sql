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

CREATE TABLE [dbo].[Role] (
    [RoleId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [Name] varchar(30) NOT NULL,
    [DisplayName] varchar(50) NOT NULL,
    [Created] datetimeoffset NOT NULL DEFAULT (GETUTCDATE()),
    [Version] rowversion NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([RoleId])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'DisplayName', N'Name') AND [object_id] = OBJECT_ID(N'[dbo].[Role]'))
    SET IDENTITY_INSERT [dbo].[Role] ON;
INSERT INTO [dbo].[Role] ([RoleId], [DisplayName], [Name])
VALUES ('84b6844f-60a9-4336-a44d-4f23ba5fd12a', 'Admin', 'AdminUser'),
('f9c078da-36f3-435f-9f52-666c659d2285', 'Common User', 'CommonUser');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'DisplayName', N'Name') AND [object_id] = OBJECT_ID(N'[dbo].[Role]'))
    SET IDENTITY_INSERT [dbo].[Role] OFF;
GO

CREATE UNIQUE INDEX [IX_Role_Name_DisplayName] ON [dbo].[Role] ([Name], [DisplayName]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250401125750_InitialCreate', N'8.0.14');
GO

COMMIT;
GO
