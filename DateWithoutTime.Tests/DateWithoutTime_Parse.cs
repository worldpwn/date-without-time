using ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
  public class DateWithoutTime_Parse
  {
    [Theory]
    [InlineData("01/01/2019/DateWithoutTime")]
    [InlineData("31/01/2019/DateWithoutTime")]
    [InlineData("31/12/2019/DateWithoutTime")]
    [InlineData("01/02/0003/DateWithoutTime")]
    public void CorrectStringDate_Should_BeParsed(string expectedDateString)
    {
      DateWithoutTime date = DateWithoutTime.Parse(expectedDateString);

      string[] valuesFromString = expectedDateString.Split('/');
      int expectedDay = int.Parse(valuesFromString[0]);
      int expectedMonth = int.Parse(valuesFromString[1]);
      int expectedYear = int.Parse(valuesFromString[2]);

      Assert.Equal(expectedDay, date.Day);
      Assert.Equal(expectedMonth, date.Month);
      Assert.Equal(expectedYear, date.Year);
    }

    [Theory]
    [InlineData("01/01/2019/")]
    [InlineData("01/01/2019/вфыro")]
    [InlineData("01/01/2019/DateWithTime")]
    [InlineData("01/01/2019/LeoDateWithoutTim")]
    [InlineData("01/01/2019/LEASEPRODATEWITHOUTTIME")]
    [InlineData("01/01/2019/leahouttime")]
    public void IncorrectSuffix_InStringDate_Should_ThrowException(string wrongSuffixString)
    {
      Assert.Throws<ArgumentException>(() =>
      {
        DateWithoutTime.Parse(wrongSuffixString);
      });
    }

    [Theory]
    [InlineData("")]
    [InlineData("01")]
    [InlineData("01/01")]
    [InlineData("01/01/2019")]
    [InlineData("01/01/2019/12")]
    public void IncorrectPartsCount_InStringDate_Should_ThrowException(string wrongPartsCountString)
    {
      Assert.Throws<ArgumentException>(() =>
      {
        DateWithoutTime.Parse(wrongPartsCountString);
      });
    }

    [Theory]
    [InlineData("01.01.2019")]
    [InlineData("01-01-2019")]
    [InlineData("01.01-2019")]
    [InlineData("01/01.2019")]
    public void IncorrectDelimeterBetweenParts_InStringDate_Should_ThrowException(string wrongDelimetersString)
    {
      Assert.Throws<ArgumentException>(() =>
      {
        DateWithoutTime.Parse(wrongDelimetersString);
      });
    }
  }
}
