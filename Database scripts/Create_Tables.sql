

CREATE TABLE [dbo].[Category] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR (100)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

------------------------------------------------------------------------

CREATE TABLE [dbo].[Tag] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR (100)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

-----------------------------------------------------------------------

CREATE TABLE [dbo].[User] (
    [Id]  UNIQUEIDENTIFIER NOT NULL,
    [Mid] NVARCHAR(10)      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

------------------------------------------------------------------------

CREATE TABLE [dbo].[Question] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [IsActive]     BIT              DEFAULT 1 NOT NULL,
    [CreatedBy]    UNIQUEIDENTIFIER     NOT NULL,
    [CreatedDate]  DATETIME             NOT NULL DEFAULT GETDATE(),
    [ModifiedDate] DATETIME             NULL DEFAULT NULL,
    [Title]        NVARCHAR (150)     NOT NULL,
    [Description]  NVARCHAR(MAX)    NOT NULL,
    [CategoryId]   UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

---------------------------------------------------------------------

CREATE TABLE [dbo].[Answer] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [QuestionId]          UNIQUEIDENTIFIER NOT NULL,
    [Description]  NVARCHAR(MAX)    NOT NULL,
    [CreatedDate]  DATETIME             NOT NULL DEFAULT GETDATE(),
    [ModifiedDate] DATETIME             NULL DEFAULT NULL,
    [CreatedBy]    UNIQUEIDENTIFIER     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

------------------------------------------------------------------------

CREATE TABLE [dbo].[QuestionTags] (
    [Id]    UNIQUEIDENTIFIER NOT NULL,
    [TagId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

-----------------------------------------------------------------------

CREATE TABLE [dbo].[Upvote] (
    [Id]     UNIQUEIDENTIFIER NOT NULL,
    [QuestionId]    UNIQUEIDENTIFIER NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
