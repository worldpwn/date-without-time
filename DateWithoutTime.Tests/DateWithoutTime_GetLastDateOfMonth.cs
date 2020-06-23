using ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_GetLastDateOfMonth
    {
        [Theory]
        [InlineData(01, 01, 2019, 31, 01, 2019)]
        [InlineData(31, 01, 2019, 31, 01, 2019)]
        [InlineData(30, 01, 2019, 31, 01, 2019)]

        [InlineData(01, 02, 2019, 28, 02, 2019)]
        [InlineData(01, 04, 2019, 30, 04, 2019)]
        public void LastDayOfMonth_Should_Be_Correct(int dateDay, int dateMonth, int dateYear, int expectedDay, int expectedMonth, int expectedYear)
        {
            // Arrange
            DateWithoutTime date = new DateWithoutTime(dateDay, dateMonth, dateYear);
            DateWithoutTime expectedDate = new DateWithoutTime(expectedDay, expectedMonth, expectedYear);


            // Act
            DateWithoutTime actualDate = date.GetLastDateOfMonth();


            // Assert
            Assert.Equal(expectedDate, actualDate);
        }
    }
}
