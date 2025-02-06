# Supabase .NET Backend

An ASP.NET Core backend integrated with Supabase for authentication, database management, and environment-based secret storage.

## 🚀 Features
- 🔑 **Authentication** – Secure user authentication via Supabase Auth.
- 📦 **Database Integration** – CRUD operations with Supabase tables.
- 🔐 **Environment-Based Secret Management** – Store secrets in `.env` locally.
- ⚡ **ASP.NET Core Backend** – Modern, scalable web API architecture.

## 📌 Prerequisites
Ensure you have the following installed:
- [.NET SDK](https://dotnet.microsoft.com/en-us/download)
- [Supabase Account](https://supabase.com/)
- PostgreSQL (Optional, if using locally)

## 🛠 Setup & Installation

1. Clone this repository:
   ```sh
   git clone https://github.com/MrEshboboyev/supabase-dotnet.git
   cd src
   ```

2. Install dependencies:
   ```sh
   dotnet restore
   ```

3. Create a `.env` file in the project root and add your Supabase credentials:
   ```ini
   SUPABASE_URL=https://your-supabase-url.supabase.co
   SUPABASE_KEY=your-secret-api-key
   ```

4. Run the application:
   ```sh
   dotnet run
   ```

## 🏗 Project Structure
```
📂 supabase-dotnet
 ├── 📂 Controllers   # API Controllers
 ├── 📂 Models        # Database Models
 ├── 📂 Services      # Business Logic
 ├── 📄 appsettings.json  # Configuration
 ├── 📄 .env          # Environment Variables (ignored in Git)
 ├── 📄 Program.cs    # Main Application Entry
src
📂 SupabaseDemo.Api
    ├── Properties
    ├── 📂 Contracts
    ├── 📂 Controllers  # API Controllers
    ├── 📂 Extensions
    ├── 📂 Models       # Database Models
    ├── 📄 .env         # Environment Variables (ignored in Git)
    ├── 📄 appsettings.json  # Configuration
    ├── 📄 Dockerfile
    └── 📄 Program.cs  # Main Application Entry
```

## 🤝 Contribution
Contributions are welcome! Feel free to submit issues or pull requests.

## 📜 License
This project is licensed under the MIT License.
