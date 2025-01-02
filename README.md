# Simple real time chat app using Blazor & .NET 9 

# Install node modules
```
1. npm install
```

# Compile js & css
```
1. npx tailwindcss -i ./wwwroot/app.css -o ./wwwroot/app.min.css --watch
2. npm run watch
```

# Migrations
```
1. dotnet ef migrations add Initial
2. dotnet ef database update
```

# App settings
```
Provide the following values into appsettings.json

{
  "ConnectionStrings": {
    "DefaultConnection": "DataSource=app.db;Cache=Shared"
  },
  "EmailAuth": {
    "Host": "",
    "Port": 587,
    "Username": "",
    "Password": ""
  }, 
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

# Production mode
```
1. npm run build
2. build the project in release mode
3. copy and paste the wwwroot directory to bin/Release/net9.0/
```