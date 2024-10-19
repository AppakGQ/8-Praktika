using System;

public abstract class ReportGenerator
{
    public void GenerateReport()
    {
        PrepareData();
        FormatReport();
        if (CustomerWantsSave())
        {
            SaveReport();
        }
        Log("Report generated successfully.");
        Hook();
    }

    protected abstract void PrepareData();
    protected abstract void FormatReport();
    protected abstract void SaveReport();

    protected virtual bool CustomerWantsSave()
    {
        Console.WriteLine("Хотите сохранить отчет? (да/нет)");
        string input = Console.ReadLine();
        return input?.Trim().ToLower() == "да";
    }

    protected virtual void Hook() { }

    protected void Log(string message) => Console.WriteLine($"[Журнал]: {message}");
}

public class PdfReport : ReportGenerator
{
    protected override void PrepareData() => Console.WriteLine("Подготовка данных для PDF отчета.");
    protected override void FormatReport() => Console.WriteLine("Форматирование PDF отчета.");
    protected override void SaveReport() => Console.WriteLine("Сохранение PDF отчета.");
}

public class ExcelReport : ReportGenerator
{
    protected override void PrepareData() => Console.WriteLine("Подготовка данных для Excel отчета.");
    protected override void FormatReport() => Console.WriteLine("Форматирование Excel отчета.");
    protected override void SaveReport() => Console.WriteLine("Сохранение Excel отчета.");
}

public class HtmlReport : ReportGenerator
{
    protected override void PrepareData() => Console.WriteLine("Подготовка данных для HTML отчета.");
    protected override void FormatReport() => Console.WriteLine("Форматирование HTML отчета.");
    protected override void SaveReport() => Console.WriteLine("Сохранение HTML отчета.");
}

public class CsvReport : ReportGenerator
{
    protected override void PrepareData() => Console.WriteLine("Подготовка данных для CSV отчета.");
    protected override void FormatReport() => Console.WriteLine("Форматирование CSV отчета.");
    protected override void SaveReport() => Console.WriteLine("Сохранение CSV отчета.");
    protected override void Hook() => Console.WriteLine("Отправка CSV отчета по электронной почте.");
}

class Program
{
    static void Main()
    {
        ReportGenerator pdfReport = new PdfReport();
        pdfReport.GenerateReport();

        ReportGenerator excelReport = new ExcelReport();
        excelReport.GenerateReport();

        ReportGenerator htmlReport = new HtmlReport();
        htmlReport.GenerateReport();

        ReportGenerator csvReport = new CsvReport();
        csvReport.GenerateReport();
    }
}
