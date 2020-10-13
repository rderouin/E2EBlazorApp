CREATE TABLE [dbo].[Authors]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Firstname] [nvarchar](50) NULL,
    [Lastname] [nvarchar](50) NULL,
    [Bio] [nvarchar](max) NULL,
    CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 4/1/2020 20:14:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books]
(
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Title] [nvarchar](100) NULL,
    [Year] [int] NULL,
    [ISBN] [nvarchar](50) NULL,
    [Summary] [nvarchar](150) NULL,
    [Image] [nvarchar](150) NULL,
    [Price] [money] NULL,
    [AuthorId] [int] NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Authors] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([Id])
GO