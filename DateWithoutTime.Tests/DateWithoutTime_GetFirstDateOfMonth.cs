using ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_GetFirstDateOfMonth
    {
        [Theory]
        [InlineData(01, 01, 2019)]
        [InlineData(02, 01, 2019)]
        [InlineData(31, 01, 2019)]
        [InlineData(28, 02, 2019)]
        [InlineData(31, 12, 2019)]
        public void For_Given_Date_Should_Return_First_Date_Of_Month(int day, int month, int year)
        {
            // Arrange
            DateWithoutTime date = new DateWithoutTime(day, month, year);
            DateWithoutTime expectedDate = new DateWithoutTime(01, month, year);


            // Act
            DateWithoutTime actualDate = date.GetFirstDateOfMonth();


            // Assert
            Assert.Equal(expectedDate, actualDate);
        }
    }
}
