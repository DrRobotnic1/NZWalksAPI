# NZWalks API

Welcome to the NZWalks API project! This API, developed using ASP.NET C#, serves as a backend solution for managing and exploring walking tracks in New Zealand. I created this project with guidance from an ASP.NET course on Udemy, which provided me with valuable insights and hands-on experience in various key concepts and technologies, including:

- **Authentication**: Implementing secure user authentication to protect sensitive data.
- **Object-Oriented Programming**: Structuring the code with OOP principles for maintainability and scalability.
- **Controllers & Models**: Creating a clean separation between application logic and data structures.
- **AutoMapper**: Utilizing AutoMapper to simplify object mapping between DTOs and entities.
- **Data Transfer Objects (DTOs)**: Designing DTOs to efficiently transfer data between client and server.
- **Repositories**: Applying the repository pattern for data access, promoting separation of concerns.
- **Image Upload**: Enabling image uploads to enhance the user experience with visual content.
- **DbContext & SQL Server**: Working with Entity Framework and SQL Server for data management and migrations.
- **Migrations**: Using Entity Framework migrations to manage database schema changes seamlessly.
- **Filters**: Implementing action filters for better request handling and response management.

## How to Run

To run the NZWalks API locally, follow these steps:

1. **Clone the repository**:

   ```bash
   https://github.com/DrRobotnic1/NZWalksAPI.git
   cd nzwalks-api
   dotnet restore
   dotnet ef database update
   dotnet run
   
 

