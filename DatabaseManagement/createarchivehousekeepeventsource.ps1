# CreateArchiveHousekeepEventSource.ps1
# Creates event source for use in housekeeping
# version 0.1
# Paddy Carroll 
# version 0.1 initial 13/06/2012

"Creating Event Source 'ArchiveHousekeep' in application Log"
[System.Diagnostics.EventLog]::CreateEventSource("ArchiveHousekeep", "Application")
