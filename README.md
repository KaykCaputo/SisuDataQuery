# SisuDataQuery

A lightweight .NET console application for querying official academic data from Brazil's Unified Selection System (SISU) API.

This tool allows you to easily fetch course weights, minimum cutoff grades, and other key information for any course registered in the SISU platform, directly from your terminal.

### Features

- **List Institutions**: Browse all participating universities and institutes.
- **List Courses**: View all courses offered by a specific institution.
- **Query Course Weights**: Get the specific weights for each subject area (Natural Sciences, Humanities, Languages, Math, and Essay) for a chosen course.
- **Query Minimum Grades**: Find the minimum required grade for each subject area to be eligible for a course.

Feel free to suggest new features or improvements by opening an issue!

### How It Works

The application interacts directly with the official SISU API in a logical sequence:

1.  **Fetch Institutions**: First, it retrieves a complete list of all registered institutions.
2.  **Fetch Courses**: Once you select an institution by its acronym, it queries all courses offered by it.
3.  **Fetch Course Details**: Finally, after you select a course by its code, the application fetches its specific details, including weights and minimum grades.

### Getting Started

To get the project up and running on your local machine, follow these steps.

#### Prerequisites

1.  **.NET 8.0 SDK (or later)**: You need the .NET SDK to build and run the project.
    - [Download .NET](https://dotnet.microsoft.com/download)
2.  **ChromeDriver**: The application uses Selenium to render the API's content. ChromeDriver is required to automate the Google Chrome browser.
    - [Download ChromeDriver](https://googlechromelabs.github.io/chrome-for-testing/)
    - **Important**: Make sure the downloaded `chromedriver.exe` is placed either in the same directory as the final project executable (`/bin/Debug/...`) or in a folder included in your system's PATH environment variable.

#### Installation & Usage

1.  **Clone the repository**:

    ```bash
    git clone https://github.com/your-username/SisuDataQuery.git
    cd SisuDataQuery
    ```

2.  **Restore dependencies**:
    This command downloads and installs the necessary NuGet packages (Selenium, HtmlAgilityPack, etc.).

    ```bash
    dotnet restore
    ```

3.  **Run the application**:
    This command will build and execute the project.

    ```bash
    dotnet run
    ```

4.  **Follow the on-screen instructions**:
    - First, enter the acronym of the institution (e.g., `UFRJ`).
    - Next, find the code for the course you want to query from the displayed list.
    - Enter the course code to see its detailed information.

### Example Usage

Here is a sample of what a session looks like in the console:

```bash
$ dotnet run
=============== SISU DATA QUERY ===============

Choose an option:
1. Search for an institution
2. Exit
Option: 1

**ENTER THE ACRONYM OF THE DESIRED INSTITUTION:
UFRJ
--------------------------------------------------------------------------------------------------------
| Code       | Acronym         | Name                                | Municipality              |
--------------------------------------------------------------------------------------------------------
| 335        | UFRJ            | UNIVERSIDADE FEDERAL DO RIO DE JANEIRO | Rio de Janeiro            |
--------------------------------------------------------------------------------------------------------

Searching for courses from the institution...

------------------------------------------------------------------------------------------------------------------------------------------------
| Code       | Name                                | Degree               | Shift           | Campus                         | Municipality              |
------------------------------------------------------------------------------------------------------------------------------------------------
| 1190479    | ENGENHARIA DE COMPUTAÇÃO E INFORMAÇÃO | Bacharelado          | Integral        | Campus Cidade Universitária    | Rio de Janeiro            |
| 1190481    | CIÊNCIA DA COMPUTAÇÃO                 | Bacharelado          | Integral        | Campus Cidade Universitária    | Rio de Janeiro            |
... (other courses) ...
------------------------------------------------------------------------------------------------------------------------------------------------

**ENTER THE COURSE CODE:
1190481

------------------------------------------------------------------------------------------------------------------------------------------------
| Code       | Name                                | Degree               | Shift           | Campus                         | Municipality              |
------------------------------------------------------------------------------------------------------------------------------------------------
| 1190481    | CIÊNCIA DA COMPUTAÇÃO                 | Bacharelado          | Integral        | Campus Cidade Universitária    | Rio de Janeiro            |
------------------------------------------------------------------------------------------------------------------------------------------------

Collecting course information...

Weights:
| Peso CN         | Peso CH         | Peso Linguagens | Peso Matemática | Peso Redação    |
-------------------------------------------------------------------------------------------
| 2.00            | 1.00            | 1.50            | 4.00            | 1.50            |

Minimum Grades:
| Nota Mínima CN      | Nota Mínima CH      | Nota Mínima Ling    | Nota Mínima Mat     | Nota Mínima Red     |
-----------------------------------------------------------------------------------------------------------------
| 400.00              | 400.00              | 400.00              | 400.00              | 400.00              |
```

### Core Dependencies

- **Selenium WebDriver**: For browser automation to scrape JavaScript-rendered content from the API endpoints.
- **HtmlAgilityPack**: For robust HTML parsing.
- **Newtonsoft.Json**: For efficient serialization and deserialization of JSON data returned by the API.

### Contributing

If you have suggestions for new features or improvements, feel free to open an issue or submit a pull request! All contributions are welcome.

### License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.md) file for details.
