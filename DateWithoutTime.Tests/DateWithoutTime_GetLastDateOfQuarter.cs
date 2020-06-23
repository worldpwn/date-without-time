using ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_GetLastDateOfQuarter
    {
        [Theory]
        [InlineData(1, 03)]
        [InlineData(3, 03)]

        [InlineData(4, 06)]
        [InlineData(6, 06)]

        [InlineData(7, 09)]
        [InlineData(9, 09)]

        [InlineData(10, 12)]
        [InlineData(12, 12)]
        public void LastDate_Of_Quarter_Should_BeCorrect(int monthOfDate, int quarterEndMonth)
        {
            // Arrange
            int year = 2000;
            DateWithoutTime date = new DateWithoutTime(17, monthOfDate, year);
            DateWithoutTime expectedDate = DateWithoutTime.GetLastDateOfMonth(quarterEndMonth, year);


            // Act
            DateWithoutTime lastDateOfQuarter = date.GetLastDateOfQuarter();


            // Assert
            Assert.Equal(expectedDate, lastDateOfQuarter);
        }
    }
}
