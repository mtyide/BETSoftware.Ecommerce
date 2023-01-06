# BETSoftware.Ecommerce
This is an ongoing assessment (Target completion: 11 January 2023 - 10.00am C.A.T)  
UI is currently not hooked up yet and SQL script is not included (Database)  
This assessment currently uses DB First approach (Entity Framework Core)
# Swagger UI/API
You can use the Swagger API to test data creation (Products)  
The UI (ASP.NET Core with AngularJS/MVC/ReactJS) will be used to create Orders and manage the Checkout process of the eCommerce site once API completed
# Not included
BackOffice to manage Products IS NOT part of this assessment. Refer to Swagger API to test endpoints (Products). Products are NOT deleted but rather marked/flagged as INACTIVE, same applies to ORDERS
# Login Controller
Yandisa's Notes - We can assume Api Key (Guid) will be used for authorization after successful authentication  
Yandisa's Notes - In a real world, oAuth (JWT) can be used instead to authorize users  
Yandisa's Notes - The returned token is NOT saved or persisted for this assessment due to time constraints  
# Tech stack
ASP.NET Core 6.0 Web API  
SQL Server 2022  
MediatR (Design Pattern)  
ASP.NET Core with AngularJS  
C#.NET 6.0  
