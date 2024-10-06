## Worker Service Project
In this project, I developed a file monitoring system using a Worker Service. I designed it following a layered architecture and I am logging file activities into an SQL database. Additionally, I created an API that allows me to retrieve specific types of changes from the database for monitoring purposes. This service tracks and logs CRUD operations on the file system, making it easier to monitor file movements efficiently.

### Technologies Used
Back-End:
- C#
- Microsoft SQL Server (MSSQL)
- ASP.NET Core (.NET 8.0)
  
API:
- Swagger
  
Architecture:
- N-Layered Architecture(Data Layer, Presentation Layer, Service Layer, Entity Layer)
- Repository Design Pattern
  
ORM:
- Entity Framework Core, Code First
  
Background Services:
- Worker Service
- FileSystemWatcher

Logging:
- Log4Net
  
### Swagger UI

![Ekran görüntüsü 2024-10-06 174653](https://github.com/user-attachments/assets/7a39b233-7c2a-492d-b44b-ecb88747ddd4)
![Ekran görüntüsü 2024-10-06 174622](https://github.com/user-attachments/assets/6e9d46a2-01be-4a39-9956-cb1a39d0ee7c)

### Log Messages

![Ekran görüntüsü 2024-10-06 174901](https://github.com/user-attachments/assets/302b9b11-e92e-4e5d-b10e-388cbf8ee15f)

### SQL Tables

![Ekran görüntüsü 2024-10-06 174741](https://github.com/user-attachments/assets/da31e345-00b0-4fbb-a3d1-aeb411298321)

