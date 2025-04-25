# **SisuDataQuery**
### A tool to query SISU data via a console application.

This project allows you to query the minimum grades, course weights, and other information for any course registered in SISU by interacting with the SISU API.

### Features

Currently, you can query:
- Course weights
- Minimum cutoff grades for each course
- List of institutions and courses available in the SISU system

Other features are under development. Feel free to suggest improvements or new features!

### How it works

The application queries the SISU API in three steps:
1. **Query Institutions**: Retrieves the registered institutions in SISU, filtered by state.
2. **Query Courses**: For each institution, you can query the offered courses.
3. **Course Information**: After selecting the institution and course, the application returns details such as the minimum cutoff grade and course weights.

### How to use

1. **Installing dependencies**:
   - Clone the repository to your local machine.
   - Install the necessary dependencies with the following command:
     ```bash
     pip install -r requirements.txt
     ```
   > **Note**: This project uses C# and requires Selenium WebDriver and ChromeDriver for web scraping.

2. **Running the application**:
   After installing the dependencies, run the code and follow the instructions in the console:
   - First, enter the acronym of the institution (e.g., "UFRJ").
   - Then, provide the course code you want to query.
   - The application will display the list of available institutions and courses, then query the course weights and minimum grades.
3. **Example usage**:
```bash
$ dotnet run
SISU DATA QUERY
**Enter the desired institution acronym:
UFRJ
-------------------------
| Code                    | Acronym                 | Name                  | City                 |
-------------------------
| 12345                   | UFRJ                    | Universidade Federal do Rio de Janeiro | Rio de Janeiro      |
Searching for courses from the institution...
**Enter the course code:
12345
-------------------------
| Code                    | Name                    | Degree                | Shift                | Campus               | City                 |
-------------------------
| 12345                   | Computer Science        | Bachelor              | Morning              | Rio de Janeiro       | Rio de Janeiro      |
Collecting course information...
-------------------------
| Peso CN    | Peso CH    | Peso Ling  | Peso Mtm   | Peso Dis   |
| 2          | 3          | 1          | 4          | 2          |
| Nota minima CN|Nota minima CH|Nota minima Lng|Nota minima Mt|Nota minima Dis|
| 600        | 620        | 580        | 630        | 610        |

```
**4. Course Data**:

After selecting a course, the application will fetch and display the course weights and minimum grades for different subjects like Natural Sciences, Humanities, Language, Mathematics, and others.

#### Dependencies:
  - Selenium WebDriver: Used for web scraping to interact with the SISU API pages.

  - HtmlAgilityPack: Used for parsing and navigating HTML content.

  - Newtonsoft.Json: Used for handling JSON data from the SISU API.

### Suggestions and Contributions**:  
  
If you have any suggestions for new features or improvements, feel free to open an issue or submit a pull request!

### License:  
  
This project is licensed under the MIT License
  
