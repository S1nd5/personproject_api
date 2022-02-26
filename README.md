# Person Project Api
REST API for person and project membership management. First .NET implementation.

## Instructions for trying out
1. Run the project
2. Create new user to the /user endpoint
3. Include authorization header with Basic Authentication (Base64 encoded string including username:password)
4. Now you can use routes which need more permissions such as /project /member

[Try it out here](#)

### Content

* ASP.Net
* Entity Framework
* SQL Server as Database
* Serilog
* REST API Service with Basic Authentication
  * This will be done with Authorization Header which includes username and password in Base64 Format.

[Database File](https://github.com/S1nd5/personproject_api/blob/main/SQL_Server_Kanta.sql)

### Rest API
1. /user endpoint
  * GET,POST,PUT,DELETE
3. /project endpoint
  * GET,POST,PUT,DELETE
  * Authorization is reguired with Authorization Header (Base64 encoded string with the contents of username:password)
4. /member endpoint
  * GET,POST,PUT,DELETE 
  * Authorization is reguired with Authorization Header (Base64 encoded string with the contents of username:password)
 
Check the API Documentation from the Swagger file, due to rush i haven't published it.
 
[Swagger File](https://github.com/S1nd5/personproject_api/blob/main/swagger.json)
[Live Api Url](https://apiproject20220226091115.azurewebsites.net/api/user)

Listed endpoints

1. https://apiproject20220226091115.azurewebsites.net/api/user
2. https://apiproject20220226091115.azurewebsites.net/api/project
3. https://apiproject20220226091115.azurewebsites.net/api/member

Project is Deployed to the Azure Cloud ( Azure App Service, Azure SQL )
