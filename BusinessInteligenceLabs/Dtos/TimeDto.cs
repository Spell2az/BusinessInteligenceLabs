using System;
using System.Runtime.InteropServices;

namespace BusinessInteligenceLabs.Dtos
{
  public class TimeDto
  {
    private readonly DateTime dateTime;

    public TimeDto(DateTime dateTime)
    {
      this.dateTime = dateTime;
    }

    public int Id { get; set; }
    public string DayName => dateTime.DayOfWeek.ToString();
    public int DayNumber => (int) dateTime.DayOfWeek;
    public string MonthName => dateTime.ToString("MMMM");
    public int MonthNumber => dateTime.Month;
    public int WeekNumber => (int)dateTime.DayOfYear / 7;
    public int Year => dateTime.Year;
    public DateTime Date => dateTime;
    public int DayOfYear => dateTime.DayOfYear;
    public bool Weekend => dateTime.ToString("dddd") == "Sunday" || dateTime.ToString("dddd") == "Saturday";

    public override string ToString()
    {
      return $"{nameof(dateTime)}: {dateTime}, {nameof(Id)}: {Id}, {nameof(DayName)}: {DayName}, {nameof(DayNumber)}: {DayNumber}, {nameof(MonthName)}: {MonthName}, {nameof(MonthNumber)}: {MonthNumber}, {nameof(WeekNumber)}: {WeekNumber}, {nameof(Date)}: {Date}, {nameof(DayOfYear)}: {DayOfYear}, {nameof(Weekend)}: {Weekend}";
    }
  }
}