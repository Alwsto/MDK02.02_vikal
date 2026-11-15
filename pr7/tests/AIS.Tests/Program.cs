using AIS.Core.Models;
using AIS.Core.Rules;

var valid = new WorkloadItem("tch-01", "Иванова Мария", "Информатика", "2026-2027", 120, "Рассчитано");
var negative = valid with { PlannedHours = -1 };
var invalidYear = valid with { AcademicYear = "2026/2027" };
var wrongStatus = valid with { Status = "Готово" };

Assert(WorkloadRules.IsValid(valid), "Допустимая запись должна проходить проверку.");
Assert(!WorkloadRules.IsValid(negative), "Отрицательное число часов должно отклоняться.");
Assert(!WorkloadRules.IsValid(invalidYear), "Неверный формат учебного года должен отклоняться.");
Assert(!WorkloadRules.IsValid(wrongStatus), "Неизвестный статус должен отклоняться.");
Assert(WorkloadRules.SumHours([valid, valid]) == 240, "Сумма часов должна рассчитываться корректно.");

Console.WriteLine("Все проверки AIS.Core прошли успешно.");

static void Assert(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}
