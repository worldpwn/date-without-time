using System;
using System.Collections.Generic;
using System.Linq;

namespace ValueObjects
{
  public class DateWithoutTime : ValueObject, IComparable<DateWithoutTime>
  {
    public readonly int Day;
    public readonly int Month;
    public readonly int Year;

    private static readonly string _stringSuffix = "DateWithoutTime";
    private static readonly string _stringFormat = $"dd/MM/yyyy/{_stringSuffix}";

    public DateWithoutTime(string str)
    {
      DateWithoutTime dateWithoutTime = DateWithoutTime.Parse(str);

      this.Day = dateWithoutTime.Day;
      this.Month = dateWithoutTime.Month;
      this.Year = dateWithoutTime.Year;
    }

    public DateWithoutTime(int day, int month, int year)
    {
      ValidateValues(day, month, year);
      Day = day;
      Month = month;
      Year = year;
    }

    public DateWithoutTime(DateWithoutTime dateWithoutTime)
    {
       ValidateValues(dateWithoutTime.Day, dateWithoutTime.Month, dateWithoutTime.Year);
       Day = dateWithoutTime.Day;
       Month = dateWithoutTime.Month;
       Year = dateWithoutTime.Year;
    }

    public DateWithoutTime(DateTime datetime)
    {
      ValidateValues(datetime.Day, datetime.Month, datetime.Year);
      Day = datetime.Day;
      Month = datetime.Month;
      Year = datetime.Year;
    }

    private void ValidateValues(int day, int month, int year)
    {
      // Year validation
      if (year < 1)
        throw new ArgumentOutOfRangeException(nameof(year), year, "Year should be greater than zero");
      // Month validation
      if (month < 1)
        throw new ArgumentOutOfRangeException(nameof(month), month, "Month should be greater than zero");
      if (month > 12)
        throw new ArgumentOutOfRangeException(nameof(month), month, "Month should be less than or equal to 12");

      // Day validation
      if (day < 1)
        throw new ArgumentOutOfRangeException(nameof(day), day, "Day should be greater than zero");

      int daysInMonth = DateTime.DaysInMonth(year, month);
      if (day > daysInMonth)
        throw new ArgumentOutOfRangeException(nameof(day), day, $"In {new DateTime(year, month, 1):MMMM} of {year} just {daysInMonth} days, but there was an attempt to create with {day} day");
     
    }

    public DateWithoutTime AddMonths(int months)
    {
      DateTime beforeAdd = new DateTime(this.Year, this.Month, this.Day);
      DateTime afterAdd = beforeAdd.AddMonths(months);

      return new DateWithoutTime(afterAdd.Day, afterAdd.Month, afterAdd.Year);
    }

    public DateWithoutTime AddYears(int years)
    {
      DateTime beforeAdd = new DateTime(this.Year, this.Month, this.Day);
      DateTime afterAdd = beforeAdd.AddYears(years);

      return new DateWithoutTime(afterAdd.Day, afterAdd.Month, afterAdd.Year);
    }

    public static int CalculateYearsCount(DateWithoutTime beginDate, DateWithoutTime endDate)
    {
      if (beginDate > endDate) throw new ArgumentException($"Cannot calculate years count because start date {beginDate} is greater than end date {endDate}");

      int yearsCount = endDate.Year - beginDate.Year;
      if (endDate.Month < beginDate.Month ||
          endDate.Month == beginDate.Month && endDate.Day < beginDate.Day)
      {
        yearsCount--;
      }

      return yearsCount;
    }

    public static int CalculateMonthsCount(DateWithoutTime beginDate, DateWithoutTime endDate)
    {
      if (beginDate > endDate) throw new ArgumentException($"Cannot calculate months count because start date {beginDate} is greater than end date {endDate}");

      int result =
          (endDate.Year - beginDate.Year) * 12 +
          (endDate.Month - beginDate.Month) + 1;

      return result;
    }

    public static int CalculateDaysCount(DateWithoutTime beginDate, DateWithoutTime endDate)
    {
      if (beginDate > endDate) throw new ArgumentException($"Cannot calculate days count because start date {beginDate} is greater than end date {endDate}");

      DateTime beginDateTime = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day);
      DateTime endDateTime = new DateTime(endDate.Year, endDate.Month, endDate.Day);

      int daysCount = (int)(endDateTime - beginDateTime).TotalDays;

      return daysCount;
    }

    public static IEnumerable<DateWithoutTime> GetMonthsEndDates(DateWithoutTime beginDate, DateWithoutTime endDate)
    {
      if (beginDate > endDate) throw new ArgumentException($"Cannot return months end dates becase start date {beginDate} is greater than end date {endDate}");


      DateWithoutTime firstDate = new DateWithoutTime(1, beginDate.Month, beginDate.Year);
      DateWithoutTime lastDate = endDate.GetLastDateOfMonth();
      int monthsCount = DateWithoutTime.CalculateMonthsCount(firstDate, lastDate);

      DateWithoutTime[] monthsEndDates = new DateWithoutTime[monthsCount];
      for (int i = 0; i < monthsCount; i++)
      {
        monthsEndDates[i] = firstDate.AddMonths(i).GetLastDateOfMonth();
      }

      return monthsEndDates;
    }

    public static DateWithoutTime GetFirstDateOfQuarter(int month, int year)
    {
      int quarter = (int)Math.Floor((month - 1) / 3d) + 1;
      int firstMonthOfQuarter = (quarter - 1) * 3 + 1;
      DateWithoutTime firstDateOfQuarter = DateWithoutTime.GetFirstDateOfMonth(firstMonthOfQuarter, year);

      return firstDateOfQuarter;
    }

    /// <summary>
    /// Quarters start dates:
    /// Q1 - 01.01;
    /// Q2 - 01.04;
    /// Q3 - 01.07;
    /// Q4 - 01.10;
    /// </summary>
    public DateWithoutTime GetFirstDateOfQuarter()
    {
      return DateWithoutTime.GetFirstDateOfQuarter(this.Month, this.Year);
    }

    public static DateWithoutTime GetLastDateOfQuarter(int month, int year)
    {
      int quarter = (int)Math.Ceiling(month / 3d);
      int lastMonthOfQuarter = quarter * 3;
      DateWithoutTime lastDateOfQuarter = DateWithoutTime.GetLastDateOfMonth(lastMonthOfQuarter, year);

      return lastDateOfQuarter;
    }

    /// <summary>
    /// Quarters end dates:
    /// Q1 - 31.03;
    /// Q2 - 30.06;
    /// Q3 - 30.09;
    /// Q4 - 31.12;
    /// </summary>
    public DateWithoutTime GetLastDateOfQuarter()
    {
      return DateWithoutTime.GetLastDateOfQuarter(this.Month, this.Year);
    }

    public static DateWithoutTime GetFirstDateOfMonth(int month, int year)
    {
      return new DateWithoutTime(
          day: 1,
          month: month,
          year: year);
    }

    public DateWithoutTime GetFirstDateOfMonth()
    {
      return DateWithoutTime.GetFirstDateOfMonth(this.Month, this.Year);
    }

    public static DateWithoutTime GetLastDateOfMonth(int month, int year)
    {
      return new DateWithoutTime(
          day: DateTime.DaysInMonth(year, month),
          month: month,
          year: year);
    }

    public DateWithoutTime GetLastDateOfMonth()
    {
      return DateWithoutTime.GetLastDateOfMonth(this.Month, this.Year);
    }

    public static DateWithoutTime Parse(string s)
    {
      string[] splittedValues = s.Split('/');
      if (splittedValues.Length != 4) throw new ArgumentException($"Date in string {s} cannot be parsed, because it does not match the format {_stringFormat}");

      int parsedDay = int.Parse(splittedValues[0]);
      int parsedMonth = int.Parse(splittedValues[1]);
      int parsedYear = int.Parse(splittedValues[2]);

      string parsedSuffix = splittedValues[3];
      if (parsedSuffix != _stringSuffix) throw new ArgumentException($"Date in string {s} cannot be parsed, because it does not contain expected suffix {_stringSuffix}");

      return new DateWithoutTime(parsedDay, parsedMonth, parsedYear);
    }

    /// <summary>
    /// Will convert to our special format. Example: 01/03/2000/LeaseProDateWithoutTime.
    /// </summary>
    public override string ToString()
    {
      return $"{Day:D2}/{Month:D2}/{Year:D4}/{_stringSuffix}";
    }

    /// <summary>
    /// Will convert to format for CBR dd/mm/yyyy. Exmple: 02/03/2002.
    /// </summary>
    public string ToStringCBR()
    {
      return $"{Day:D2}/{Month:D2}/{Year:D4}";
    }

    public int CompareTo(DateWithoutTime other)
    {
      int yearCompare = this.Year.CompareTo(other.Year);
      if (yearCompare != 0) return yearCompare;

      int monthCompare = this.Month.CompareTo(other.Month);
      if (monthCompare != 0) return monthCompare;

      int dayCompare = this.Day.CompareTo(other.Day);
      if (dayCompare != 0) return dayCompare;

      return 0;
    }

    public static bool operator >(DateWithoutTime firstDate, DateWithoutTime secondDate)
    {
      return firstDate.CompareTo(secondDate) > 0;
    }

    public static bool operator >=(DateWithoutTime firstDate, DateWithoutTime secondDate)
    {
      return firstDate.CompareTo(secondDate) >= 0;
    }

    public static bool operator <(DateWithoutTime firstDate, DateWithoutTime secondDate)
    {
      return firstDate.CompareTo(secondDate) < 0;
    }

    public static bool operator <=(DateWithoutTime firstDate, DateWithoutTime secondDate)
    {
      return firstDate.CompareTo(secondDate) <= 0;
    }

    public static DateWithoutTime Min(DateWithoutTime firstDate, DateWithoutTime secondDate)
    {
      return firstDate < secondDate ? firstDate : secondDate;
    }

    public static DateWithoutTime Max(DateWithoutTime firstDate, DateWithoutTime secondDate)
    {
      return firstDate > secondDate ? firstDate : secondDate;
    }


    /// <summary>
    /// If atleast one param is null - will return null.
    /// </summary>
    public static DateWithoutTime ConvertFromNullable(int? day, int? month, int? year)
    {
      if (day == null || day == 0 || month == null || month == 0 || year == null || year == 0) return null;
      else return new DateWithoutTime(day ?? default, month ?? default, year ?? default);
    }

    public static DateWithoutTime ConvertFromNullable(string date)
    {
      if (date == null) return null;

      return DateWithoutTime.Parse(date);
    }

    public DateWithoutTime NextDay()
    {
      DateTime dateTime = new DateTime(Year, Month, Day).AddDays(1);
      return new DateWithoutTime(dateTime.Day, dateTime.Month, dateTime.Year);
    }

    public DateWithoutTime PreviousDay()
    {
      DateTime dateTime = new DateTime(Year, Month, Day).AddDays(-1);
      return new DateWithoutTime(dateTime.Day, dateTime.Month, dateTime.Year);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
      yield return Day;
      yield return Month;
      yield return Year;
    }

    public static IEnumerable<DateWithoutTime> InMonth(int month, int year)
    {
      for (int i = 0; i < DateTime.DaysInMonth(month: month, year: year); i++)
        yield return new DateWithoutTime(
          day: i + 1,
          month: month,
          year: year);
    }

    public DateTime ToDateTime()
    {
      return new DateTime(
        year: this.Year,
        month: this.Month,
        day: this.Day);
    }
  }
}
