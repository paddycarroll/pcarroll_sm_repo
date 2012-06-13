Script support notes
Paddy Carroll 13/06/2012 V 0.2

Script Description
these scripts are described in the order in which they should be executed for a new build

dmcdbenvironment.sql
Installs the CLR assembly in dmcdbenvironment.dll and creates the functions that it exposes
requires directory dmcdbenvironment and dmcdbenvironment.dll 
this assembly is required because SQL servers doesn't have access to the o/s environment but it needs to know the domain and platform name
to form backup names
**********************

backup_paths_create.sql
creates the master.dbo.backup_paths table and populates it by assuming any database with db_id > 4 needs a backup 
backup_paths holds the backup path for each database and the minute past the hour on which it's backup is made it is used by 
create_schedule.sql, create_schedules.sql and create_backup_all.sql
**********************

create_errors.sql
creates all the bespoke errors that we generate in the database
**********************

create_backup_all.sql
creates the stored procedure master.dbo.backup_all, depends on create_errors.sql having been run, gets used by master.dbo.add_backup_job 
which is created by create_job.sql
**********************

create_schedule.sql and create_schedules.sql
create_schedule.sql takes one argument, the name of the client/database and creates a scheduled based on the info in backup_paths
create_schedules.sql creates all the schedules based upon the client/databases in backup_paths
**********************


create_job.sql
creates the stored procedure master.dbo.add_backup_job, this takes one argument, the name of the database and creates a scheduled job based using 
a previously created schedule for the database
**********************


**********************
Misc Helper Scripts
**********************

failover.cmd
a simple script to fail one database instance over to it's mirror
**********************

mirror_back.sql
this script prepares an initial backup set for setting up a mirror, it takes two arguments backup_path and db_name
**********************

mirror_restore.sql
this script creates the mirror copy from the resultant backup created by mirror_back.sql, it takes two arguments restore_path and db_name

e.g.
from the command line, to create a database ready for mirroring from called NBC from a cinegy created database on a server BRIVWARC01 using a share \\EUROPE\backup\database

sqlcmd -S BRIVWARC01 -d master -v db_name="NBC" -v backup_path="\\EUROPE\backup\database" -i mirror_back.sql 

and using the backup, to create the mirror copy, from scratch on BRIVWARC02

sqlcmd -S BRIVWARC02 -d master -v db_name="NBC" -v restore_path="\\EUROPE\backup\database" -i mirror_restore.sql 


**********************
Powershell scripts supporting scheduled housekeeping tasks
**********************

createarchivehousekeepeventsource.ps1
This script creates an event source in the application log and must be run under and administrative account in order to succeed
**********************

archivebackuphousekeep.ps1
This script gets run under the ArcSchedule account as a scheduled task at 02:00 
and backs up all midnight backups over n days old to the daily directory, it deletes everything else over n days old from the hourly directory
it depends on createarchivehousekeepeventsource.ps1 having been run under an administrative account previously
**********************

