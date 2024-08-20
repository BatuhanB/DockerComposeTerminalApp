Docker Compose Terminal Manager
===============================

A simple WPF application built with .NET 8 that allows users to manage Docker containers through Docker Compose. This application provides an easy-to-use interface for running and stopping Docker containers, with a terminal output panel to display logs and errors in real-time.

Features
--------

-   **Folder Selection:** Choose a directory containing a `docker-compose.yml` file.
-   **Build & Run Containers:** Execute `docker-compose build & docker-compose up` commands to build and start the containers.
-   **Stop Containers:** Execute the `docker-compose down` command to stop and remove the containers.
-   **Terminal Output:** View real-time logs and error messages in a terminal-like interface within the application.

Prerequisites
-------------

-   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
-   Docker

Getting Started
---------------

### 1\. Clone the Repository

```bash
git clone https://github.com/BatuhanB/DockerComposeTerminalApp.git
cd DockerComposeTerminalApp
```

### 2\. Open the Project

-   Open the project in Visual Studio 2022 or later.

### 3\. Build the Solution

-   Build the solution to restore dependencies and compile the project.

### 4\. Run the Application

-   Start the application by pressing `F5` in Visual Studio or using the `dotnet run` command in the terminal.

Usage
-----

1.  **Select Folder**: Click on the "Select Folder" button and navigate to the directory containing your `docker-compose.yml` file.
2.  **Build & Run**: Once a folder is selected, the "Build & Run" button will be enabled. Click it to build and start the Docker containers.
3.  **Stop Containers**: To stop and remove the running containers, click the "Stop Containers" button.
4.  **View Logs**: The terminal output panel on the right will display logs and errors as they occur.

Error Handling
--------------

-   The application checks for any errors in the output stream that contain "Error", "error", or "err". If such errors are detected, they are logged to the console and displayed in the terminal output panel.

Troubleshooting
---------------

-   **InvalidOperationException**: If you encounter an `InvalidOperationException` related to thread access when updating the UI, ensure that UI updates are done using `Dispatcher.Invoke`, as described in the source code.
-   **Docker Not Running**: Ensure Docker Desktop is running on your machine before executing any Docker commands through the application.

Contributing
------------

Contributions are welcome! Please feel free to submit a Pull Request with any enhancements, bug fixes, or additional features.

License
-------

This project is licensed under the MIT License - see the LICENSE file for details.

Acknowledgments
---------------

-   [Microsoft](https://dotnet.microsoft.com/) for the .NET platform
-   [Docker](https://www.docker.com/) for containerization technology
