using System.Drawing;
using System.Reflection;
using WorkSchedule.Application.Persistency.Entities;
using WorkSchedule.Application.Services.DropboxService;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace WorkSchedule.Application.Services.FileService
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
            using (DocX document = DocX.Load(baseDocument))
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

            GC.Collect();
            var wordFile = new WordFile
            {
                Id = id,
                FilePath = $"SavedDocuments/{fileName}",
                FileName = fileName
            };
            return wordFile;
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

        private Table GenerateScheduleTable(MonthlySchedule schedule, Table table, int max)
        {
            int rowCount = 0;
            var days = schedule.Days.OrderBy(day => day.Date).ToList();
            var dateRow = table.InsertRow();
            var morningRow = table.InsertRow();
            var forenoonRow = table.InsertRow();
            morningRow.Cells[0] = InsertHeader(morningRow.Cells[0], "8:00-16:30");
            forenoonRow.Cells[0] = InsertHeader(forenoonRow.Cells[0], "9:00-18:00");
            for (int dayIndex = 0; dayIndex < schedule.Days.Count; dayIndex++)
            {
                var day = days[dayIndex];

                if ((dayIndex == 0 || day.Date.DayOfWeek == DayOfWeek.Sunday) && day.IsWeekend)
                {
                    continue;
                }
                if (day.Date.DayOfWeek == DayOfWeek.Monday)
                {
                    dateRow = table.InsertRow();
                    morningRow = table.InsertRow();
                    forenoonRow = table.InsertRow();
                    morningRow.Cells[0] = InsertHeader(morningRow.Cells[0], "8:00-16:30");
                    forenoonRow.Cells[0] = InsertHeader(forenoonRow.Cells[0], "9:00-18:00");
                }

                dateRow = InsertDate(dateRow, day);
                if (day.IsWeekend)
                {
                    continue;
                }
                if (day.IsHoliday)
                {
                    morningRow = InsertHoliday(morningRow, day);
                    forenoonRow = InsertHoliday(forenoonRow, day);
                }
                for (int i = 0; i < day.UsersScheduledForMorning.Count; i++)
                {
                    List<MorningSchedule> schedules = day.UsersScheduledForMorning.ToList();
                    morningRow = InsertPerson(morningRow, day, schedules[i].User.Name, schedules[i].IsRequest);
                }
                for (int i = 0; i < day.UsersScheduledForForenoon.Count; i++)
                {
                    List<Forenoonschedule> schedules = day.UsersScheduledForForenoon.ToList();
                    forenoonRow = InsertPerson(forenoonRow, day, schedules[i].User.Name, schedules[i].IsRequest);
                }
                if (day.UsersOnHoliday.Count > 0)
                {
                    var usersOnHoliday = day.UsersOnHoliday.Select(holiday => holiday.User.Name).ToArray();
                    int index = 0;
                    while (day.UsersScheduledForMorning.Count + index < max && usersOnHoliday.Length != 0)
                    {
                        var name = usersOnHoliday[index];
                        morningRow = InsertPerson(morningRow, day, name, false, true);
                        usersOnHoliday = usersOnHoliday.Where((user) => user != name).ToArray();
                    }
                    if (usersOnHoliday.Length > 0)
                    {
                        index = 0;
                        var name = usersOnHoliday[index];
                        while (day.UsersScheduledForForenoon.Count + index < max)
                        {
                            forenoonRow = InsertPerson(forenoonRow, day, name, false, true);
                            index++;
                        }
                    }
                }
            }
            table = DeleteUnusedParagraphs(table);
            table = SetBorders(table);
            return table;
        }

        private Row InsertHoliday(Row row, Day day)
        {
            Cell cell = null;
            switch (day.Date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    cell = row.Cells[1];
                    break;
                case DayOfWeek.Tuesday:
                    cell = row.Cells[2];
                    break;
                case DayOfWeek.Wednesday:
                    cell = row.Cells[3];
                    break;
                case DayOfWeek.Thursday:
                    cell = row.Cells[4];
                    break;
                case DayOfWeek.Friday:
                    cell = row.Cells[5];
                    break;
            }
            if (cell is not null)
            {
                var paragraph = cell.Paragraphs[0];
                cell.VerticalAlignment = VerticalAlignment.Center;
                paragraph.Append("Feiertag").Bold().FontSize(10);
                paragraph.Alignment = Alignment.center;
                paragraph.Color(Color.FromArgb(1, 255, 0, 0));
            }
            return row;
        }

        private Cell InsertHeader(Cell cell, string headerText)
        {
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.FillColor = Color.FromArgb(1, 198, 224, 180);
            cell.Paragraphs[0].Alignment = Alignment.center;
            cell.Paragraphs[0].Append(headerText).Bold().FontSize(10);
            return cell;
        }

        private Table SetBorders(Table table)
        {
            var thickBorder = new Border(BorderStyle.Tcbs_single, BorderSize.six, 0, Color.Black);
            foreach (var row in table.Rows)
            {
                foreach (var cell in row.Cells)
                {
                    cell.SetBorder(TableCellBorderType.Top, thickBorder);
                    cell.SetBorder(TableCellBorderType.Left, thickBorder);
                    cell.SetBorder(TableCellBorderType.Bottom, thickBorder);
                    cell.SetBorder(TableCellBorderType.Right, thickBorder);
                }
            }
            return table;
        }

        private Row InsertDate(Row row, Day day)
        {
            var date = day.Date.ToString("dd.MM.yyyy");
            switch (day.Date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    row.Cells[1] = GetPreparedCell(row.Cells[1], date);
                    break;
                case DayOfWeek.Tuesday:
                    row.Cells[2] = GetPreparedCell(row.Cells[2], date);
                    break;
                case DayOfWeek.Wednesday:
                    row.Cells[3] = GetPreparedCell(row.Cells[3], date);
                    break;
                case DayOfWeek.Thursday:
                    row.Cells[4] = GetPreparedCell(row.Cells[4], date);
                    break;
                case DayOfWeek.Friday:
                    row.Cells[5] = GetPreparedCell(row.Cells[5], date);
                    break;
            }
            return row;
        }

        private Cell GetPreparedCell(Cell cell, string date)
        {
            cell.Paragraphs[0].Alignment = Alignment.center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.FillColor = Color.FromArgb(1, 198, 224, 180);
            cell.Paragraphs[0].Append(date).Bold().FontSize(10);
            return cell;
        }

        private Stream LoadBaseDocument()
        {
            var resourceName = "WorkSchedule.Application.Resources._base_document.docx";
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream baseDocument = assembly.GetManifestResourceStream(resourceName);
            if (baseDocument is null)
            {
                throw new FileNotFoundException("Base Word document is not found!");
            }
            return baseDocument;
        }

        private Row InsertPerson(Row row, Day day, string name, bool isRequest, bool isHoliday = false)
        {
            Cell cell = null;
            switch (day.Date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    cell = row.Cells[1];
                    break;
                case DayOfWeek.Tuesday:
                    cell = row.Cells[2];
                    break;
                case DayOfWeek.Wednesday:
                    cell = row.Cells[3];
                    break;
                case DayOfWeek.Thursday:
                    cell = row.Cells[4];
                    break;
                case DayOfWeek.Friday:
                    cell = row.Cells[5];
                    break;
            }
            if (cell is not null)
            {
                cell.VerticalAlignment = VerticalAlignment.Center;
                AppendName(cell, name, isRequest, isHoliday);
            }
            return row;
        }

        private void AppendName(Cell cell, string name, bool isRequest, bool isHoliday)
        {
            var paragraph = cell.InsertParagraph();
            paragraph.Alignment = Alignment.both;
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

        private Table DeleteUnusedParagraphs(Table table)
        {
            foreach (var row in table.Rows)
            {
                foreach (var cell in row.Cells)
                {
                    if (cell.Paragraphs.Count != 1)
                    {
                        cell.RemoveParagraph(cell.Paragraphs[0]);
                    }
                }
            }
            return table;
        }
    }
}
