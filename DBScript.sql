O
/****** Object:  Table [dbo].[Admixture_Proportions]    Script Date: 2/23/2022 7:49:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admixture_Proportions](
	[Admixture_Proportions_ID] [int] IDENTITY(1,1) NOT NULL,
	[Prototype_ID] [tinyint] NOT NULL,
	[Admixture_ID] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Admixture_Proportions_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admixtures]    Script Date: 2/23/2022 7:49:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admixtures](
	[Admixture_ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Minimum_Dosage] [decimal](3, 1) NOT NULL,
	[Maximum_Dosage] [decimal](3, 1) NOT NULL,
	[Is_ByCWT] [bit] NOT NULL,
 CONSTRAINT [PK_Admixtures] PRIMARY KEY CLUSTERED 
(
	[Admixture_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Aggregates]    Script Date: 2/23/2022 7:49:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aggregates](
	[Aggregate_ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Aggregate_Grade] [varchar](20) NOT NULL,
	[Absorption] [decimal](2, 1) NOT NULL,
	[Material_ID] [tinyint] NOT NULL,
 CONSTRAINT [PK_Aggregates] PRIMARY KEY CLUSTERED 
(
	[Aggregate_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CA_Proportions]    Script Date: 2/23/2022 7:49:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CA_Proportions](
	[CA_Proportions_ID] [int] IDENTITY(1,1) NOT NULL,
	[Prototype_ID] [tinyint] NOT NULL,
	[CA_ID] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CA_Proportions_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cement_Proportions]    Script Date: 2/23/2022 7:49:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cement_Proportions](
	[Cement_Proportions_ID] [int] IDENTITY(1,1) NOT NULL,
	[Prototype_ID] [tinyint] NOT NULL,
	[Cement_ID] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Cement_Proportions_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cements]    Script Date: 2/23/2022 7:49:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cements](
	[Cement_ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Cement_Type] [varchar](30) NULL,
	[Material_ID] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Cement_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coarse_Aggregates]    Script Date: 2/23/2022 7:49:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coarse_Aggregates](
	[CA_ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[CA_Size] [decimal](4, 3) NOT NULL,
	[CA_UW] [decimal](4, 1) NOT NULL,
	[Aggregate_ID] [tinyint] NOT NULL,
 CONSTRAINT [PK_Coarse_Aggregates] PRIMARY KEY CLUSTERED 
(
	[CA_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fine_Aggregates]    Script Date: 2/23/2022 7:49:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fine_Aggregates](
	[FN_ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Fineness_Modulus] [decimal](2, 1) NOT NULL,
	[Aggregate_ID] [tinyint] NOT NULL,
 CONSTRAINT [PK_Fine_Aggregates] PRIMARY KEY CLUSTERED 
(
	[FN_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FN_Proportions]    Script Date: 2/23/2022 7:49:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FN_Proportions](
	[FN_Proportions_ID] [int] IDENTITY(1,1) NOT NULL,
	[Prototype_ID] [tinyint] NOT NULL,
	[FN_ID] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FN_Proportions_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Materials]    Script Date: 2/23/2022 7:49:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Materials](
	[Material_ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Material_Name] [varchar](50) NOT NULL,
	[Material_Source] [varchar](50) NULL,
	[Relative_Density] [decimal](3, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Material_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prototypes]    Script Date: 2/23/2022 7:49:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prototypes](
	[Prototype_ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[Prototype_Name] [varchar](10) NOT NULL,
	[Prototype_Serial] [tinyint] NOT NULL,
	[Concrete_Class] [varchar](4) NOT NULL,
	[Is_Air_Entrained] [bit] NOT NULL,
	[Target_Air] [decimal](4, 2) NULL,
	[Total_CM] [int] NULL,
	[Total_CA] [decimal](3, 2) NULL,
	[Calculated_Density] [decimal](5, 1) NULL,
 CONSTRAINT [PK_Prototypes] PRIMARY KEY CLUSTERED 
(
	[Prototype_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SCM_Proportions]    Script Date: 2/23/2022 7:49:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SCM_Proportions](
	[SCM_Proportions_ID] [int] IDENTITY(1,1) NOT NULL,
	[Prototype_ID] [tinyint] NOT NULL,
	[SCM_ID] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SCM_Proportions_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SCMs]    Script Date: 2/23/2022 7:49:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SCMs](
	[SCM_ID] [tinyint] IDENTITY(1,1) NOT NULL,
	[SCM_Class] [varchar](30) NULL,
	[Material_ID] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[SCM_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Materials] ADD  DEFAULT ((0.00)) FOR [Relative_Density]
GO
ALTER TABLE [dbo].[Admixture_Proportions]  WITH CHECK ADD  CONSTRAINT [FK_Admixtures_Admixture_Proportions] FOREIGN KEY([Admixture_ID])
REFERENCES [dbo].[Admixtures] ([Admixture_ID])
GO
ALTER TABLE [dbo].[Admixture_Proportions] CHECK CONSTRAINT [FK_Admixtures_Admixture_Proportions]
GO
ALTER TABLE [dbo].[Admixture_Proportions]  WITH CHECK ADD  CONSTRAINT [FK_Prototypes_Admixture_Proportions] FOREIGN KEY([Prototype_ID])
REFERENCES [dbo].[Prototypes] ([Prototype_ID])
GO
ALTER TABLE [dbo].[Admixture_Proportions] CHECK CONSTRAINT [FK_Prototypes_Admixture_Proportions]
GO
ALTER TABLE [dbo].[Aggregates]  WITH CHECK ADD  CONSTRAINT [FK_Materials_Aggregates] FOREIGN KEY([Material_ID])
REFERENCES [dbo].[Materials] ([Material_ID])
GO
ALTER TABLE [dbo].[Aggregates] CHECK CONSTRAINT [FK_Materials_Aggregates]
GO
ALTER TABLE [dbo].[CA_Proportions]  WITH CHECK ADD  CONSTRAINT [FK_Course_Aggregates_CA_Proportions] FOREIGN KEY([CA_ID])
REFERENCES [dbo].[Coarse_Aggregates] ([CA_ID])
GO
ALTER TABLE [dbo].[CA_Proportions] CHECK CONSTRAINT [FK_Course_Aggregates_CA_Proportions]
GO
ALTER TABLE [dbo].[CA_Proportions]  WITH CHECK ADD  CONSTRAINT [FK_Prototypes_CA_Proportions] FOREIGN KEY([Prototype_ID])
REFERENCES [dbo].[Prototypes] ([Prototype_ID])
GO
ALTER TABLE [dbo].[CA_Proportions] CHECK CONSTRAINT [FK_Prototypes_CA_Proportions]
GO
ALTER TABLE [dbo].[Cement_Proportions]  WITH CHECK ADD  CONSTRAINT [FK_Cements_Cement_Proportions] FOREIGN KEY([Cement_ID])
REFERENCES [dbo].[Cements] ([Cement_ID])
GO
ALTER TABLE [dbo].[Cement_Proportions] CHECK CONSTRAINT [FK_Cements_Cement_Proportions]
GO
ALTER TABLE [dbo].[Cement_Proportions]  WITH CHECK ADD  CONSTRAINT [FK_Prototypes_Cement_Proportions] FOREIGN KEY([Prototype_ID])
REFERENCES [dbo].[Prototypes] ([Prototype_ID])
GO
ALTER TABLE [dbo].[Cement_Proportions] CHECK CONSTRAINT [FK_Prototypes_Cement_Proportions]
GO
ALTER TABLE [dbo].[Cements]  WITH CHECK ADD  CONSTRAINT [FK_Materials_Cements] FOREIGN KEY([Material_ID])
REFERENCES [dbo].[Materials] ([Material_ID])
GO
ALTER TABLE [dbo].[Cements] CHECK CONSTRAINT [FK_Materials_Cements]
GO
ALTER TABLE [dbo].[Coarse_Aggregates]  WITH CHECK ADD  CONSTRAINT [FK_Coarse_Aggregates_Aggregates] FOREIGN KEY([Aggregate_ID])
REFERENCES [dbo].[Aggregates] ([Aggregate_ID])
GO
ALTER TABLE [dbo].[Coarse_Aggregates] CHECK CONSTRAINT [FK_Coarse_Aggregates_Aggregates]
GO
ALTER TABLE [dbo].[Fine_Aggregates]  WITH CHECK ADD  CONSTRAINT [FK_Aggregates_Fine_Aggregates] FOREIGN KEY([Aggregate_ID])
REFERENCES [dbo].[Aggregates] ([Aggregate_ID])
GO
ALTER TABLE [dbo].[Fine_Aggregates] CHECK CONSTRAINT [FK_Aggregates_Fine_Aggregates]
GO
ALTER TABLE [dbo].[FN_Proportions]  WITH CHECK ADD  CONSTRAINT [FK_Fine_Aggregates_FN_Proportions] FOREIGN KEY([FN_ID])
REFERENCES [dbo].[Fine_Aggregates] ([FN_ID])
GO
ALTER TABLE [dbo].[FN_Proportions] CHECK CONSTRAINT [FK_Fine_Aggregates_FN_Proportions]
GO
ALTER TABLE [dbo].[FN_Proportions]  WITH CHECK ADD  CONSTRAINT [FK_Prototypes_FN_Proportions] FOREIGN KEY([Prototype_ID])
REFERENCES [dbo].[Prototypes] ([Prototype_ID])
GO
ALTER TABLE [dbo].[FN_Proportions] CHECK CONSTRAINT [FK_Prototypes_FN_Proportions]
GO
ALTER TABLE [dbo].[SCM_Proportions]  WITH CHECK ADD  CONSTRAINT [FK_Prototypes_SCM_Proportions] FOREIGN KEY([Prototype_ID])
REFERENCES [dbo].[Prototypes] ([Prototype_ID])
GO
ALTER TABLE [dbo].[SCM_Proportions] CHECK CONSTRAINT [FK_Prototypes_SCM_Proportions]
GO
ALTER TABLE [dbo].[SCM_Proportions]  WITH CHECK ADD  CONSTRAINT [FK_SCMS_SCM_Proportions] FOREIGN KEY([SCM_ID])
REFERENCES [dbo].[SCMs] ([SCM_ID])
GO
ALTER TABLE [dbo].[SCM_Proportions] CHECK CONSTRAINT [FK_SCMS_SCM_Proportions]
GO
ALTER TABLE [dbo].[SCMs]  WITH CHECK ADD  CONSTRAINT [FK_Materials_SCMs] FOREIGN KEY([Material_ID])
REFERENCES [dbo].[Materials] ([Material_ID])
GO
ALTER TABLE [dbo].[SCMs] CHECK CONSTRAINT [FK_Materials_SCMs]
GO
USE [master]
GO
ALTER DATABASE [C:\USERS\DUSTI\ONEDRIVE\DOCUMENTS\CAPSTONE PROJECT\CONCRETE MIX DESIGN TRACKER\CONCRETE MIX DESIGN TRACKER\MIXDATA.MDF] SET  READ_WRITE 
GO
