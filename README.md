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

[Swagger File](https://github.com/S1nd5/personproject_api/blob/main/swagger.json)
[Database File](https://github.com/S1nd5/personproject_api/blob/main/SQL_Server_Kanta.sql)

I uploaded this in a hurry before weekend.
Cloud deployement and API publification are still to be done. The plan is to deploy this API to the Azure Cloud.
