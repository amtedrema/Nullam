# Nullam
Trial application for RIK

## Installation
1. Clone the repository to your local machine using the following command:

    ```git clone https://github.com/amtedrema/Nullam.git``` - Main project

    ```git clone https://github.com/amtedrema/Nullam.Tests.git``` - Tests

    *Make sure they are together on the same parent folder.*

2. Build the project to restore the NuGet packages and compile the code.
3. Apply database migrations:
    Run the following command on Package Manager Console to apply the migrations:

    ```Update-Database```

    This command will create or update the database schema based on the migrations defined in the project.
4. Run the project
