# ToDoListApp

ToDoListApp is a RESTful API designed for simple and effective task management

## Features

- User registration and authentication using JWT
- CRUD operations for Tasks and Task Lists

## API Reference

### Authentication

- `POST /api/auth/register` - Register a new user
    - **Request Body**: `UserRegisterDto` (Username, Password, FirstName)
    - **Response**: `UserResponseDto` (Id, Username, FirstName)

- `POST /api/auth/login` - Authenticate a user and get a JWT
    - **Request Body**: `UserLoginDto` (Username, Password)
    - **Response**: `string` JWT token 

- `POST /api/auth/change-username` - Change the authenticated user's username
    - **Request Body**: New username (string)
    - **Response**: Status message

- `POST /api/auth/change-password` - Change the authenticated user's password
    - **Request Body**: New password (string)
    - **Response**: Status message

### Users

- `GET /api/users/{id}` - Get user details
    - **Response**: `UserResponseDto` (Id, Username, FirstName)

- `PUT /api/users/{id}` - Update user details
    - **Request Body**: `UserUpdateDto` (Username, FirstName, Password)
    - **Response**: `UserResponseDto` (Id, Username, FirstName)

- `DELETE /api/users/{id}` - Delete a user
    - **Response**: Status message

### Task Lists

- `GET /api/tasklists` - Get all task lists of the authenticated user
    - **Response**: List of `TaskListResponseDto` (Id, Name, Tasks)

- `GET /api/tasklists/{id}` - Get a task list by id
    - **Response**: `TaskListResponseDto` (Id, Name, Tasks)

- `POST /api/tasklists` - Create a new task list
    - **Request Body**: `TaskListRequestDto` (Name)
    - **Response**: `TaskListResponseDto` (Id, Name, Tasks)

- `PUT /api/tasklists/{id}` - Update a task list
    - **Request Body**: `TaskListRequestDto` (Name)
    - **Response**: `TaskListResponseDto` (Id, Name, Tasks)

- `DELETE /api/tasklists/{id}` - Delete a task list
    - **Response**: Status message

- `GET /api/tasklists/{id}/tasks` - Get tasks from a specific task list
    - **Response**: List of `TaskResponseDto` (Id, Title, Description, DueDate, Status, TaskListId)

### Tasks

- `GET /api/tasks` - Get all tasks
    - **Response**: List of `TaskResponseDto` (Id, Title, Description, DueDate, Status, TaskListId)

- `GET /api/tasks/{id}` - Get a task by id
    - **Response**: `TaskResponseDto` (Id, Title, Description, DueDate, Status, TaskListId)

- `POST /api/tasks` - Create a new task
    - **Request Body**: `TaskCreateDto` (Title, Description, DueDate, TaskListId)
    - **Response**: `TaskResponseDto` (Id, Title, Description, DueDate, Status, TaskListId)

- `PUT /api/tasks/{id}` - Update a task
    - **Request Body**: `TaskUpdateDto` (Title, Description, DueDate, Status)
    - **Response**: `TaskResponseDto` (Id, Title, Description, DueDate, Status, TaskListId)

- `DELETE /api/tasks/{id}` - Delete a task
    - **Response**: Status message
