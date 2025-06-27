# Martian Robots

A solution for the Martian Robots programming challenge, implementing a robot navigation system on a rectangular grid representing the surface of Mars.

---

## Table of Contents

1. [Problem Overview](#problem-overview)
2. [Architecture](#architecture)
3. [Project Structure](#project-structure)
4. [Getting Started](#getting-started)
5. [Usage](#usage)
6. [Input/Output Format](#input-output-format)
7. [Running Tests](#running-tests)
8. [Design Patterns and Principles](#design-patterns-and-principles)
9. [Implementation Details](#implementation-details)
10. [Error Handling](#error-handling)
11. [Performance Considerations](#performance-considerations)
12. [Future Enhancements](#future-enhancements)
13. [Contributing](#contributing)

---

## Problem Overview

The surface of Mars is modeled as a rectangular grid where robots can move according to instructions from Earth. Each robot has:
- **Position**: A set of x, y coordinates.
- **Orientation**: A cardinal direction (North, South, East, or West).
- **Instructions**: A series of movements (Left, Right, Forward).

### Key Features:
- **Grid Boundaries**: Robots that move off the edge of the grid are marked as "LOST".
- **Scent Mechanism**: Lost robots leave a "scent" at their last position, preventing future robots from falling off at the same location.
- **Sequential Processing**: Each robot completes its instructions before the next robot begins.

---

## Architecture

This solution follows **Clean Architecture** principles with **Domain-Driven Design (DDD)**, ensuring:
- **Separation of concerns**
- **High testability**
- **Maintainability**
- **Extensibility**

### Architectural Layers:
- **Domain Layer**: Core business logic with zero external dependencies.
- **Application Layer**: Use cases and orchestration of domain logic.
- **Infrastructure Layer**: External concerns (I/O, parsing, formatting).
- **Presentation Layer**: Console application entry point.

---

## Project Structure

MartianRobots/
│
├── src/
│ ├── MartianRobots.Domain/
│ │ ├── Entities/
│ │ │ ├── Grid.cs # Mars surface grid with scent tracking
│ │ │ └── Robot.cs # Robot entity with movement logic
│ │ ├── Enums/
│ │ │ └── Orientation.cs # Cardinal directions (N, S, E, W)
│ │ ├── ValueObjects/
│ │ │ └── Position.cs # Immutable coordinate record
│ │ └── MartianRobots.Domain.csproj
│ │
│ ├── MartianRobots.Application/
│ │ ├── DTOs/
│ │ │ ├── RobotInput.cs # Input data for a robot
│ │ │ ├── SimulationInput.cs # Complete simulation input
│ │ │ └── SimulationResult.cs # Robot's final state
│ │ ├── Interfaces/
│ │ │ ├── IInputParser.cs # Input parsing contract
│ │ │ ├── IOutputFormatter.cs # Output formatting contract
│ │ │ └── IRobotSimulationService.cs
│ │ ├── Services/
│ │ │ └── RobotSimulationService.cs # Core simulation logic
│ │ ├── ApplicationServicesRegistration.cs
│ │ └── MartianRobots.Application.csproj
│ │
│ ├── MartianRobots.Infrastructure/
│ │ ├── Formatters/
│ │ │ └── OutputFormatter.cs # Formats results to text
│ │ ├── Parsers/
│ │ │ └── InputParser.cs # Parses and validates input
│ │ ├── InfrastructureServicesRegistration.cs
│ │ └── MartianRobots.Infrastructure.csproj
│ │
│ └── MartianRobots.ConsoleApp/
│ ├── Application.cs # Application orchestration
│ ├── Program.cs # Entry point with DI setup
│ └── MartianRobots.ConsoleApp.csproj
│
├── tests/
│ └── MartianRobots.Tests/
│ ├── Application/
│ │ └── RobotSimulationServiceTests.cs
│ ├── Domain/
│ │ ├── GridTests.cs
│ │ └── RobotTests.cs
│ ├── Infrastructure/
│ │ ├── InputParserTests.cs
│ │ └── OutputFormatterTests.cs
│ ├── IntegrationTests.cs
│ └── MartianRobots.Tests.csproj
│
├── .gitattributes
├── .gitignore
├── MartianRobots.sln
├── README.md
└── sample_input.txt



---

## Getting Started

### Prerequisites
- **.NET 8.0 SDK or later**
- Visual Studio 2022, VS Code, or any preferred IDE (optional)
- **Git** (for cloning the repository)

### Installation

1. **Clone the repository**:
    ```bash
    git clone [repository-url]
    cd MartianRobots
    ```

2. **Build the solution**:
    ```bash
    dotnet build
    ```

3. **Run tests** to verify everything is working:
    ```bash
    dotnet test
    ```

---

## Usage

After cloning the repository, you can run the console application as follows:

1. **Run the Console Application**:
    ```bash
    dotnet run --project MartianRobots.ConsoleApp
    ```

2. **Input format**:
   - The input should be in the format of grid dimensions, followed by robot positions and their movement instructions. For example:

    ```
    5 3
    1 1 E
    RFRFRFRF
    3 2 N
    FRRFLLFFRRFLL
    0 3 W
    LLFFFLFLFL
    ```

3. **Output format**:
   - The output will show the final position and orientation of each robot, along with the "LOST" status if applicable.

---

## Input/Output Format

### Input Format
1. The first line contains two integers, **x** and **y**, which represent the dimensions of the Mars grid (e.g., `5 3`).
2. Each robot is described by two lines:
    - The first line contains the initial position of the robot (e.g., `1 1 E` for position `(1,1)` and orientation `East`).
    - The second line contains the movement instructions (e.g., `RFRFRFRF`).

### Output Format
- For each robot, output the final position and orientation.
- If a robot falls off the grid, output "LOST" after its position.


---

## Running Tests

To run the tests, simply execute:

```bash
dotnet test
