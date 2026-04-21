# 📦 Azure Blob Storage Fullstack Example

This project demonstrates a simple fullstack application using an ASP.NET Core backend connected to Azure Blob Storage via Azure Key Vault, with a React (TypeScript) frontend.

---

## 🚀 Tech Stack

**Backend**

* ASP.NET Core Web API
* Azure Blob Storage
* Azure Key Vault

**Frontend**

* React (Vite)
* TypeScript

---

## 📌 Features

* Retrieve files from Azure Blob Storage
* Display files in a frontend UI
* Open or download files
* Securely store secrets using Azure Key Vault

---

## ⚠️ Prerequisites (Important)

Before running this project, you must **already have the following Azure resources created and configured**:

* An Azure Storage Account (Blob Storage enabled)
* A Blob container (e.g. `handbooks`) with uploaded files
* An Azure Key Vault
* A secret in Key Vault containing your **Blob Storage connection string**

Example:

* **Secret Name:** `blobstoragekey`
* **Secret Value:** `<your Azure Blob Storage connection string>`

> ⚠️ The application will not work without these resources being set up beforehand.

---

## 🔗 How It Works

1. The backend authenticates using Azure CLI (`az login`)
2. It retrieves the connection string from Azure Key Vault
3. It connects to Azure Blob Storage
4. It fetches files from the specified container
5. The frontend displays the files

---

## 🧱 Project Structure

```bash
blob-backend/   # ASP.NET Core API
blob-frontend/  # React frontend
```

---

## ⚙️ Setup

### 1. Install Dependencies

#### Backend

```bash
dotnet add package Azure.Storage.Blobs
dotnet add package Azure.Identity
dotnet add package Azure.Extensions.AspNetCore.Configuration.Secrets
```

#### Frontend

```bash
cd blob-frontend
npm install
```

---

### 2. Configure Azure Key Vault

Update `appsettings.Development.json`:

```json
{
  "KeyVaultKey": {
    "KeyVaultURL": "https://your-keyvault-name.vault.azure.net/"
  }
}
```

---

### 3. Configure Secret Name in Code

In `Program.cs`, replace:

```csharp
builder.Configuration["your-secret-name"];
```

with your actual Key Vault secret name:

```csharp
builder.Configuration["blobstoragekey"];
```

---

### 4. Run Backend

```bash
cd blob-backend
dotnet run
```

---

### 5. Configure Frontend

Create a `.env` file in `blob-frontend`:

```env
VITE_API_URL=http://localhost:5063/api/handbooks
```

---

### 6. Run Frontend

```bash
npm run dev
```

---

## 📡 API Endpoints

```
GET /api/handbooks
GET /api/handbooks/{fileName}
```

---

## 🔐 Authentication

To run the backend locally, authenticate with Azure:

```bash
az login
```

This allows the app to access Azure Key Vault using your credentials.

---

## 🔐 Security Notes

* Secrets are stored securely in Azure Key Vault
* No connection strings are stored in source code
* `.env` and development settings should not be committed

---

## 💡 Notes

This project is intended as a learning example of how to integrate:

* Azure Blob Storage
* Azure Key Vault
* Secure configuration in ASP.NET Core
* A simple React frontend

---
