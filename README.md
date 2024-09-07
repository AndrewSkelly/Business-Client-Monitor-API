# Business-Client-Monitor-API
`Created By Andrew Skelly`

## Overview

The **Business-Client-Monitor-API** is a simple RESTful service built using ASP.NET Core that manages client data. This API provides an endpoint to retrieve a list of clients from a PostgreSQL database. The project is designed to support typical client management operations, with the current focus on retrieving clients' data.

## Features

- **GET /api/Clients**: Returns a list of clients stored in the database.
  
  Each client record includes the following fields:
  - `clientid`: Unique identifier for the client
  - `name`: Client's full name
  - `email`: Client's email address
  - `phone`: Client's phone number
  - `tags`: A string representing the client's status or characteristics (e.g., "New Customer", "Banned")
  - `notes`: Additional notes about the client

## Technologies Used

- **ASP.NET Core**: The backend framework used to build the API.
- **Entity Framework Core**: An ORM for connecting and interacting with the PostgreSQL database.
- **PostgreSQL**: The database used to store client data.

## Project Structure

- **Controllers/ClientsController.cs**: Handles the API's HTTP requests, specifically the `GET` request to retrieve clients.
- **Data/ApplicationDbContext.cs**: Configures the database context for Entity Framework and maps the `Client` model to the `clients` table.
- **Models/Client.cs**: Defines the `Client` entity with the properties listed above.

## Prerequisites

- **PostgreSQL**: Ensure you have a PostgreSQL instance running and configured.

## Running the Project
    1. TBC
