var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5080");
var app = builder.Build();

var data = new List<WorkloadItem>
{
    new("tch-001", "Иванова Мария Сергеевна", "Информатика", "2026-2027", 612, "Рассчитано"),
    new("tch-002", "Петров Алексей Олегович", "Математика", "2026-2027", 548, "Требует проверки"),
    new("tch-003", "Смирнова Елена Павловна", "Информатика", "2026-2027", 590, "Рассчитано")
};

app.MapGet("/api/v1/workloads", (string? academicYear, string? teacherName) =>
{
    if (academicYear is not null && !System.Text.RegularExpressions.Regex.IsMatch(academicYear, @"^\d{4}-\d{4}$"))
        return Results.ValidationProblem(new Dictionary<string, string[]> { ["academicYear"] = ["Ожидается формат YYYY-YYYY."] });
    if (teacherName is { Length: > 100 })
        return Results.ValidationProblem(new Dictionary<string, string[]> { ["teacherName"] = ["Максимальная длина — 100 символов."] });

    IEnumerable<WorkloadItem> result = data;
    if (!string.IsNullOrWhiteSpace(academicYear)) result = result.Where(x => x.AcademicYear == academicYear);
    if (!string.IsNullOrWhiteSpace(teacherName)) result = result.Where(x => x.TeacherName.Contains(teacherName, StringComparison.OrdinalIgnoreCase));
    return Results.Ok(result);
}).WithName("GetWorkloads");

app.MapGet("/", () => Results.Ok(new { application = "StudentLoad.Api", endpoint = "/api/v1/workloads" }));
app.Run();

public sealed record WorkloadItem(string TeacherId, string TeacherName, string Department, string AcademicYear, int PlannedHours, string Status);
