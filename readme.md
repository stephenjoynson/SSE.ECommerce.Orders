# Configure
## Database
In appsettings enter the orders database connection string instead of DBDBDBDBDBDBDB
```
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
        }
    },
    "OrdersSettings": {
        "DbConnectionString": "DBDBDBDBDBDBDB",
        "CustomerApiKey": "",
        "CustomerApiUrl": ""
    }
}
```
## Customer API
In appsettings enter the customer API key instead of KYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKY and URLURLURLURLURLURLURLURLURLURL with base URL of the customer API
```
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft": "Warning",
            "Microsoft.Hosting.Lifetime": "Information"
        }
    },
    "OrdersSettings": {
        "DbConnectionString": "",
        "CustomerApiKey": "KYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKY",
        "CustomerApiUrl": "URLURLURLURLURLURLURLURLURLURL"
    }
}
```
# Considerations for Production Release
## Security
- Configure Authentication & Authorization
- Guard against injection
- Store connection strings and API keys seperately e.g. in Azure Vault
- Configure Circuit Breaker policies
## Caching
- Cache API and database responses e.g. using Redis
## Other
- Configure logging provider to be e.g. Application Insights
- Add more Unit Tests
- Move query into a Stored Procedure
- Use an IMapper