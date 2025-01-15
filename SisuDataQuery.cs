using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

#pragma warning disable



class SisuDataQuery
{
    public static List<Institution> listInstitutions = new List<Institution>();
    public static List<Course> listIesCourses = new List<Course>();
    const int columnWidth = 40;

    public class Institution
    {
        public Institution(string coIes, string sgIes, string noIes, string noMunicipio)
        {
            this.institutionCode = coIes;
            this.institutionSg = sgIes;
            this.institutionName = noIes;
            this.institutionCity = noMunicipio;
        }

        public string? institutionCode { get; set; }
        public string? institutionSg { get; set; }
        public string? institutionName { get; set; }
        public string? institutionCity { get; set; }
        public override string ToString()
        {
            return $"| {institutionCode,-columnWidth} | {institutionSg,-columnWidth} | {institutionName,-columnWidth} | {institutionCity,-columnWidth} |\n";
        }
    }
    public class Course
    {
        public Course(string co_curso, string no_curso, string no_turno, string no_campus, string no_municipio_campus)
        {
            this.courseCode = co_curso;
            this.courseName = no_curso;
            this.courseShift = no_turno;
            this.campusName = no_campus;
            this.campusCity = no_municipio_campus;

        }

        public string? courseCode { get; set; }
        public string? courseName { get; set; }
        public string? courseShift { get; set; }
        public string? campusName { get; set; }
        public string? campusCity { get; set; }

        public override string ToString()
        {
            return $"| {courseCode,-columnWidth} | {courseName,-columnWidth} | {courseShift,-columnWidth} | {campusName,-columnWidth} | {campusCity,-columnWidth} |\n";
        }
    }

    static void Main(string[] args)
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless");
        IWebDriver driver = new ChromeDriver(options);
        ObtainIes(driver);
        Console.WriteLine("Insira a sigla da instituição que deseja:");
        string siglaIes = Console.ReadLine();
        foreach (Institution i in listInstitutions)
        {
            if (siglaIes.ToUpper() == i.institutionSg)
            {
                Console.WriteLine(new string('-', columnWidth * 5 + 10));
                Console.WriteLine($"| {"Código",-columnWidth} | {"Sigla",-columnWidth} | {"Nome",-columnWidth} | {"Municipio",-columnWidth} |");
                Console.WriteLine(new string('-', columnWidth * 5 + 10));
                Console.WriteLine(i);
                Console.WriteLine("Procurando cursos da instituição...\n...");
                getCourses(driver, i.institutionCode);
                Console.WriteLine("Insira o codigo do curso:");
                string courseCode = Console.ReadLine();
                foreach (Course c in listIesCourses)
                {
                    if (courseCode == c.courseCode)
                    {
                        Console.WriteLine(new string('-', columnWidth * 5 + 20));
                        Console.WriteLine($"| {"Código",-columnWidth} | {"Nome",-columnWidth} | {"Turno",-columnWidth} | {"Campus",-columnWidth} | {"Municipio",-columnWidth} |");
                        Console.WriteLine(new string('-', columnWidth * 5 + 20));
                        Console.WriteLine(c);
                    }
                }
            }
        }
        driver.Quit();
    }
    public static void ObtainIes(IWebDriver driver)
    {
        driver.Navigate().GoToUrl("https://sisu-api.sisu.mec.gov.br/api/v1/oferta/instituicoes/uf");

        System.Threading.Thread.Sleep(5000);

        var htmlContent = driver.PageSource;

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(htmlContent);

        var preNode = htmlDocument.DocumentNode.SelectSingleNode("//pre");

        if (preNode != null)
        {
            var jsonContent = preNode.InnerText;
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonContent);

            foreach (var state in data)
            {
                var stateValue = state.Value?.ToString();
                if (stateValue != null)
                {
                    var institutions = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(stateValue);
                    foreach (var institution in institutions)
                    {
                        var coIes = institution.GetValueOrDefault("co_ies");
                        var sgIes = institution.GetValueOrDefault("sg_ies");
                        var noIes = institution.GetValueOrDefault("no_ies");
                        var noMunicipio = institution.GetValueOrDefault("no_municipio");

                        if (coIes != null && noIes != null && sgIes != null && noMunicipio != null)
                        {
                            Institution _institution = new Institution(coIes, sgIes, noIes, noMunicipio);
                            listInstitutions.Add(_institution);
                        }
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Não foi encontrado o elemento <pre> no HTML.");
        }
    }
    public static void getCourses(IWebDriver driver, string iesCode)
    {
        driver.Navigate().GoToUrl($"https://sisu-api.sisu.mec.gov.br/api/v1/oferta/instituicao/{iesCode}");

        System.Threading.Thread.Sleep(5000);

        var htmlContent = driver.PageSource;

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(htmlContent);
        var preNode = htmlDocument.DocumentNode.SelectSingleNode("//pre");
        if (preNode != null)
        {
            var jsonContent = preNode.InnerText;

            var cleanedJsonContent = CleanJson(jsonContent);

            if (!string.IsNullOrEmpty(cleanedJsonContent))
            {
                var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(cleanedJsonContent);

                data.Remove("search_rule");

                for (int i = 0; i < data.Count; i++)
                {
                    var courseKey = i.ToString();
                    if (data.ContainsKey(courseKey))
                    {
                        var courseValue = data[courseKey]?.ToString();

                        if (courseValue != null)
                        {
                            var courseDetails = JsonConvert.DeserializeObject<Dictionary<string, string>>(courseValue);

                            var coCurso = courseDetails.GetValueOrDefault("co_curso");
                            var noCurso = courseDetails.GetValueOrDefault("no_curso");
                            var noTurno = courseDetails.GetValueOrDefault("no_turno");
                            var noCampus = courseDetails.GetValueOrDefault("no_campus");
                            var noMunicipioCampus = courseDetails.GetValueOrDefault("no_municipio_campus");

                            if (coCurso != null && noCurso != null && noTurno != null && noCampus != null && noMunicipioCampus != null)
                            {
                                Course _course = new Course(coCurso, noCurso, noTurno, noCampus, noMunicipioCampus);
                                listIesCourses.Add(_course);
                                Console.WriteLine(new string('-', columnWidth * 5 + 20));
                                Console.WriteLine($"| {"Código",-columnWidth} | {"Nome",-columnWidth} | {"Turno",-columnWidth} | {"Campus",-columnWidth} | {"Municipio",-columnWidth} |");
                                Console.WriteLine(new string('-', columnWidth * 5 + 20));
                                Console.WriteLine(_course);
                            }

                        }
                    }
                }
            }
        }
    }

    public static string CleanJson(string jsonContent)
    {
        var trimmedJson = jsonContent.Trim();

        if (trimmedJson.EndsWith(","))
        {
            trimmedJson = trimmedJson.Substring(0, trimmedJson.Length - 1);
        }
        return trimmedJson;
    }


}

