# .NET Sample Web Application

This is a sample .NET web application that consists of:
- A Web API project (SampleAPI)
- A Web application (SampleWeb)

## Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) or [Visual Studio Code](https://code.visualstudio.com/)

## Solution Structure

```
C#_.NET_ENPA_TEMP/
│
├── API/                     # Web API Project
│   ├── Controllers/         # API Controllers
│   ├── appsettings.json     # Configuration
│   └── Program.cs           # Application entry point
│
├── Web/                     # Web Application Project
│   ├── Controllers/         # MVC Controllers
│   ├── Models/              # View Models
│   ├── Views/               # Razor Views
│   ├── wwwroot/             # Static files
│   ├── appsettings.json     # Configuration
│   └── Program.cs           # Application entry point
│
└── web.sln                 # Solution file
```

## Getting Started

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd C#_.NET_ENPA_TEMP
   ```

2. **Run the API**
   ```bash
   cd API
   dotnet run
   ```
   The API will be available at `https://localhost:5001`

3. **Run the Web Application**
   ```bash
   cd ../Web
   dotnet run
   ```
   The web application will be available at `https://localhost:5002`

## Features

- **SampleAPI**:
  - RESTful API with Swagger documentation
  - Weather forecast endpoint

- **SampleWeb**:
  - Responsive web interface
  - Fetches and displays weather data from the API
  - Built with Bootstrap 5

## Development

- **API Project**: `https://localhost:5001`
- **Web Project**: `https://localhost:5002`
- **Swagger UI**: `https://localhost:5001/swagger`

## Deployment

To deploy this application, you can publish it to Azure App Service, IIS, or any other .NET Core compatible hosting environment.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
