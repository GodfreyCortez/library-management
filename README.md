# Library
An app to manage books you've read, want to read, and notes you may have on them

## Currently Supported
- Lookup for books using the OpenLibrary API
- Adding the books to your library and creating entities in RavenDB
- Deleting of books from your library and removal of corresponding entities

## Prerequisites
- Node v22
- .NET SDK 9
- Docker

## Installing and Running (Dockerized backend and backend)
If following these steps, you should only need Docker installed. To start all services, run `docker compose up`

You should be able to access the application at `http://localhost:5174` once it completes building.

## Installing and Running (Non-docker backend and frontend)
Once the pre-requisites have been installed, perform the following to get the full app running
1. Start the RavenDB docker container using `docker compose up`
2. Start a new terminal, inside the `book-library-manager` folder, run `dotnet restore` followed by `dotnet run`
3. Start another new terminal, inside the `book-library-manager-client`, run `npm install`, followed by `npm run dev`

You should be able to access the application at `http://localhost:5174`

