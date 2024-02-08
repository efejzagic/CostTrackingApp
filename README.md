# Implementation of a cost tracking application using microservices architecture

This project is the practical implementation of the final year thesis at the Faculty of Electrical Engineering, focusing on developing an Expense Tracking Application tailored for construction companies. The application is built using microservices architecture, .NET Core for backend services, Docker Compose for containerization, and React.js for the frontend.

## Project Overview

Managing expenses efficiently is crucial for the success of any construction company. This application provides a comprehensive solution for tracking expenses, enabling construction firms to monitor their financial activities effectively.

## Technologies Used

- .NET Core for backend services
- Docker Compose for containerization
- React.js for the frontend
- Keycloak for authentication management

## Currently implementing
 - Redux
 - RabbitMQ

## Prerequisites

Before you begin, ensure you have the following installed:

- **Docker**: [Get Docker](https://www.docker.com/get-started)


## How to Run the Application

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/efejzagic/CostTrackingApp.git
2. **Navigate to the Project Directory**:
   
   ```bash
    cd CostTrackingApp/CostTrackingApi
3. **Run the Application with Docker Compose**:
   
   ```bash
    docker-compose up

The application services will start, and you can access the frontend interface in your browser at http://localhost:3000.

## Project Structure


- **/CostTrackingApi: Contains individual microservices implementations, each responsible for specific functionality.**.
- **/cost-tracking-fe: Includes the React.js frontend codebase for the user interface.**
- **docker-compose.yml: Defines the services, networks, postgresql db, and volumes configuration for Docker Compose.**

## User credentials
You can use these credentials for login to the app: 
- **Username: admin, Password: AdminPassword**
- **Username: finance, Password: FinancePassword**
- **Username: equipment, Password: EquipmentPassword**

## Postman collection
Link for postman collection: [postman collection](https://www.postman.com/lunar-module-specialist-38372248/workspace/costtrackingapp).

## Notes

- **Ensure that port 3000 is not occupied by other services on your local machine to avoid conflicts.**

Feel free to explore the codebase, make enhancements, and adapt the application to meet your specific requirements.
For testing purposes, please reach out to [efejzagic2@etf.unsa.ba](mailto:efejzagic2@etf.unsa.ba).





