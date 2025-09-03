USE [WPF]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 02/09/2025 23:08:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NULL,
	[Status] [bit] NOT NULL,
	[Role] [varchar](50) NOT NULL
) ON [PRIMARY]
GO

