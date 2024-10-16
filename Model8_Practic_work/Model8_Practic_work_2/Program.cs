using System;
namespace Model8_Practic_work_2
{
    public abstract class ReportGenerator
    {
        // Шаблонный метод для создания отчета
        public void GenerateReport()
        {
            PrepareData();
            GenerateHeader();
            GenerateBody();
            if (CustomerWantsSave())
            {
                SaveReport();
            }
            else
            {
                SendByEmail();
            }
            LogReportGeneration();
        }

        // Шаг 1: Подготовка данных (общий для всех отчетов)
        protected void PrepareData()
        {
            Console.WriteLine("Preparing data...");
        }

        // Шаг 2: Генерация заголовка (абстрактный метод)
        protected abstract void GenerateHeader();

        // Шаг 3: Генерация тела отчета (абстрактный метод)
        protected abstract void GenerateBody();

        // Шаг 4: Сохранение отчета (абстрактный метод)
        protected abstract void SaveReport();

        // Опциональный метод для отправки по email
        protected virtual void SendByEmail()
        {
            Console.WriteLine("Sending report via email...");
        }

        // Хук для решения о сохранении отчета
        protected virtual bool CustomerWantsSave()
        {
            while (true)
            {
                Console.WriteLine("Do you want to save the report? (y/n): ");
                string input = Console.ReadLine()?.ToLower();
                if (input == "y" || input == "n")
                {
                    return input == "y";
                }
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
            }
        }

        // Логирование процесса генерации отчета
        protected virtual void LogReportGeneration()
        {
            Console.WriteLine("Logging: Report generation step completed.");
        }
    }
    public class PdfReport : ReportGenerator
    {
        protected override void GenerateHeader()
        {
            Console.WriteLine("Generating PDF header...");
        }

        protected override void GenerateBody()
        {
            Console.WriteLine("Generating PDF body...");
        }

        protected override void SaveReport()
        {
            Console.WriteLine("Saving PDF report...");
        }
    }
    public class ExcelReport : ReportGenerator
    {
        protected override void GenerateHeader()
        {
            Console.WriteLine("Generating Excel header...");
        }

        protected override void GenerateBody()
        {
            Console.WriteLine("Generating Excel body...");
        }

        protected override void SaveReport()
        {
            Console.WriteLine("Saving Excel report...");
        }
    }
    public class HtmlReport : ReportGenerator
    {
        protected override void GenerateHeader()
        {
            Console.WriteLine("Generating HTML header...");
        }

        protected override void GenerateBody()
        {
            Console.WriteLine("Generating HTML body...");
        }

        protected override void SaveReport()
        {
            Console.WriteLine("Saving HTML report...");
        }
    }
    public class CsvReport : ReportGenerator
    {
        protected override void GenerateHeader()
        {
            Console.WriteLine("Generating CSV header...");
        }

        protected override void GenerateBody()
        {
            Console.WriteLine("Generating CSV body...");
        }

        protected override void SaveReport()
        {
            Console.WriteLine("Saving CSV report...");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ReportGenerator pdfReport = new PdfReport();
            ReportGenerator excelReport = new ExcelReport();
            ReportGenerator htmlReport = new HtmlReport();
            ReportGenerator csvReport = new CsvReport();

            Console.WriteLine("PDF Report:");
            pdfReport.GenerateReport();

            Console.WriteLine("\nExcel Report:");
            excelReport.GenerateReport();

            Console.WriteLine("\nHTML Report:");
            htmlReport.GenerateReport();

            Console.WriteLine("\nCSV Report:");
            csvReport.GenerateReport();

            Console.ReadKey();
        }
    }
}
