START TRANSACTION;

CREATE TABLE dbo."BankLoan" (
    "BankLoanId" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "UserId" uuid NOT NULL,
    "Status" text NOT NULL,
    "Amount" numeric(14,2) NOT NULL,
    "PaymentTerm" timestamp with time zone NOT NULL,
    "Created" timestamp with time zone NOT NULL DEFAULT (now()),
    CONSTRAINT "PK_BankLoan" PRIMARY KEY ("BankLoanId"),
    CONSTRAINT "FK_BankLoan_User_UserId" FOREIGN KEY ("UserId") REFERENCES dbo."User" ("UserId") ON DELETE CASCADE
);

INSERT INTO dbo."BankLoan" ("BankLoanId", "Amount", "PaymentTerm", "Status", "UserId")
VALUES ('2a08545b-cf87-4501-ae6d-cc389ad3d6a0', 1900000.0, TIMESTAMPTZ '2025-09-19T20:11:00+03:00', 'Approved', 'c880a1fd-2c32-46cb-b744-a6fad6175a53');
INSERT INTO dbo."BankLoan" ("BankLoanId", "Amount", "PaymentTerm", "Status", "UserId")
VALUES ('c51322bd-4ae7-48dc-a1d4-55b8f3660f6b', 70800000.0, TIMESTAMPTZ '2025-06-22T15:20:00+03:00', 'Pending', 'c880a1fd-2c32-46cb-b744-a6fad6175a53');
INSERT INTO dbo."BankLoan" ("BankLoanId", "Amount", "PaymentTerm", "Status", "UserId")
VALUES ('d4aca576-9a20-4ce4-b349-fc528fb76ce7', 120500000.0, TIMESTAMPTZ '2027-02-12T10:00:00+03:00', 'Rejected', 'c880a1fd-2c32-46cb-b744-a6fad6175a53');

CREATE INDEX "IX_BankLoan_Status_Amount" ON dbo."BankLoan" ("Status", "Amount");

CREATE INDEX "IX_BankLoan_UserId" ON dbo."BankLoan" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250405080050_AddedBankLoanEntity', '8.0.14');

COMMIT;
