USE [RPA]
GO

/****** Object:  Table [dbo].[EmailMessageQueue]    Script Date: 3/23/2021 10:41:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmailMessageQueue](
	[MailID] [varchar](62) NOT NULL,
	[Sender] [varchar](200) NULL,
	[Receipient] [varchar](max) NULL,
	[Subject] [varchar](250) NULL,
	[Body] [nvarchar](max) NULL,
	[IsHtml] [bit] NULL,
	[Status] [varchar](10) NULL,
	[TryCount] [int] NULL,
	[Error] [varchar](max) NULL,
	[QueueTime] [datetime] NULL,
	[ProcessedTime] [datetime] NULL,
	[Resources] [varchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[MailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


