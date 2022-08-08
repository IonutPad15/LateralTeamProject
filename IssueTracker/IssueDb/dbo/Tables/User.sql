CREATE TABLE [dbo].[User] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    [UserName]  NVARCHAR (30)    NOT NULL,
    [Email]     NVARCHAR (450)   NOT NULL,
    [Password]  NVARCHAR (256)   NOT NULL,
    [IsDeleted] BIT DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [UserName_IsDeleted_Unique] UNIQUE ([UserName], [IsDeleted]),
    CONSTRAINT [Email_IsDeleted_Unique] UNIQUE ([Email], [IsDeleted]),
);
