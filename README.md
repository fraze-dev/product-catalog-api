# 🛒 Product Catalog API - Azure Container Apps

A production-ready, containerized REST API for managing product catalogs, built with .NET 8 and deployed to Azure Container Apps.

![Azure](https://img.shields.io/badge/Azure-0078D4?style=for-the-badge&logo=microsoft-azure&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

## 🎯 Project Overview

This project demonstrates modern cloud-native application development using Azure services. It's part of my Azure certification (AZ-204) preparation and portfolio development.

**Live API:** [https://ca-productcatalog-api.salmonrock-a90fd3cb.eastus2.azurecontainerapps.io/swagger](https://ca-productcatalog-api.salmonrock-a90fd3cb.eastus2.azurecontainerapps.io/swagger)

## ✨ Features

- ✅ **Full CRUD Operations** - Create, Read, Update, Delete products
- ✅ **RESTful API** - Standard HTTP methods and status codes
- ✅ **Containerized** - Docker containerization for consistency
- ✅ **Cloud-Native** - Deployed to Azure Container Apps
- ✅ **Secure** - Azure Key Vault for secrets management
- ✅ **Scalable** - Auto-scaling with Azure Container Apps
- ✅ **Database** - Azure SQL Database with Entity Framework Core
- ✅ **API Documentation** - Interactive Swagger/OpenAPI documentation
- ✅ **Managed Identity** - Secure authentication to Azure services

## 🏗️ Architecture

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
│   Client    │ ──────────────▶│  Azure Container     │
│  (Browser)  │                  │       Apps           │
└─────────────┘                  └──────────────────────┘
                                          │
                                          │ Managed Identity
                                          ▼
                         ┌────────────────────────────────┐
                         │                                │
                    ┌────▼─────┐              ┌──────────▼────────┐
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
| GET | `/api/Products` | Get all products |
| GET | `/api/Products/{id}` | Get product by ID |
| GET | `/api/Products/category/{category}` | Get products by category |
| POST | `/api/Products` | Create a new product |
| PUT | `/api/Products/{id}` | Update a product |
| DELETE | `/api/Products/{id}` | Delete a product |

### **Example Request**
```bash
# Get all products
curl -X GET https://ca-productcatalog-api.salmonrock-a90fd3cb.eastus2.azurecontainerapps.io/api/Products

# Create a new product
curl -X POST https://ca-productcatalog-api.salmonrock-a90fd3cb.eastus2.azurecontainerapps.io/api/Products \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Wireless Headphones",
    "description": "Noise-canceling wireless headphones",
    "price": 199.99,
    "stockQuantity": 75,
    "category": "Electronics"
  }'
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

### **Azure Resources**

The application uses the following Azure services:

- **Resource Group:** `rg-productcatalog`
- **Container Registry:** `acrproductcatalog`
- **Container App:** `ca-productcatalog-api`
- **SQL Server:** `sql-productcatalog-dev`
- **SQL Database:** `productcatalog-db`
- **Key Vault:** `kv-productcat`

### **Deployment Steps**

1. **Create Azure Resources**
```bash
# Create resource group
az group create --name rg-productcatalog --location eastus2

# Create Container Registry
az acr create --resource-group rg-productcatalog --name acrproductcatalog --sku Basic --admin-enabled true

# Create SQL Server and Database
az sql server create --resource-group rg-productcatalog --name sql-productcatalog-dev --location eastus2 --admin-user sqladmin --admin-password YourPassword
az sql db create --resource-group rg-productcatalog --server sql-productcatalog-dev --name productcatalog-db --edition GeneralPurpose --compute-model Serverless

# Create Key Vault
az keyvault create --resource-group rg-productcatalog --name kv-productcat --location eastus2
```

2. **Build and Push Docker Image**
```bash
# Login to ACR
az acr login --name acrproductcatalog

# Build and tag image
docker build -t product-catalog-api:v1 .
docker tag product-catalog-api:v1 acrproductcatalog.azurecr.io/product-catalog-api:v1

# Push to ACR
docker push acrproductcatalog.azurecr.io/product-catalog-api:v1
```

3. **Deploy to Container Apps**
```bash
# Create Container Apps Environment
az containerapp env create --resource-group rg-productcatalog --name cae-productcatalog --location eastus2

# Deploy Container App
az containerapp create --resource-group rg-productcatalog --name ca-productcatalog-api --environment cae-productcatalog --image acrproductcatalog.azurecr.io/product-catalog-api:v1 --target-port 8080 --ingress external
```

## 🔐 Security

- **Managed Identity:** Used for secure authentication to Azure Key Vault
- **Key Vault:** Stores database connection strings securely
- **SQL Authentication:** Uses SQL authentication with strong passwords
- **HTTPS:** All external traffic is encrypted
- **No Secrets in Code:** All sensitive information stored in Key Vault

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

1. Navigate to the Swagger UI: [API Swagger](https://ca-productcatalog-api.salmonrock-a90fd3cb.eastus2.azurecontainerapps.io/swagger)
2. Try the GET `/api/Products` endpoint
3. Create a new product with POST `/api/Products`
4. Verify the product was created

### **Seeded Data**

The application seeds the following products on initial deployment:
- Laptop - $1,299.99
- Wireless Mouse - $29.99
- Mechanical Keyboard - $149.99

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

- [ ] Add authentication and authorization (Azure AD B2C)
- [ ] Implement caching with Azure Cache for Redis
- [ ] Add Application Insights for detailed monitoring
- [ ] Implement CI/CD pipeline with GitHub Actions
- [ ] Add automated testing (unit tests, integration tests)
- [ ] Implement API versioning
- [ ] Add rate limiting
- [ ] Implement pagination for large result sets

## 📝 License

This project is open source and available under the [MIT License](LICENSE).

## 👤 Author

**Aaron Fraze**
- GitHub: [@fraze-dev](https://github.com/fraze-dev)

## 🙏 Acknowledgments

- Built as part of Azure AZ-204 certification preparation
- Third project in my Azure portfolio series
- Thanks to the Azure and .NET communities for excellent documentation

---

⭐ If you found this project helpful, please give it a star!