# Azure Web App Deployment Configuration

## Prerequisites
1. Azure subscription
2. Azure CLI installed locally
3. Git repository (already set up)

## Deployment Steps

### 1. Create Azure Resources
```bash
# Login to Azure
az login

# Create resource group
az group create --name rg-agrienergy --location "East US"

# Create App Service Plan
az appservice plan create --name plan-agrienergy --resource-group rg-agrienergy --sku F1 --is-linux

# Create Web App
az webapp create --name agrienergy-connect-demo --resource-group rg-agrienergy --plan plan-agrienergy --runtime "DOTNETCORE:8.0"

# Create SQL Database Server
az sql server create --name sql-agrienergy-server --resource-group rg-agrienergy --location "East US" --admin-user sqladmin --admin-password "YourStrongPassword123!"

# Create SQL Database
az sql db create --server sql-agrienergy-server --resource-group rg-agrienergy --name AgriEnergyConnect --service-objective Basic

# Configure firewall (allow Azure services)
az sql server firewall-rule create --server sql-agrienergy-server --resource-group rg-agrienergy --name AllowAzureServices --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0
```

### 2. Configure App Settings
```bash
# Set connection string
az webapp config connection-string set --name agrienergy-connect-demo --resource-group rg-agrienergy --connection-string-type SQLAzure --settings DefaultConnection="Server=tcp:sql-agrienergy-server.database.windows.net,1433;Initial Catalog=AgriEnergyConnect;Persist Security Info=False;User ID=sqladmin;Password=YourStrongPassword123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# Set environment to Production
az webapp config appsettings set --name agrienergy-connect-demo --resource-group rg-agrienergy --settings ASPNETCORE_ENVIRONMENT=Production
```

### 3. Deploy using GitHub Actions or Azure DevOps
- The project is already connected to GitHub
- You can set up continuous deployment from the Azure portal

## Important Notes
- Replace `agrienergy-connect-demo` with your preferred unique app name
- Replace `YourStrongPassword123!` with a strong password
- The Free (F1) tier is used for demonstration purposes
- Database will be created automatically when the app first runs
