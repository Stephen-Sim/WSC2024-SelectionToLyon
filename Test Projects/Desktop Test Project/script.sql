USE [master]
GO
/****** Object:  Database [WSC2024Selection_Desktop_Stephen]    Script Date: 2/15/2024 12:12:34 PM ******/
CREATE DATABASE [WSC2024Selection_Desktop_Stephen]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WSC2024Selection_Desktop_Stephen', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\WSC2024Selection_Desktop_Stephen.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WSC2024Selection_Desktop_Stephen_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\WSC2024Selection_Desktop_Stephen_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WSC2024Selection_Desktop_Stephen].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET ARITHABORT OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET  MULTI_USER 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET QUERY_STORE = ON
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [WSC2024Selection_Desktop_Stephen]
GO
/****** Object:  Table [dbo].[locations]    Script Date: 2/15/2024 12:12:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[locations](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_locations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[routes]    Script Date: 2/15/2024 12:12:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[routes](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[location1Id] [bigint] NOT NULL,
	[location2Id] [bigint] NOT NULL,
	[cost] [decimal](18, 2) NOT NULL,
	[duration] [int] NOT NULL,
 CONSTRAINT [PK_routes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[locations] ON 

INSERT [dbo].[locations] ([id], [name]) VALUES (1, N'Alor Setar')
INSERT [dbo].[locations] ([id], [name]) VALUES (2, N'George Town')
INSERT [dbo].[locations] ([id], [name]) VALUES (3, N'Taiping')
INSERT [dbo].[locations] ([id], [name]) VALUES (4, N'Seremban')
INSERT [dbo].[locations] ([id], [name]) VALUES (5, N'Alor Gajah')
INSERT [dbo].[locations] ([id], [name]) VALUES (6, N'Batu Pahat')
INSERT [dbo].[locations] ([id], [name]) VALUES (7, N'Ipoh')
INSERT [dbo].[locations] ([id], [name]) VALUES (8, N'Shah Alam')
INSERT [dbo].[locations] ([id], [name]) VALUES (9, N'Klang')
INSERT [dbo].[locations] ([id], [name]) VALUES (10, N'Kuantan')
INSERT [dbo].[locations] ([id], [name]) VALUES (11, N'Kuala Terengganu')
INSERT [dbo].[locations] ([id], [name]) VALUES (12, N'Senai')
INSERT [dbo].[locations] ([id], [name]) VALUES (13, N'Singapore')
SET IDENTITY_INSERT [dbo].[locations] OFF
GO
SET IDENTITY_INSERT [dbo].[routes] ON 

INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (1, 1, 2, CAST(22.40 AS Decimal(18, 2)), 3)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (2, 2, 3, CAST(11.20 AS Decimal(18, 2)), 2)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (3, 3, 4, CAST(44.80 AS Decimal(18, 2)), 3)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (4, 4, 5, CAST(29.80 AS Decimal(18, 2)), 3)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (5, 5, 6, CAST(52.50 AS Decimal(18, 2)), 4)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (6, 3, 7, CAST(43.20 AS Decimal(18, 2)), 2)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (7, 4, 8, CAST(102.40 AS Decimal(18, 2)), 1)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (8, 7, 8, CAST(22.90 AS Decimal(18, 2)), 3)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (9, 8, 9, CAST(11.20 AS Decimal(18, 2)), 2)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (10, 7, 10, CAST(14.20 AS Decimal(18, 2)), 4)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (11, 10, 12, CAST(37.60 AS Decimal(18, 2)), 2)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (12, 10, 11, CAST(19.80 AS Decimal(18, 2)), 4)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (13, 6, 12, CAST(22.60 AS Decimal(18, 2)), 1)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (14, 12, 13, CAST(134.40 AS Decimal(18, 2)), 1)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (16, 1, 5, CAST(89.60 AS Decimal(18, 2)), 2)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (17, 8, 12, CAST(93.90 AS Decimal(18, 2)), 1)
INSERT [dbo].[routes] ([id], [location1Id], [location2Id], [cost], [duration]) VALUES (18, 11, 13, CAST(46.50 AS Decimal(18, 2)), 2)
SET IDENTITY_INSERT [dbo].[routes] OFF
GO
ALTER TABLE [dbo].[routes]  WITH CHECK ADD  CONSTRAINT [FK_routes_locations] FOREIGN KEY([location1Id])
REFERENCES [dbo].[locations] ([id])
GO
ALTER TABLE [dbo].[routes] CHECK CONSTRAINT [FK_routes_locations]
GO
ALTER TABLE [dbo].[routes]  WITH CHECK ADD  CONSTRAINT [FK_routes_locations1] FOREIGN KEY([location2Id])
REFERENCES [dbo].[locations] ([id])
GO
ALTER TABLE [dbo].[routes] CHECK CONSTRAINT [FK_routes_locations1]
GO
USE [master]
GO
ALTER DATABASE [WSC2024Selection_Desktop_Stephen] SET  READ_WRITE 
GO
