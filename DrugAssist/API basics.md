# My ASP.NET cheat-sheet.

## What does the Web APIs contain?

**Web APIs are the backbone of modern web applications**. They allow different systems to communicate and exchange data, enabling rich, dynamic user experiences. Understanding how to build and interact with Web APIs is crucial for creating modern web applications that are responsive and scalable.

![](https://www.thecsharpacademy.com/img/courseimages/c3-ch1-webapis-flight-api-banner.png)

### What are We APIs?

Web APIs, or  **Web Application Programming Interfaces**, are sets of rules and protocols that allow different software applications to communicate with each other over the internet. They serve as intermediaries, enabling applications to request and exchange data seamlessly.

In simple terms, when you think about a Web API, imagine it as **a bridge between two applications**. For instance, when you use a weather app on your smartphone, the app communicates with a web service that provides weather data. This communication happens through a Web API. The app **sends a request** to the API, which then fetches the weather data from a database and sends it back to the app in a structured format.

Web APIs use standard web protocols like **HTTP/HTTPS** for communication, making them **accessible via URLs**. They typically handle requests and responses in formats such as JSON or XML, which are easy for both humans and machines to read and process.

### Modern Web

All major platforms you use every day, such as Google, Facebook, Twitter, Instagram, Discord, Reddit and online games offer Web APIs that allow third-party developers to **integrate their services** into other applications. This means you can see integrations with these serves from on a variety of platforms, not just on the original website or app.

In essence, Web APIs are crucial for creating **interconnected systems** where different applications can work together, share data, and enhance functionality.  **They are the foundation of the modern web**, enabling the creation of complex, dynamic, and integrated digital experiences.


**This file will remind you:**
☑️  **The Basics**: Get acquainted with the fundamental concepts of ASP.NET Core Web APIs and RESTful services.  
☑️  **How to Set Up Your Development Environment**: Learn how to install and configure the tools you need to build ASP.NET Core applications.  
☑️  **Create Your First Web API**: Step-by-step guidance on creating a simple yet functional Web API from scratch.  
☑️  **Coding with Routing and Controllers**: Dive deep into routing mechanisms and how controllers handle HTTP requests.  
☑️  **Operations with Data**: Connecting to databases using Entity Framework Core.  
☑️  **Implement CRUD Operations**: Master the essential Create, Read, Update, and Delete operations in your API.  
☑️  **Testing**: Testing Web APIs with the industry-standard Postman tool

## The Project Files

The template generated upon project creation is a Minimal API that serves data about the weather. **This is what each file does.**

➡️  **Program.cs:** Serves as the entry point and main configuration file for the application. Configures the web application, adds services such as Swagger for API documentation, sets up middleware, and defines endpoints.

➡️  **appsettings.json**: Stores configuration settings for the application. Initially contains logging configuration and allowed hosts.

➡️  **appsettings.Development.json** : Overrides settings in appsettings.json when the application runs in the development environment.

➡️  **launchsettings.json** : Typically found in the Properties folder. Defines profiles for launching the application in different environments (e.g., development, production). Specifies the environment variables, command-line arguments, and other settings for running the application.

➡️  **TCSA.WebAPI.FlightData.http:** Used for testing API endpoints directly from within the code editor, often Visual Studio Code, which supports HTTP request files through extensions like REST Client. It allows developers to send HTTP requests to their API without needing to use external tools. However, in this tutorial we will use Postman for testing our endpoints.

Convert our project from a Minimal API to a Controller-Based Web API. In Program.cs, start by removing the following snippets => Replace .IsDevelopement with **SwaggerUI** and **Swagger** utilities.

## 



