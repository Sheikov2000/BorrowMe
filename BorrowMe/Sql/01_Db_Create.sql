USE [master]
GO

IF db_id('BorrowMe') IS NOT NULL
BEGIN
  ALTER DATABASE [BorrowMe] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP DATABASE [BorrowMe]
END
GO

CREATE DATABASE [BorrowMe]
GO

USE [BorrowMe]
GO

-----------------------------------------------------------------------------------
----------------------------------TABLE CREATION-----------------------------------

CREATE TABLE [User] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FirebaseId] nvarchar(255) NOT NULL,
  [FirstName] nvarchar(255) NOT NULL,
  [LastName] nvarchar(255) NOT NULL,
  [Email] nvarchar(255) NOT NULL,
  [Phone] nvarchar(255) NOT NULL,
  [ZipCode] int NOT NULL
)
GO

CREATE TABLE [Messages] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [SenderId] int NOT NULL,
  [RecipientId] int NOT NULL,
  [Text] nvarchar(255) NOT NULL,
  [IsRead] bit NOT NULL
)
GO

CREATE TABLE [Borrowing] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [BorrowerId] int NOT NULL,
  [ItemId] int NOT NULL
)
GO

CREATE TABLE [Item] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserId] int NOT NULL,
  [Title] nvarchar(255) NOT NULL,
  [Description] nvarchar(255) NOT NULL,
  [ImageUrl] nvarchar(255),
  [CategoryId] int NOT NULL
)
GO

CREATE TABLE [Category] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL
)
GO

ALTER TABLE [Messages] ADD FOREIGN KEY ([SenderId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Messages] ADD FOREIGN KEY ([RecipientId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Item] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [Borrowing] ADD FOREIGN KEY ([ItemId]) REFERENCES [Item] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [Borrowing] ADD FOREIGN KEY ([BorrowerId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [Item] ADD FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([id])
GO
