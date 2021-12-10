using System.Drawing;
using System.Reflection;
using WorkScheduleMaker.Entities;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace WorkScheduleMaker.Services
{
    public static class FileService
    {
        public static string GenerateWordDoc(MonthlySchedule schedule, int max)
        {
            var baseDocument = LoadBaseDocument();
            var fileName = $"{schedule.Year}_{schedule.Month}_{RandomString(10)}.docx";
            using DocX document = DocX.Load(baseDocument);
            var table = document.Tables[0];
            int rowCount = 0;
            
            for (int x = 0; x < schedule.Days.Count; x++)
            {
                var day = (schedule.Days as List<Day>)[x];
                // Create rows for each week
                if (x == 0 || day.Date.DayOfWeek == DayOfWeek.Monday)
                {
                    for (int i = 1; i <= max * 2; i++)
                    {
                        var row = table.InsertRow();
                        rowCount++;
                        if (i == 1)
                        {
                            row = table.InsertRow();
                            rowCount++;
                            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                            row.Cells[0].FillColor = Color.FromArgb(1, 198, 224, 180);
                            row.Cells[0].Paragraphs[0].Alignment = Alignment.center;
                            row.Cells[0].Paragraphs[0].Append("8:00-16:30").Bold();

                        }
                        if (i == ((max * 2) - max) + 1)
                        {
                            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                            row.Cells[0].FillColor = Color.FromArgb(1, 198, 224, 180);
                            row.Cells[0].Paragraphs[0].Alignment = Alignment.center;
                            row.Cells[0].Paragraphs[0].Append("9:30-18:00").Bold();
                        }
                    }
                    table.MergeCellsInColumn(0, rowCount - (max * 2 - 1), rowCount - max);
                    table.MergeCellsInColumn(0, rowCount - (max - 1), rowCount);
                }
                if (day.IsWeekend || day.IsHoliday)
                {
                    continue;
                }
                InsertDate(table.Rows[rowCount-max * 2], day);
                for (int i = 0; i < day.UsersScheduledForMorning.Count; i++)
                {
                    var rowIndex = (rowCount - (max * 2 - 1)) + i;
                    var currentRow = table.Rows[rowIndex];
                    InsertPerson(currentRow, day, (day.UsersScheduledForMorning as List<MorningSchedule>)[i].User.Name);
                }
                for (int i = 0; i < day.UsersScheduledForForenoon.Count; i++)
                {
                    var rowIndex = (rowCount - (max - 1)) + i;
                    var currentRow = table.Rows[rowIndex];
                    InsertPerson(currentRow, day, (day.UsersScheduledForForenoon as List<Forenoonschedule>)[i].User.Name);
                }
                if (day.UsersOnHoliday.Count > 0) 
                {
                    if (day.UsersScheduledForMorning.Count < day.UsersScheduledForForenoon.Count) 
                    {
                        for (int i = 0; i < day.UsersOnHoliday.Count; i++)
                        {
                            var rowIndex = (rowCount - (max * 2 - 1)) + i;
                        }
                    }
                }
                
            }
            document.SaveAs(new FileStream($"./{fileName}", FileMode.Create, FileAccess.Write));
            document.Dispose();
            return fileName;
        }

        private static void InsertDate(Row row, Day day)
        {
            switch (day.Date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    row.Cells[1].Paragraphs[0].Alignment = Alignment.center;
                    row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[1].FillColor = Color.FromArgb(1, 198, 224, 180);
                    row.Cells[1].Paragraphs[0].Append(day.Date.ToString("dd.MM.yyyy")).Bold();
                    break;
                case DayOfWeek.Tuesday:
                    row.Cells[2].Paragraphs[0].Alignment = Alignment.center;
                    row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[2].FillColor = Color.FromArgb(1, 198, 224, 180);
                    row.Cells[2].Paragraphs[0].Append(day.Date.ToString("dd.MM.yyyy")).Bold();
                    break;
                case DayOfWeek.Wednesday:
                    row.Cells[3].Paragraphs[0].Alignment = Alignment.center;
                    row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[3].FillColor = Color.FromArgb(1, 198, 224, 180);
                    row.Cells[3].Paragraphs[0].Append(day.Date.ToString("dd.MM.yyyy")).Bold();
                    break;
                case DayOfWeek.Thursday:
                    row.Cells[4].Paragraphs[0].Alignment = Alignment.center;
                    row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[4].FillColor = Color.FromArgb(1, 198, 224, 180);
                    row.Cells[4].Paragraphs[0].Append(day.Date.ToString("dd.MM.yyyy")).Bold();
                    break;
                case DayOfWeek.Friday:
                    row.Cells[5].Paragraphs[0].Alignment = Alignment.center;
                    row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[5].FillColor = Color.FromArgb(1, 198, 224, 180);
                    row.Cells[5].Paragraphs[0].Append(day.Date.ToString("dd.MM.yyyy")).Bold();
                    break;
            }
        }

        private static Stream LoadBaseDocument()
        {
            var resourceName = "WorkScheduleMaker.Resources._base_document.docx";
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream baseDocument = assembly.GetManifestResourceStream(resourceName);
            if (baseDocument is null)
            {
                throw new FileNotFoundException("Base Word document is not found!");
            }
            return baseDocument;
        }

        private static void InsertPerson(Row row, Day day, string name)
        {
            switch (day.Date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    row.Cells[1].Paragraphs[0].Append(name);
                    break;
                case DayOfWeek.Tuesday:
                    row.Cells[2].Paragraphs[0].Append(name);
                    break;
                case DayOfWeek.Wednesday:
                    row.Cells[3].Paragraphs[0].Append(name);
                    break;
                case DayOfWeek.Thursday:
                    row.Cells[4].Paragraphs[0].Append(name);
                    break;
                case DayOfWeek.Friday:
                    row.Cells[5].Paragraphs[0].Append(name);
                    break;
            }
        }

        private static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
