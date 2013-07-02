# DnsChanger.ps1
# Change the DNS Entries for Arbitrary Servers
# version 0.1 15/08/2012
# Paddy Carroll 
# 2 arguments (1) physical server name (2) server alias
# 

# setup event source

if (! [System.Diagnostics.EventLog]::SourceExists("DmcDnsManager"))
{
    "Administrative script to create event source has not been run: DmcDnsManager.ps1"
    throw 'error'
}

# initialise event

$evt=new-object System.Diagnostics.EventLog("Application")
$evt.Source="DmcDnsManager"
$errevent=[System.Diagnostics.EventLogEntryType]::error
$infoevent=[System.Diagnostics.EventLogEntryType]::information


# set up variables

$domain = [System.DirectoryServices.ActiveDirectory.Domain]::GetCurrentDomain().Name  

# get arguments

if($args.count -lt 2)
{
   "Script requires two parameters, a physical server name and an alias. FDQN names will be truncated and the current domain will be appended"
   $evt.WriteEntry("Wrong number of arguments supplied to DnsManager " ,$errevent,2)
   throw 'error'
}

$phys = $args[0].split('.')[0]
$alias = $args[1].split('.')[0]
$filter = "ContainerName = " + [char]34 + $domain + [char]34 

$filter

# test for existence of CNAMEType alias

try{
   $rec = Get-WmiObject -Namespace 'root\MicrosoftDNS' -Class MicrosoftDNS_CNAMEType -filter $filter
   if (!$rec)
      {
	# if it doesnt exist, get all AType aliases
         $rec = Get-WmiObject -Namespace 'root\MicrosoftDNS' -Class MicrosoftDNS_AType -filter $filter
      }
   }catch{
      $type = $_.Exception.getType().FullName
	  $detail = $_.Exception.ErrorDetails().toString()
	  $evt.WriteEntry("Problem getting DNS Namespace for " + $domain  ,$errevent,3)
	  throw 'error'
   }


# create the alias

try{
$rec[0].CreateInstanceFromTextRepresentation($domain,$domain,$alias + '.' + $domain + ' IN CNAME ' + $phys + '.'+$domain)
}catch{
	$type = $_.Exception.getType().FullName
	$detail = $_.Exception.ErrorDetails().toString()
	$evt.WriteEntry("Problem creating Alias for " + $phys + " as " + $alias + " : " + $type + " : " + $detail,$errevent,1)
	throw 'error'
   }
$evt.WriteEntry("Alias created for " + $phys + " as " + $alias ,$infoevent,1)




