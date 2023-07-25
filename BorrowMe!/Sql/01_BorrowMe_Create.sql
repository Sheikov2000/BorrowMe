CREATE TABLE [Users] (
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
  [ItemId] int NOT NULL,
  [TakeDate] datetime NOT NULL,
  [ReturnDate] datetime NOT NULL,
  [IsReserved] bit NOT NULL
)
GO

CREATE TABLE [Item] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserId] int,
  [Name] nvarchar(255) NOT NULL,
  [Description] nvarchar(255) NOT NULL,
  [ItemTypeId] int NOT NULL,
  [ImageUrl] nvarchar(255)
)
GO

CREATE TABLE [ItemType] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL
)
GO


ALTER TABLE [Messages] ADD FOREIGN KEY ([SenderId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [Messages] ADD FOREIGN KEY ([RecipientId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [Items] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [Borrowing] ADD FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [Borrowing] ADD FOREIGN KEY ([BorrowerId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [Items] ADD FOREIGN KEY ([ItemTypeId]) REFERENCES  [ItemType] ([Id])
GO

INSERT INTO  Users (FirstName, LastName, FirebaseId, Email, Phone, ZipCode) VALUES
    ('Nick', 'Nickson', 'faA3HakUjPXBMgQpRH6z0DxYWT03', 'nick@nick.org', '123-456-7890', '12345'),
    ('Michael', 'Johnson', 'odc13SBtfQUOhB8Re2PIDLbcs9g1', 'admin@admin.com', '555-555-5555', '67890')
   