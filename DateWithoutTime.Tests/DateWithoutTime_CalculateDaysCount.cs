using ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_CalculateDaysCount
    {
        [Theory]
        [InlineData(01, 01, 2019, 01, 01, 2019)]
        [InlineData(01, 01, 2019, 02, 01, 2019)]
        [InlineData(31, 12, 2019, 01, 01, 2020)]

        [InlineData(01, 01, 2019, 01, 01, 2020)]
        public void CorrectDates_Should_ReturnDaysCount_AsDateTimeSubstraction(int beginDay, int beginMonth, int beginYear, int endDay, int endMonth, int endYear)
        {
            DateTime beginDateTime = new DateTime(beginYear, beginMonth, beginDay);
            DateTime endDateTime = new DateTime(endYear, endMonth, endDay);
            DateWithoutTime beginDate = new DateWithoutTime(beginDateTime);
            DateWithoutTime endDate = new DateWithoutTime(endDateTime);

            int expectedDaysDifference = (int)(endDateTime - beginDateTime).TotalDays;


            int actualDaysDifference = DateWithoutTime.CalculateDaysCount(beginDate, endDate);


            Assert.Equal(expectedDaysDifference, actualDaysDifference);
        }

        [Theory]
        [InlineData(02, 01, 2019, 01, 01, 2019)]
        [InlineData(01, 01, 2020, 01, 01, 2019)]
        public void BeginDateGreaterThanEndDate_Should_ThrowException(int beginDay, int beginMonth, int beginYear, int endDay, int endMonth, int endYear)
        {
            DateTime beginDateTime = new DateTime(beginYear, beginMonth, beginDay);
            DateTime endDateTime = new DateTime(endYear, endMonth, endDay);
            DateWithoutTime beginDate = new DateWithoutTime(beginDateTime);
            DateWithoutTime endDate = new DateWithoutTime(endDateTime);


            Assert.ThrowsAny<ArgumentException>(() =>
            {
                DateWithoutTime.CalculateDaysCount(beginDate, endDate);
            });
        }
    }
}
