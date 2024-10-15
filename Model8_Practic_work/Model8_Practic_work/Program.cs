using System;

abstract class ReportGenerator
{
    public void GenerateReport()
    {
        PrepareData();
        FormatData();
        CreateHeader();
        SaveReport();
        SendReport();
    }

    protected abstract void FormatData();

    protected virtual void PrepareData()
    {
        Console.WriteLine("Preparing data for the report.");
    }

    protected virtual void CreateHeader()
    {
        Console.WriteLine("Creating default header for the report.");
    }

    protected virtual void SaveReport()
    {
        Console.WriteLine("Saving report.");
    }

    protected virtual void SendReport()
    {
        Console.WriteLine("Sending report.");
    }
}

class PdfReport : ReportGenerator
{
    protected override void FormatData()
    {
        Console.WriteLine("Formatting data for PDF report.");
    }

    protected override void CreateHeader()
    {
        Console.WriteLine("Creating header for PDF report.");
    }

    protected override void SaveReport()
    {
        Console.WriteLine("Saving PDF report.");
    }
}

class ExcelReport : ReportGenerator
{
    protected override void FormatData()
    {
        Console.WriteLine("Formatting data for Excel report.");
    }

    protected override void SaveReport()
    {
        Console.WriteLine("Saving Excel report.");
    }
}

class HtmlReport : ReportGenerator
{
    protected override void FormatData()
    {
        Console.WriteLine("Formatting data for HTML report.");
    }

    protected override void CreateHeader()
    {
        Console.WriteLine("Creating header for HTML report.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ReportGenerator pdfReport = new PdfReport();
        pdfReport.GenerateReport();

        Console.WriteLine();

        ReportGenerator excelReport = new ExcelReport();
        excelReport.GenerateReport();

        Console.WriteLine();

        ReportGenerator htmlReport = new HtmlReport();
        htmlReport.GenerateReport();
    }
}
