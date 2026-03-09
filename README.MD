MiniCRM — Local Development Setup

This guide explains how to run the project locally for development.

📦 Prerequisites

Make sure you have installed:

.NET SDK 8+

Node.js 18+

Docker Desktop

🗄️ 1. Start PostgreSQL Database (Docker)

The database runs inside Docker.

From the project root:

cd backend
docker compose up -d

This will start:

PostgreSQL database

Default port: 5432

To stop the database:

docker compose down
⚙️ 2. Run Backend API

From project root:

cd backend
dotnet restore
dotnet ef database update --project MiniCRM.Infrastructure
dotnet run --project MiniCRM.API

Backend will start at:

http://localhost:5022

Swagger UI:

http://localhost:5022/swagger
💻 3. Run Frontend (React)

Open a new terminal and run:

cd frontend
npm install
npm start

Frontend will start at:

http://localhost:3000
🔄 Full Startup Order

1️⃣ Start Docker database
2️⃣ Run backend API
3️⃣ Run frontend

🧹 Useful Commands
Reset database
dotnet ef database drop --project MiniCRM.Infrastructure
dotnet ef database update --project MiniCRM.Infrastructure
Stop all Docker containers
docker compose down
🧪 Default Development URLs
Service	URL
Frontend	http://localhost:3000

Backend API	http://localhost:5022

Swagger	http://localhost:5022/swagger

PostgreSQL	localhost:5433
✅ You're Ready

Open the frontend in your browser and start using the app.

### TODO
- ask an sql query for listing late tasks
- implement comments for the task logic
