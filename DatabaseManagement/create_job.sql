/*****************************************************/
/* Paddy Carroll                                     */
/* create_job.sql                                    */
/* Version 0.1, 03/06/2012                           */
/* creates proc add_backup_job,                      */
/* invokes backup_all on                             */
/* previously created schedule                       */
/* Dependencies: master.dbo.backup_all               */
/*                                                   */
/* V 0.1 Initial Version 07/06/2012                  */
/*****************************************************/
USE [master]
GO
/****** Object:  StoredProcedure [dbo].[add_backup_job]    Script Date: 06/07/2012 05:43:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER procedure [dbo].[add_backup_job] @db varchar(50) 
as
declare @backup_name varchar(100)
declare @owner varchar(50)
declare @job_command varchar(50)
declare @schedule_id_value int
begin
/****** Object:  Job [generic_backup]    Script Date: 05/23/2012 16:12:13 ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0
/******  Check for Database maintenance syscategory       ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'Database Maintenance' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'Database Maintenance'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
END
/****** Create the job from the database name ************/
select @backup_name = @db + '_database_backup'
select @owner = master.dbo.getDomainName()+ '\sql'
select @job_command = N'exec master.dbo.backup_all '+ CHAR(39) + @db  + CHAR(39)
DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=@backup_name, 
		@enabled=1, 
		@notify_level_eventlog=3, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'Generic backup template.', 
		@category_name=N'Database Maintenance', 
		@owner_login_name=@owner, @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Add the job step to the job ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'do_backup_generic', 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=@job_command, 
		@database_name=N'master', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/******* Add the schedule to the Job ***********/
select @schedule_id_value=schedule_id from msdb.dbo.sysschedules where name=@db;
exec @ReturnCode = msdb.dbo.sp_attach_schedule @job_id=@jobId,@schedule_id=@schedule_id_value
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:

end

