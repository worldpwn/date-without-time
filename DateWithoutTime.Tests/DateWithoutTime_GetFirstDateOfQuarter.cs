using ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_GetFirstDateOfQuarter
    {
        [Theory]
        [InlineData(1, 01)]
        [InlineData(3, 01)]

        [InlineData(4, 04)]
        [InlineData(6, 04)]

        [InlineData(7, 07)]
        [InlineData(9, 07)]

        [InlineData(10, 10)]
        [InlineData(12, 10)]
        public void FirstDate_Of_Quarter_Should_BeCorrect(int monthOfDate, int quarterStartMonth)
        {
            // Arrange
            int year = 2000;
            DateWithoutTime date = new DateWithoutTime(17, monthOfDate, year);
            DateWithoutTime expectedDate = DateWithoutTime.GetFirstDateOfMonth(quarterStartMonth, year);


            // Act
            DateWithoutTime lastDateOfQuarter = date.GetFirstDateOfQuarter();


            // Assert
            Assert.Equal(expectedDate, lastDateOfQuarter);
        }
    }
}
