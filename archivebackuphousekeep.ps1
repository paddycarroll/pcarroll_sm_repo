# ArchiveDbBackupHousekeep.ps
# File backup and housekeep script
# backups made at midnight UTC get > n days old get moved to Daily 
# All backups more than n days old get deleted
# version 0.3
# 
# version 0.1 initial 08/06/2012
# version 0.2 10/06/2012 include logging & bugfixes
# version 0.3 move creation of event source to separate script
# check for arguments

if($args.count -lt 1)
{
"Script requires one parameter, a numeric value, number of days old for files to be backed up or deleted"
throw 'error'
}

# set up event source


if (! [System.Diagnostics.EventLog]::SourceExists("ArchiveHousekeep"))
{
    "Administrative script to create event source has not been run: CreateArchiveHousekeepEventSource.ps1"
    throw 'error'
}

# set up variables

$num_days = $args[0]
$domain = [System.DirectoryServices.ActiveDirectory.Domain]::GetCurrentDomain()  
$path_hour = "\\"+$domain.name+"\backup\database\Hourly"
$path_day = "\\"+$domain.name+"\backup\database\Daily"
$dirObj = gci $path_hour


# initialise event

$evt=new-object System.Diagnostics.EventLog("Application")
$evt.Source="ArchiveHousekeep"
$errevent=[System.Diagnostics.EventLogEntryType]::error
$infoevent=[System.Diagnostics.EventLogEntryType]::information


# go through the hourly backups

$evt.WriteEntry("Start housekeeping with parameter " + $num_days + " days old ",$infoevent,1)

foreach ($file in $dirObj)
{

# If it's more than $num_days old
try{
   if ($file.CreationTimeUTC -lt [System.DateTime]::UtcNow.Subtract([System.TimeSpan]::FromDays($num_days)))
      {

# if it was created midnight hour UTC and it's a full backup
         if ($file.creationTimeUTC.Hour -eq 0 -and $file.name -match ".*db\.bak")
         {
# back up to daily
	    $evt.WriteEntry("moving backup " + $file.name + " to " + $path_day,$infoevent,3) 
            mv $file.FullName $path_day
         }
         else   
         {
# else trash it
	   $evt.WriteEntry("Deleting " + $file.name + " from " + $path_hour,$infoevent,4)
            rm $file.FullName
         }
      }
   }catch{
	$type = $_.Exception.getType().FullName
	$detail = $_.Exception.ErrorDetails().toString()
	$evt.WriteEntry("Archive housekeeping has had a problem moving or deleting data backups:"+$type+":"+$detail,$errevent,1)
   }
}
$evt.WriteEntry("Stop housekeeping" ,$infoevent,2)
