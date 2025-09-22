# Azure Web App Deployment Configuration

## Prerequisites
1. Azure subscription
2. Azure CLI installed locally
3. Git repository (already set up)

## Your Current Setup
- **SQL Server**: portfoliores.database.windows.net
- **Database**: agrienergyconnect
- **Authentication**: Active Directory Default (no username/password needed)

## Deployment Steps

### 1. Create Azure Web App
```bash
# Login to Azure
az login

# Create resource group (if not exists)
az group create --name rg-portfolio --location "East US"

# Create App Service Plan
az appservice plan create --name plan-agrienergy --resource-group rg-portfolio --sku F1 --is-linux

# Create Web App
az webapp create --name agrienergy-connect-demo --resource-group rg-portfolio --plan plan-agrienergy --runtime "DOTNETCORE:8.0"
```

### 2. Configure App Settings
```bash
# Set connection string using your provided connection string
az webapp config connection-string set --name agrienergy-connect-demo --resource-group rg-portfolio --connection-string-type SQLAzure --settings DefaultConnection="Server=tcp:portfoliores.database.windows.net,1433;Initial Catalog=agrienergyconnect;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";"

# Set environment to Production
az webapp config appsettings set --name agrienergy-connect-demo --resource-group rg-portfolio --settings ASPNETCORE_ENVIRONMENT=Production

# Enable managed identity for Azure AD authentication
az webapp identity assign --name agrienergy-connect-demo --resource-group rg-portfolio
```

### 3. Database Permissions
Since you're using Active Directory Default authentication, you'll need to:
1. **Assign permissions** to your Web App's managed identity in the SQL database
2. **Run this SQL command** in your database (via Azure Portal Query Editor):
```sql
CREATE USER [agrienergy-connect-demo] FROM EXTERNAL PROVIDER;
ALTER ROLE db_datareader ADD MEMBER [agrienergy-connect-demo];
ALTER ROLE db_datawriter ADD MEMBER [agrienergy-connect-demo];
ALTER ROLE db_ddladmin ADD MEMBER [agrienergy-connect-demo];
```

### 4. Deploy using GitHub Actions
- The GitHub Actions workflow is already configured
- Once you push code, it will automatically deploy to Azure
- Make sure to add the Azure publish profile to GitHub secrets

## Important Notes
- Replace `agrienergy-connect-demo` with your preferred unique app name
- The database connection uses Azure AD authentication (more secure)
- Managed identity handles authentication automatically
- Database schema will be created automatically when the app first runs
