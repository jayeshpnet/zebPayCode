USE [master]
GO
/****** Object:  Database [CurrencyConversion]    Script Date: 7/26/2016 2:33:06 AM ******/
CREATE DATABASE [CurrencyConversion]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CurrencyConversion', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\CurrencyConversion.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CurrencyConversion_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\CurrencyConversion_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CurrencyConversion] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CurrencyConversion].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CurrencyConversion] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CurrencyConversion] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CurrencyConversion] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CurrencyConversion] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CurrencyConversion] SET ARITHABORT OFF 
GO
ALTER DATABASE [CurrencyConversion] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CurrencyConversion] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [CurrencyConversion] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CurrencyConversion] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CurrencyConversion] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CurrencyConversion] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CurrencyConversion] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CurrencyConversion] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CurrencyConversion] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CurrencyConversion] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CurrencyConversion] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CurrencyConversion] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CurrencyConversion] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CurrencyConversion] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CurrencyConversion] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CurrencyConversion] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CurrencyConversion] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CurrencyConversion] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CurrencyConversion] SET RECOVERY FULL 
GO
ALTER DATABASE [CurrencyConversion] SET  MULTI_USER 
GO
ALTER DATABASE [CurrencyConversion] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CurrencyConversion] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CurrencyConversion] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CurrencyConversion] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CurrencyConversion', N'ON'
GO
USE [CurrencyConversion]
GO
/****** Object:  User [manish]    Script Date: 7/26/2016 2:33:06 AM ******/
CREATE USER [manish] FOR LOGIN [manish] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Currencies]    Script Date: 7/26/2016 2:33:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Currencies](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[fromcurrency] [varchar](3) NOT NULL,
	[tocurrency] [varchar](3) NOT NULL,
	[rate] [numeric](10, 0) NOT NULL,
 CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [CurrencyConversion] SET  READ_WRITE 
GO
