# Azure Web App Deployment Configuration

## Prerequisites
1. Azure subscription
2. Azure CLI installed locally
3. Git repository (already set up)
4. Azure SQL Database already created: `agrienergyconnect` on server `portfoliores.database.windows.net`

## Your Current Azure Resources
- **SQL Server**: `portfoliores.database.windows.net`
- **Database**: `agrienergyconnect`
- **Authentication**: Active Directory Default (Azure AD)

## Deployment Steps

### 1. Create Azure Web App Resources
```bash
# Login to Azure
az login

# Create resource group (if not exists)
az group create --name rg-portfolio --location "East US"

# Create App Service Plan
az appservice plan create --name plan-agrienergy --resource-group rg-portfolio --sku F1 --is-linux

# Create Web App (choose a unique name)
az webapp create --name agrienergy-connect-portfolio --resource-group rg-portfolio --plan plan-agrienergy --runtime "DOTNETCORE:8.0"
```

### 2. Configure App Settings
```bash
# Set connection string (using your existing database)
az webapp config connection-string set --name agrienergy-connect-portfolio --resource-group rg-portfolio --connection-string-type SQLAzure --settings DefaultConnection="Server=tcp:portfoliores.database.windows.net,1433;Initial Catalog=agrienergyconnect;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";"

# Set environment to Production
az webapp config appsettings set --name agrienergy-connect-portfolio --resource-group rg-portfolio --settings ASPNETCORE_ENVIRONMENT=Production

# Enable Azure AD authentication for the web app (important for database access)
az webapp identity assign --name agrienergy-connect-portfolio --resource-group rg-portfolio
```

### 3. Configure Database Permissions
Since you're using Active Directory authentication, you need to grant your web app access to the database:

1. **Get the web app's managed identity**:
```bash
az webapp identity show --name agrienergy-connect-portfolio --resource-group rg-portfolio --query principalId -o tsv
```

2. **Add the web app as a database user** (run this in Azure Data Studio or SQL Server Management Studio):
```sql
-- Connect to your agrienergyconnect database first
CREATE USER [agrienergy-connect-portfolio] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [agrienergy-connect-portfolio];
ALTER ROLE db_datawriter ADD MEMBER [agrienergy-connect-portfolio];
ALTER ROLE db_ddladmin ADD MEMBER [agrienergy-connect-portfolio];
```

### 4. Deploy using GitHub Actions
The GitHub Actions workflow is already configured. You just need to:

1. Go to your Azure Web App in the portal
2. Go to **Deployment Center**
3. Choose **GitHub** as source
4. Select your repository: `Younous87/AgriEnergyConnect`
5. Choose **GitHub Actions** as build provider
6. Azure will automatically configure the workflow

### 5. Alternative: Manual Deployment
If you prefer manual deployment:
```bash
# Build and publish
dotnet publish -c Release -o ./publish

# Create a zip file
Compress-Archive -Path ./publish/* -DestinationPath ./app.zip

# Deploy to Azure
az webapp deployment source config-zip --resource-group rg-portfolio --name agrienergy-connect-portfolio --src ./app.zip
```

## Important Notes
- Replace `agrienergy-connect-portfolio` with your preferred unique app name
- Your database `agrienergyconnect` is already set up with Active Directory authentication
- The Free (F1) tier is used for demonstration purposes
- Database migrations will run automatically when the app starts
- Make sure your Azure account has permission to access the SQL database
