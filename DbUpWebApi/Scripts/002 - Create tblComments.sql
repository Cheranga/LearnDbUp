CREATE TABLE [dbo].[tblComments]
(
	[CommentId] INT NOT NULL PRIMARY KEY, 
    [PostId] INT NOT NULL, 
    [Id] INT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Body] NVARCHAR(500) NOT NULL, 
)