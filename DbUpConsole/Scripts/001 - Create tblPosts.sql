CREATE TABLE [dbo].[tblPosts]
(
	[PostId] INT NOT NULL PRIMARY KEY, 
    [Id] INT NOT NULL, 
    [UserId] INT NOT NULL,
    [Title] NVARCHAR(200) NOT NULL, 
    [Body] NVARCHAR(500) NOT NULL, 
)