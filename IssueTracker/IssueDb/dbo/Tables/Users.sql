CREATE TABLE [dbo].[Users] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [UserName]  NVARCHAR (30)    NOT NULL,
    [Email]     NVARCHAR (450)   NOT NULL,
    [Password]  NVARCHAR (256)   NOT NULL,
    [IsDeleted] BIT DEFAULT (CONVERT([bit],(0))) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UserName_IsDeleted_Unique] UNIQUE ([UserName], [IsDeleted]),
    CONSTRAINT [Email_IsDeleted_Unique] UNIQUE ([Email], [IsDeleted]),
);
