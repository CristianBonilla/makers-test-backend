START TRANSACTION;

ALTER TABLE dbo."User" ADD "RoleId" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

CREATE TABLE dbo."Role" (
    "RoleId" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "Name" character varying(30) NOT NULL,
    "DisplayName" character varying(50) NOT NULL,
    "Created" timestamp with time zone NOT NULL DEFAULT (now()),
    CONSTRAINT "PK_Role" PRIMARY KEY ("RoleId")
);

INSERT INTO dbo."Role" ("RoleId", "DisplayName", "Name")
VALUES ('84b6844f-60a9-4336-a44d-4f23ba5fd12a', 'Admin', 'AdminUser');
INSERT INTO dbo."Role" ("RoleId", "DisplayName", "Name")
VALUES ('f9c078da-36f3-435f-9f52-666c659d2285', 'Common User', 'CommonUser');

UPDATE dbo."User" SET "RoleId" = '84b6844f-60a9-4336-a44d-4f23ba5fd12a'
WHERE "UserId" = 'c880a1fd-2c32-46cb-b744-a6fad6175a53';

CREATE INDEX "IX_User_RoleId" ON dbo."User" ("RoleId");

CREATE UNIQUE INDEX "IX_Role_Name_DisplayName" ON dbo."Role" ("Name", "DisplayName");

ALTER TABLE dbo."User" ADD CONSTRAINT "FK_User_Role_RoleId" FOREIGN KEY ("RoleId") REFERENCES dbo."Role" ("RoleId") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250405075701_AddedRoleEntity', '8.0.14');

COMMIT;
