# Azure Blob Storage Example

This project demonstrates how to build a simple fullstack application using an ASP.NET Core backend connected to Azure Blob Storage via Azure Key Vault, with a React (TypeScript) frontend.

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

## 📌 What this project does

* Retrieves files from Azure Blob Storage
* Displays them in a frontend UI
* Allows users to open/download files
* Stores sensitive data securely using Azure Key Vault

---

## 🧱 Project Structure

```bash id="r1zjkb"
blob-backend/   # ASP.NET Core API
blob-frontend/  # React frontend
```

---

## ⚙️ Setup

### 1. Install dependencies

#### Backend

Install required NuGet packages:

```bash id="xazdpo"
dotnet add package Azure.Storage.Blobs
dotnet add package Azure.Identity
dotnet add package Azure.Extensions.AspNetCore.Configuration.Secrets
```

---

#### Frontend

```bash id="fgb4y0"
cd blob-frontend
npm install
```

---

### 2. Azure requirements

Make sure you have the following:

* An Azure Storage Account (Blob Storage)
* A Blob container (e.g. `handbooks`)
* An Azure Key Vault

---

### 3. Add a secret to Key Vault

Create a secret in your Key Vault:

Name:

```id="v2d8n3"
your-secret-name
```

Value:

```id="d0n9x4"
<your Azure Blob Storage connection string>
```

Make sure this is the **Blob Storage connection string**.

---

### 4. Configure backend

Update `appsettings.Development.json`:

```json id="a0b9xz"
{
  "KeyVaultKey": {
    "KeyVaultURL": "https://your-keyvault-name.vault.azure.net/"
  }
}
```

---

### 5. Update secret name in code

In `Program.cs`, replace:

```csharp id="q1z7wr"
builder.Configuration["your-secret-name"];
```

with the name of your Key Vault secret that contains your Blob Storage connection string.

Example:

```csharp id="z6l8qp"
builder.Configuration["blobstoragekey"];
```

---

### 6. Run backend

```bash id="mv8o3c"
cd blob-backend
dotnet run
```

---

### 7. Configure frontend

Create a `.env` file in `blob-frontend`:

```env id="k8m4zx"
VITE_API_URL=your-backend-api-url
```

Example:

```id="f4n2qp"
http://localhost:5063/api/handbooks
```

---

### 8. Run frontend

```bash id="y8p3zk"
npm run dev
```

---

## 📡 API Endpoints

```id="r7x1lm"
GET /api/handbooks
GET /api/handbooks/{fileName}
```

---

## 🔐 Authentication

To run the backend locally, sign in using Azure CLI:

```bash id="l8q2xp"
az login
```

This allows the application to access Azure Key Vault using your credentials.

---

## 🔐 Notes

* Secrets are stored in Azure Key Vault
* No connection strings are stored in the code
* `.env` and development files are excluded from version control
