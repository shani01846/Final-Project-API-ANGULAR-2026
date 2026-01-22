🎟️ Chinese Auction Management System
Full-Stack Web Application (ASP.NET Core Web API + Angular)

This project is a full-stack web application designed to manage a Chinese auction system, combining a robust backend built with ASP.NET Core Web API and a modern frontend developed using Angular.

The system supports the complete auction lifecycle — from donating prizes, through ticket purchases, to conducting raffles and selecting winners.

🧩 Project Overview

The system includes several types of users, each with different roles and permissions:

👤 Donors

Can donate prizes to be included in the auction

Each prize contains details such as name, description, and category

Donated prizes are displayed to buyers in the system

🛒 Buyers

Can browse available prizes

Purchase raffle tickets for specific prizes

Each ticket represents a chance to win in the raffle

🛠️ Administrator

Manages users, prizes, and purchases

Can run raffles at scheduled times

Views raffle results and winning tickets

⚙️ Technologies Used
Backend

ASP.NET Core Web API

Entity Framework Core

SQL Server

JWT Authentication

Custom Middleware (Logging, Rate Limiting)

Frontend

Angular

TypeScript

RESTful API communication

Role-based access control

🔐 Security & Authorization

Secure authentication using JWT tokens

Role-based authorization (Admin / Donor / Buyer)

Protected endpoints for sensitive operations

🎯 Project Goals

This project was developed as an educational and practical implementation demonstrating:

Client–server architecture

RESTful API design

Authentication and authorization

Database modeling and relationships

Integration between backend and frontend

🚀 Future Enhancements

Payment gateway integration

Advanced raffle statistics

Admin dashboard with analytics

Email notifications for winners
