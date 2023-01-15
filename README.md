# BETSoftware.Ecommerce
Light/basic eCommerce assessment for BET Software as per requirements.
# Swagger UI/API
Test Api endpoints => BETSoftware.Ecommerce.Api
# Not included
BackOffice to manage Products IS NOT part of this assessment. Refer to Swagger API to test endpoints (Products). Products are NOT deleted but rather marked/flagged as INACTIVE, same applies to ORDERS  
Logging
# Login Controller
Notes:  
We can assume returned Api Key (Token) will be used for authorization after successful authentication  
In a real world, oAuth (JWT) can be used instead to authorize users  
The returned token is NOT saved or persisted for this assessment due to time constraints  
# Tech stack
ASP.NET Core 6.0 Web API  
SQL Server 2022  
MediatR (Design Pattern)  
ASP.NET Core with AngularJS  
C#.NET 6.0  
# Restore or Import Data-Tier Application
BETSoftware.Data project  
Folder: Database  
Filename: betsoftware.bacpac  
# Sample Login Details
Username: mtyide  
Password: BANDil@123456
# Multiple StartUp Projects
BETSoftware.Ecommerce.Api  
BETSoftware.Ecommerce.App  
Just make sure that both projects are launched at the same time. Use App to login or Api to test endpoints (Swagger API)  
# Requirements
Change urls in the App's environment component (environments folder) => baseApiUrl & baseImagesUrl
# Project Notes
Start: 05 January 2023  
Completion: 09 January 2023  
IDE: Visual Studio 2022 (Api) & Visual Studio Code (App)  
Database: SQL Server 2022  
Design: MediatR (Design Pattern) & DDD (Domain Driven Design)  
Architecture: Microservice  
Target Deployment: Microsoft Azure (Pipelines)  
Frontend: AngularJS/2+  
Backend: ASP.NET Core (6.0)  
Language: C#.NET 6.0
# Dependencies
NodeJS  
AngularJS/2+  
Entity Framework Core  
Auto Mapper  
Auto Mapper Extensions  
SQL Client  
MediatR (CQRS)
# Copyright
Author: Yandisa Mtyide. 2023 VS Projects
