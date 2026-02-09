# 🛒 Product Catalog API - Azure Container Apps

A production-ready, containerized REST API for managing product catalogs, built with .NET 8 and deployed to Azure Container Apps.

![Azure](https://img.shields.io/badge/Azure-0078D4?style=for-the-badge&logo=microsoft-azure&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

## 🎯 Project Overview

This project demonstrates modern cloud-native application development using Azure services. It's part of my Azure certification (AZ-204) preparation and portfolio development.

## 🚀 Live Deployment

**Status:** ✅ Currently deployed and running on Azure

**Live API:** https://ca-productcatalog-api.ambitiousmushroom-5fc6c415.eastus2.azurecontainerapps.io

**Swagger UI:** https://ca-productcatalog-api.ambitiousmushroom-5fc6c415.eastus2.azurecontainerapps.io/swagger

### Azure Resources
- **Container App:** `ca-productcatalog-api`
- **Container Registry:** `acrproductcataaron.azurecr.io`
- **SQL Server:** `sql-productcat-aaron.database.windows.net`
- **SQL Database:** `productcatalog-db` (serverless)
- **Key Vault:** `kv-productcat-aaron`
- **Container Apps Environment:** `cae-productcatalog`
- **Resource Group:** `rg-productcatalog`
- **Region:** East US 2
- **Monthly Cost:** $8-12

### Test the Live API
```bash
# Get product by ID
curl https://ca-productcatalog-api.ambitiousmushroom-5fc6c415.eastus2.azurecontainerapps.io/api/Products/1

# Get products by category
curl https://ca-productcatalog-api.ambitiousmushroom-5fc6c415.eastus2.azurecontainerapps.io/api/Products/category/Electronics
```

### Sample Data
The API comes pre-seeded with sample products:
1. **Laptop** - High-performance laptop for development ($1299.99)
2. **Wireless Mouse** - Ergonomic wireless mouse ($29.99)
3. **Mechanical Keyboard** - RGB mechanical keyboard ($149.99)

## ✨ Features

- ✅ **RESTful API** - Standard HTTP methods and status codes
- ✅ **Containerized** - Docker containerization for consistency
- ✅ **Cloud-Native** - Deployed to Azure Container Apps
- ✅ **Secure** - Azure Key Vault for secrets management
- ✅ **Scalable** - Auto-scaling with Azure Container Apps
- ✅ **Database** - Azure SQL Database with Entity Framework Core
- ✅ **API Documentation** - Interactive Swagger/OpenAPI documentation
- ✅ **Managed Identity** - Secure authentication to Azure services

## 🗺️ Architecture

### **Technology Stack**

- **Backend:** .NET 8 (C#)
- **Framework:** ASP.NET Core Web API
- **ORM:** Entity Framework Core
- **Database:** Azure SQL Database (Serverless tier)
- **Container Registry:** Azure Container Registry
- **Hosting:** Azure Container Apps
- **Secrets Management:** Azure Key Vault
- **Authentication:** Azure Managed Identity

### **Architecture Diagram**
```
┌─────────────┐      HTTPS      ┌──────────────────────┐
│   Client    │ ─────────────────▶│  Azure Container     │
│  (Browser)  │                  │       Apps           │
└─────────────┘                  └──────────────────────┘
                                          │
                                          │ Managed Identity
                                          ▼
                         ┌────────────────────────────────┐
                         │                                │
                    ┌────▼──────┐              ┌──────────▼─────────┐
                    │  Azure   │              │   Azure SQL       │
                    │ Key Vault│              │   Database        │
                    │          │              │  (Serverless)     │
                    └──────────┘              └───────────────────┘
                         │
                         │ Connection String
                         └──────────────────────────────────────────┘
```

## 🚀 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Products/{id}` | Get product by ID |
| GET | `/api/Products/category/{category}` | Get products by category |

### **Example Requests**
```bash
# Get a product by ID
curl https://ca-productcatalog-api.ambitiousmushroom-5fc6c415.eastus2.azurecontainerapps.io/api/Products/1

# Get all Electronics
curl https://ca-productcatalog-api.ambitiousmushroom-5fc6c415.eastus2.azurecontainerapps.io/api/Products/category/Electronics

# Get all Accessories
curl https://ca-productcatalog-api.ambitiousmushroom-5fc6c415.eastus2.azurecontainerapps.io/api/Products/category/Accessories
```

### **Example Response**
```json
{
  "id": 1,
  "name": "Laptop",
  "description": "High-performance laptop for development",
  "price": 1299.99,
  "stockQuantity": 50,
  "category": "Electronics",
  "createdAt": "2026-02-09T15:01:57.4795313Z",
  "updatedAt": "2026-02-09T15:01:57.4795313Z"
}
```

## 🛠️ Local Development

### **Prerequisites**

- .NET 8 SDK
- Docker Desktop
- Azure CLI
- Git

### **Running Locally**
```bash
# Clone the repository
git clone https://github.com/fraze-dev/product-catalog-api.git
cd product-catalog-api/ProductCatalog.Api

# Restore dependencies
dotnet restore

# Run the application
dotnet run

# Access Swagger UI
# Open browser to: http://localhost:5081/swagger
```

### **Running with Docker**
```bash
# Build the Docker image
docker build -t product-catalog-api:local .

# Run the container
docker run -d -p 8080:8080 --name product-api product-catalog-api:local

# Access Swagger UI
# Open browser to: http://localhost:8080/swagger
```

## ☁️ Azure Deployment

### **Deployment Steps**

1. **Create Azure Resources**
```bash
# Create resource group
az group create --name rg-productcatalog --location eastus2

# Create Container Registry
az acr create \
  --resource-group rg-productcatalog \
  --name acrproductcataaron \
  --sku Basic \
  --admin-enabled true

# Create SQL Server and Database
az sql server create \
  --resource-group rg-productcatalog \
  --name sql-productcat-aaron \
  --location eastus2 \
  --admin-user sqladmin \
  --admin-password YourStr0ngP@ssw0rd!

az sql db create \
  --resource-group rg-productcatalog \
  --server sql-productcat-aaron \
  --name productcatalog-db \
  --edition GeneralPurpose \
  --compute-model Serverless \
  --family Gen5 \
  --capacity 1

# Create Key Vault
az keyvault create \
  --resource-group rg-productcatalog \
  --name kv-productcat-aaron \
  --location eastus2
```

2. **Build and Push Docker Image**
```bash
# Login to ACR
az acr login --name acrproductcataaron

# Build and tag image
docker build -t product-catalog-api:latest .
docker tag product-catalog-api:latest acrproductcataaron.azurecr.io/product-catalog-api:latest

# Push to ACR
docker push acrproductcataaron.azurecr.io/product-catalog-api:latest
```

3. **Deploy to Container Apps**
```bash
# Create Container Apps Environment
az containerapp env create \
  --resource-group rg-productcatalog \
  --name cae-productcatalog \
  --location eastus2

# Deploy Container App
az containerapp create \
  --resource-group rg-productcatalog \
  --name ca-productcatalog-api \
  --environment cae-productcatalog \
  --image acrproductcataaron.azurecr.io/product-catalog-api:latest \
  --target-port 8080 \
  --ingress external \
  --registry-server acrproductcataaron.azurecr.io \
  --min-replicas 1 \
  --max-replicas 3
```

## 🔒 Security

- **Managed Identity:** Used for secure authentication to Azure Key Vault
- **Key Vault:** Stores database connection strings securely
- **SQL Authentication:** Uses SQL authentication with strong passwords
- **HTTPS:** All external traffic is encrypted
- **No Secrets in Code:** All sensitive information stored in environment variables or Key Vault

## 📊 Database Schema

### **Products Table**

| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary key (auto-increment) |
| Name | string | Product name (required, max 200 chars) |
| Description | string | Product description (max 1000 chars) |
| Price | decimal | Product price (18,2 precision) |
| StockQuantity | int | Available stock count |
| Category | string | Product category (required, max 100 chars) |
| CreatedAt | datetime | Record creation timestamp |
| UpdatedAt | datetime | Last update timestamp |

## 🧪 Testing

### **Test in Swagger UI**

1. Navigate to the Swagger UI: https://ca-productcatalog-api.ambitiousmushroom-5fc6c415.eastus2.azurecontainerapps.io/swagger
2. Try the GET `/api/Products/{id}` endpoint
3. Try the GET `/api/Products/category/{category}` endpoint
4. Explore the available endpoints and schemas

## 📚 What I Learned

This project helped me master several Azure services and concepts:

- ✅ **Azure Container Apps** - Deploying containerized applications
- ✅ **Azure Container Registry** - Managing Docker images
- ✅ **Azure SQL Database** - Serverless database configuration
- ✅ **Azure Key Vault** - Secrets management
- ✅ **Managed Identity** - Secure authentication between Azure services
- ✅ **Docker** - Containerizing .NET applications
- ✅ **Entity Framework Core** - Database migrations and seeding
- ✅ **RESTful API Design** - Best practices for API development
- ✅ **Troubleshooting** - Debugging container crashes and connection issues

## 🎓 AZ-204 Exam Coverage

This project covers the following AZ-204 exam objectives:

- **Develop Azure Container Apps** (NEW - heavily tested!)
- Implement containerized solutions
- Configure Azure Container Registry
- Implement Azure SQL Database solutions
- Implement Azure Key Vault
- Configure managed identities
- Monitor and troubleshoot solutions

## 🚀 Future Enhancements

- [ ] Add full CRUD operations (POST, PUT, DELETE endpoints)
- [ ] Add authentication and authorization (Azure AD B2C)
- [ ] Implement caching with Azure Cache for Redis
- [ ] Add Application Insights for detailed monitoring
- [ ] Implement CI/CD pipeline with GitHub Actions
- [ ] Add automated testing (unit tests, integration tests)
- [ ] Implement API versioning
- [ ] Add rate limiting
- [ ] Implement pagination for large result sets

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 👤 Author

**Aaron Fraze**
- GitHub: [@fraze-dev](https://github.com/fraze-dev)
- Pursuing Microsoft Azure Developer Associate (AZ-204) certification
- University of South Florida - Azure for Students subscription

## 🙏 Acknowledgments

- Built as part of Azure AZ-204 certification preparation
- Third project in my Azure portfolio series
- Thanks to the Azure and .NET communities for excellent documentation

---

⭐ If you found this project helpful, please give it a star!
