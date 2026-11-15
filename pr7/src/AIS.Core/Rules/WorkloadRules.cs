using AIS.Core.Models;

namespace AIS.Core.Rules;

public static class WorkloadRules
{
    public static bool IsValid(WorkloadItem item) =>
        !string.IsNullOrWhiteSpace(item.TeacherId) &&
        !string.IsNullOrWhiteSpace(item.TeacherName) &&
        !string.IsNullOrWhiteSpace(item.Department) &&
        System.Text.RegularExpressions.Regex.IsMatch(item.AcademicYear, @"^\d{4}-\d{4}$") &&
        item.PlannedHours >= 0 &&
        (item.Status is "Рассчитано" or "Требует проверки");

    public static int SumHours(IEnumerable<WorkloadItem> items) => items.Sum(x => x.PlannedHours);
}
