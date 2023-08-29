# Football Data Management Web Application
This repository contains a C# program built on the ASP.NET Core framework, designed to manage football-related data. The application utilizes a SQLite database through Entity Framework Core to enable seamless data storage and retrieval.

Key Features:

**Entity Framework Core Integration:** 
The program seamlessly integrates with Entity Framework Core, facilitating efficient interaction with a SQLite database for managing football data.


**JSON Serialization:** 
Leveraging the capabilities of the System.Text.Json.Serialization namespace, the program customizes JSON serialization to handle cyclic references gracefully.


**Swagger/OpenAPI Documentation:**
Comprehensive API documentation powered by Swagger/OpenAPI offers an interactive interface to explore available endpoints, request/response models, and more.


**Secure Communication:** 
The program enforces HTTPS redirection to ensure secure communication, bolstering the application's overall security.


**Authorization Mechanisms:**
Robust authorization measures are implemented to regulate access to endpoints, enhancing data security.


**Data Seeding:** 
A dedicated FootballInitializer class facilitates seamless database setup by seeding initial data, streamlining testing and development.


**Environment Adaptability:**
The program dynamically adjusts its behavior based on the environment. In the development setting, Swagger and SwaggerUI are enabled for simplified API testing and debugging.


This project serves as a foundational solution for creating web-based systems focused on football data management. The utilization of ASP.NET Core, Entity Framework Core, and associated technologies ensures structured and scalable data handling through RESTful API endpoints.
