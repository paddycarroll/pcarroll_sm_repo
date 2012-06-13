/****** Object:  Database $(db_name)    Script Date: 05/14/2012 16:18:59 ******/

USE [master]

if exists (select name from sys.databases where name = '$(db_name)') 
begin
	drop database $(db_name);
end
GO

CREATE DATABASE $(db_name) ON  PRIMARY 
( NAME = N'$(db_name)_Data', FILENAME = N'E:\DatabaseFiles\$(db_name)_Data.mdf' , SIZE = 24384KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'$(db_name)_Log', FILENAME = N'E:\TransactionLogs\$(db_name)_Log.ldf' , SIZE = 3456KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE $(db_name) SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC $(db_name).[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE $(db_name) SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE $(db_name) SET ANSI_NULLS OFF 
GO

ALTER DATABASE $(db_name) SET ANSI_PADDING OFF 
GO

ALTER DATABASE $(db_name) SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE $(db_name) SET ARITHABORT OFF 
GO

ALTER DATABASE $(db_name) SET AUTO_CLOSE OFF 
GO

ALTER DATABASE $(db_name) SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE $(db_name) SET AUTO_SHRINK OFF 
GO

ALTER DATABASE $(db_name) SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE $(db_name) SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE $(db_name) SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE $(db_name) SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE $(db_name) SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE $(db_name) SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE $(db_name) SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE $(db_name) SET  DISABLE_BROKER 
GO

ALTER DATABASE $(db_name) SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE $(db_name) SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE $(db_name) SET TRUSTWORTHY OFF 
GO

ALTER DATABASE $(db_name) SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE $(db_name) SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE $(db_name) SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE $(db_name) SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE $(db_name) SET  READ_WRITE 
GO

ALTER DATABASE $(db_name) SET RECOVERY FULL 
GO

ALTER DATABASE $(db_name) SET  MULTI_USER 
GO

ALTER DATABASE $(db_name) SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE $(db_name) SET DB_CHAINING OFF 
GO



RESTORE DATABASE $(db_name) 
    FROM DISK = '$(restore_path)\$(db_name)_database.bak' 
    WITH NORECOVERY, REPLACE
GO
 

RESTORE LOG $(db_name) 
    FROM DISK = '$(restore_path)\$(db_name)_logs.bak' 
    WITH FILE=1, NORECOVERY
GO
 

