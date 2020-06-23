using ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_AddMonths
    {
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(7)]
        public void AddMonths_Shouldnt_Change_Year(int monthsToAdd)
        {
            // Arrange
            int initialDay = 1;
            int initialMonth = 1;
            int initialYear = 2019;
            DateWithoutTime initialDate = new DateWithoutTime(initialDay, initialMonth, initialYear);

            
            // Act
            DateWithoutTime actualDate = initialDate.AddMonths(monthsToAdd);


            // Assert
            Assert.Equal(initialDay, actualDate.Day);
            Assert.Equal(initialMonth + monthsToAdd, actualDate.Month);
            Assert.Equal(initialYear, actualDate.Year);
        }

        [Theory]
        [InlineData(31)]
        [InlineData(30)]
        [InlineData(29)]
        public void AddMonths_To_Last_Day_Should_Be_Equal_DateTime(int initialDay)
        {
            // Arrange
            int initialMonth = 1;
            int initialYear = 2019;

            DateTime initalDateTime = new DateTime(initialYear, initialMonth, initialDay);
            DateTime expectedDate = initalDateTime.AddMonths(1);
    
            DateWithoutTime initialDate = new DateWithoutTime(initialDay, initialMonth, initialYear);


            // Act
            DateWithoutTime actualDate = initialDate.AddMonths(1);


            // Assert
            Assert.Equal(expectedDate.Day, actualDate.Day);
            Assert.Equal(expectedDate.Month, actualDate.Month);
            Assert.Equal(expectedDate.Year, actualDate.Year);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(29)]
        public void AddMonths_To_Edge_Month_Should_Increase_Year(int monthsToAdd)
        {
            // Arrange
            int initialDay = 1;
            int initialMonth = 1;
            int initialYear = 2019;

            DateTime initalDateTime = new DateTime(initialYear, initialMonth, initialDay);
            DateTime expectedDate = initalDateTime.AddMonths(monthsToAdd);

            DateWithoutTime initialDate = new DateWithoutTime(initialDay, initialMonth, initialYear);


            // Act
            DateWithoutTime actualDate = initialDate.AddMonths(monthsToAdd);


            // Assert
            Assert.Equal(expectedDate.Day, actualDate.Day);
            Assert.Equal(expectedDate.Month, actualDate.Month);
            Assert.Equal(expectedDate.Year, actualDate.Year);
        }
    }
}
