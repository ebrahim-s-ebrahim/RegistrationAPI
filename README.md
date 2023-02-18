
# Registration API

The ProfileAPI is a RESTful API that provides endpoints for users to register and authenticate.

## Technologies

The following technologies have been used in the development of the project:

- ASP.NET Core
- Entity Framework Core
- C#
- SQL Server
- JWT Authentication

I got the countries and dial codes from: https://gist.github.com/anubhavshrimal/75f6183458db8c453306f93521e93d37

Sendinblue SMTP server was used to send the verification code needed for confirmation of the user.


## API Reference
### Endpoints

#### Registers a new user with the provided details:

```http
  POST /api/register
```

#### Authenticates a user with their email and password and returns a JWT token:

```http
  POST /api/login
```

## Setup
To set up the project on your local machine, follow the steps below:

1- Clone the project repository

2- Navigate to the project directory and run the following commands:
```http
  dotnet restore
  dotnet run
```
3- The API will be hosted at http://localhost:5000.

## Security
The ProfileAPI uses **JWT Authentication** for secure communication between the client and server. The server generates a JWT token upon successful authentication of a user, which is then used to authenticate subsequent requests. The token expires after 15 minutes.
