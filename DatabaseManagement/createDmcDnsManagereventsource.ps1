# CreateDmcDnsManagerEventSource.ps1
# Creates event source for use in Dmc Dns management
# version 0.1
# Paddy Carroll 
# version 0.1 initial 15/08/2012

"Creating Event Source 'DmcDnsManager' in application Log"
[System.Diagnostics.EventLog]::CreateEventSource("DmcDnsManager", "Application")
