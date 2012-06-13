/*****************************************************/
/* Paddy Carroll                                     */
/* Version 0.1, 03/06/2012                           */
/* Creates the backup_paths table in master          */
/* Dependencies: None                                */
/* N.B. This will TRASH THE OLD TABLE IF IT EXISTS   */
/* V 0.1 Initial Version 03/06/2012                  */
/*****************************************************/
USE [master]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_backup_paths_min_past]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[backup_paths] DROP CONSTRAINT [DF_backup_paths_min_past]
END

GO

USE [master]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[backup_paths]') AND type in (N'U'))
DROP TABLE [dbo].[backup_paths]
GO

USE [master]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[backup_paths](
	[db] [nchar](50) NULL,
	[path] [nchar](100) NULL,
	[min_past] [int] NULL
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[backup_paths] ADD  CONSTRAINT [DF_backup_paths_min_past]  DEFAULT ((0)) FOR [min_past] 
ALTER TABLE [dbo].[backup_paths] ADD  CONSTRAINT [DF_backup_paths_min_past_check]  check(min_past >-1 and min_past< 60)
GO

/******* Populate table *************/

declare @counter int
declare @db varchar(50)
declare @db_path varchar(100)
declare @path_string varchar(100)
declare dbs cursor for select name from sys.databases where database_id > 4
select @counter = 0
open  dbs
fetch next from dbs into @db 
while @@FETCH_STATUS = 0
begin
select @db_path = '\\' + rtrim(master.dbo.getDomainName()) + '\backups\database\hourly'
insert into master.dbo.backup_paths (db,path,min_past) values (@db,@db_path,@counter)
select @counter = @counter + 5
fetch next from dbs into @db 
end
close dbs
deallocate dbs
