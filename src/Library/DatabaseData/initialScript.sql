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

CREATE TABLE [Books] (
    [BookId] int NOT NULL IDENTITY,
    [Title] nvarchar(256) NOT NULL,
    [Description] nvarchar(max) NULL,
    [CoverUrl] nvarchar(max) NULL,
    [ReleaseYear] int NOT NULL,
    [FirstName] nvarchar(64) NULL,
    [Surname] nvarchar(512) NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY ([BookId])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231125180328_Initial', N'6.0.25');
GO

COMMIT;
GO

