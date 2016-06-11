# ExpenseTracker
Expense Tracker SPA

I have started this little project to learn new ASP.Net Core framework. This project is build on .net core RC 2, EF 7 and SQL server expess.
It uses AngularJS and AngularJS material for front-end.

Authentication is done by Cookie Middleware without ASP.NET Core Identity.

Execute below comand to make database

           dotnet ef  datatbase update 
           
Following libraries are used:
  
      1. AutoMapper - Map viewmodel to/from Business Model  
      2. Autofac - For IoC container
      3. FluentValidation - Model Validation
      4. AngularJS - for Single page Application
      5. AngularJS material  - For material UI desing
      6. angular-chart.js - To generate HTML5 chart


Work is penging in below area

      1. Modal Validations in AngularJs and Controllers
      2. Except Account controller, every controllers dirrectly communicates with Data layer.
      3. Error logging and handling
      4. Use CDN for production environments
