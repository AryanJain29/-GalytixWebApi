
---

# Galytix Web API

A simple self-hosted C# Web API built with .NET that reads data from a CSV file and calculates average values for lines of business based on country input.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Project Structure](#project-structure)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [API Usage](#api-usage)
- [Testing the API](#testing-the-api)
- [Known Issues & Warnings](#known-issues--warnings)
- [License](#license)

## Overview

The Galytix Web API is designed to:

1. **Load CSV Data:** Reads data from a CSV file (`gwpByCountry.csv`) located in the **Data** folder.
2. **Calculate Averages:** Filters and calculates average values for specified lines of business (LOB) for a given country.
3. **Expose an API Endpoint:** Provides a POST endpoint at `/api/gwp/avg` that accepts JSON input and returns a JSON response with average values.

## Features

- Self-hosted using Kestrel.
- Asynchronous processing of data.
- Simple controller architecture following the SOLID principles.
- Basic error handling.
- Swagger integration for API documentation.

## Project Structure

```
Galytix
├── Controllers
│   ├── CountryGwpController.cs   // POST endpoint for average calculations
│   └── ServerController.cs         // A simple ping endpoint
├── Data
│   └── gwpByCountry.csv           // CSV data file
├── Models
│   ├── CountryGwpRequest.cs       // Input model for POST endpoint
│   ├── CountryGwpResponse.cs      // Output model (dictionary)
│   └── GwpRecord.cs               // Represents one CSV data row
├── Services
│   └── GwpDataService.cs          // Service to load CSV and compute averages
├── Program.cs                     // Application entry point
├── Startup.cs                     // Configures services and middleware
└── Galytix.WebApi.csproj          // Project file
```

## Installation

### Prerequisites

- [.NET SDK (version 9.0 or later)](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/downloads) (for version control)

### Clone the Repository

Open your terminal and run:

```bash
git clone https://github.com/YourUsername/GalytixWebApi.git
```

Replace `YourUsername` and `GalytixWebApi` with your actual GitHub username and repository name.

### Navigate to the Project Directory

```bash
cd GalytixWebApi
```

## Running the Application

1. **Build the project:**

   ```bash
   dotnet build
   ```

2. **Run the application:**

   ```bash
   dotnet run
   ```

The application should start, and you’ll see a message similar to:

```
Now listening on: http://[::]:9091
Application started. Press Ctrl+C to shut down.
```

## API Usage

### POST `/api/gwp/avg`

#### Description

Calculates average values for specified lines of business (LOB) for a given country.

#### Request Format

- **URL:** `http://localhost:9091/api/gwp/avg`
- **Method:** POST
- **Headers:** `Content-Type: application/json`
- **Body:**

```json
{
  "country": "ae",
  "lob": ["property", "transport"]
}
```

#### Response Format

- **Success (200 OK):**

```json
{
  "property": 123456.78,
  "transport": 987654.32
}
```

The actual values depend on your CSV data.

#### Error Responses

- **400 Bad Request:** When input is invalid.
- **500 Internal Server Error:** If an error occurs during processing.

## Testing the API

### Using Swagger UI

1. Open your web browser and navigate to:

   ```
   http://localhost:9091/swagger
   ```

2. Use the “Try it out” feature to test the `/api/gwp/avg` endpoint.

### Using Postman

1. Open Postman and create a new **POST** request.
2. Set the URL to: `http://localhost:9091/api/gwp/avg`
3. Under the **Body** tab, choose **raw** and select **JSON** as the format.
4. Paste the following JSON:

   ```json
   {
     "country": "ae",
     "lob": ["property", "transport"]
   }
   ```

5. Click **Send** and review the JSON response.

## Known Issues & Warnings

- **Package Warning:**  
  The project currently uses `Swashbuckle.AspNetCore.SwaggerUI` version 5.6.3 which has a moderate severity vulnerability. Consider updating the package to a newer version if security is a concern.

- **Async Method Warning:**  
  Some methods (like in `ServerController.cs`) may trigger warnings if they are marked as async without using await. These are not critical but can be improved in future updates.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---
