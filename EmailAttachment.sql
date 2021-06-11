USE [RPA]
GO

/****** Object:  Table [dbo].[EmailAttachment]    Script Date: 3/23/2021 10:39:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmailAttachment](
	[AttachmentID] [int] IDENTITY(1,1) NOT NULL,
	[MailID] [varchar](62) NOT NULL,
	[AttachmentPath] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[AttachmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
