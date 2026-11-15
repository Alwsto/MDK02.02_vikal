namespace StudentLoad.Client.Models;

public sealed record WorkloadItem(
    string TeacherId,
    string TeacherName,
    string Department,
    string AcademicYear,
    int PlannedHours,
    string Status);
