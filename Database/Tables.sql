USE [Detention facility]
GO

CREATE TABLE [DeliveriesOfDetainees](
	[DeliveryID] [int] IDENTITY(1,1) NOT NULL,
	[DetaineeID] [int] NOT NULL,
	[DetentionID] [int] NOT NULL,
	[PlaceAddress] [nvarchar](50) NOT NULL,
	[DeliveredByEmployeeID] [int] NOT NULL,
	[DeliveryDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Detentions] PRIMARY KEY CLUSTERED 
(
	[DeliveryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Detainees](
	[DetaineeID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[Patronymic] [nvarchar](30) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[MaritalStatus] [nvarchar](30) NOT NULL,
	[Job] [nvarchar](50) NOT NULL,
	[MobilePhoneNumber] [nvarchar](40) NOT NULL,
	[HomePhoneNumber] [nvarchar](40) NOT NULL,
	[Photo] [image] NULL,
	[ExtraInfo] [nvarchar](max) NULL,
	[ResidencePlace] [nvarchar](max) NOT NULL,
 CONSTRAINT [XPKDetainee] PRIMARY KEY CLUSTERED 
(
	[DetaineeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [Detentions](
	[DetentionID] [int] IDENTITY(1,1) NOT NULL,
	[DetentionDate] [datetime] NOT NULL,
	[DetainedByEmployeeID] [int] NOT NULL,
 CONSTRAINT [XPKDetention] PRIMARY KEY CLUSTERED 
(
	[DetentionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [DetentionsOfDetainees](
	[DetaineeID] [int] NOT NULL,
	[DetentionID] [int] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[Lastname] [nvarchar](30) NOT NULL,
	[Patronymic] [nvarchar](30) NOT NULL,
	[Position] [nvarchar](50) NOT NULL,
	[EmployeeRank] [nvarchar](30) NOT NULL,
 CONSTRAINT [XPKEmployee] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [ReleasesOfDetaineesOfDetainees](
	[ReleaseID] [int] IDENTITY(1,1) NOT NULL,
	[DetaineeID] [int] NOT NULL,
	[ReleasedByEmployeeID] [int] NOT NULL,
	[DetentionID] [int] NOT NULL,
	[ReleaseDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ReleaseOfDetainees] PRIMARY KEY CLUSTERED 
(
	[ReleaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [Users](
	[UserID] [int] NOT NULL,
	[Email] [nvarchar](30) NOT NULL,
	[Password] [nvarchar](30) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [DeliveriesOfDetainees]  WITH CHECK ADD  CONSTRAINT [FK_Detentions_Arrests] FOREIGN KEY([DetentionID])
REFERENCES [Detentions] ([DetentionID])
GO
ALTER TABLE [DeliveriesOfDetainees] CHECK CONSTRAINT [FK_Detentions_Arrests]
GO
ALTER TABLE [DeliveriesOfDetainees]  WITH CHECK ADD  CONSTRAINT [FK_Detentions_Detainees] FOREIGN KEY([DetaineeID])
REFERENCES [Detainees] ([DetaineeID])
GO
ALTER TABLE [DeliveriesOfDetainees] CHECK CONSTRAINT [FK_Detentions_Detainees]
GO
ALTER TABLE [DeliveriesOfDetainees]  WITH CHECK ADD  CONSTRAINT [FK_Detentions_Employee] FOREIGN KEY([DeliveredByEmployeeID])
REFERENCES [Employees] ([EmployeeID])
GO
ALTER TABLE [DeliveriesOfDetainees] CHECK CONSTRAINT [FK_Detentions_Employee]
GO
ALTER TABLE [Detentions]  WITH CHECK ADD  CONSTRAINT [FK_Detentions_Employee1] FOREIGN KEY([DetainedByEmployeeID])
REFERENCES [Employees] ([EmployeeID])
GO
ALTER TABLE [Detentions] CHECK CONSTRAINT [FK_Detentions_Employee1]
GO
ALTER TABLE [DetentionsOfDetainees]  WITH CHECK ADD  CONSTRAINT [FK_ArrestsOfDetainees_Arrests] FOREIGN KEY([DetentionID])
REFERENCES [Detentions] ([DetentionID])
GO
ALTER TABLE [DetentionsOfDetainees] CHECK CONSTRAINT [FK_ArrestsOfDetainees_Arrests]
GO
ALTER TABLE [DetentionsOfDetainees]  WITH CHECK ADD  CONSTRAINT [FK_ArrestsOfDetainees_Detainees] FOREIGN KEY([DetaineeID])
REFERENCES [Detainees] ([DetaineeID])
GO
ALTER TABLE [DetentionsOfDetainees] CHECK CONSTRAINT [FK_ArrestsOfDetainees_Detainees]
GO
ALTER TABLE [ReleasesOfDetaineesOfDetainees]  WITH CHECK ADD  CONSTRAINT [FK_ReleasesOfDetainees_Detentions] FOREIGN KEY([DetentionID])
REFERENCES [Detentions] ([DetentionID])
GO
ALTER TABLE [ReleasesOfDetaineesOfDetainees] CHECK CONSTRAINT [FK_ReleasesOfDetainees_Detentions]
GO
ALTER TABLE [ReleasesOfDetaineesOfDetainees]  WITH CHECK ADD  CONSTRAINT [FK_ReleasesOfDetainees_Employees] FOREIGN KEY([ReleasedByEmployeeID])
REFERENCES [Employees] ([EmployeeID])
GO
ALTER TABLE [ReleasesOfDetaineesOfDetainees] CHECK CONSTRAINT [FK_ReleasesOfDetainees_Employees]
GO
