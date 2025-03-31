BEGIN TRANSACTION;
GO

CREATE TABLE [dbo].[User] (
    [UserId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [RoleId] uniqueidentifier NOT NULL,
    [DocumentNumber] varchar(20) NOT NULL,
    [Mobile] varchar(20) NOT NULL,
    [Username] varchar(100) NOT NULL,
    [Password] varchar(max) NOT NULL,
    [Email] varchar(100) NOT NULL,
    [Firstname] varchar(50) NOT NULL,
    [Lastname] varchar(50) NOT NULL,
    [IsActive] bit NOT NULL,
    [Salt] varbinary(max) NOT NULL,
    [Created] datetimeoffset NOT NULL DEFAULT (GETUTCDATE()),
    [Version] rowversion NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_User_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'DocumentNumber', N'Email', N'Firstname', N'IsActive', N'Lastname', N'Mobile', N'Password', N'RoleId', N'Salt', N'Username') AND [object_id] = OBJECT_ID(N'[dbo].[User]'))
    SET IDENTITY_INSERT [dbo].[User] ON;
INSERT INTO [dbo].[User] ([UserId], [DocumentNumber], [Email], [Firstname], [IsActive], [Lastname], [Mobile], [Password], [RoleId], [Salt], [Username])
VALUES ('c880a1fd-2c32-46cb-b744-a6fad6175a53', '1023944678', 'cristian10camilo95@gmail.com', 'Cristian Camilo', CAST(1 AS bit), 'Bonilla', '+573163534451', 'oO63zcP14ylquh+FDz/NdI3v2Zltfk2p4gmLcZ6bmmwcwCJlEMjIH95egAt/BGZiWjKVTkblXoQOuxv/OAFegw==', '84b6844f-60a9-4336-a44d-4f23ba5fd12a', 0xA0EEB7CDC3F5E3296ABA1F850F3FCD748DEFD9996D7E4DA9E2098B719E9B9A6C, 'chris__boni');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'DocumentNumber', N'Email', N'Firstname', N'IsActive', N'Lastname', N'Mobile', N'Password', N'RoleId', N'Salt', N'Username') AND [object_id] = OBJECT_ID(N'[dbo].[User]'))
    SET IDENTITY_INSERT [dbo].[User] OFF;
GO

CREATE UNIQUE INDEX [IX_User_DocumentNumber_Mobile_Username_Email] ON [dbo].[User] ([DocumentNumber], [Mobile], [Username], [Email]);
GO

CREATE INDEX [IX_User_RoleId] ON [dbo].[User] ([RoleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250401130547_AddedUserEntity', N'8.0.14');
GO

COMMIT;
GO
