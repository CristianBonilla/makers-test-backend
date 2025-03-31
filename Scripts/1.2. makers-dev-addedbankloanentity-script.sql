BEGIN TRANSACTION;
GO

CREATE TABLE [dbo].[BankLoan] (
    [BankLoanId] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [UserId] uniqueidentifier NOT NULL,
    [Status] nvarchar(450) NOT NULL,
    [Amount] decimal(14,2) NOT NULL,
    [PaymentTerm] datetimeoffset NOT NULL,
    [Created] datetimeoffset NOT NULL DEFAULT (GETUTCDATE()),
    [Version] rowversion NOT NULL,
    CONSTRAINT [PK_BankLoan] PRIMARY KEY ([BankLoanId]),
    CONSTRAINT [FK_BankLoan_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId]) ON DELETE CASCADE
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BankLoanId', N'Amount', N'PaymentTerm', N'Status', N'UserId') AND [object_id] = OBJECT_ID(N'[dbo].[BankLoan]'))
    SET IDENTITY_INSERT [dbo].[BankLoan] ON;
INSERT INTO [dbo].[BankLoan] ([BankLoanId], [Amount], [PaymentTerm], [Status], [UserId])
VALUES ('2a08545b-cf87-4501-ae6d-cc389ad3d6a0', 1900000.0, '2025-09-19T20:11:00.0000000+03:00', N'Approved', 'c880a1fd-2c32-46cb-b744-a6fad6175a53'),
('c51322bd-4ae7-48dc-a1d4-55b8f3660f6b', 70800000.0, '2025-06-22T15:20:00.0000000+03:00', N'Pending', 'c880a1fd-2c32-46cb-b744-a6fad6175a53'),
('d4aca576-9a20-4ce4-b349-fc528fb76ce7', 120500000.0, '2027-02-12T10:00:00.0000000+03:00', N'Rejected', 'c880a1fd-2c32-46cb-b744-a6fad6175a53');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BankLoanId', N'Amount', N'PaymentTerm', N'Status', N'UserId') AND [object_id] = OBJECT_ID(N'[dbo].[BankLoan]'))
    SET IDENTITY_INSERT [dbo].[BankLoan] OFF;
GO

CREATE INDEX [IX_BankLoan_Status_Amount] ON [dbo].[BankLoan] ([Status], [Amount]);
GO

CREATE INDEX [IX_BankLoan_UserId] ON [dbo].[BankLoan] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250401131249_AddedBankLoanEntity', N'8.0.14');
GO

COMMIT;
GO
