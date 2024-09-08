# Business-Client-Monitor-API
`Created By Andrew Skelly`

## Overview

The **Business-Client-Monitor-API** is a RESTful service built using ASP.NET Core to manage client data. It supports basic client management operations including retrieving, creating, and deleting client records. The data is stored in a PostgreSQL database.

## Features

- **GET /api/Clients**: Retrieves a list of all clients from the database.
  
  Each client record includes the following fields:
  - `clientid`: Unique identifier for the client
  - `name`: Client's full name
  - `email`: Client's email address
  - `phone`: Client's phone number
  - `tags`: A string representing the client's status or characteristics (e.g., "New Customer", "Banned")
  - `notes`: Additional notes about the client

- **POST /api/Clients**: Creates a new client record in the database.
  
  Expects a JSON body with the following fields:
  - `name`: Client's full name
  - `email`: Client's email address
  - `phone`: Client's phone number
  - `tags`: A string representing the client's status or characteristics
  - `notes`: Additional notes about the client

  Returns the created client with a 201 Created status.

- **GET /api/Clients/{id}**: Retrieves a specific client by their ID.
  
  Replace `{id}` with the client's ID. Returns the client record or a 404 Not Found if the client does not exist.

- **DELETE /api/Clients/{id}**: Deletes a specific client by their ID.
  
  Replace `{id}` with the client's ID. Removes the client record from the database and returns a 204 No Content status if successful. Returns a 404 Not Found if the client does not exist.

## Technologies Used

- **ASP.NET Core**: The backend framework used to build the API.
- **Entity Framework Core**: An ORM for connecting and interacting with the PostgreSQL database.
- **PostgreSQL**: The database used to store client data.

## Project Structure

- **Controllers/ClientsController.cs**: Manages API requests including retrieval, creation, and deletion of client records.
- **Data/ApplicationDbContext.cs**: Configures the database context for Entity Framework and maps the `Client` model to the `clients` table.
- **Models/Client.cs**: Defines the `Client` entity with properties like `clientid`, `name`, `email`, `phone`, `tags`, and `notes`.

## Prerequisites

- **PostgreSQL**: Ensure you have a PostgreSQL instance running and configured.

## Running the Project
- TDB