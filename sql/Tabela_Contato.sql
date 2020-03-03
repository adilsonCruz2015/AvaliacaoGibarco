USE [AvaliacaoTailorit]
GO

/****** Object:  Table [dbo].[Contato]    Script Date: 28/02/2020 07:36:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contato](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NULL,
	[Canal] [varchar](100) NULL,
	[Valor] [varchar](100) NULL,
	[Obs] [varchar](500) NULL,
 CONSTRAINT [PK_Contato] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


