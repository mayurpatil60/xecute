USE [master]
GO
/****** Object:  Database [Xp_Connect]    Script Date: 8/31/2017 2:12:06 PM ******/
CREATE DATABASE [Xp_Connect]
GO
USE [Xp_Connect]
GO
/****** Object:  Table [dbo].[tbl_Candidate_Document]    Script Date: 8/31/2017 2:12:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Candidate_Document](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[DocumentId] [int] NULL,
	[Document_Url] [nvarchar](max) NULL,
	[Status] [varchar](50) NULL,
	[ApprovedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_DocumentList]    Script Date: 8/31/2017 2:12:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_DocumentList](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[DocumentName] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Roles]    Script Date: 8/31/2017 2:12:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_User_LoginDetails]    Script Date: 8/31/2017 2:12:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_User_LoginDetails](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Password] [binary](1) NULL,
	[FirstName] [varchar](100) NOT NULL,
	[MiddleName] [varchar](100) NULL,
	[LastName] [varchar](100) NOT NULL,
	[EmailId] [varchar](100) NOT NULL,
	[ContactNumber] [varchar](20) NOT NULL,
	[DOB] [varchar](20) NULL,
	[Gender] [varchar](10) NULL,
	[Role] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_Roles] ON 

GO
INSERT [dbo].[tbl_Roles] ([Id], [Role]) VALUES (1, N'Admin')
GO
INSERT [dbo].[tbl_Roles] ([Id], [Role]) VALUES (2, N'Recruiter')
GO
INSERT [dbo].[tbl_Roles] ([Id], [Role]) VALUES (3, N'Applicant')
GO
SET IDENTITY_INSERT [dbo].[tbl_Roles] OFF
GO
SET ANSI_PADDING ON

GO

CREATE TABLE tbl_Templates
(
	TemplateId int primary key identity(1,1),
	TemplateName Varchar(200) not null,
	TemplateContents nvarchar(max)
)

CREATE TABLE tbl_JoineeEmailTriggered
(
	EmailId varchar(100) not null,
	TemplateID int,
	Flag bit,
	Foreign key(TemplateId) references tbl_Templates(TemplateId)
)

create table tbl_user_basicInfo
(
	UserId int not null,
	BloodGroup varchar(20),
	RhGroup varchar(50),
	CurrentAddress nvarchar(max),
	PermanantAddress nvarchar(max),
	EmergrncyContactPersonName varchar(100),
	EmergencyContactNumber varchar(20),
	EmergencyContactRelationship varchar(100),
	PassportNumber varchar(50),
	ExpiryDate date,
	Flag bit,  -- set 1 if all information is submitted
	foreign key(UserId) references tbl_User_LoginDetails(UserId)
)


/****** Object:  Index [Uc_EmailId]    Script Date: 8/31/2017 2:12:06 PM ******/
ALTER TABLE [dbo].[tbl_User_LoginDetails] ADD  CONSTRAINT [Uc_EmailId] UNIQUE NONCLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbl_Candidate_Document]  WITH CHECK ADD FOREIGN KEY([ApprovedBy])
REFERENCES [dbo].[tbl_User_LoginDetails] ([UserId])
GO
ALTER TABLE [dbo].[tbl_Candidate_Document]  WITH CHECK ADD FOREIGN KEY([DocumentId])
REFERENCES [dbo].[tbl_DocumentList] ([DocumentId])
GO
ALTER TABLE [dbo].[tbl_Candidate_Document]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[tbl_User_LoginDetails] ([UserId])
GO
ALTER TABLE [dbo].[tbl_User_LoginDetails]  WITH CHECK ADD FOREIGN KEY([Role])
REFERENCES [dbo].[tbl_Roles] ([Id])
GO
USE [master]
GO
ALTER DATABASE [Xp_Connect] SET  READ_WRITE 
GO





