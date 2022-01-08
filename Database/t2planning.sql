/*CREATE DATABASE T2PLANNING*/

/****** Create Table  **********/
/****** Table **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table](
	[tableId] [int] IDENTITY(1,1) NOT NULL,
	[tableAdmin] [nvarchar](50) NOT NULL,
	[tableName] [nvarchar](50) NOT NULL,
	[tableTeam] [int] NOT NULL,
	[tablePermission] [int] NOT NULL,
 CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED 
(
	[tableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** User **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Uid] [nvarchar](50) NOT NULL,
	[userName] [nvarchar](50) NOT NULL,
	[userEmail] [nvarchar](50) NOT NULL,
	[userAvatar] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Member **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[memberId] [int] IDENTITY(1,1) NOT NULL,
	[tableId] [int] NOT NULL,
	[Uid] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[memberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** ListCard **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListCard](
	[listCardId] [int] IDENTITY(1,1) NOT NULL,
	[listCardName] [nvarchar](50) NOT NULL,
	[tableId] [int] NOT NULL,
	FOREIGN KEY (tableId) REFERENCES [dbo].[Table](tableId),
 CONSTRAINT [PK_ListCard] PRIMARY KEY CLUSTERED 
(
	[listCardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Card **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Card](
	[cardId] [int] IDENTITY(1,1) NOT NULL,
	[tableId] [int] NOT NULL,
	[listCardId] [int] NOT NULL,
	[cardName] [nvarchar](50) NOT NULL,
	[cardDescription] [nvarchar](50) NULL,
	[cardDeadline] [DATETIME] NULL,
	FOREIGN KEY (tableId) REFERENCES [dbo].[Table](tableId),
	FOREIGN KEY (listCardId) REFERENCES [dbo].[ListCard](listCardId),
 CONSTRAINT [PK_Card] PRIMARY KEY CLUSTERED 
(
	[cardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Procedure  **********/
/****** InsertTable  **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[InsertTable]  
(
@tableAdmin nvarchar(50),
@tableName nvarchar(50),
@tableTeam int,  
@tablePermission int 
)  
as  
begin 
	INSERT INTO [dbo].[Table]  
           ([tableAdmin]
		   ,[tableName]  
           ,[tableTeam] 
           ,[tablePermission])  
	OUTPUT Inserted.[tableId]
    VALUES  
           (  
		   @tableAdmin,
           @tableName,  
           @tableTeam,  
           @tablePermission
          )
End  
/****** GetTable  **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetTable]
(
@Uid nvarchar(50)
)
as  
begin  

	select * 
	from [dbo].[Table]
	where [tableId] in (select [tableId]
						from [dbo].[Member]
						where [Uid] = @Uid)

End
GO

/****** UpdateTable  **********/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[UpdateTable]
(
@tableId int,
@tableName nvarchar(50)
)
AS 
BEGIN 
	UPDATE [dbo].[Table] 
	SET [tableName] =  @tableName
	WHERE [tableId] = @tableId
END 

/****** DeleteTable  **********/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[DeleteTable]
@tableId int 
AS 
BEGIN 
	DELETE FROM [dbo].[Card]
	WHERE [tableId] = @tableId

	DELETE FROM [dbo].[ListCard]
	WHERE [tableId] = @tableId

	DELETE FROM [dbo].[Member]
	WHERE [tableId] = @tableId

	DELETE FROM [dbo].[Table]
	WHERE [tableId] = @tableId
END 

/****** InsertUser  **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[InsertUser]  
(
@Uid nvarchar(50),
@userName nvarchar(50),
@userEmail nvarchar(50),
@userAvatar nvarchar(50)
)  
as  
begin  
  
	INSERT INTO [dbo].[User] 
           ([Uid]
		   ,[userName]  
           ,[userEmail] 
           ,[userAvatar])  
     VALUES  
           (  
		   @Uid,
           @userName,  
           @userEmail,  
           @userAvatar
          )

End 

/****** GetUser  **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetUser]
(
@Uid nvarchar(50)
)
as  
begin  

	select * 
	from [dbo].[User]
	where [Uid] = @Uid

End
GO

/****** UpdateUser  **********/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[UpdateUser]
(
@Uid nvarchar(50),
@userName nvarchar(50),
@userAvatar nvarchar(50)
)
AS 
BEGIN 
	UPDATE [dbo].[User] 
	SET [userName] =  @userName, [userAvatar] = @userAvatar 
	WHERE [Uid] = @Uid 
END 


/****** InsertMember  **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[InsertMember]  
(
@tableId int,
@Uid nvarchar(50)
)  
as  
begin  
  
	INSERT INTO [dbo].[Member] 
           ([tableId]
		   ,[Uid])  
     VALUES  
           (  
		   @tableId,
           @Uid
          )  
End 

/****** GetMember **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetMember]
(
@Uid nvarchar(50)
)
as  
begin  

	select * 
	from [dbo].[Member]
	where [Uid] = @Uid

End
GO

/****** GetTableMember **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetTableMember]
(
@tableId int
)
as  
begin  

	select * 
	from [dbo].[Member]
	where [tableId] = @tableId

End
GO
/****** DeleteMember  **********/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[DeleteMember]
@tableId int,
@Uid nvarchar(50)
AS 
BEGIN 
	DELETE FROM [dbo].[Member]
	WHERE [tableId] = @tableId and [Uid] = @Uid
END 

Select *
FROM [dbo].[Member]
WHERE [tableId] = 11  and [Uid] = 'Dsp4twV6jjOboXYSiT1mgBh9sJC3'

select * 
from [dbo].[Table]

select * 
from [dbo].[Member]

select * 
from [dbo].[User]
/****** InsertListCard  **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[InsertListCard]  
(
@listCardName nvarchar(50),
@tableId int
)  
as  
begin  
  
	INSERT INTO [dbo].[ListCard]  
           ([listCardName]  
           ,[tableId])  
     VALUES  
           (  
           @listCardName,  
           @tableId
		   )
End 

/****** GetListCard  **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetListCard]
(
@Uid nvarchar(50)
)
as  
begin  

	select * 
	from [dbo].[ListCard]
	where [tableId] in (select [tableId]
						from [dbo].[Member]
						where [Uid] = @Uid)

End
GO

/****** UpdateListcard  **********/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[UpdateListCard]
(
@listCardId int,
@listCardName nvarchar(50)
)
AS 
BEGIN 
	UPDATE [dbo].[ListCard]
	SET [listCardName] =  @listCardName
	WHERE [listCardId] = @listCardId
END 

/****** DeleteListCard  **********/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[DeleteListCard]
@listCardId int 
AS 
BEGIN 
	DELETE FROM [dbo].[Card]
	WHERE [listCardId] = @listCardId

	DELETE FROM [dbo].[ListCard]
	WHERE [listCardId] = @listCardId
END 



/****** InsertCard  **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[InsertCard]  
(
@tableId int,
@listCardId int,
@cardName nvarchar(50),
@cardDescription nvarchar(50),
@cardDeadline DATETIME
)  
as  
begin  
  
INSERT INTO [dbo].[Card]  
           ([tableId],
			[listCardId],
			[cardName],
			[cardDescription],
			[cardDeadline])  
     VALUES  
           (  
			@tableId,
			@listCardId,
			@cardName,
			@cardDescription,
			@cardDeadline
		   )
End 

/****** GetCard  **********/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[GetCard]
(
@Uid nvarchar(50)
)
as  
begin  

	select * 
	from [dbo].[Card]
	where [tableId] in (select [tableId]
						from [dbo].[Member]
						where [Uid] = @Uid)

End
GO

/****** UpdateCard  **********/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[UpdateCard]
(
@cardId int,
@cardName nvarchar(50),
@cardDescription nvarchar(50),
@cardDeadline DateTime
)
AS 
BEGIN 
	UPDATE [dbo].[Card]
	SET [cardName] =  @cardName, [cardDescription] = @cardDescription, [cardDeadline] = @cardDeadline
	WHERE [cardId] = @cardId
END 

/****** DeleteCard  **********/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[DeleteCard]
@cardId int 
AS 
BEGIN 
	DELETE FROM [dbo].[Card]
	WHERE [cardId] = @cardId
END 


/******************************************/

select * from [dbo].[Table]

select * from [dbo].[Card]