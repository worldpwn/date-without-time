using ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_GetMonthsEndDates
    {
        [Fact]
        public void MonthEndDate_In_One_Month_Should_Be_Single_And_Last_Date()
        {
            // Arrange
            int month = 1;
            int year = 2019;

            DateWithoutTime beginDate = new DateWithoutTime(01, month, year);
            DateWithoutTime endDate = new DateWithoutTime(21, month, year);

            int lastDay = DateTime.DaysInMonth(year, month);


            // Act
            IEnumerable<DateWithoutTime> actualMonthsEndDates = DateWithoutTime.GetMonthsEndDates(
                beginDate: beginDate,
                endDate: endDate);


            // Assert
            Assert.Single(actualMonthsEndDates);

            DateWithoutTime monthEndDate = actualMonthsEndDates.First();
            Assert.Equal(lastDay, monthEndDate.Day);
            Assert.Equal(month, monthEndDate.Month);
            Assert.Equal(year, monthEndDate.Year);
        }

        [Fact]
        public void MonthsEndDates_For_Two_First_Months_Dates_Should_Be_Two_And_Last()
        {
            // Arrange
            int day = 1;
            int beginMonth = 1;
            int endMonth = beginMonth + 1;
            int year = 2019;

            DateWithoutTime beginDate = new DateWithoutTime(day, beginMonth, year);
            DateWithoutTime endDate = new DateWithoutTime(day, endMonth, year);

            IEnumerable<DateWithoutTime> expectedMonthsEndDates = new List<DateWithoutTime>
            {
                new DateWithoutTime(DateTime.DaysInMonth(year, beginMonth), beginMonth, year),
                new DateWithoutTime(DateTime.DaysInMonth(year, endMonth), endMonth, year)
            };


            // Act
            IEnumerable<DateWithoutTime> actualMonthsEndDates = DateWithoutTime.GetMonthsEndDates(
                beginDate: beginDate,
                endDate: endDate);


            // Assert
            Assert.Equal(expectedMonthsEndDates, actualMonthsEndDates);
        }

        [Fact]
        public void MonthEndDate_For_One_DateWithoutTime_Should_Be_One_And_Last_Date()
        {
            // Arrange
            DateWithoutTime date = new DateWithoutTime(01, 01, 2019);
            DateWithoutTime expectedMonthEndDate = new DateWithoutTime(DateTime.DaysInMonth(date.Year, date.Month), date.Month, date.Year);


            // Act
            IEnumerable<DateWithoutTime> actualMonthsEndDates = DateWithoutTime.GetMonthsEndDates(
                beginDate: date,
                endDate: date);


            // Assert
            DateWithoutTime actualMonthEndDate = actualMonthsEndDates.First();
            Assert.Equal(expectedMonthEndDate.Day, actualMonthEndDate.Day);
            Assert.Equal(expectedMonthEndDate.Month, actualMonthEndDate.Month);
            Assert.Equal(expectedMonthEndDate.Year, actualMonthEndDate.Year);
        }

        [Theory]
        [InlineData(01, 01, 2019, 31, 12, 2019)]
        [InlineData(01, 01, 2019, 01, 12, 2019)]
        [InlineData(01, 01, 2019, 31, 12, 2020)]

        [InlineData(31, 01, 2019, 31, 01, 2019)]

        [InlineData(30, 01, 2019, 31, 01, 2019)]
        [InlineData(30, 01, 2019, 01, 02, 2019)]
        [InlineData(31, 01, 2019, 01, 02, 2019)]
        public void MonthsEndDates_Should_Be_Correct(int beginDay, int beginMonth, int beginYear, int endDay, int endMonth, int endYear)
        {
            // Arrange
            DateWithoutTime beginDate = new DateWithoutTime(beginDay, beginMonth, beginYear);
            DateWithoutTime endDate = new DateWithoutTime(endDay, endMonth, endYear);

            List<DateWithoutTime> expectedMonthsEndDates = new List<DateWithoutTime>();
            DateWithoutTime firstDate = new DateWithoutTime(1, beginDate.Month, beginDate.Year);
            DateWithoutTime lastDate = new DateWithoutTime(DateTime.DaysInMonth(endDate.Year, endDate.Month), endDate.Month, endDate.Year);

            DateWithoutTime currentMonthFirstDate = firstDate;
            while (currentMonthFirstDate < lastDate)
            {
                DateWithoutTime currentMonthLastDate = new DateWithoutTime(
                    day: DateTime.DaysInMonth(currentMonthFirstDate.Year, currentMonthFirstDate.Month),
                    month: currentMonthFirstDate.Month,
                    year: currentMonthFirstDate.Year);
                expectedMonthsEndDates.Add(currentMonthLastDate);

                currentMonthFirstDate = currentMonthFirstDate.AddMonths(1);
            }


            // Act
            IEnumerable<DateWithoutTime> actualMonthsEndDates = DateWithoutTime.GetMonthsEndDates(
                beginDate: beginDate,
                endDate: endDate);


            // Assert
            Assert.Equal(expectedMonthsEndDates, actualMonthsEndDates);
        }

        [Theory]
        [InlineData(02, 01, 2019, 01, 01, 2019)]
        [InlineData(01, 01, 2020, 01, 01, 2019)]
        public void BeginDate_Greater_Than_EndDate_Should_Throw(int beginDay, int beginMonth, int beginYear, int endDay, int endMonth, int endYear)
        {
            DateTime beginDateTime = new DateTime(beginYear, beginMonth, beginDay);
            DateTime endDateTime = new DateTime(endYear, endMonth, endDay);
            DateWithoutTime beginDate = new DateWithoutTime(beginDateTime);
            DateWithoutTime endDate = new DateWithoutTime(endDateTime);


            Assert.Throws<ArgumentException>(() =>
            {
                DateWithoutTime.GetMonthsEndDates(beginDate, endDate);
            });
        }
    }
}
