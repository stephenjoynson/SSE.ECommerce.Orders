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
        "DBConnectionString": "DBDBDBDBDBDBDB",
        "CustomerAPIKey":  ""
    }
}
```
## Customer API
In appsettings enter the customer API key instead of KYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKY
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
        "DBConnectionString": "",
        "CustomerAPIKey":  "KYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKYKY"
    }
}
```
# Considerations for Production Release
## Security
## Caching
