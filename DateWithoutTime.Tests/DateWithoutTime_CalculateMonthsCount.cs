using ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_CalculateMonthsCount
    {
        [Theory]
        [InlineData(1)]
        [InlineData(12)]
        [InlineData(23)]
        [InlineData(24)]
        [InlineData(67)]
        public void CorrectDate_Should_ReturnCorrectMonthsCount(int monthsToAdd)
        {
            int startMonth = 1;
            DateWithoutTime startDate = new DateWithoutTime(1, startMonth, 1988);
            DateWithoutTime endDate = startDate.AddMonths(monthsToAdd);

            int result = DateWithoutTime.CalculateMonthsCount(beginDate: startDate, endDate: endDate);

            Assert.Equal(monthsToAdd + 1, result);
        }

        [Fact]
        public void EndLessThanStart_Should_Throw()
        {
            DateWithoutTime startDate = new DateWithoutTime(1, 1, 1988);
            DateWithoutTime endDate = new DateWithoutTime(1, 1, 1987);

            Assert.Throws<ArgumentException>(() =>
            {
                DateWithoutTime.CalculateMonthsCount(beginDate: startDate, endDate: endDate);
            });
        }

        [Theory]
        [InlineData(01, 01, 2019, 01, 01, 2019, 1)]
        [InlineData(01, 01, 2019, 02, 01, 2019, 1)]

        [InlineData(01, 01, 2019, 01, 02, 2019, 2)]
        [InlineData(31, 01, 2019, 01, 02, 2019, 2)]
        [InlineData(30, 01, 2019, 01, 02, 2019, 2)]

        [InlineData(01, 01, 2019, 31, 12, 2019, 12)]
        public void MonthsCountBetweenDates_Should_BeCorrect(int beginDay, int beginMonth, int beginYear, int endDay, int endMonth, int endYear, int expectedMonthDifference)
        {
            DateWithoutTime startDate = new DateWithoutTime(beginDay, beginMonth, beginYear);
            DateWithoutTime endDate = new DateWithoutTime(endDay, endMonth, endYear);


            int actualMonthDifference = DateWithoutTime.CalculateMonthsCount(startDate, endDate);


            Assert.Equal(expectedMonthDifference, actualMonthDifference);
        }

        [Theory]
        [InlineData(02, 01, 2019, 01, 01, 2019)]
        [InlineData(01, 02, 2019, 01, 01, 2019)]
        [InlineData(01, 01, 2020, 01, 01, 2019)]
        public void BeginDateGreaterThanEndDate_Should_ThrowException(int beginDay, int beginMonth, int beginYear, int endDay, int endMonth, int endYear)
        {
            DateWithoutTime beginDate = new DateWithoutTime(beginDay, beginMonth, beginYear);
            DateWithoutTime endDate = new DateWithoutTime(endDay, endMonth, endYear);


            Assert.Throws<ArgumentException>(() =>
            {
                DateWithoutTime.CalculateMonthsCount(beginDate, endDate);
            });
        }
    }
}
