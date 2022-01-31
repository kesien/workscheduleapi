using System.Drawing;
using System.Reflection;
using WorkScheduleMaker.Entities;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace WorkScheduleMaker.Services
{
    

    public class FileService : IFileService
    {
        private readonly IDropboxService _dropbox;

        public FileService(IDropboxService dropbox)
        {
            _dropbox = dropbox;
        }

        public async Task<WordFile> GenerateWordDoc(MonthlySchedule schedule, int max)
        {
            var baseDocument = LoadBaseDocument();
            var id = Guid.NewGuid();
            var fileName = $"{id}.docx";
            var fullPath = Path.Combine("./SavedDocuments", fileName);
            using(DocX document = DocX.Load(baseDocument))
            {
                var scheduleTable = document.Tables[0];
                var summaryTable = document.Tables[1];


                GenerateScheduleTable(schedule, scheduleTable, max);
                GenerateSummaryTable(schedule, summaryTable);
                if (!Directory.Exists($"./SavedDocuments"))
                {
                    Directory.CreateDirectory($"./SavedDocuments");
                }
                using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    document.SaveAs(fs);
                }
                document.Dispose();
            }
            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(fullPath)))
            {
                var result = await _dropbox.UploadFile(fullPath, ms);
                if (!result)
                {
                    return null;
                }
            }
            GC.Collect();
            var dirInfo = new DirectoryInfo($"./SavedDocuments");
            var wordFile = new WordFile
            {
                Id = id,
                FilePath = $"SavedDocuments/{fileName}",
                FileName = fileName
            };
            DeleteAllFiles("./SavedDocuments");
            return wordFile;
        }

        private void DeleteAllFiles(string path)
        {
            foreach (var file in Directory.EnumerateFiles(path))
            {
                File.Delete(file);
            }
        }

        private void GenerateSummaryTable(MonthlySchedule schedule, Table summaryTable)
        {
            foreach (var summary in schedule.Summaries)
            {
                var row = summaryTable.InsertRow();
                row.Cells[0].Paragraphs[0].Append(summary.Name).Bold();
                row.Cells[1].Paragraphs[0].Append(summary.Morning.ToString()).Bold();
                row.Cells[2].Paragraphs[0].Append(summary.Forenoon.ToString()).Bold();
                row.Cells[3].Paragraphs[0].Append(summary.Holiday.ToString()).Bold();
                row.Cells[3].Paragraphs[0].Color(Color.FromArgb(1, 41, 134, 202));
                foreach (var cell in row.Cells)
                {
                    cell.Paragraphs[0].Alignment = Alignment.center;
                }
            }
        }

        private void GenerateScheduleTable(MonthlySchedule schedule, Table table, int max)
        {

            int rowCount = 0;
            var days = schedule.Days.OrderBy(d => d.Date).ToList();

            //comment and comment
            for (int x = 0; x < schedule.Days.Count; x++)
            {
                var day = days[x];
                if (x == 0 && day.IsWeekend) 
                {
                    continue;
                }
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
                            row.Cells[0].Paragraphs[0].Append("8:00-16:30").Bold().FontSize(10);

                        }
                        if (i == ((max * 2) - max) + 1)
                        {
                            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                            row.Cells[0].FillColor = Color.FromArgb(1, 198, 224, 180);
                            row.Cells[0].Paragraphs[0].Alignment = Alignment.center;
                            row.Cells[0].Paragraphs[0].Append("9:30-18:00").Bold().FontSize(10);
                        }
                    }
                    if (max > 1) 
                    {
                        table.MergeCellsInColumn(0, rowCount - (max * 2 - 1), rowCount - max);
                        table.MergeCellsInColumn(0, rowCount - (max - 1), rowCount);
                    }
                }
                if (day.IsWeekend || day.IsHoliday)
                {
                    continue;
                }
                InsertDate(table.Rows[rowCount - max * 2], day);
                for (int i = 0; i < day.UsersScheduledForMorning.Count; i++)
                {
                    var rowIndex = (rowCount - (max * 2 - 1)) + i;
                    var currentRow = table.Rows[rowIndex];
                    List<MorningSchedule> schedules = day.UsersScheduledForMorning.ToList();
                    InsertPerson(currentRow, day, schedules[i].User.Name, schedules[i].IsRequest);
                }
                for (int i = 0; i < day.UsersScheduledForForenoon.Count; i++)
                {
                    var rowIndex = (rowCount - (max - 1)) + i;
                    var currentRow = table.Rows[rowIndex];
                    List<Forenoonschedule> schedules = day.UsersScheduledForForenoon.ToList();
                    InsertPerson(currentRow, day, schedules[i].User.Name, schedules[i].IsRequest);
                }
                if (day.UsersOnHoliday.Count > 0)
                {
                    List<string> names = day.UsersOnHoliday.Select(holiday => holiday.User.Name).ToList();
                    int index = 0;
                    while (day.UsersScheduledForMorning.Count + index < max)
                    {
                        var rowIndex = (rowCount - max - index);
                        var currentRow = table.Rows[rowIndex];
                        InsertPerson(currentRow, day, names[index], false, true);
                        names.Remove(names[index]);
                        index++;
                    }
                    if (names.Count > 0)
                    {
                        index = 0;
                        while (day.UsersScheduledForForenoon.Count + index < max)
                        {
                            var rowIndex = ((rowCount - index));
                            var currentRow = table.Rows[rowIndex];
                            InsertPerson(currentRow, day, names[index], false, true);
                            index++;
                        }
                    }
                }
            }
            SetBorders(table, max);
        }

        private void SetBorders(Table table, int max)
        {
            var thickBorder = new Border(BorderStyle.Tcbs_single, BorderSize.six, 0, Color.Black);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                foreach (var cell in table.Rows[i].Cells)
                {
                    if ((i + 1 + max * 2) % (max * 2 + 1) == 1 || i == 0)
                    {
                        cell.SetBorder(TableCellBorderType.Top, thickBorder);
                        cell.SetBorder(TableCellBorderType.Left, thickBorder);
                        cell.SetBorder(TableCellBorderType.Bottom, thickBorder);
                        cell.SetBorder(TableCellBorderType.Right, thickBorder);
                    }
                    else
                    {
                        cell.SetBorder(TableCellBorderType.Right, thickBorder);
                        if (i % ((max * 2) + 1) == max + 1)
                        {
                            cell.SetBorder(TableCellBorderType.Bottom, thickBorder);
                        }
                    }
                }
                table.Rows[i].Cells[0].SetBorder(TableCellBorderType.Top, thickBorder);
                table.Rows[i].Cells[0].SetBorder(TableCellBorderType.Left, thickBorder);
                table.Rows[i].Cells[0].SetBorder(TableCellBorderType.Bottom, thickBorder);
                table.Rows[i].Cells[0].SetBorder(TableCellBorderType.Right, thickBorder);
            }
        }

        private void InsertDate(Row row, Day day)
        {
            switch (day.Date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    row.Cells[1].Paragraphs[0].Alignment = Alignment.center;
                    row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[1].FillColor = Color.FromArgb(1, 198, 224, 180);
                    row.Cells[1].Paragraphs[0].Append(day.Date.ToString("dd.MM.yyyy")).Bold().FontSize(10);
                    break;
                case DayOfWeek.Tuesday:
                    row.Cells[2].Paragraphs[0].Alignment = Alignment.center;
                    row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[2].FillColor = Color.FromArgb(1, 198, 224, 180);
                    row.Cells[2].Paragraphs[0].Append(day.Date.ToString("dd.MM.yyyy")).Bold().FontSize(10);
                    break;
                case DayOfWeek.Wednesday:
                    row.Cells[3].Paragraphs[0].Alignment = Alignment.center;
                    row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[3].FillColor = Color.FromArgb(1, 198, 224, 180);
                    row.Cells[3].Paragraphs[0].Append(day.Date.ToString("dd.MM.yyyy")).Bold().FontSize(10);
                    break;
                case DayOfWeek.Thursday:
                    row.Cells[4].Paragraphs[0].Alignment = Alignment.center;
                    row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[4].FillColor = Color.FromArgb(1, 198, 224, 180);
                    row.Cells[4].Paragraphs[0].Append(day.Date.ToString("dd.MM.yyyy")).Bold().FontSize(10);
                    break;
                case DayOfWeek.Friday:
                    row.Cells[5].Paragraphs[0].Alignment = Alignment.center;
                    row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[5].FillColor = Color.FromArgb(1, 198, 224, 180);
                    row.Cells[5].Paragraphs[0].Append(day.Date.ToString("dd.MM.yyyy")).Bold().FontSize(10);
                    break;
            }
        }

        private Stream LoadBaseDocument()
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

        private void InsertPerson(Row row, Day day, string name, bool isRequest, bool isHoliday = false)
        {
            Paragraph paragraph = null;
            switch (day.Date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    paragraph = row.Cells[1].Paragraphs[0];
                    break;
                case DayOfWeek.Tuesday:
                    paragraph = row.Cells[2].Paragraphs[0];
                    break;
                case DayOfWeek.Wednesday:
                    paragraph = row.Cells[3].Paragraphs[0];
                    break;
                case DayOfWeek.Thursday:
                    paragraph = row.Cells[4].Paragraphs[0];
                    break;
                case DayOfWeek.Friday:
                    paragraph = row.Cells[5].Paragraphs[0];
                    break;
            }
            AppendName(paragraph, name, isRequest, isHoliday);
        }

        private void AppendName(Paragraph paragraph, string name, bool isRequest, bool isHoliday)
        {
            if (paragraph is null)
            {
                return;
            }
            if (!isHoliday)
            {
                paragraph.Append(name).Bold().FontSize(10);
            }
            if (isRequest)
            {
                paragraph.Color(Color.FromArgb(1, 194, 12, 12));
            }
            if (isHoliday)
            {
                paragraph.Append($"{name} (ferien)").Bold().FontSize(10);
                paragraph.Color(Color.FromArgb(1, 41, 134, 202));
            }
            paragraph.Alignment = Alignment.center;
        }

        public async Task DeleteFile(string path)
        {
            await _dropbox.DeleteFile($"/{path}");
        }
    }
}
