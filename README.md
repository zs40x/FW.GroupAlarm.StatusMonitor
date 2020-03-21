# FW.GroupAlarm.StatusMonitor
Dashboard für GroupAlarm - Überwachung der Einsatzbereitschaft aller Einheiten

# Configuration
Set GroupAlarmApi keys in appsettings.development.json for local usage and set the keys on deployment.

```javascript
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "GroupAlarmApi": {
    "BaseUrl": "https://app.groupalarm.com/api/v1",
    "OrganizationApiKey": "< Add Organization-API token during deployment here >",
    "PersonalAccessToken": "< Add Personal Access token during deployment here >"
  }
}
```
