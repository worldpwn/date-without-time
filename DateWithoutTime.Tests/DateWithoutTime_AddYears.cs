using ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ValueObjects.Tests
{
    public class DateWithoutTime_AddYears
	{
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(7)]
        public void AddYears_Shouldnt_Change_Year(int yearsToAdd)
        {
            // Arrange
            int initialDay = 1;
            int initialMonth = 1;
            int initialYear = 2019;
            DateWithoutTime initialDate = new DateWithoutTime(initialDay, initialMonth, initialYear);

            
            // Act
            DateWithoutTime actualDate = initialDate.AddYears(yearsToAdd);


            // Assert
            Assert.Equal(initialDay, actualDate.Day);
            Assert.Equal(initialMonth, actualDate.Month);
            Assert.Equal(initialYear + yearsToAdd, actualDate.Year);
        }


		/// <summary>
		/// 29 февраля + год
		/// </summary>
        [Fact]
		public void Case1_Should_Change_Day()
        {
			// Arrange
			int initialDay = 29;
			int initialMonth = 2;
			int initialYear = 2000;
			DateTime initalDateTime = new DateTime(initialYear, initialMonth, initialDay);
            DateWithoutTime initialDate = new DateWithoutTime(initialDay, initialMonth, initialYear);

			// Act
			DateTime expectedDate = initalDateTime.AddYears(1);
			DateWithoutTime actualDate = initialDate.AddYears(1);


            // Assert
            Assert.Equal(expectedDate.Day, actualDate.Day);
            Assert.Equal(expectedDate.Month, actualDate.Month);
            Assert.Equal(expectedDate.Year, actualDate.Year);
        }

		/// <summary>
		/// 29 февраля + 4 годa
		/// </summary>
		[Fact]
		public void Case2_Should_Not_Change_Day()
		{
			// Arrange
			int initialDay = 29;
			int initialMonth = 2;
			int initialYear = 2000;
			DateTime initalDateTime = new DateTime(initialYear, initialMonth, initialDay);
			DateWithoutTime initialDate = new DateWithoutTime(initialDay, initialMonth, initialYear);

			// Act
			DateTime expectedDate = initalDateTime.AddYears(4);
			DateWithoutTime actualDate = initialDate.AddYears(4);


			// Assert
			Assert.Equal(expectedDate.Day, actualDate.Day);
			Assert.Equal(expectedDate.Month, actualDate.Month);
			Assert.Equal(expectedDate.Year, actualDate.Year);
		}
	}
}
