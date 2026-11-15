using AIS.Core.Models;
using AIS.Core.Services;
using AIS.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5080");
builder.Services.AddSingleton<IWorkloadService, InMemoryWorkloadService>();

var app = builder.Build();
app.MapGet("/api/v1/workloads", async (string? academicYear, string? teacherName, IWorkloadService service, CancellationToken ct) =>
{
    if (academicYear is not null && !System.Text.RegularExpressions.Regex.IsMatch(academicYear, @"^\d{4}-\d{4}$"))
        return Results.ValidationProblem(new Dictionary<string, string[]> { ["academicYear"] = ["Ожидается формат YYYY-YYYY."] });
    var items = await service.GetWorkloadsAsync(ct);
    if (!string.IsNullOrWhiteSpace(academicYear)) items = items.Where(x => x.AcademicYear == academicYear).ToList();
    if (!string.IsNullOrWhiteSpace(teacherName)) items = items.Where(x => x.TeacherName.Contains(teacherName, StringComparison.OrdinalIgnoreCase)).ToList();
    return Results.Ok(items);
}).WithName("GetWorkloads");
app.MapGet("/", () => Results.Ok(new { application = "AIS.Api", endpoint = "/api/v1/workloads" }));
app.Run();
