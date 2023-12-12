## Installing dotnet entity framework tools
- Issue `dotnet add package Microsoft.EntityFrameworkCore.Design --version=7.0.12`
### local installation: preferred
- Issue `dotnet new tool-manifest` to create a local tool manifest file. This only creates the `.config` folder with the versions of the tools present
- Issue `dotnet tool install dotnet-ef`
- Issue `dotnet tool restore`
### global installation
- Issue `dotnet tool install --global dotnet-ef`
- Issue `dotnet tool update --global dotnet-ef`
### adding a migration
- Issue `dotnet ef migrations add {MyMigration}`
### updating db with latest migration(s)
- Issue `dotnet ef database update`
### unapply a specific migration
- Issue `dotnet ef database update LastGoodMigrationName`
### unapply all migrations
- Issue `dotnet ef database update 0`
### remove last migration entry from migrations folder
- Issue `dotnet ef migrations remove`

## Installing dotnet api scaffolding tools
- Issue `dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design -v 7.0.0`
- Issue `dotnet tool install dotnet-aspnet-codegenerator --version=7.0.0`
### Creating a controller via scaffolding
The `-m` is the model class name, `-dc` is the database context class name
- Issue `dotnet aspnet-codegenerator controller -name CountriesController -async -api -m Country -dc HotelListingDbContext -outDir Controllers`

