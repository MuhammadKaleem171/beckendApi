USE [master]
GO
CREATEDATABASE[Tasbeeh]
GO
USE[Tasbeeh]
GO
CREATE TABLE [dbo].[Dua's](
	[DuaID] [int] IDENTITY(1,1) NOT NULL,
	[DuaInArabic] [nvarchar](400) NOT NULL,
	[DuaInEnglish] [varchar](400) NOT NULL,
	[Count] [int] NOT NULL,
	[DuaAudio] [varchar](100) NULL,
	[AudioType] [nchar](10) NULL,
 CONSTRAINT [PK_Dua's] PRIMARY KEY CLUSTERED 
(
	[DuaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[DuaID] [int] NOT NULL,
	[DuaInArabic] [nvarchar](max) NOT NULL,
	[Total] [int] NOT NULL,
	[NumberOf33] [int] NOT NULL,
	[Counter] [int] NOT NULL,
	[Date] [varchar](14) NOT NULL,
	[LogID] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Tasbeeh] SET  READ_WRITE 
GO
