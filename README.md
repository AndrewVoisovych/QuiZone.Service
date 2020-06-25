# This software is under development...
# QuiZone
The web service for adaptive test generation and automation of knowledge testing has been designed and developed as graduation project. 
The software is designed to combine the processes of developing, carrying out, and analyzing the results of testing, and in turn, increases the effectiveness of the educational process.
The service is developed according to the designed client-server architecture, SOLID principles are observed.

## Part of the project: 
  - **backend**: ASP.NET Core Web API with Entity Framework Core.
  - **frontend**: https://github.com/AndrewVoisovych/QuiZone.Client  <br>
  Connected front-end with back-end using CORS.

## Details:
#### **Architecture:**  
- QuiZone.DataAcces - Entity models, database connect context, DTO, Mapping Profiles, Repository and UnitOfWork pattern.
- QuiZone.Common - Global Error Haldling setup, Logger, Security Helpers.
- QuiZone.BusinessLogic - Services, Utils (Email Sender, Jwt Auth).
- QuiZone.Api - Api, Controllers, Filters, Extensions Methods.
#### **Database** script:
https://bit.ly/2CHuWlK (Google Disc)
#### **Tools & Technologies:** 
  Asp.Net Core Web API, MS SQL Server, Entity Framework Core, LINQ, Azure Cloud Services (Azure SQL Database, Azure App Services), Git, Swagger, Visual Studio 2019.

