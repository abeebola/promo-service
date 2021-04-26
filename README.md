## How to run app (Testing - this displays a swagger UI that can be used to test the APIs)
Please note that running in development mode like this requires the dotnet SDK to be installed on the testing machine.
First of all, handle this checklist
- Install Postgres DB
- Install `dotnet core runtime/sdk`
- Install `dotnet ef tools`

1. From project root directory, run `cd PromoCodesAPI` to enter into the startup project directory
2. Edit the file `appsettings.Development.json` file and replace the database credentials in the `ConnectionStrings` section. You can also use your own `TokenSecret` value.
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1;Port=5432;Database=promo_db;User Id=postgres;Password=password;"
  },
  "AppSettings": {
    "TokenSecret":  "tfhtrycuvjkbjuvctxcghvjbgfcbfrgezxtytyhv"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```
3. run `dotnet ef database update` to setup the database for the app and also run migration scripts.
4. run `dotnet run watch`

At this point, the app should build and open up a swagger UI for testing.

## How to run app in "production" mode
- Complete the items in the checklist above

1. From project root directory, run `cd PromoCodesAPI` to enter into the startup project directory
2. Add a file `appsettings.Production.json` with similar content as the one used in the `appsettings.Development.json` file above
3. Run `dotnet publish -c Release -o ./dist/publish` to generate dll files
4. Run  `dotnet ./dist/publish/PromoCodesAPI.dll`
5. Access the endpoints using the base URL `http://localhost:5000`. For example, the Login endpoint will be: `http://localhost:5000/auth/login`.


## Considerations
1. Since the `InMemory` database option of EF core is fast enough, I preferred to use it for the Unit tests instead of mocking the `DBContext`.
2. No extensive mocking.
3. Swagger generated pages don't have enough descriptions. I had to squeeze time to do this. It's a very busy period for me at this time (handling a new product launch)
4. No extensive validations e.g validating if email passed is a valid email address - only uniqueness is currently validated.
