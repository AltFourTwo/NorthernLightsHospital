# NorthernLightsHospital
Final Assignment for the "Development Techniques in a Graphical Environment" course/class.   
The goal was to create a WPF application using XAML and databindings along with Entity Framework, LINQ and Microsoft SQL Server.

Things I've Learned

- How to use Entity Framework and using LINQ to access the database
- How to create a database using Microsoft SQL Server
- Back up and restore database models in Microsoft SQL Server
- Do multiple data bindings in a sleek interface

Self Imposed Challenges

- Use a stack of pages in a single window to make a single window application (Success)
- Implement methods to send objects between pages to accelerate searches and selections (Success)

Difficulties Encountered

- Somehow, entity framework was not properly reading a manually set unique constraint on "Medecin" which meant an "Employe" could  be 0..x "Medecin" which made no sense at all...
