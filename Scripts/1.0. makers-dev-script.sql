CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'dbo') THEN
        CREATE SCHEMA dbo;
    END IF;
END $EF$;

CREATE TABLE dbo."User" (
    "UserId" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "DocumentNumber" character varying(20) NOT NULL,
    "Mobile" character varying(20) NOT NULL,
    "Username" character varying(100) NOT NULL,
    "Password" varchar NOT NULL,
    "Email" character varying(100) NOT NULL,
    "Firstname" character varying(50) NOT NULL,
    "Lastname" character varying(50) NOT NULL,
    "IsActive" boolean NOT NULL,
    "Salt" bytea NOT NULL,
    "Created" timestamp with time zone NOT NULL DEFAULT (now()),
    CONSTRAINT "PK_User" PRIMARY KEY ("UserId")
);

INSERT INTO dbo."User" ("UserId", "DocumentNumber", "Email", "Firstname", "IsActive", "Lastname", "Mobile", "Password", "Salt", "Username")
VALUES ('c880a1fd-2c32-46cb-b744-a6fad6175a53', '1023944678', 'cristian10camilo95@gmail.com', 'Cristian Camilo', TRUE, 'Bonilla', '+573163534451', 'oO63zcP14ylquh+FDz/NdI3v2Zltfk2p4gmLcZ6bmmwcwCJlEMjIH95egAt/BGZiWjKVTkblXoQOuxv/OAFegw==', BYTEA E'\\xA0EEB7CDC3F5E3296ABA1F850F3FCD748DEFD9996D7E4DA9E2098B719E9B9A6C', 'chris__boni');

CREATE UNIQUE INDEX "IX_User_DocumentNumber_Mobile_Username_Email" ON dbo."User" ("DocumentNumber", "Mobile", "Username", "Email");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250405075224_InitialCreate', '8.0.14');

COMMIT;
